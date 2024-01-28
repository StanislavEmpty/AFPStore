using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View.LiveChartsViews;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.ViewModel
{
    class OrdersViewModel : ObservableObject
    {
        #region Команды
        public RelayCommand CartesianChartCommand { get; set; }
        public RelayCommand PieChartCommand { get; set; }

        #endregion

        public CartesianChartView CartesianChartView { get; set; }
        public PieChartView PieChartView { get; set; }

        private object _currentChart;
        public object CurrentChart
        {
            get { return _currentChart; }
            set
            {
                _currentChart = value;
                OnPropertyChanged();
            }
        }

        public OrdersViewModel()
        {
            using StoreDbContext db = new();
            List<Sale> sales = db.Sales.Include(o => o.Product).ToList();
            CartesianChartView = new CartesianChartView(sales);
            PieChartView = new PieChartView(sales);
            CurrentChart = CartesianChartView;

            CartesianChartCommand = new RelayCommand(_ => CurrentChart = CartesianChartView);
            PieChartCommand = new RelayCommand(_ => CurrentChart = PieChartView);
        }
    }
}
