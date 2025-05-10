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
using System.Collections.ObjectModel;


namespace Sapphire_Book_Ryazantsev
{
    /// <summary>
    /// Логика взаимодействия для Page1Test.xaml
    /// </summary>
    public partial class Page1Test : Page
    {
      
    private ObservableCollection<Sapphire_Book_Ryazantsev.Books> _allBooks; // Коллекция всех книг
        private ObservableCollection<Sapphire_Book_Ryazantsev.Books> _filteredBooks; // Отфильтрованная коллекция

        public Page1Test()
        {
            InitializeComponent();

            // Загрузка всех книг из базы данных
            var allBooksFromDb = DataBase1Entities.GetContext().Books.ToList();
            _allBooks = new ObservableCollection<Sapphire_Book_Ryazantsev.Books>(allBooksFromDb);

            // Инициализация отфильтрованной коллекции
            _filteredBooks = new ObservableCollection<Sapphire_Book_Ryazantsev.Books>(_allBooks);

            // Привязка к ListView
            lwProducts.ItemsSource = _filteredBooks;
        }

        private static BookDetails currentBookDetailsWindow = null;

        private void lwProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lwProducts.SelectedItem != null)
            {
                var selectedBook = lwProducts.SelectedItem as Sapphire_Book_Ryazantsev.Books;
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

            
            var filtered = _allBooks.Where(book =>
                book.Name.ToLower().Contains(searchText) || // Поиск по названию
                book.Author.ToLower().Contains(searchText) || // Поиск по автору
                book.Category.ToLower().Contains(searchText) // Поиск по категории
            );

            
            _filteredBooks.Clear();
            foreach (var book in filtered)
            {
                _filteredBooks.Add(book);
            }
        }
    }

}
   













