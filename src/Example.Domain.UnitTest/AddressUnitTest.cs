namespace Example.Domain.UnitTest;
public class AddressUnitTest
{
    [Fact]
    public void AddressWithoutProvinceTest()
    {
        // Arrage
        var province = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutCityTest()
    {
        // Arrage
        var province = "provice";
        var city = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutCountyTest()
    {
        // Arrage
        var province = "provice";
        var city = "city";
        var county = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, county, It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutStreetTest()
    {
        // Arrage
        var province = "provice";
        var city = "city";
        var county = "county";
        var street = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, county, street, It.IsAny<string>()));
    }

    [Fact]
    public void AddressSuccessTest()
    {
        // Arrage
        var province = "provice";
        var city = "city";
        var county = "county";
        var street = "street";
        var street2 = "street2";
        // Act
        var address1 = new Address(province, city, county, street, It.IsAny<string>());
        var address2 = new Address(province, city, county, street, It.IsAny<string>());
        var address3 = new Address(province, city, county, street2, It.IsAny<string>());
        // Assert
        Assert.True(address1 == address2);
        Assert.Equal(address1, address2);
        Assert.True(address1.Equals((object)address2));
        Assert.False(address1 == (object)address2);
        Assert.True(address1.GetHashCode() == address2.GetHashCode());
        Assert.True(address2 != address3);
        Assert.True(address1 != address3);
    }
}
