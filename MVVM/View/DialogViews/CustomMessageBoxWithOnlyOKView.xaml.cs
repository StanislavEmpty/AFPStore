﻿using System;
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
    /// Логика взаимодействия для CustomMessageBoxWithOnlyOKView.xaml
    /// </summary>
    public partial class CustomMessageBoxWithOnlyOKView : Window
    {
        public CustomMessageBoxWithOnlyOKView(string messageText)
        {
            InitializeComponent();
            TextBoxMessage.Text = messageText;
        }
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
