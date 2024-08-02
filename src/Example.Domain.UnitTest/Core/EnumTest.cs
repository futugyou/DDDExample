namespace Example.Domain.UnitTest;

public class EnumTest
{
    [Fact]
    public void EnumTostringShouldEqualName()
    {
        // Arrange
        var id = 1;
        var name = "name";
        var e = new StubEnum(id, name);
        var tostring = e.ToString();
        // Act

        // Assert
        Assert.NotNull(tostring);
        Assert.True(name == tostring);
    }

    [Fact]
    public void GetAllShouldReturnAllEnumType()
    {
        // Arrange
        var enums = Enumeration.GetAll<StubEnum>();
        // Act

        // Assert
        Assert.NotNull(enums);
        Assert.True(1 == enums.Count());
    }

    [Fact]
    public void FromValuWithExistIDeShouldReturnEnum()
    {
        // Arrange
        var s = Enumeration.FromValue<StubEnum>(1);
        // Act

        // Assert
        Assert.NotNull(s);
        Assert.True(1 == s.Id);
    }

    [Fact]
    public void FromValuWithNotexistIDeShouldThrowInvalidOperationException()
    {
        // Arrange
        // Act

        // Assert
        Assert.Throws<InvalidOperationException>(() => Enumeration.FromValue<StubEnum>(2));
    }

    [Fact]
    public void FromValuWithExistNameeShouldReturnEnum()
    {
        // Arrange
        var s = Enumeration.FromDisplayName<StubEnum>("submitted");
        // Act

        // Assert
        Assert.NotNull(s);
        Assert.True("submitted" == s.Name);
    }

    [Fact]
    public void FromValuWithNotexistNameeShouldThrowInvalidOperationException()
    {
        // Arrange
        // Act

        // Assert
        Assert.Throws<InvalidOperationException>(() => Enumeration.FromDisplayName<StubEnum>("Submitted1"));
    }

    [Fact]
    public void EqualIDAndEqualTypeShouldEqualEnum()
    {
        // Arrange
        var id = 1;
        var name = "name";
        var e = new StubEnum(id, name);
        var f = new StubEnum(id, name);
        // Act

        // Assert
        Assert.True(e == f);
        Assert.True(e.Equals(f));
        Assert.False(e.Equals(new object()));
        Assert.False(null! == f);
        Assert.False(e == null!);
        Assert.True(e.GetHashCode() == f.GetHashCode());
    }

    [Fact]
    public void NotEqualIDOrNotEqualTypeShouldNotEqualEnum()
    {
        // Arrange
        var e = new StubEnum(1, "name");
        var f = new StubEnum(2, "name");
        var e1 = new StubEnum1(1, "name");
        // Act

        // Assert
        Assert.True(e != f);
        Assert.True(!e.Equals(f));

        Assert.True(e != e1);
        Assert.True(!e.Equals(e1));
    }
}
