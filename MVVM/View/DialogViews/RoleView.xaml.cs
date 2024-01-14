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
    /// Логика взаимодействия для RoleView.xaml
    /// </summary>
    public partial class RoleView : Window
    {
        public Role Role { get; set; }
        public RoleView(Role role)
        {
            InitializeComponent();
            Role = role;
            DataContext = Role;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text.Length == 0)
                return;
            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
