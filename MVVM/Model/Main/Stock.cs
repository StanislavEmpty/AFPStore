using AFPStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.Model.Main
{
    public class Stock : ObservableObject
    {
        int quantity;
        int productId;
        public int Id { get; set; }
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
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
    }
}
