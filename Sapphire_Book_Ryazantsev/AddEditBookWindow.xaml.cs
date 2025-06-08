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
using Microsoft.Win32;
using System.IO;


namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для AddEditBookWindow.xaml
    /// </summary>
    public partial class AddEditBookWindow : Window
    {

        public Books CurrentBook { get; set; }

        public AddEditBookWindow(Books book = null)
        {
            InitializeComponent();

            if (book != null)
            {
                CurrentBook = book;
            }
            else
            {
                CurrentBook = new Books();
            }

            this.DataContext = CurrentBook;
            UpdateImagePreview(); // Загружаем превью, если книга редактируется
        }

        private void UpdateImagePreview()
        {
            if (!string.IsNullOrWhiteSpace(CurrentBook.ImagePath))
            {
                string imagePath = $"/Resources/{CurrentBook.ImagePath}";

                if (File.Exists(imagePath))
                {
                    imgPreview.Source = new BitmapImage(new Uri(imagePath));
                }
                else
                {
                    imgPreview.Source = null;
                }
            }
            else
            {
                imgPreview.Source = null;
            }
        }





        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (dialog.ShowDialog() == true)
            {
                string selectedFilePath = dialog.FileName;
                string fileName = Path.GetFileName(selectedFilePath);

                // Путь к папке Resources
                string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string resourcesFolder = Path.Combine(projectDir, "Resources");

                try
                {
                    if (!Directory.Exists(resourcesFolder))
                    {
                        Directory.CreateDirectory(resourcesFolder);
                    }

                    string destinationPath = Path.Combine(resourcesFolder, fileName);

                    // Если файл уже существует — не копируем его заново
                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(selectedFilePath, destinationPath, overwrite: true);
                    }

                    CurrentBook.ImagePath = fileName;
                    txtImagePath.Text = fileName;

                    UpdateImagePreview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования файла: {ex.Message}");
                }
            }
        }




        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentBook.Name) ||
         string.IsNullOrWhiteSpace(CurrentBook.Author) ||
         string.IsNullOrWhiteSpace(CurrentBook.Category))
            {
                MessageBox.Show("Заполните обязательные поля: Название, Автор, Категория");
                return;
            }

            var context = DataBase1Entities.GetContext();

            if (CurrentBook.ProductID == 0)
            {
                context.Books.Add(CurrentBook);
            }
            else
            {
                var existingBook = context.Books.Find(CurrentBook.ProductID);
                if (existingBook != null)
                {
                    existingBook.Name = CurrentBook.Name;
                    existingBook.Author = CurrentBook.Author;
                    existingBook.Category = CurrentBook.Category;
                    existingBook.Genre = CurrentBook.Genre;
                    existingBook.Description = CurrentBook.Description;
                    existingBook.Shelf = CurrentBook.Shelf;
                    existingBook.Status = CurrentBook.Status;
                    existingBook.ImagePath = CurrentBook.ImagePath;
                }
            }

            try
            {
                context.SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }




        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}


  







