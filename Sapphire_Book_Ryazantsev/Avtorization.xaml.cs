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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для Avtorization.xaml
    /// </summary>
    public partial class Avtorization : Window
    {
        public Avtorization()
        {
            InitializeComponent();
            
            Loaded += Avtorization_Loaded;
            
        }

        private void Avtorization_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем общую анимацию прозрачности
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            // Создаем анимацию увеличения размера текста
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            // Анимация для надписи "Sapphire Book" (без задержки)
            lblSapphireBook.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            // Масштабирование надписи "Sapphire Book"
            ScaleTransform scaleTransform = (ScaleTransform)lblSapphireBook.RenderTransform;
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            // Анимация для панели входа с задержкой
            DoubleAnimation loginPanelAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.5) // Задержка в 1.5 секунды
            };

            loginPanel.BeginAnimation(UIElement.OpacityProperty, loginPanelAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем пользователя в базе данных
                var UserLogin = DataBase1Entities.GetContext().Users.FirstOrDefault(x =>
                    x.Username == login.Text && x.Password == psbPassword.Password);

                if (UserLogin == null)
                {
                    MessageBox.Show("Такого пользователя нет!");
                }
                else
                {
                    switch (UserLogin.UserID)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте " + UserLogin.Username);
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show(); // Открываем MainWindow
                            this.Close(); // Закрываем окно авторизации
                            break;

                        case 2:
                            MessageBox.Show("Здравствуйте " + UserLogin.Username);

                            
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message + "\nКритическая ошибка приложения");
            }
        }

        private void btnsmotr_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); // Открываем MainWindow
            this.Close(); // Закрываем окно авторизации
        }
    }
}
