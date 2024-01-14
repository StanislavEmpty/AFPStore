using AFPStore.Core;

namespace AFPStore.MVVM.Model.Main
{
    public class UserProfile : ObservableObject
    {
        string firstName;
        string lastName;
        int userId;
        int roleId;
        public int Id { get; set; }
        public string FirstName
        { 
            get => firstName; 
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public int RoleId
        {
            get => roleId;
            set
            {
                roleId = value;
                OnPropertyChanged("RoleId");
            }
        }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}