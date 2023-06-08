using STS.ViewModels;
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

namespace STS.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public string[] roles { get; set; }
        public RegWindow()
        {
            InitializeComponent();

            this.DataContext = new RegVM();

            roles = new string[] { "Соискатель", "Работодатель"};

            this.roleBox.ItemsSource = roles;
            this.roleBox.SelectedIndex = 0;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwBox.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        
        

    }
}
