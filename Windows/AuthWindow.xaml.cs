using Microsoft.EntityFrameworkCore;
using STS.DAL.Entities;
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

namespace STS
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            this.DataContext = new AuthVM();
            pwBox.Password = "1";
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(pwBox.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
