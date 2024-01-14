using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View;
using Microsoft.Extensions.Options;

namespace AFPStore.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        #region Команды
        public RelayCommand SalesViewCommand { get; set; }
        public RelayCommand StorageViewCommand { get; set; }
        public RelayCommand OrdersViewCommand { get; set; }
        public RelayCommand OptionsViewCommand { get; set; }

        #endregion

        public SalesViewModel SalesVM { get; set; }
        public StorageViewModel StorageVM { get; set; }
        public OrdersViewModel OrdersVM { get; set; }
        public OptionsViewModel OptionsVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            SalesVM = new SalesViewModel();
            StorageVM = new StorageViewModel();
            OrdersVM = new OrdersViewModel();
            OptionsVM = new OptionsViewModel();
            CurrentView = SalesVM;

            SalesViewCommand = new RelayCommand(_ => CurrentView = SalesVM);
            StorageViewCommand = new RelayCommand(_ => CurrentView = StorageVM);
            OrdersViewCommand = new RelayCommand(_ => CurrentView = OrdersVM);
            OptionsViewCommand = new RelayCommand(_ => CurrentView = OptionsVM);
        }
    }
}
