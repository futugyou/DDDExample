using System.Reflection;

namespace Example.Infrastruct;
public class AggregateInvoker<T> : IAggregateInvoker<T> where T : AggregateRoot
{
    public T? CreateInstanceOfAggregateRoot()
    {
        return (T?)typeof(T)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, [], [])
            ?.Invoke([]);
    }
}