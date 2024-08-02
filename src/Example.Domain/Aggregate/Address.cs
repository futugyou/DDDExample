namespace Example.Domain;

public record Address : ValueObject<Address>
{
    public string Province { get; init; }
    public string City { get; init; }
    public string County { get; init; }
    public string Street { get; init; }

    internal Address()
    {
        Province = string.Empty;
        City = string.Empty;
        County = string.Empty;
        Street = string.Empty;
    }

    public Address(string province, string city, string county, string street)
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

        Province = province;
        City = city;
        County = county;
        Street = street;
    }
}