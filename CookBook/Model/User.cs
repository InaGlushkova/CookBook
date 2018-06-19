using System;

public class User
{
	public User() //: Recipe()
	{
	}
    public string UserName { get; set; }
    public string Pass { get; set; }

    public Book Book { get; set; }

    public override string ToString() { return this.UserName; }
}
