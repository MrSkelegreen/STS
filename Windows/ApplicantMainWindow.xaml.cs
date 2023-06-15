using STS.DAL.Entities;
using STS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;

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

            /*string image = "https://papik.pro/uploads/posts/2022-01/1642329969_39-papik-pro-p-testirovanie-klipart-42.png";

            ObservableCollection<Test> Tests = new ObservableCollection<Test>() 
            { 
                new Test() {Title = "Тест на знание вин", 
                                Description = "Тест насколько хорошо вы разбираетесь в сортах вин. Рекомендуется для прохождения сомелье.",
                                Categoryid = 1, Creationdate = new DateOnly(2023,06,11), Rating = 0, Author = 1,},
                new Test() {Title = "Тест на знание вин",
                                Description = "Тест насколько хорошо вы разбираетесь в сортах вин. Рекомендуется для прохождения сомелье.",
                                Categoryid = 1, Creationdate = new DateOnly(2023,06,11), Rating = 0, Author = 1,},
                new Test() {Title = "Тест на знание вин",
                                Description = "Тест насколько хорошо вы разбираетесь в сортах вин. Рекомендуется для прохождения сомелье.",
                                Categoryid = 1, Creationdate = new DateOnly(2023,06,11), Rating = 0, Author = 1,},
                new Test() {Title = "Тест на знание вин",
                                Description = "Тест насколько хорошо вы разбираетесь в сортах вин. Рекомендуется для прохождения сомелье.",
                                Categoryid = 1, Creationdate = new DateOnly(2023,06,11), Rating = 0, Author = 1,},
                new Test() {Title = "Тест на знание вин",
                                Description = "Тест насколько хорошо вы разбираетесь в сортах вин. Рекомендуется для прохождения сомелье.",
                                Categoryid = 1, Creationdate = new DateOnly(2023,06,11), Rating = 0, Author = 1,}
            };*/

            //ListBox.ItemsSource = Tests;

            
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

        



    }

    

}
