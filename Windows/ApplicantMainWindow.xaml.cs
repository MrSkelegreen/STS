using STS.DAL.Entities;
using STS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ApplicantMainWindow.xaml
    /// </summary>
    public partial class ApplicantMainWindow : Window
    {
        public ApplicantMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = new AMWvm(user);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
