
using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Factories;
using BotFactory.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;

namespace BotFactory.Pages
{
    /// <summary>
    /// Interaction logic for FactoryTest.xaml
    /// </summary>
    public partial class FactoryTest : Page
    {
        FactoryDataContext _dataContext = new FactoryDataContext();
        UnitTest _unitTestPage; 

        public FactoryTest()
        {
            InitializeComponent();
            DataContext = _dataContext;
        }

        public void SetTestingFactory(UnitFactory factory)
        {
            _dataContext.Builder = factory;
            _dataContext.Builder.FactoryProgress += Builder_FactoryProgress;
        }

        private void Builder_FactoryProgress(object sender, IStatusChangedEventArgs e)
        {
            _dataContext.ForceUpdate();

            // This block is is required without an observablecollection
            // [[

            // http://dotnetpattern.com/wpf-dispatcher
            // Dispatcher provides two methods for registering method to execute into the message queue.
            //
            // Invoke method takes an Action or Delegate and execute the method synchronously. 
            //
            // That means it does not return until the Dispatcher complete the execution of the method.
            // BeginInvoke method take a Delegate but it executes the method asynchronously.
            // That means it immediately returns before calling the method.

             Dispatcher.BeginInvoke((Action)(() =>
             {
                 // INIT ?
                 QueueList.ItemsSource = new List<IFactoryQueueElement>();
                 // REFRESH ?
                 QueueList.ItemsSource = _dataContext.Builder.Queue;

                 // INIT ?
                 StorageList.ItemsSource = new List<ITestingUnit>();
                 // REFRESH ?
                 StorageList.ItemsSource = _dataContext.Builder.Storage;

             }));

            // ]]
            /*
                        // INIT ?
                        QueueList.ItemsSource = new List<IFactoryQueueElement>();
                        // REFRESH ?
                        QueueList.ItemsSource = _dataContext.Builder.Queue;

                        // INIT ?
                        StorageList.ItemsSource = new List<ITestingUnit>();
                        // REFRESH ?
                        StorageList.ItemsSource = _dataContext.Builder.Storage;*/

          /* QueueList.ItemsSource = new List<IFactoryQueueElement>();
           
            QueueList.Items.Refresh();
             StorageList.ItemsSource = new List<ITestingUnit>();
            StorageList.Items.Refresh();*/

        }
        private void AddUnitToQueue_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ModelsList.SelectedIndex >= 0 && !String.IsNullOrEmpty(UnitName.Text))
            {
                Type item = (Type)ModelsList.SelectedItem;
                var name = UnitName.Text;
                _dataContext.Builder.AddWorkableUnitToQueue(item, name, new Coordinates(0, 0), new Coordinates(10, 10));
                _dataContext.ForceUpdate();
            }
        }

        private void UpdateButtonValidity()
        {
            AddUnitToQueue.IsEnabled = ModelsList.SelectedIndex >= 0 && !String.IsNullOrEmpty(UnitName.Text);
        }

        private void UnitName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtonValidity();
        }

        private void ModelsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonValidity();
        }

        private void StorageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StorageList.SelectedIndex >= 0)
            {
                if (_unitTestPage == null)
                {
                    _unitTestPage = new UnitTest();
                    _unitTestPage.SetUnitToTest((ITestingUnit)StorageList.SelectedItem);
                    UnitFrame.Navigate(_unitTestPage);
                }
                else
                {
                    _unitTestPage.SetUnitToTest((ITestingUnit)StorageList.SelectedItem);
                }
            }
        }
    }
}
