using AFPStore.MVVM.Model.Main;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window
    {
        public UserProfile Profile { get; set; }
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TbLogin.Text;
            string password = TbPassword.Text;
            using StoreDbContext db = new();
            var user = db.Users.Include("Profile").Where(o => o.Login == login && o.Password == password).FirstOrDefault();
            if(user != null)
            {
                Profile = user.Profile;
                Profile.Role = db.Roles.Where(o=>o.Id == Profile.RoleId).First();
                DialogResult = true;
                Close();
            }
            else
            {
                CustomMessageBoxWithOnlyOKView dialog = new("Неверное имя пользователя или пароль!");
                dialog.ShowDialog();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(sender is TextBox textbox)
            {
                if(e.Key == Key.Enter)
                {
                    if(textbox.Name.Contains("Login"))
                    {
                        TbPassword.Focus();
                    }
                    else
                    {
                        BtnOK.Focus();
                    }
                }
            }
        }
    }
}
