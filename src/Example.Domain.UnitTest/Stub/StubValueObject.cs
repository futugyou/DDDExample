namespace Example.Domain.UnitTest;

public class StubValueObject : ValueObject<StubValueObject>
{
    public StubValueObject(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}

public class Stub1ValueObject : ValueObject<Stub1ValueObject>
{
    public Stub1ValueObject(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}