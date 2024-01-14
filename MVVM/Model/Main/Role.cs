using AFPStore.Core;
using System.Collections.Generic;

namespace AFPStore.MVVM.Model.Main
{
    public class Role : ObservableObject
    {
        string name = null!;
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
    }
}