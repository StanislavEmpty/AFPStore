using System.Linq;
using System.Windows;
using System.Windows.Input;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View.DialogViews;

namespace AFPStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserProfile UserProfile { get; set; }
        public MainWindow()
        {
            InitializeComponent();            
        }

        private bool isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    isMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    isMaximized = true;
                }
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RadioButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using StoreDbContext db = new();
            if(db.Users.ToList().Count > 0)
            {
                Visibility = Visibility.Hidden;
                AuthorizationView view = new();
                if(view.ShowDialog() == true)
                {
                    UserProfile = view.Profile;
                    if(!UserProfile.Role.Name.Contains("Администратор") && db.Roles.ToList().Count > 1)
                    {
                        MenuButtonGraph.Visibility = Visibility.Hidden;
                        MenuButtonOptions.Visibility = Visibility.Hidden;
                    }
                    Visibility = Visibility.Visible;
                    MainTextBlock.Text = string.Format("Привет, {0}!", view.Profile.FirstName);
                }
            }
        }
    }
}
