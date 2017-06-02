using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace BotFactory.Factories
{
    public class UnitFactory : ReportingUnit, IUnitFactory
    {
        private bool _isWaiting = false;
        private Thread _thread;
        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        static readonly object _object = new object();

        public event FactoryProgress FactoryProgress;

        /// <summary>
        /// Indique l'emplacement libre pour la queue
        /// </summary>
        public int QueueFreeSlots { get; set; }

        public TimeSpan QueueTime { get; set; }

        /// <summary>
        ///  Indique l'emplacement libre pour l’entrepôt
        /// </summary>
        public int StorageFreeSlots { get; set; }

        /// <summary>
        /// Queue contenant les commandes à fabriquer
        /// </summary>
        public List<IFactoryQueueElement> Queue { get; set; }

        public List<ITestingUnit> Storage { get; set; }

        public int QueueCapacity { get; set; }

        public int StorageCapacity { get; set; }

        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            QueueCapacity = QueueFreeSlots = queueCapacity;
            StorageCapacity = StorageFreeSlots = storageCapacity;

            Queue = new List<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();

            //ON INITIALISE L'OBJECT THREAD EN LUI PASSANT LA MÉTHODE
            //À EXÉCUTER DANS LE NOUVEAU THREAD
            _thread = new Thread(BuildingQueue);

            //COMMENCER L'EXÉCUTION DU THREAD
            _thread.Start();
        }

        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            if (Queue.Count > QueueCapacity || QueueFreeSlots == 0)
                return false;

            Queue.Add(new FactoryQueueElement(model, name, parkingPos, workingPos));

            if (QueueFreeSlots > 0)
                QueueFreeSlots--;

            lock (_object)
            {
                if (this._isWaiting)
                    _autoResetEvent.Set();
            }

            FactoryProgress(Queue.First(), new StatusChangedEventArgs("NOUVEAU ROBOT AJOUTÉ DANS LA FILE D'ATTENTE"));

            return true;   
        }

        public void BuildingQueue()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                // CONSTRUCTION

                if (Queue.Count > 0 && StorageFreeSlots > 0)
                {
                    IFactoryQueueElement fqe = Queue.FirstOrDefault();
                       
                    FactoryProgress(fqe, new StatusChangedEventArgs("NOUVEAU ROBOT EN CONSTRUCTION"));
                    CreateAndStoreUnit(fqe);
                    Queue.Remove(fqe);

                    if (QueueFreeSlots < QueueCapacity)
                        QueueFreeSlots += 1;

                    if (StorageFreeSlots >= 1)
                        StorageFreeSlots -= 1;

                    FactoryProgress(fqe, new StatusChangedEventArgs("NOUVEAU ROBOT CONSTRUIT ET PRÊT A ÊTRE TESTÉ"));
                }
                else
                {
                    // THREAD DE CONSTRUCTION EN ATTENTE

                    lock (_object)
                       this._isWaiting = true;

                    _autoResetEvent.WaitOne();
                }
            };
        }

        public WorkingUnit CreateAndStoreUnit(IFactoryQueueElement fqe)
        {
            WorkingUnit unitToBeAdded = null;

            if (fqe == null)
                return null;

            unitToBeAdded = Activator.CreateInstance(fqe.Model) as WorkingUnit;

            // SIMULATION DU TEMPS DE CONSTRUCTION 

            Thread.Sleep(TimeSpan.FromSeconds(unitToBeAdded.BuildTime));
            
            // AJOUT D'UN ROBOT DANS L'ENTREPÔT

            Storage.Add(unitToBeAdded);
                
            QueueTime += TimeSpan.FromSeconds(unitToBeAdded.BuildTime);

            return unitToBeAdded;
        }           
    }
}

