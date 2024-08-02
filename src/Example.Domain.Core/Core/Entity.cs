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

    /// <summary>
    /// 重写方法 相等运算
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is not Entity compareTo)
        {
            return false;
        }

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

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
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

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
}