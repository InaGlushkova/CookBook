using System;

public class Book
{
    public Book()
    {

    }
    public int BookId { get; set; }
    public string BookName { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}
