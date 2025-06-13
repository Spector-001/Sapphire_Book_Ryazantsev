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
            string confirmPassword = txtConfirmPassword.Password.Trim();

            // Проверка пустых полей
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Проверка минимальной длины пароля
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
            {
                MessageBox.Show("Пароль должен содержать:\n- Не менее 6 символов\n- Заглавные и строчные буквы\n- Цифры");
                return;
            }


            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают. Пожалуйста, введите пароль еще раз.");
                txtPassword.Password = "";
                txtConfirmPassword.Password = "";
                txtPassword.Focus();
                return;
            }

            try
            {
                var context = DataBase1Entities.GetContext();

                // Проверка существующего пользователя
                if (context.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }

                // Создание нового пользователя
                var newUser = new Users
                {
                    Username = username,
                    Password = password, // В реальном приложении пароль должен хэшироваться!
                    Role = "Клиент"
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");

                // Переход на главное окно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }
    }
}
