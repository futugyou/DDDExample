namespace Example.Infrastruct.Data;

public interface IAggregateInvoker<out T> where T : AggregateRoot
{
    T CreateInstanceOfAggregateRoot();
}