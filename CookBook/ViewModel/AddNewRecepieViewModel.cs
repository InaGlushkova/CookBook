using CookBook.Helpers;
using CookBook.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CookBook.ViewModel
{
    class AddNewRecipeViewModel : ViewModelBase
    {
        private RecipeStoreDBEntities _ctx = new RecipeStoreDBEntities();
        private Recipe NewRecepie{ set; get; }
        public RelayCommand BrowseCommand { set; get; }
        public RelayCommand SubmitCommand { set; get; }
        public RelayCommand ClearCommand { set; get; }
        public string LeftControlButton { set; get; }
        public string RightControlButton { set; get; }
        private static int UserId { set; get; }

        public AddNewRecipeViewModel(int userId)
        {
            UserId = userId;
            BrowseCommand = new RelayCommand(Browse);
            SubmitCommand = new RelayCommand(Submit, CanSubmit);
            ClearCommand = new RelayCommand(Clear);
            LeftControlButton = "Submit";
            RightControlButton = "Clear";
        }

        public AddNewRecipeViewModel(Recipe editRecipe, int userId)
        {
            UserId = userId;
            BrowseCommand = new RelayCommand(Browse);
            SubmitCommand = new RelayCommand(EditedSubmit);
            ClearCommand = new RelayCommand(Cancel);
            LeftControlButton = "Save";
            RightControlButton = "Cancel";
            LoadRecepieForEdit(editRecipe);
        }

        private void Cancel(object obj)
        {
            var book = new BookView(UserId) ;
            book.Show();
            CloseWindow();
        }

        private void LoadRecepieForEdit(Recipe _recipe)
        {
            string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
            Recipe = _recipe.RecipeContent;
            Ingredients = _recipe.RecipeProd;
            RecipeName = _recipe.RecipeName;
            OriginFilePath = folderpath + _recipe.Image;
        }

        private void EditedSubmit(object obj)
        {
            MessageBoxResult result;

            result = MessageBox.Show("Are you sure you want to save the edited recipe?", " ", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _ctx.Recipes.Remove(_ctx.Recipes.ToList().Find(x=> (x.RecipeName == RecipeName && x.UserId == UserId)));
                _ctx.SaveChangesAsync();
                NewRecepie = new Recipe();
                string imageName = CopyFileGetName();
                DateTime localDate = DateTime.Now;
                String cultureName = "bg-BG";
                var culture = new CultureInfo(cultureName);

                if (imageName.Length != 0)
                    NewRecepie.Image = imageName;

                NewRecepie.UserId = UserId;
                NewRecepie.RecipeContent = Recipe;
                NewRecepie.RecipeProd = Ingredients;
                NewRecepie.RecipeName = RecipeName;
                NewRecepie.RecipeCrDate = DateTime.Now;
                _ctx.Recipes.Add(NewRecepie);
                _ctx.SaveChangesAsync();
            }
        }

        private void Clear(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all fileds?", " ", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                Recipe = " ";
                Ingredients = " ";
                RecipeName = " ";
                OriginFilePath = " ";
            }
        }

        private string CopyFileGetName()
        {
            string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
            string filePath;

            if (OriginFilePath != null && OriginFilePath.Length != 0)
            {
                if (File.Exists(OriginFilePath))
                {
                    filePath = folderpath + System.IO.Path.GetFileName(OriginFilePath);
                    System.IO.File.Copy(OFD.FileName, filePath, true);
                    return System.IO.Path.GetFileName(OriginFilePath);
                }
                else
                {
                    //File Not Found
                }
            }
            return String.Empty;
        }

        private bool CanSubmit(object obj)
        {
            if (RecipeName != null && RecipeName.Length != 0)
                return true;
            else return false;
        }

        private void Submit(object obj)
        {
            NewRecepie = new Recipe();
            string imageName = CopyFileGetName();
            DateTime localDate = DateTime.Now;
            String cultureName = "bg-BG";
            var culture = new CultureInfo(cultureName);

            if (imageName.Length != 0)
            NewRecepie.Image = imageName;

            NewRecepie.UserId = UserId;
            NewRecepie.RecipeContent = Recipe;
            NewRecepie.RecipeProd = Ingredients;
            NewRecepie.RecipeName = RecipeName;
            NewRecepie.RecipeCrDate = DateTime.Now;
            _ctx.Recipes.Add(NewRecepie);
            _ctx.SaveChangesAsync();

            MessageBox.Show("Recipe saved.");
        }

        private string _recipeName;
        public string RecipeName
        {
            get
            {
                return _recipeName;
            }
            set
            {
                _recipeName = value;
                RaisePropertyChanged("RecipeName");
            }
        }

        private string _ingredients;
        public string Ingredients
        {
            get
            {
                return _ingredients;
            }
            set
            {
                _ingredients = value;
                RaisePropertyChanged("Ingredients");
            }
        }

        private string _recipe;
        public string Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;
                RaisePropertyChanged("Recipe");
            }
        }

        private string _originFilePath;
        public string OriginFilePath
        {
            get
            {
                return _originFilePath;
            }
            set
            {
                if (_originFilePath != value)
                {
                    _originFilePath = value;
                    RaisePropertyChanged("OriginFilePath");
                }
            }
        }
        private OpenFileDialog OFD { get; set; }
        private string ResourceFilderPath { get; set; }

        private void Browse(object obj)
        {
            OFD = new OpenFileDialog();

            string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
            OFD.Title = "Select a picture";
            OFD.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            bool? myResult = OFD.ShowDialog();;

            if (myResult != null && myResult == true)
            {
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
            }
            OriginFilePath = OFD.FileName;
        }
    }
}
