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
using System.Collections.ObjectModel;
using System.IO;


namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для Page1Test.xaml
    /// </summary>
    public partial class Page1Test : Page
    {
        public bool IsAdmin { get; set; } = false;

        private ObservableCollection<Books> _allBooks;
        private ObservableCollection<Books> _filteredBooks;

        public Page1Test() : this(null)
        {
            InitializeComponent();
            LoadBooks();
        }

        public Page1Test(string userRole) : base()
        {
            InitializeComponent();
            IsAdmin = userRole == "Admin";
            LoadBooks();
            Loaded += Page_Loaded; // Обновляем интерфейс после загрузки страницы
        }

        private void LoadBooks()
        {
            var allBooksFromDb = DataBase1Entities.GetContext().Books.ToList();
            _allBooks = new ObservableCollection<Books>(allBooksFromDb);
            _filteredBooks = new ObservableCollection<Books>(_allBooks);
            lwProducts.ItemsSource = _filteredBooks;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAdminControlsVisibility();
        }

        private void UpdateAdminControlsVisibility()
        {
            btnAddBook.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            btnEditBook.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            btnDeleteBook.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }



    
        private void lwProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lwProducts.SelectedItem != null)
            {
                var selectedBook = lwProducts.SelectedItem as Books;
                if (selectedBook != null)
                {
                    BookDetails bookDetails = new BookDetails(selectedBook);
                    bookDetails.Show();
                }
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TBoxSearch.Text.Trim().ToLower();

            var filtered = _allBooks.Where(b =>
                b.Name.ToLower().Contains(searchText) ||
                b.Author.ToLower().Contains(searchText) ||
                b.Category.ToLower().Contains(searchText));

            _filteredBooks.Clear();
            foreach (var book in filtered)
            {
                _filteredBooks.Add(book);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooks = _filteredBooks.Where(b => b.IsSelected).ToList();

            if (selectedBooks.Count == 0)
            {
                MessageBox.Show("Выберите книгу для редактирования.");
                return;
            }

            foreach (var book in selectedBooks)
            {
                var editWindow = new AddEditBookWindow(book);
                if (editWindow.ShowDialog() == true)
                {
                    var context = DataBase1Entities.GetContext();
                    context.SaveChanges();

                    UpdateList(); // Обновляем список книг
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooks = _filteredBooks.Where(b => b.IsSelected).ToList();

            if (selectedBooks.Count == 0)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            if (MessageBox.Show($"Вы уверены, что хотите удалить {selectedBooks.Count} книг?", "Подтверждение",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var context = DataBase1Entities.GetContext();

                foreach (var book in selectedBooks)
                {
                    context.Books.Remove(book);
                }

                context.SaveChanges();

                UpdateList(); // Обновляем список книг
            }
        }


        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditBookWindow(); // Передаём null → новая книга
            if (addWindow.ShowDialog() == true)
            {
                var context = DataBase1Entities.GetContext();
                var newBook = addWindow.CurrentBook;

                // Копируем изображение в Resources
                if (!string.IsNullOrEmpty(newBook.ImagePath) && File.Exists(newBook.ImagePath))
                {
                    string projectDir = AppDomain.CurrentDomain.BaseDirectory;
                    string resourcesDir = System.IO.Path.Combine(projectDir, "Resources");

                    if (!Directory.Exists(resourcesDir))
                    {
                        Directory.CreateDirectory(resourcesDir);
                    }

                    string fileName = System.IO.Path.GetFileName(newBook.ImagePath);
                    string destinationPath = Path.Combine(resourcesDir, fileName);

                    // Только если файла ещё нет в Resources — копируем
                    if (!File.Exists(destinationPath))
                    {
                        try
                        {
                            File.Copy(newBook.ImagePath, destinationPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка копирования файла: {ex.Message}");
                        }
                    }

                    // Сохраняем только имя файла, не весь путь
                    newBook.ImagePath = fileName;
                }

                context.Books.Add(newBook);
                context.SaveChanges();

                _allBooks.Add(newBook);
                _filteredBooks.Add(newBook);
            }
        }



        private void UpdateList()
        {
            string searchText = TBoxSearch.Text.Trim().ToLower();

            var filtered = _allBooks.Where(b =>
                b.Name.ToLower().Contains(searchText) ||
                b.Author.ToLower().Contains(searchText) ||
                b.Category.ToLower().Contains(searchText));

            _filteredBooks.Clear();
            foreach (var book in filtered)
            {
                _filteredBooks.Add(book);
            }
        }


        private void RemoveDuplicateImages()
        {
            string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string resourcesFolder = Path.Combine(projectDir, "Resources");

            if (!Directory.Exists(resourcesFolder))
                return;

            var files = Directory.GetFiles(resourcesFolder, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                         f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                         f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));

            var fileNames = new HashSet<string>();
            var duplicates = new List<string>();

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                if (fileNames.Contains(fileName))
                {
                    duplicates.Add(file);
                }
                else
                {
                    fileNames.Add(fileName);
                }
            }

            foreach (var duplicate in duplicates)
            {
                try
                {
                    File.Delete(duplicate);
                    MessageBox.Show($"Удалён дубликат: {duplicate}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить дубликат {duplicate}: {ex.Message}");
                }
            }

            if (duplicates.Count == 0)
            {
                MessageBox.Show("Дубликатов не найдено.");
            }
        }
    }
}




















