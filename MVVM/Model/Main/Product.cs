using AFPStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.Model.Main
{
    public class Product : ObservableObject
    {
        #region private fields
        string name = null!;
        double price;
        string parameters = string.Empty;
        DateTime createAt;
        #endregion

        public int Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public string Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                OnPropertyChanged("Parameters");
            }
        }
        public DateTime CreateAt
        {
            get => createAt;
            init
            {
                createAt = value;
                OnPropertyChanged("CreateAt");
            }
        }
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
