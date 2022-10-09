namespace Example.Domain.UnitTest;
public class EntityTest
{
    [Fact]
    public void EntityBaseMethodTest()
    {
        // Arrage
        var id = Guid.NewGuid();
        var a = new StubEntity(id);
        var b = new StubEntity(Guid.NewGuid());
        StubEntity? c = null;
        var d = new StubEntity(id);
        StubEntity? e = null;
        // Act

        // Assert
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(c));
        Assert.True(a == d);
        Assert.True(c == e);
        Assert.False(a == e);
        Assert.False(c == a);
        Assert.True(a != b);
        Assert.True(a.GetHashCode() == d.GetHashCode());
        Assert.Contains(a.Id.ToString(), a.ToString());
    }

}
