using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BotFactory.Factories
{
    public class UnitFactory : ReportingUnit, IUnitFactory
    {
        private bool _isWaiting = false;
        private bool _isBuilding = false;

        /// <summary>
        /// Taille de l’entrepôt
        /// </summary>
        private int _storageCapacity;
        private int _storageFreeSlots;

        /// <summary>
        ///  Taille de la queue 
        /// </summary>
        private int _queueCapacity;
        private int _queueFreeSlots;

        private List<IFactoryQueueElement> _queue;
        private List<ITestingUnit> _storage;
        //private Thread thread;
        private static AutoResetEvent event_1 = new AutoResetEvent(false);
        private TimeSpan _queueTime;
        private object _lock = new object();


        public UnitFactory(int queueCapacity, int storageCapacity)
        {
            _queueCapacity = queueCapacity;
            _storageCapacity = storageCapacity;

            _queueFreeSlots = queueCapacity;
            _storageFreeSlots = storageCapacity;

            _queue = new List<IFactoryQueueElement>();
            _storage = new List<ITestingUnit>();

            //ON INITIALISE L'OBJECT THREAD EN LUI PASSANT LA MÉTHODE
            //À EXÉCUTER DANS LE NOUVEAU THREAD
            /*  thread = new Thread(BuildingQueue);

              //COMMENCER L'EXÉCUTION DU THREAD
              thread.Start();*/

            // ou

            // THIS CREATE A SEPARATED THREAD FOR THE TASK
            Task.Run(() => BuildingQueueAsync());
        }

        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            // SI LA QUEUE EST PLEINE
            if (_queue.Count > _queueCapacity || _queueFreeSlots == 0)
            {
                return false;
            }
            else
            {
                IFactoryQueueElement factoryQueueElement = new FactoryQueueElement(model, name, parkingPos, workingPos);

                // PLACER LA COMMANDE DANS LA QUEUE
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Ajout d'une commande de robot dans la queue (model : {model}; nom : {name} ; coordonnées place de rechargement : {parkingPos.X}, {parkingPos.Y}; coordonnées lieu de travail : {workingPos.X}, {workingPos.X})" + Environment.NewLine);

                Queue.Add(factoryQueueElement);

                if(_queueFreeSlots > 0)
                    _queueFreeSlots--;

                //if (this._isWaiting)
                   // event_1.Set();

                return true;
            }
        }

        public bool CreateWorkableUnitToQueue()
        {
            // SI LA QUEUE EST PLEINE
            if (_queue.Count > _queueCapacity)
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

                    _queue.Add(factoryQueueElement);         
                }

                return true;
            }
        }

        public async Task BuildingQueueAsync()
        {
            //while (Thread.CurrentThread.IsAlive)
            while (true)
            {
                if (_queue.Count > 0 && _storageFreeSlots > 0)
                {
                    if (!this._isBuilding)
                    {
                        OnFactoryChanged(new EventArgs());
                        this._isBuilding = true;

                        IFactoryQueueElement fqe = _queue.FirstOrDefault();
                        Queue.RemoveAt(0);

                        if (fqe != null)
                        {
                            // http://stackoverflow.com/questions/981330/instantiate-an-object-with-a-runtime-determined-type
                            // http://stackoverflow.com/questions/2451336/how-to-pass-parameters-to-activator-createinstancet

                            var uf = (ITestingUnit) Activator.CreateInstance(fqe.Model, BindingFlags.CreateInstance |
                                                                                        BindingFlags.Public |
                                                                                        BindingFlags.Instance |

                                                                                        // http://stackoverflow.com/questions/2421994/invoking-methods-with-optional-parameters-through-reflection
                                                                                        BindingFlags.OptionalParamBinding,
                                                                                        null, new object[] { fqe.Model.ToString() + Guid.NewGuid(), Type.Missing }, CultureInfo.CurrentCulture);

                            await Task.Delay(TimeSpan.FromSeconds(uf.BuildTime));

                            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Ajout d'un robot dans l'entrepôt (nom : {fqe.Name})");

                            _storage.Add(uf);

                            if (_queueFreeSlots < _queueCapacity)
                                _queueFreeSlots++;

                            if (_storageFreeSlots > 0)
                                _storageFreeSlots--;
                            
                            this._isBuilding = false;

                            OnFactoryChanged(new EventArgs());
                            OnStatusChanged(new StatusChangedEventArgs() { NewStatus = "NOUVEAU ROBOT PRÊT A ÊTRE TESTÉ" });
                        }
                    }
                }
                else
                {
                    this._isWaiting = true;
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : Thread de construction en attente.");
                    //event_1.WaitOne();
                }
            };
        }

        /// <summary>
        /// Indique l'emplacement libre pour la queue
        /// </summary>
        public int QueueFreeSlots
        {
            get
            {
                return _queueFreeSlots;
            }

            set
            {
                _queueFreeSlots = value;
            }
        }

        public TimeSpan QueueTime
        {
            get
            {
                return _queueTime;
            }

            set
            {
                _queueTime = value;
            }
        }


        /// <summary>
        ///  Indique l'emplacement libre pour l’entrepôt
        /// </summary>
        public int StorageFreeSlots
        {
            get
            {
                return _storageFreeSlots;
            }

            set
            {
                _storageFreeSlots = value;
            }
        }

        /// <summary>
        /// Queue contenant les commandes à fabriquer
        /// </summary>
        public List<IFactoryQueueElement> Queue
        {
            get
            {
                return _queue;
            }

            set
            {
                _queue = value;
            }
        }

        public List<ITestingUnit> Storage
        {
            get
            {
                return _storage;
            }
            set
            {
                _storage = value;
            }
        }

        public int QueueCapacity
        {
            get
            {
                return _queueCapacity;
            }
        }

        public int StorageCapacity
        {
            get
            {
                return _storageCapacity;
            }
        }

        ~UnitFactory()
        {
            //thread.Abort();
        }
    }
}

