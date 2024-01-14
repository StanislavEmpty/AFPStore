using AFPStore.Core;
using System.Collections.Generic;

namespace AFPStore.MVVM.Model.Main
{
    public class User : ObservableObject
    {
        string login;   
        string password;
        public int Id { get; set; }
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public virtual UserProfile Profile { get; set; }
    }
}

