using System.Data.Entity;

public class BookContext : DbContext
{
    public BookContext() : base()
    { }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
}
