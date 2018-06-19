using System;

public class User
{
	public User() //: Recipe()
	{
	}
    public string UserName { get; set; }
    public string Pass { get; set; }

    public Standard Standard { get; set; }

    public override string ToString() { return this.UserName; }
}
