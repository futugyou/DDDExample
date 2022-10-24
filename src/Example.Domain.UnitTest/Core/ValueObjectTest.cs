﻿namespace Example.Domain.UnitTest;

public class ValueObjectTest
{
    [Fact]
    public void OneNullValueObjectShouldNotEqual()
    {
        // Arrage
        var name = "thisisname";
        var a = new StubValueObject(name);
        StubValueObject? b = null;

        // Act

        // Assert
        Assert.NotEqual(a, b);
        Assert.True(!a.Equals(b));
        Assert.True(a != b);
        Assert.True(b != a);
        Assert.False(a == b);
        Assert.False(b == a);
    }

    [Fact]
    public void TwoNullValueObjectShouldEqual()
    {
        // Arrage
        StubValueObject? b = null;
        StubValueObject? c = null;

        // Act

        // Assert
        Assert.True(c == b);
        Assert.True(b == c);
        Assert.False(c != b);
        Assert.False(b != c);
    }

    [Fact]
    public void DifferentValueObjectShouldNotEqual()
    {
        // Arrage
        var name = "thisisname";
        var a = new StubValueObject(name);
        var b = new Stub1ValueObject(name);

        // Act

        // Assert
        Assert.True(!a.Equals(b));
    }

    [Fact]
    public void EqualValueObjectShouldEqualHashCode()
    {
        // Arrage
        var name = "thisisname";
        var a = new StubValueObject(name);
        var b = new StubValueObject(name);

        // Act

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), a.GetHashCode());
    }

    [Fact]
    public void NullEqualityComponentsValueObjectShouldEqualHashCode()
    {
        // Arrage
        var a = new StubNullValueObject();
        var b = new StubNullValueObject();

        // Act

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), a.GetHashCode());
    }

    [Fact]
    public void CloneValueObjectShouldEqual()
    {
        // Arrage
        var name = "thisisname";
        var a = new StubValueObject(name);
        var b = a.Clone();

        // Act

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), a.GetHashCode());
    }

    [Fact]
    public void DifferentValueObjectPropShouldNotEqual()
    {
        // Arrage
        var name = "thisisname";
        var a = new Stub1ValueObject(name);
        name = "thisisname1";
        var b = new Stub1ValueObject(name);

        // Act

        // Assert
        Assert.NotEqual(a, b);
        Assert.False(a.Equals(b));
        Assert.True(a != b);
    }
}
