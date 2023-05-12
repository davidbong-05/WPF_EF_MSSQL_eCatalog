using System.Windows;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using Newtonsoft.Json;
using WPF_EF_MSSQL_eCatalog.Models;
using WPF_EF_MSSQL_eCatalog.Contexts;
namespace WPF_EF_MSSQL_eCatalog.Views
{
    public partial class LoadingDataWindow : Window
    {

        public LoadingDataWindow()
        {
            InitializeComponent();
            GetAsyncData();
        }
        private void UpdateProgress(int value)
        {
            MyProgressBar.Value = value;
        }
        private void UpdateProgressMax(int value)
        {
            MyProgressBar.Maximum = value;
        }
        private async void GetAsyncData()
        {
            string username = ConfigurationManager.AppSettings["USERNAME"];
            string password = ConfigurationManager.AppSettings["PASSWORD"];
            string api = ConfigurationManager.AppSettings["API_URL"];
            int progress = 0;
            var options = new RestClientOptions(api)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
            var client = new RestClient(options);
            var request = new RestRequest("products");
            request.AddParameter("per_page", 50);
            request.AddParameter("page", 1);
            var response = await client.GetAsync(request);
            var totalPages = 0;
            foreach (var header in response.Headers)
            {
                if (header.Name == "X-WP-Total")
                {
                    UpdateProgressMax(int.Parse(header.Value.ToString()));
                }
                if (header.Name == "X-WP-TotalPages")
                {
                    totalPages = int.Parse(header.Value.ToString());
                }
            }
            dynamic responseJson = JsonConvert.DeserializeObject(response.Content);

            foreach (var item in responseJson)
            {
                Debug.WriteLine($"ID: {item.id} Product: {item.name}");
                await AddProductAsync(new Product(
                       (int)item.id.Value,
                       item.name.ToString(),
                       item.images[0].src.ToString(),
                       "Adipisicing ut pariatur aliqua sit mollit magna nisi do ullamco nostrud nulla veniam.",
                       8.00));
                foreach (var category in item.categories)
                {
                    await AddCategoryAsync(new Category((int)category.id.Value, category.name.ToString()));
                    await AddProductCategoryAsync((int)item.id.Value, (int)category.id.Value);
                }
                progress++;
                UpdateProgress(progress);
            }
            for (int page = 2; page <= totalPages; page++)
            {
                request = new RestRequest("products");
                request.AddParameter("per_page", 50);
                request.AddParameter("page", page);
                response = await client.GetAsync(request);
                responseJson = JsonConvert.DeserializeObject(response.Content);
                foreach (var item in responseJson)
                {
                    Debug.WriteLine($"ID: {item.id} Product: {item.name}");
                    await AddProductAsync(new Product(
                           (int)item.id.Value,
                           item.name.ToString(),
                           item.images[0].src.ToString(),
                           "Adipisicing ut pariatur aliqua sit mollit magna nisi do ullamco nostrud nulla veniam.",
                           8.00));
                    foreach (var category in item.categories)
                    {
                        await AddCategoryAsync(new Category((int)category.id.Value, category.name.ToString()));
                        await AddProductCategoryAsync((int)item.id.Value, (int)category.id.Value);
                    }
                    progress++;
                    UpdateProgress(progress);
                }
                Debug.WriteLine(page);
            }
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }
        public async Task AddProductAsync(Product product)
        {
            var _context = new ApplicationDbContext();
            var exist = _context.Products.FirstOrDefault(e => e.ProductId == product.ProductId);
            if (exist == null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddCategoryAsync(Category category)
        {
            var _context = new ApplicationDbContext();
            var exist = _context.Categories.FirstOrDefault(e => e.CategoryId == category.CategoryId);
            if (exist == null)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProductCategoryAsync(int productId, int categoryId)
        {
            var _context = new ApplicationDbContext();
            var product = _context.Products.FirstOrDefault(e => e.ProductId == productId);
            var category = _context.Categories.FirstOrDefault(e => e.CategoryId == categoryId);
            if (product != null && category != null)
            {
                var exist = _context.ProductCategories.FirstOrDefault(e => e.ProductId == product.Id && e.CategoryId == category.Id);
                if (exist == null)
                {
                    _context.ProductCategories.Add(new ProductCategory(product, category));
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
