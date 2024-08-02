namespace Example.Domain.UnitTest;
public class AddressUnitTest
{
    [Fact]
    public void AddressWithoutProvinceTest()
    {
        // Arrange
        var province = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutCityTest()
    {
        // Arrange
        var province = "province";
        var city = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutCountyTest()
    {
        // Arrange
        var province = "province";
        var city = "city";
        var county = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, county, It.IsAny<string>()));
    }

    [Fact]
    public void AddressWithoutStreetTest()
    {
        // Arrange
        var province = "province";
        var city = "city";
        var county = "county";
        var street = "";
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Address(province, city, county, street));
    }

    [Fact]
    public void AddressSuccessTest()
    {
        // Arrange
        var province = "province";
        var city = "city";
        var county = "county";
        var street = "street";
        var street2 = "street2";
        // Act
        var address1 = new Address(province, city, county, street);
        var address2 = new Address(province, city, county, street);
        var address3 = new Address(province, city, county, street2);
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
