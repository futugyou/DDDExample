namespace Example.Domain.Core;

public abstract class ValueObject<T> where T : ValueObject<T>
{
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject<T>)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }

    public virtual T Clone()
    {
        return (T)MemberwiseClone();
    }
}
