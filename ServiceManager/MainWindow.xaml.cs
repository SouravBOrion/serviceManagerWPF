using ServiceManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceControllerRepository presentSc;

        public MainWindow()
        {
            InitializeComponent();
            presentSc = new ServiceControllerRepository("MSSQL$SOURAVSQLXPRESS");
            statuslbl.Content = presentSc.getStatus();
            bindList();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            String status;
            statuslbl.Content = "Starting Please Wait";
            presentSc.Start(6000, out status);
            statuslbl.Content = status;
            MessageBox.Show("Local SQL Server Started", "Service Manager Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            String status;
            statuslbl.Content = "Stopping Please Wait";
            presentSc.Stop(6000, out status);
            statuslbl.Content = status;
            MessageBox.Show("Local SQL Server Stopped", "Service Manager Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            String status;
            statuslbl.Content = "Stopping Please Wait";
            presentSc.Restart(10000, out status);
            statuslbl.Content = status;
            MessageBox.Show("Local SQL Server Restarted", "Service Manager Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void bindList(string filter="all")
        {
            var filteredServiceList = presentSc.getAllServices(filter);
            serviceList.ItemsSource = filteredServiceList;

        }
    
    }
}
