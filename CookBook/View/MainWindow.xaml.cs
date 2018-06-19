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
using CookBook.ViewModel;

namespace CookBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LoginRegisterViewModel();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
