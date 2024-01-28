using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.ViewModel;
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
using System.Windows.Shapes;

namespace AFPStore.MVVM.View.DialogViews
{
    /// <summary>
    /// Логика взаимодействия для CheckView.xaml
    /// </summary>
    public partial class CheckView : Window
    {
        CheckViewModel viewModel;
        public CheckView(ObservableCollection<Sale> sales)
        {
            InitializeComponent();
            viewModel = new()
            {
                Sales = sales
            };
            foreach (var item in viewModel.Sales)
            {
                viewModel.AllSum += item.TotalPrice;
            }
            DataContext = viewModel;
        }

        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
