using AFPStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.Model.Main
{
    public class Sale : ObservableObject
    {
        DateTime saleDate;
        int productId;
        int quantity;
        double totalPrice;
        public int Id { get; set; }
        public DateTime SaleDate
        { 
            get => saleDate;
            set
            {
                saleDate = value;
                OnPropertyChanged("SaleDate");
            }
        }
        public int ProductId
        {
            get => productId;
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
            }
        }
        public virtual Product Product { get; set; }
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public double TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
    }

}
