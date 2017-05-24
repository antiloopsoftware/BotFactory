using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace BotFactory.Factories
{
    public class UnitFactory : ReportingUnit, IUnitFactory
    {
        private bool _isWaiting = false;
        private bool _isBuilding = false;

        private Thread thread;
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public event FactoryProgress FactoryProgress;

        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            QueueCapacity = QueueFreeSlots = queueCapacity;
            StorageCapacity = StorageFreeSlots = storageCapacity;

            Queue = new List<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();

            //ON INITIALISE L'OBJECT THREAD EN LUI PASSANT LA M�THODE
            //� EX�CUTER DANS LE NOUVEAU THREAD
            thread = new Thread(BuildingQueue);

            //COMMENCER L'EX�CUTION DU THREAD
            thread.Start();

            // ou

            // THIS CREATE A SEPARATED THREAD FOR THE TASK
            //Task.Run(() => BuildingQueueAsync());
        }

        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            // SI LA QUEUE EST PLEINE
            if (Queue.Count > QueueCapacity || QueueFreeSlots == 0)
            {
                return false;
            }
            else
            {
                WorkingUnit unitToBeAdded = AddFactoryQueueElement(model, name, parkingPos, workingPos);
                QueueTime += TimeSpan.FromSeconds(unitToBeAdded.BuildTime);

                if (QueueFreeSlots > 0)
                    QueueFreeSlots--;

                if (this._isWaiting)
                   autoResetEvent.Set();

                FactoryProgress(Queue.First(), new StatusChangedEventArgs("NOUVEAU ROBOT AJOUT� DANS LA FILE D'ATTENTE"));

                return true;
            }
        }

        public WorkingUnit AddFactoryQueueElement(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            IFactoryQueueElement factoryQueueElement = new FactoryQueueElement(model, name, parkingPos, workingPos);

            // PLACER LA COMMANDE DANS LA QUEUE
            Console.WriteLine(
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Ajout d'une commande de robot dans la queue (model : {model}; nom : {name} ; lieu de rechargement : {parkingPos.X}, {parkingPos.Y}; lieu de travail : {workingPos.X}, {workingPos.X})"
                + Environment.NewLine);

            Queue.Add(factoryQueueElement);

            return Activator.CreateInstance(Queue.First().Model) as WorkingUnit;
        }

        public void BuildingQueue()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                int i = 0;

                int numberOfCore = Environment.ProcessorCount;
                int maxTask = 0;

                // CONSTRUCTION
                if (Queue.Count > 0 && StorageFreeSlots > 0)
                {
                    if (!this._isBuilding)
                    {
                        this._isBuilding = true;

                        IFactoryQueueElement fqe = Queue.FirstOrDefault();
                       
                        FactoryProgress(fqe, new StatusChangedEventArgs("NOUVEAU ROBOT EN CONSTRUCTION"));

                        if (Queue.Count < numberOfCore)
                            maxTask = Queue.Count;
                        else if (Queue.Count >= numberOfCore)
                            maxTask = numberOfCore;

                        if (maxTask > StorageFreeSlots)
                            maxTask = StorageFreeSlots;

                        var tasks = new List<Task>();

                        for (int j = 0; j < maxTask; j++)
                            tasks.Add(Task.Run(() =>
                            {
                                CreateAndStoreUnit(fqe);
                            }));

                        // UTILISATION DU THREADPOOL : MOINS COUTEUX QUE LA CR�ATION ET LA DESTRUCTION DE THREADS

                        Task.WhenAll(tasks).GetAwaiter().GetResult();
                        
                        Queue.RemoveRange(0, maxTask);

                        if (QueueFreeSlots < QueueCapacity + maxTask)
                            QueueFreeSlots += maxTask;

                        if (StorageFreeSlots >= maxTask)
                            StorageFreeSlots -= maxTask;
                            
                        this._isBuilding = false;

                        FactoryProgress(fqe, new StatusChangedEventArgs("NOUVEAU ROBOT CONSTRUIT ET PR�T A �TRE TEST�"));
                    }

                    i++;
                }
                else
                {
                    this._isWaiting = true;
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Thread de construction en attente");
                    autoResetEvent.WaitOne();

                    if (Queue.Count == 0)
                    {
                        QueueTime = TimeSpan.FromSeconds(0);
                        i = 0;
                    }
                }
            };
        }

        public WorkingUnit CreateAndStoreUnit(IFactoryQueueElement fqe)
        {
            WorkingUnit unitToBeAdded = null;

            if (fqe != null)
            {
                unitToBeAdded = Activator.CreateInstance(fqe.Model) as WorkingUnit;

                Storage.Add(unitToBeAdded);

                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Ajout d'un robot dans l'entrep�t (nom : {fqe.Name})");
                
                QueueTime += TimeSpan.FromSeconds(unitToBeAdded.BuildTime);
                Thread.Sleep(TimeSpan.FromSeconds(unitToBeAdded.BuildTime));
            }

            return unitToBeAdded;
        }

        /// <summary>
        /// Indique l'emplacement libre pour la queue
        /// </summary>
        public int QueueFreeSlots { get; set; }

        public TimeSpan QueueTime { get; set; }

        /// <summary>
        ///  Indique l'emplacement libre pour l�entrep�t
        /// </summary>
        public int StorageFreeSlots { get; set; }

        /// <summary>
        /// Queue contenant les commandes � fabriquer
        /// </summary>
        public List<IFactoryQueueElement> Queue { get; set; }

        public List<ITestingUnit> Storage { get; set; }

        public int QueueCapacity { get; set; }

        public int StorageCapacity { get; set; }
        
        public async Task BuildingQueueAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(/*uf.BuildTime*/5000));
        }
        
        public bool CreateWorkableUnitToQueue()
        {
            // SI LA QUEUE EST PLEINE
            if (Queue.Count > QueueCapacity)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    IFactoryQueueElement factoryQueueElement = (IFactoryQueueElement)new FactoryQueueElement(typeof(HAL), "name" + i, new Coordinates(1 + i, 2 + i), new Coordinates(3 + i, 4 + i));

                    // PLACER LA COMMANDE DANS LA QUEUE
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Ajout d'une commande de robot dans la queue (model : {typeof(HAL)}; nom : {"name" + i} ;" + Environment.NewLine);

                    Queue.Add(factoryQueueElement);
                }

                return true;
            }
        }
    }
}

