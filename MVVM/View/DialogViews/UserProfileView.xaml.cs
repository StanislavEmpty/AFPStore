using AFPStore.MVVM.Model.Main;
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
using System.Windows.Shapes;

namespace AFPStore.MVVM.View.DialogViews
{
    /// <summary>
    /// Логика взаимодействия для UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView : Window
    {
        List<Role> roles;
        public UserProfile UserProfile { get; set; }
        public UserProfileView(UserProfile userProfile)
        {
            InitializeComponent();
            UserProfile = userProfile;
            DataContext = UserProfile;
            using StoreDbContext db = new();
            roles = db.Roles.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxRole.SelectedIndex == -1)
            {
                CustomMessageBoxWithOnlyOKView dialog = new("Выберите роль!");
                dialog.ShowDialog();
                return;
            }
            if(TbFirstName.Text.Length == 0 || TbLogin.Text.Length == 0)
            {
                CustomMessageBoxWithOnlyOKView dialog = new("Проверьте заполнение полей!");
                dialog.ShowDialog();
                return;
            }
            UserProfile.RoleId = (int)ComboBoxRole.SelectedValue;
            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using StoreDbContext db = new();
            ComboBoxRole.DataContext = roles;
            ComboBoxRole.ItemsSource = roles;
            ComboBoxRole.SelectedValuePath = "Id";
            ComboBoxRole.SelectedIndex = roles.FindIndex(r => r.Id == UserProfile.RoleId);
        }
    }
}
