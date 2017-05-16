using BotFactory.Factories;
using BotFactory.Pages;
using BotFactory.Tools;
using System.Windows;

namespace BotFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _dataContext = new DataContext();
        private FactoryTest _factoryTestPage = new FactoryTest();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(_factoryTestPage);
            _factoryTestPage.SetTestingFactory(new UnitFactory(5, 10));
        }
    }
}
