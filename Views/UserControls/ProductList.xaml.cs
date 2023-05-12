using System.Windows.Controls;
using WPF_EF_MSSQL_eCatalog.ViewModels;

namespace WPF_EF_MSSQL_eCatalog.Views.UserControls
{
    public partial class ProductList : UserControl
    {
        private readonly ProductListViewModel _productListViewModel = new ProductListViewModel();

        public ProductList()
        {
            InitializeComponent();
            DataContext = _productListViewModel;
        }
    }

}
