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
        }

        private void PlayFadeInAnimation(FrameworkElement element)
        {
            if (element == null) return;

            var storyboard = FindResource("ImageFadeIn") as Storyboard;
            if (storyboard != null)
            {
                storyboard = storyboard.Clone(); // Чтобы можно было воспроизвести несколько раз
                Storyboard.SetTarget(storyboard, element);
                storyboard.Begin();
            }
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            PlayFadeInAnimation(imgLogo);
           
        }




        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FRM.Navigate(new Page1Test());
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}




