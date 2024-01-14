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
    /// Логика взаимодействия для ParameterView.xaml
    /// </summary>
    public partial class ParameterView : Window
    {
        public KeyValuePair<string,string> Pair { get; set; }
        public ParameterView()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Pair = new(TbName.Text,TbValue.Text);
            DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
