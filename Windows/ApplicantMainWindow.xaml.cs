using STS.DAL.Entities;
using STS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace STS.Windows
{
   
    public partial class ApplicantMainWindow : Window
    {
        public ApplicantMainWindow()
        {
            InitializeComponent();        
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;   
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            this.LBox.Items.Refresh();
        }

        private void SubsortingsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.LBox.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.LBox.Items.Refresh();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {               
                SearchButton.Command.Execute(SearchBox.Text);
            }
        }      
    }  
}
