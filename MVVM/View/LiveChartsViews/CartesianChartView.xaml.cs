using LiveCharts.Wpf;
using LiveCharts;
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
using AFPStore.MVVM.Model.Main;
using Microsoft.EntityFrameworkCore;

namespace AFPStore.MVVM.View.LiveChartsViews
{
    /// <summary>
    /// Логика взаимодействия для CartesianChartView.xaml
    /// </summary>
    public partial class CartesianChartView : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public string YTitle { get; set; }
        public string XTitle { get; set; }

        public CartesianChartView(List<Sale> sales)
        {
            InitializeComponent();
            Labels = new List<string>();
            LineSeries lineSum = new()
            {
                Title = "Сумма",
                Values = new ChartValues<double>()
            };
            LineSeries lineQuantity = new()
            {
                Title = "Количество",
                Values = new ChartValues<int>()
            };
            sales.ForEach(item =>
            {
                lineSum.Values.Add(item.TotalPrice);
                lineQuantity.Values.Add(item.Quantity);
                Labels.Add(item.SaleDate.ToString());
            });
            SeriesCollection = new SeriesCollection { lineSum, lineQuantity };

            YTitle = "Продажи";
            XTitle = "Даты";
            DataContext = this;
        }
    }
}
