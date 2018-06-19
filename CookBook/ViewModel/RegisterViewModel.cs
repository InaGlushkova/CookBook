using CookBook.Helpers;
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
    class RegisterViewModel : ViewModelBase
    {
        public RecipeStoreDBEntities _ctx = new RecipeStoreDBEntities();
        private List<User> usersList;

        public RelayCommand RegisterCommand{ set; get; }
        public RelayCommand BackCommand { set; get; }
        private string Password { set; get; }
        private string RePassword { set; get; }

        public RegisterViewModel()
        {
            Background = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\LoginScreen.png", UriKind.Absolute));
            RegisterCommand = new RelayCommand(Register, RegistrationIsValid);
            BackCommand = new RelayCommand(Back);
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

        private void Back(object obj)
        {
            var login = new MainWindow();
            login.Show();
            CloseWindow();
        }

        private string _RegStatus;
        public string RegStatus
        {
            get
            {
                return _RegStatus;
            }
            set
            {
                if (_RegStatus != value)
                {
                    _RegStatus = value;
                    RaisePropertyChanged("RegStatus");
                }
            }
        }

        private bool RegistrationIsValid(object obj)
        {
            var value = (object[])obj;
            TextBox username = (TextBox)value[0];
            PasswordBox boxPass = (PasswordBox)value[1];
            PasswordBox reBoxPass = (PasswordBox)value[2];
            Password = boxPass.Password;
            RePassword = reBoxPass.Password;

            if (username.Text.ToString().Length != 0)
            {
                if ((Password.CompareTo(RePassword) == 0) && (Password.Length >= 6))
                {
                    RegStatus = " ";
                    return true;
                }
                else
                {
                    if(RePassword.Length >= Password.Length && RePassword.Length >= 6)
                    {
                        RegStatus = "Passwords don't match.";
                    }
                    else if  (Password.Length <= 6 && Password.Length != 0 && RePassword.Length != 0)
                    {
                        RegStatus = "Password too short.";
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Register(object obj)
        {
            if (obj == null) return;

            var value = (object[])obj;
            TextBox username = (TextBox)value[0];
            PasswordBox boxPass = (PasswordBox)value[1];
            PasswordBox boxRePass = (PasswordBox)value[2];

            User user = usersList.Find(x => x.UserName.ToString() == username.Text.ToString());

            if (user == null)
            {
                user = new User();
                Password = boxPass.Password;
                user.Passw = Password;
                user.UserName = username.Text.ToString();
                _ctx.Users.Add(user);
                _ctx.SaveChangesAsync();
                var Main = new MainWindow();
                Main.Show();
                CloseWindow();
            }
            else
            {
                MessageBox.Show("Username already taken.");
                username.Clear();
                boxPass.Clear();
                boxRePass.Clear();
            }
        }
    }
}
