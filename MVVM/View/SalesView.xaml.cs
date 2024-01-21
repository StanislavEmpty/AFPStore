using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.ViewModel;
using Microsoft.EntityFrameworkCore;
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

namespace AFPStore.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        SalesViewModel ViewModel { get; set; }
        public SalesView()
        {
            InitializeComponent();
            ViewModel = new SalesViewModel();
            DataContext = ViewModel;
        }

        private async void SearchBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox searchBox)
            {
                using StoreDbContext db = new();
                if (searchBox.Text.Length > 0)
                {
                    await db.Stocks.Include(s => s.Product).Where(o => o.Product.Name.ToLower().Contains(searchBox.Text.ToLower())).OrderBy(o=>o.Quantity).LoadAsync();
                    ViewModel.StorageStocks = db.Stocks.Local.ToObservableCollection();
                    Update();
                }
                else
                {
                    await db.Stocks.Where(o => o.Quantity > 0).Include(s => s.Product).OrderBy(o => o.Product.Name).LoadAsync();
                    ViewModel.StorageStocks = db.Stocks.Local.ToObservableCollection();
                    Update();
                }
            }
        }

        private void ButtonAddToBacket_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddsStockToBacketCommand.Execute(ProdDataGrid.SelectedItem);
            Update();
        }

        private void ButtonShowProduct_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowProductCommand.Execute(ProdDataGrid.SelectedItem);
        }

        private void ButtonRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveStockToBacketCommand.Execute(BacketDataGrid.SelectedItem);
            Update();
        }

        private void ButtonAddQuantityProduct_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddQuantityProductCommand.Execute(BacketDataGrid.SelectedItem);
            Update();
        }

        private void Button_Sale_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaleProductsCommand.Execute(BacketDataGrid.SelectedItem);
            Update();
        }
        private void Update()
        {
            BacketDataGrid.ItemsSource = null;
            BacketDataGrid.ItemsSource = ViewModel.BacketStocks;
            ProdDataGrid.ItemsSource = null;
            ProdDataGrid.ItemsSource = ViewModel.StorageStocks;
        }
    }
}
