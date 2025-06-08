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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = FRM;

            this.WindowState = WindowState.Maximized;

            var fontFamily = new FontFamily(Properties.Settings.Default.FontFamily);
            var fontSize = Properties.Settings.Default.FontSize;

            App.Current.Resources["UserFontFamily"] = fontFamily;
            App.Current.Resources["UserFontSize"] = fontSize;

            this.FontFamily = fontFamily;
            this.FontSize = fontSize;

        }



        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string role = Properties.Settings.Default.UserRole;

    if (role == "Администратор")
    {
        FRM.Navigate(new Page1Test("Admin"));
    }
    else
    {
        FRM.Navigate(new Page1Test());
    }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            FRM.Navigate(new SettingsPage());
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (FRM.CanGoBack)
            {
                FRM.GoBack();
            }
        }
    }
}