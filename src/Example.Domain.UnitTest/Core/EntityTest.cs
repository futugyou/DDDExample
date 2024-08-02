namespace Example.Domain.UnitTest;

public class EntityTest
{
    [Fact]
    public void DifferentEntityTypeShouldNotEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        var b = new StubEntityA(id);

        //Act

        //Assert
        Assert.NotEqual<Entity>(a, b);
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void NullEntityShouldNotEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        StubEntity? n = null;

        //Act

        //Assert
        Assert.NotEqual<Entity>(a, n);
        Assert.False(a.Equals(n));
        Assert.False(a == n!);
        Assert.False(n! == a);
        Assert.True(a != n!);
    }

    [Fact]
    public void ReferenceEqualsEntityShouldEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        var b = a;

        //Act

        //Assert
        Assert.Equal<Entity>(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void EqualEntityShoudEqualHashCode()
    {
        // Arrange
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        var b = new StubEntity(id);

        //Act

        //Assert
        Assert.Equal<Entity>(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }

    [Fact]
    public void EntityToStringShouldContainsID()
    {
        // Arrange
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        var str = a.ToString();

        //Act

        //Assert
        Assert.Contains(id.ToString(), str);
    }
}
