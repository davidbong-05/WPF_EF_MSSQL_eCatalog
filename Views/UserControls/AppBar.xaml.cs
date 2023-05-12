using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace WPF_EF_MSSQL_eCatalog.Views.UserControls
{
    public partial class AppBar : UserControl
    {
        public AppBar()
        {
            InitializeComponent();
        }

        private void menuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuGetData_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            LoadingDataWindow loadingDataWindow = new LoadingDataWindow();
            Opacity = 0.4;
            loadingDataWindow.ShowDialog();
            window.Close();
        }
    }

}
