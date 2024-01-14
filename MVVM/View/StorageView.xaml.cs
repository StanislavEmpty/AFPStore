using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для StorageView.xaml
    /// </summary>
    public partial class StorageView : UserControl
    {
        StorageViewModel viewModel;
        public StorageView()
        {
            InitializeComponent();
            viewModel = new StorageViewModel();
            DataContext = viewModel;
        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteProductCommand.Execute(ProdDataGrid.SelectedItem);
        }

        private void ButtonEditProduct_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditProductCommand.Execute(ProdDataGrid.SelectedItem);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox searchBox)
            {
                using StoreDbContext db = new();
                if(searchBox.Text.Length > 0)
                {
                    viewModel.Stocks = db.Stocks.Include("Product").Where(o=> o.Product.Name.ToLower().Contains(searchBox.Text.ToLower())).ToList();
                }
                else
                {
                    viewModel.Stocks = db.Stocks.Include("Product").ToList();
                }
            }
        }

        private void ButtonAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddQuantityCommand.Execute(ProdDataGrid.SelectedItem);
        }
    }
}
