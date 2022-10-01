namespace Example.Domain.Core;

/// <summary>
/// 定义领域实体基类
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public Guid Id { get; protected set; }

    private List<DomainEvent> _domainEvents = new List<DomainEvent>();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    /// <summary>
    /// 重写方法 相等运算
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }
    /// <summary>
    /// 重写方法 实体比较 ==
    /// </summary>
    /// <param name="a">领域实体a</param>
    /// <param name="b">领域实体b</param>
    /// <returns></returns>
    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }
    /// <summary>
    /// 重写方法 实体比较 !=
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }
    /// <summary>
    /// 获取哈希
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }
    /// <summary>
    /// 输出领域对象的状态
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return GetType().Name + " [Id=" + Id + "]";
    }

    public void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void RemoveDomainEvent(DomainEvent @event)
    {
        _domainEvents.Remove(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}