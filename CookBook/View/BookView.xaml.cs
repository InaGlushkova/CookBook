using CookBook.ViewModel;
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

namespace CookBook.View
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookView : Window
    {
        private static int UserId {set; get;}

        public BookView(int userId)
        {
            UserId = userId;
            InitializeComponent();
            OpenSearchview(UserId);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            OpenSearchview(UserId);
        }

        private void Add_New_Click(object sender, RoutedEventArgs e)
        {
            OpenAddNewOrEditView(UserId);
        }
        
        public void OpenAddNewOrEditView(int userId)
        {
            SearchButton.IsEnabled = true;
            AddNewButton.IsEnabled = false;
            DataContext = new AddNewRecipeViewModel(userId);
        }

        public void OpenSearchview(int userId)
        {
            SearchButton.IsEnabled = false;
            AddNewButton.IsEnabled = true;
            DataContext = new SearchViewModel(userId);
        }
    }
}
