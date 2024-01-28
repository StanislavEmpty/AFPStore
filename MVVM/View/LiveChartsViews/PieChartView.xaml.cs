using AFPStore.MVVM.Model.Main;
using LiveCharts;
using LiveCharts.Wpf;
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

namespace AFPStore.MVVM.View.LiveChartsViews
{
    /// <summary>
    /// Логика взаимодействия для PieChartView.xaml
    /// </summary>
    public partial class PieChartView : UserControl
    {
        public SeriesCollection SumCollection { get; set; }
        public SeriesCollection QuantityCollection { get; set; }
        public List<string> SLabels { get; set; }
        public List<string> QLabels { get; set; }
        public string STitle { get; set; }
        public string QTitle { get; set; }
        public PieChartView(List<Sale> sales)
        {
            InitializeComponent();
            var salesGroupedByProducts = sales.GroupBy(o=> o.Product.Name).ToList();
            SLabels = new List<string>();
            QLabels = new List<string>();
            SumCollection = new SeriesCollection();
            QuantityCollection = new SeriesCollection();
            salesGroupedByProducts.ForEach(item =>
            {
                Sale sale = item.First();
                SumCollection.Add(new PieSeries()
                {

                    Title = sale.Product.Name,
                    Values = new ChartValues<double> { sales.Where(o => o.Product.Id.Equals(sale.Product.Id)).Select(o => o.TotalPrice).Sum() },
                    DataLabels = true
                });
                SLabels.Add(sale.Product.Name);
                QuantityCollection.Add(new PieSeries()
                {
                    
                    Title = sale.Product.Name,
                    Values = new ChartValues<int> { sales.Where(o => o.Product.Id.Equals(sale.Product.Id)).Select(o => o.Quantity).Sum() },
                    DataLabels = true
                });
                QLabels.Add(sale.Product.Name);
            });
            STitle = "Сумма";
            QTitle = "Количество";
            DataContext = this;
        }
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart)chartpoint.ChartView;
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
