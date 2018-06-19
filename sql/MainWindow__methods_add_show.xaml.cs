using System;

private void Recipt_Click(object sender, RoutedEventArgs e)
{
	using (var ctx = new BookContext())
    {
        Recipe rcp = new Recipe()
        {
            RecipeName = "New Recipe" + DateTime.Now.ToLongTimeString()
        };
        ctx.Recipe.Add(rcp);
        ctx.SaveChanges();
    }
}

private void ShowRecipeClick(object sender, RoutedEventArgs e)
{
    using (var ctx = new BookContext())
    {
        RecipesList.ItemsSource = ctx.Recipes.ToList();
    }
}
