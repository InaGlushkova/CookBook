using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

public class BookContext : Context
{
    public BookContext() : base()
    { }
    public ISet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
}
