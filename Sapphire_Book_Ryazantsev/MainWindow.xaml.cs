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
            Loaded += MainWindow_Loaded;
            this.WindowState = WindowState.Maximized;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем анимацию изменения прозрачности изображения
            DoubleAnimation imageOpacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            // Запускаем анимацию изменения прозрачности изображения
            imgLogo.BeginAnimation(UIElement.OpacityProperty, imageOpacityAnimation);

            // Создаем анимацию увеличения размера текста "Sapphire Book"
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            // Анимация для надписи "Sapphire Book" (изменение прозрачности)
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            // Применяем анимацию к надписи "Sapphire Book"
            lblSapphireBook.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            // Масштабирование надписи "Sapphire Book"
            ScaleTransform scaleTransform = (ScaleTransform)lblSapphireBook.RenderTransform;
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }




        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FRM.Navigate(new Page1Test());
        }

    }
}




