using System;

public class Recipe
{
	public Recipe()
	{
	}
    public string RecipeName { get; set; }
    public string Content { get; set; }
    public DateTime? CreationDate { get; set; }
    public byte[] Photo { get; set; }
    public decimal Height { get; set; }
    public float Weight { get; set; }

    public User User { get; set; }

    public override string ToString() { return this.RecipeName; }
}
