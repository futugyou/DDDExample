namespace Example.Domain.UnitTest;

public class StubEnum : Enumeration
{
    public static StubEnum Submitted = new StubEnum(1, nameof(Submitted).ToLowerInvariant());
    public StubEnum(int id, string name) : base(id, name)
    {
    }
}

public class StubEnum1 : Enumeration
{
    public StubEnum1(int id, string name) : base(id, name)
    {
    }
}
