namespace Example.Domain.UnitTest;

public class ValueObjectTest
{
    [Fact]
    public void ValueObjectBaseMethodTest()
    {
        // Arrage
        var name = "thisisname";
        var a = new StubValueObject(name);
        var b = new StubValueObject(name);
        var c = new Stub1ValueObject(name);

        StubValueObject? d = null;
        StubValueObject? d2 = null;

        name = "thisisname2";
        var e = new StubValueObject(name);
        var f = a.Clone();

        // Act

        // Assert
        Assert.False(a.Equals(d));
        Assert.False(a.Equals(c));
        Assert.True(a.Equals(b));

        Assert.True(a.GetHashCode() == b.GetHashCode());
        Assert.True(0 == c.GetHashCode());

        Assert.True(a == b);
        Assert.False(d == a);
        Assert.True(d == d2);

        Assert.True(a != e);

        Assert.True(a == f);
    }
}
