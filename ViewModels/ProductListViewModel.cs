using System.Collections.Generic;
using System.Windows.Input;
using WPF_EF_MSSQL_eCatalog.Services;
using WPF_EF_MSSQL_eCatalog.Models;
using WPF_EF_MSSQL_eCatalog.Contexts;
using WPF_EF_MSSQL_eCatalog.Commands;
using System;

namespace WPF_EF_MSSQL_eCatalog.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private readonly ProductService _productService;
        private readonly int _pageSize = 10;
        private int _currentPage = 1;
        private int _totalPages;
        private List<Product> _products;

        public ProductListViewModel()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            _productService = new ProductService(dbContext);
            LoadProducts();
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    LoadProducts();
                }
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));
                }
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        public ICommand NextPageCommand => new ProductListViewModelCommand(NextPage, CanNextPage);
        public ICommand PreviousPageCommand => new ProductListViewModelCommand(PreviousPage, CanPreviousPage);
        public ICommand NextPagesCommand => new ProductListViewModelCommand(NextPages, CanNextPages);
        public ICommand PreviousPagesCommand => new ProductListViewModelCommand(PreviousPages, CanPreviousPages);

        private bool CanNextPage() => CurrentPage < TotalPages;
        private bool CanPreviousPage() => CurrentPage > 1;
        private bool CanNextPages() => CurrentPage + 9 < TotalPages;
        private bool CanPreviousPages() => CurrentPage - 9 > 1;
        private void NextPage() => CurrentPage++;
        private void PreviousPage() => CurrentPage--;
        private void NextPages() => CurrentPage += 10;
        private void PreviousPages() => CurrentPage -= 10;

        private async void LoadProducts()
        {
            Products = await _productService.GetProductsAsync(_currentPage, _pageSize);
            var totalProduct = await _productService.GetTotalProductCountAsync();
            TotalPages = (int)Math.Ceiling((double)totalProduct / _pageSize);
        }
    }

}