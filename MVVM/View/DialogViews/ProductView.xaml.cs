using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace AFPStore.MVVM.View.DialogViews
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public Dictionary<string,string>? Parameters { get; set; }
        public KeyValuePair<string,string> SelectedPair { get; set; }
        public Stock Stock { get; set; }
        public ProductView(Stock stock, bool IsReadOnly)
        {
            InitializeComponent();
            Stock = stock;
            DataContext = Stock;
            if(stock.Product.Parameters.Length > 0)
            {
                Parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(Stock.Product.Parameters);
                if(Parameters != null)
                {
                    ParamsDataGrid.DataContext = Parameters;
                    ParamsDataGrid.ItemsSource = Parameters;
                }
            }
            else
            {
                Parameters = new();
                ParamsDataGrid.DataContext = Parameters;
                ParamsDataGrid.ItemsSource = Parameters;
            }
            if(IsReadOnly)
            {
                TbName.IsReadOnly = true;
                TbPrice.IsReadOnly = true;
                TbQuantity.IsReadOnly = true;
                BtnSave.Visibility = Visibility.Hidden;
                BtnSave.IsEnabled = false;
                BtnCreateParam.Visibility = Visibility.Hidden;
                BtnCreateParam.IsEnabled = false;
                ParamsDataGrid.Columns.Last().Visibility = Visibility.Hidden;
            }
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ParamsDataGrid.IsReadOnly)
                return;
            if (ParamsDataGrid.SelectedItem is KeyValuePair<string,string>  pair) 
            {
                Parameters?.Remove(pair.Key);
                Parameters.TryAdd(Uid, pair.Value);
                Parameters.Remove(Uid);
                ParamsDataGrid.ItemsSource = null;
                ParamsDataGrid.ItemsSource = Parameters;
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string str = JsonConvert.SerializeObject(Parameters, Formatting.Indented);
            Stock.Product.Parameters = str;
            Stock.Product.Name = TbName.Text;
            try
            {
                Stock.Quantity = int.Parse(TbQuantity.Text);
                Stock.Product.Price = double.Parse(TbPrice.Text);
                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void ButtonAddParams_Click(object sender, RoutedEventArgs e)
        {
            ParameterView view = new();
            if(view.ShowDialog() == true)
            {
                bool res = Parameters.TryAdd(view.Pair.Key, view.Pair.Value);
                if(res)
                {
                    ParamsDataGrid.ItemsSource = null;
                    ParamsDataGrid.ItemsSource = Parameters;
                }
            }
        }
    }
}
