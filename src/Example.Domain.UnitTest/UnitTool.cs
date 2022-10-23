namespace Example.Domain.UnitTest;

public class UnitTool
{
    public const string LongString = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678900";

    public static T? CreateNewAggregate<T>() where T : AggregateRoot
    {
        var t = typeof(T)
            .GetConstructor(BindingFlags.Instance |
                                     BindingFlags.NonPublic |
                                     BindingFlags.Public,
                            null,
                            Array.Empty<Type>(),
                            Array.Empty<ParameterModifier>())
            ?.Invoke(Array.Empty<object>());
        return t as T;
    }
}
