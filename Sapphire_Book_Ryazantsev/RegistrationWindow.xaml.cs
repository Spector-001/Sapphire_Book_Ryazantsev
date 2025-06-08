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

namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми.");
                return;
            }

            try
            {
                // Проверяем, существует ли такой пользователь
                var existingUser = DataBase1Entities.GetContext().Users.FirstOrDefault(u => u.Username == username);

                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }

                // Создаем нового пользователя
                var newUser = new Users
                {
                    Username = username,
                    Password = password,
                    Role = "Клиент" // Устанавливаем роль "Клиент" по умолчанию
                };

                DataBase1Entities.GetContext().Users.Add(newUser);
                DataBase1Entities.GetContext().SaveChanges();

                MessageBox.Show("Вы успешно зарегистрированы!");

                // Переход к главному окну
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Закрываем окно регистрации
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }

        }
    }
}
