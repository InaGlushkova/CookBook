using CookBook.Helpers;
using CookBook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CookBook.ViewModel
{
    class LoginRegisterViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand { set; get; }
        public RelayCommand RegisterCommand { set; get; }
        public RelayCommand ExitCommand { set; get; }
        public string Password { set; get; }

        private User _user { set; get; }
        private RecipeStoreDBEntities _ctx = new RecipeStoreDBEntities();
        private List<User> usersList;

        public LoginRegisterViewModel()
        {
            Background = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\LoginScreen.png", UriKind.Absolute));
            LoginCommand = new RelayCommand(Login, ValidLogin);
            RegisterCommand = new RelayCommand(Register);
            ExitCommand = new RelayCommand(Exit);
            LoginResultVisibility = false;
            usersList = _ctx.Users.ToList();
        }

        private BitmapImage _background;
        public BitmapImage Background
        {
            get
            {
                return _background;
            }
            set
            {
                if (_background != value)
                    _background = value;
                RaisePropertyChanged("Background");
            }
        }

        public bool _LoginResultVisibility;
        public bool LoginResultVisibility
        {
            get
            {
                return _LoginResultVisibility;
            }
            set
            {
                _LoginResultVisibility = value;
                RaisePropertyChanged("LoginResultVisibility");
            }
        }

        public string _LoginResult;
        public string LoginResult
        {
            get
            {
                return _LoginResult;
            }
            set
            {
                if (_LoginResult != value)
                {
                    _LoginResult = value;
                    RaisePropertyChanged("LoginResult");
                }
            }
        }

        private void Exit(object obj)
        {
            CloseWindow();
        }

        private bool ValidLogin(object obj)
        {
            var value = (object[])obj;
            string username = (string)value[0];
            PasswordBox boxPass = (PasswordBox)value[1];

            if((username.Length != 0) && (boxPass.Password.Length != 0) && (boxPass.Password.Length >= 6))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Login(object parameter)
        {
            if (parameter == null) return;

            var value = (object[])parameter;
            string username = (string)value[0];
            PasswordBox boxPass = (PasswordBox)value[1];
            User user = usersList.Find(x => x.UserName.ToString() == username);

            Password = boxPass.Password;

            if(user == null)
            {
                LoginResult = "Invalid username or password.";
                LoginResultVisibility = true;
            }
            else 
            {
                if (Password == user.Passw as string)
                {
                    // If login successful open new window with default view BookSearchView
                    var book = new BookView(user.UserId);
                    book.Show();
                    // After opening the new Window close the MainWindow
                    CloseWindow();
                }
                else
                {
                    LoginResult = "Invalid username or password.";
                    LoginResultVisibility = true;
                }
            }
        }

        private void Register(object parameter)
        {
            if (parameter == null) return;
            var registration = new RegisterView { DataContext = new RegisterViewModel() };
            registration.Show();
            CloseWindow();
        }
    }
}
