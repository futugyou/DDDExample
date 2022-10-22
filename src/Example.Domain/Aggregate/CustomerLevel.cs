namespace Example.Domain;

public class CustomerLevel : Enumeration
{
    public static CustomerLevel Comman = new(1, nameof(Comman));
    public static CustomerLevel Silver = new(1, nameof(Silver));
    public static CustomerLevel Golden = new(1, nameof(Golden));
    public static CustomerLevel Diamond = new(1, nameof(Diamond));
    public static CustomerLevel Platinum = new(1, nameof(Platinum));

    public CustomerLevel(int id, string name) : base(id, name)
    {
    }
}
