using System.Xml.Linq;

namespace Example.Domain;

public class Address : ValueObject<Address>
{
    internal Address()
    {
    }

    public Address(string province, string city,
        string county, string street, string zip)
    {
        if (string.IsNullOrEmpty(province))
        {
            throw new ArgumentNullException(nameof(province));
        }

        if (string.IsNullOrEmpty(city))
        {
            throw new ArgumentNullException(nameof(city));
        }

        if (string.IsNullOrEmpty(county))
        {
            throw new ArgumentNullException(nameof(county));
        }

        if (string.IsNullOrEmpty(street))
        {
            throw new ArgumentNullException(nameof(street));
        }

        this.Province = province;
        this.City = city;
        this.County = county;
        this.Street = street;
    }

    /// <summary>
    /// 省份
    /// </summary>
    public string Province { get; private set; }
    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; private set; }
    /// <summary>
    /// 区县
    /// </summary>
    public string County { get; private set; }
    /// <summary>
    /// 街道
    /// </summary>
    public string Street { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return Province;
        yield return County;
    }
}
