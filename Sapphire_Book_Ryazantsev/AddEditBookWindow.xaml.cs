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
    
    public partial class AddEditBookWindow : Window
    {
        private DataBase1Entities _context = DataBase1Entities.GetContext();
        public Books CurrentBook { get; set; }
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Shelf> Shelves { get; set; }

      

        public AddEditBookWindow(Books book = null)
        {
            InitializeComponent();

            // Загрузка справочников
            Authors = _context.Author.ToList();
            Genres = _context.Genre.ToList();
            Shelves = _context.Shelf.ToList();

            cbAuthor.ItemsSource = Authors;
            cbGenre.ItemsSource = Genres;
            cbShelf.ItemsSource = Shelves;

            if (book != null)
            {
                CurrentBook = book;

                // Установка выбранных значений
                cbAuthor.SelectedValue = CurrentBook.AuthorID;
                cbGenre.SelectedValue = CurrentBook.GenreID;
                cbShelf.SelectedValue = CurrentBook.ShelfID;
            }
            else
            {
                CurrentBook = new Books();

                // Значения по умолчанию
                if (Authors.Any()) cbAuthor.SelectedIndex = 0;
                if (Genres.Any()) cbGenre.SelectedIndex = 0;
                if (Shelves.Any()) cbShelf.SelectedIndex = 0;
            }

            DataContext = CurrentBook;
            UpdateImagePreview();
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
            // Привязка выбранных значений
            CurrentBook.AuthorID = (int)cbAuthor.SelectedValue;
            CurrentBook.GenreID = (int)cbGenre.SelectedValue;
            CurrentBook.ShelfID = (int)cbShelf.SelectedValue;

            if (string.IsNullOrWhiteSpace(CurrentBook.Name) ||
                cbAuthor.SelectedValue == null ||
                string.IsNullOrWhiteSpace(CurrentBook.Category))
            {
                MessageBox.Show("Заполните обязательные поля: Название, Автор, Категория");
                return;
            }

            try
            {
                if (CurrentBook.ProductID == 0) // Новая книга
                {
                    _context.Books.Add(CurrentBook);
                }
                else // Редактирование существующей
                {
                    var existingBook = _context.Books.Find(CurrentBook.ProductID);
                    if (existingBook != null)
                    {
                        // Обновляем свойства
                        existingBook.Name = CurrentBook.Name;
                        existingBook.AuthorID = CurrentBook.AuthorID;
                        existingBook.Category = CurrentBook.Category;
                        existingBook.GenreID = CurrentBook.GenreID;
                        existingBook.Description = CurrentBook.Description;
                        existingBook.ShelfID = CurrentBook.ShelfID;
                        existingBook.Status = CurrentBook.Status;
                        existingBook.ImagePath = CurrentBook.ImagePath;
                    }
                }

                _context.SaveChanges();
                DialogResult = true;
                Close();
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


  







