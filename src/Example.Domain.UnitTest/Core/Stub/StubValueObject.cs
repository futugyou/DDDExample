namespace Example.Domain.UnitTest;

public record StubValueObject(string Name) : ValueObject<StubValueObject>
{
    public string Name { get; init; } = Name ?? throw new ArgumentNullException(nameof(Name));
}

public record Stub1ValueObject(string Name) : ValueObject<Stub1ValueObject>
{
    public string Name { get; init; } = Name ?? throw new ArgumentNullException(nameof(Name));
}

public record StubNullValueObject() : ValueObject<StubNullValueObject>
{
}