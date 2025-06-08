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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
            Manager.MainFrame = FRM;
            UpdateUserInfo();
        }

        private void LoadSettings()
        {
            // Загрузка размера шрифта
            double fontSize = Properties.Settings.Default.FontSize;
            foreach (var item in FontSizeSelector.Items)
            {
                if (item is ComboBoxItem comboItem && comboItem.Tag is string sizeStr && double.TryParse(sizeStr, out double currentSize))
                {
                    if (Math.Abs(currentSize - fontSize) < 0.1)
                    {
                        FontSizeSelector.SelectedItem = item;
                        break;
                    }
                }
            }

            // Загрузка шрифта
            string fontFamilyName = Properties.Settings.Default.FontFamily;
            foreach (var item in FontFamilySelector.Items)
            {
                if (item is ComboBoxItem comboItem && comboItem.Tag?.ToString() == fontFamilyName)
                {
                    FontFamilySelector.SelectedItem = item;
                    break;
                }
            }
        }

        // Обработчик выбора шрифта
        private void FontFamilySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilySelector.SelectedItem is ComboBoxItem item && item.Tag is string fontName)
            {
                Properties.Settings.Default.FontFamily = fontName;
                Properties.Settings.Default.Save();

                ApplyFontFamily(new FontFamily(fontName));
            }
        }

        // Обработчик выбора размера шрифта
        private void FontSizeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeSelector.SelectedItem is ComboBoxItem item && item.Tag is string sizeStr && double.TryParse(sizeStr, out double fontSize))
            {
                Properties.Settings.Default.FontSize = fontSize;
                Properties.Settings.Default.Save();

                ApplyFontSize(fontSize);
            }
        }

        // Применить шрифт ко всем окнам
        private void ApplyFontFamily(FontFamily family)
        {
            App.Current.Resources["UserFontFamily"] = family;

            foreach (Window window in Application.Current.Windows)
            {
                window.FontFamily = family;
                foreach (var element in LogicalTreeHelper.GetChildren(window))
                {
                    if (element is FrameworkElement fe)
                    {
                        fe.InvalidateVisual();
                    }
                }
            }
        }

        // Применить размер шрифта ко всем окнам
        private void ApplyFontSize(double size)
        {
            App.Current.Resources["UserFontSize"] = size;
            UpdateAllWindows();
        }

        // Обновить все открытые окна, чтобы изменения вступили в силу
        private void UpdateAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.FontFamily = App.Current.Resources["UserFontFamily"] as FontFamily ?? new FontFamily("Segoe UI");
                window.FontSize = (double)App.Current.Resources["UserFontSize"];
            }
        }

        // Очистка кэша
        private void ClearCache_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кэш успешно очищен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Сброс настроек
        private void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset(); // Сброс к значениям по умолчанию
            LoadSettings(); // Перезагрузка настроек

            // Применяем стандартные настройки
            ApplyFontFamily(new FontFamily(Properties.Settings.Default.FontFamily));
            ApplyFontSize(Properties.Settings.Default.FontSize);

            MessageBox.Show("Настройки сброшены до значений по умолчанию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateUserInfo()
        {
            string currentUser = Properties.Settings.Default.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                UserInfoText.Text = $"Авторизован как: {currentUser}";
                LogoutButton.Visibility = Visibility.Visible; // Показываем кнопку "Выйти"
            }
            else
            {
                UserInfoText.Text = "Не авторизован";
                LogoutButton.Visibility = Visibility.Collapsed; // Скрываем кнопку "Выйти"
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var avtorization = new Avtorization();
            avtorization.Show();

            Application.Current.MainWindow?.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход из аккаунта",
                          MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.CurrentUser = string.Empty;
                Properties.Settings.Default.UserRole = string.Empty;
                Properties.Settings.Default.Save();

                var loginWindow = new Avtorization();
                loginWindow.Show();
                Application.Current.MainWindow?.Close();
            }
        }

    }
}




