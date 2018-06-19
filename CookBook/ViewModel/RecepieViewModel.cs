using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CookBook.ViewModel
{
    class RecipeViewModel : ViewModelBase
    {
        private string RecepieImagePath { get; set; }
        public RecipeViewModel(Recipe _selectedRecipe)
        {
            Recipe = _selectedRecipe;
            RecepieImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + Recipe.Image.ToString();
            Background  = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Recipes.jpg", UriKind.Absolute));

            if (File.Exists(RecepieImagePath))
            {
                ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + Recipe.Image.ToString(), UriKind.Absolute));
            }
        }

        private Recipe _recipe;
        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                if (_recipe != value)
                {
                    _recipe = value;

                    RecipeName = _recipe.RecipeName.ToString();
                    Ingredients = _recipe.RecipeProd.ToString();
                    RecipeContents = _recipe.RecipeContent.ToString();
                }

            }
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
        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                if (_imageSource != value)
                    _imageSource = value;
                RaisePropertyChanged("ImageSource");
            }

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
                if (_recipeName != value)
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
                if (_ingredients != value)
                {
                    _ingredients = value;
                    RaisePropertyChanged("Ingredients");
                }
            }
        }

        private string _recepieContents;
        public string RecipeContents
        {
            get
            {
                return _recepieContents;
            }
            set
            {
                if (_recepieContents != value)
                {
                    _recepieContents = value;
                    RaisePropertyChanged("RecipeContents");
                }
            }
        }
    }
}
