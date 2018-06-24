using CookBook.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookBook.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        private RecipeStoreDBEntities _ctxDB = new RecipeStoreDBEntities();
        private List<Recipe> _recipeList;
        public DelegateCommand ItemSelectedCommand { get; private set; }
        public DelegateCommand ItemEditCommand { get; private set; }
        public DelegateCommand ItemRemoveCommand { get; private set; }

        private static int UserId { get; set; }

        public SearchViewModel()
        {
            InitializeCommands();
            FillRecipeNames();
        }

        public SearchViewModel(int userId)
        {
            UserId = userId;
            InitializeCommands();
            FillRecipeNames();
        }

        private void InitializeCommands()
        {
            ItemSelectedCommand = new DelegateCommand(OnItemSelected);
            ItemEditCommand = new DelegateCommand(OnItemEdit);
            ItemRemoveCommand = new DelegateCommand(OnItemRemoveAsync);
        }

        private async void OnItemRemoveAsync(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this recipe?", " ", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Recipe deleted.");
                _ctxDB.Recipes.Remove(_ctxDB.Recipes.ToList().Find(x => x.RecipeName == SelectedRecipe));
                await _ctxDB.SaveChangesAsync();
                FillRecipeNames();
            }
        }

        private void OnItemEdit(object obj)
        {
            var recipe = _recipeList.Find(x => x.RecipeName.ToString() == SelectedRecipe);
            var editRecepie = new EditRecipeView { DataContext = new AddNewRecipeViewModel(recipe, UserId) };
            editRecepie.Show();
            CloseWindow();
        }

        private void FillRecipeNames()
        {
            _recipeList = _ctxDB.Recipes.ToList().FindAll(x => x.UserId == UserId);
            Recipes.Clear();
            for (int i = 0; i < _recipeList.Count(); i++)
            {
                Recipes.Add(_recipeList[i].RecipeName.ToString());
            }
        }

        private ObservableCollection<string> RealTimeSearch(string SearchItem)
        {
            Recipes.Clear();
            for (int i = 0; i < _recipeList.Count(); i++)
            {
                if (_recipeList[i].RecipeName.ToString().ToLower().Contains(SearchItem.ToLower()))
                Recipes.Add(_recipeList[i].RecipeName.ToString());
            }

            return Recipes;
        }

        public string _searchItem;
        public string SearchItem
        {
            get
            {
                return _searchItem;
            }
            set
            {
                if (value != _searchItem)
                    _searchItem = value;
                Recipes = RealTimeSearch(_searchItem);
                RaisePropertyChanged("SearchItem");
            }
        }

        private string _selectedRecipe;
        public string SelectedRecipe
        {
            get
            {
                return _selectedRecipe;
            }
            set
            {
                if (value != _selectedRecipe)
                    _selectedRecipe = value;
            }
        }

        private void OnItemSelected(object sender)
        {
            var recipe = _recipeList.Find(x => x.RecipeName.ToString() == SelectedRecipe);
            var recipeView = new RecipeView{ DataContext = new RecipeViewModel(recipe)};
            recipeView.Show();
        }

        public ObservableCollection<string> _recipes = new ObservableCollection<string>();
        public ObservableCollection<string> Recipes
        {
            get
            {
                return _recipes;

            }
            set
            {
                if (value != _recipes)
                    _recipes = value;
                RaisePropertyChanged("Recipes");
            }
        }
    }
}
