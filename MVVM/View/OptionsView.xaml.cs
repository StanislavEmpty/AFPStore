using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AFPStore.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        OptionsViewModel viewModel;
        public OptionsView()
        {
            InitializeComponent();
            viewModel = new();
            DataContext = viewModel;
        }

        private void ButtonEditUser_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditUserProfileCommand.Execute(UserProliesDataGrid.SelectedItem);
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteUserProfileCommand.Execute(UserProliesDataGrid.SelectedItem);
        }

        private void ButtonEditRole_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditRoleCommand.Execute(RolesDataGrid.SelectedItem);
        }

        private void ButtonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteRoleCommand.Execute(RolesDataGrid.SelectedItem);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox searchBox)
            {
                using StoreDbContext db = new();
                if (searchBox.Text.Length > 0)
                {
                    viewModel.UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User)
                        .Where(o => o.FirstName.ToLower().Contains(searchBox.Text.ToLower()) ||
                        o.LastName.ToLower().Contains(searchBox.Text.ToLower())).ToList();
                }
                else
                {
                    viewModel.UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                }
            }
        }
    }
}
