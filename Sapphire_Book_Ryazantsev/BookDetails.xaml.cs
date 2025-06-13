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
    /// Логика взаимодействия для BookDetails.xaml
    /// </summary>
    public partial class BookDetails : Window
    {
  
        public BookDetails(Books book)
        {
            InitializeComponent();

            // Загружаем связанные данные, если они не загружены
            if (book.Author == null || book.Genre == null || book.Shelf == null)
            {
                var context = DataBase1Entities.GetContext();
                context.Entry(book).Reference(b => b.Author).Load();
                context.Entry(book).Reference(b => b.Genre).Load();
                context.Entry(book).Reference(b => b.Shelf).Load();
            }

            DataContext = book;
        }


        
          

        private void btnTakeBook_Click(object sender, RoutedEventArgs e)
        {
           
                }
            }
        }
    


    
    

