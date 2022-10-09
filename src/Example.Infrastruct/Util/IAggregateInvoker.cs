namespace Example.Infrastruct;

public interface IAggregateInvoker<out T> where T : AggregateRoot
{
    T CreateInstanceOfAggregateRoot();
}