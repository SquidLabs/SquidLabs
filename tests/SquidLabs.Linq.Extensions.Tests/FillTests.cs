using System;
using System.Collections.Generic;
using System.Linq;
using SquidLabs.Linq.Extensions;
using Xunit;

namespace Tentacles.Extensions.Test;

public class FillTests
{
    [Fact]
    public void Fill_WithinRange_ChangesElements()
    {
        var source = new List<int> { 1, 2, 3, 4, 5 };
        var filled = source.Fill(0, 1, 3).ToList();

        Assert.Equal([1, 0, 0, 0, 5], filled);
    }

    [Fact]
    public void Fill_StartAtBeginning_ChangesElements()
    {
        var source = new List<int> { 1, 2, 3 };
        var filled = source.Fill(0, 0, 2).ToList();

        Assert.Equal([0, 0, 3], filled);
    }

    [Fact]
    public void Fill_StartAtEnd_NoChange()
    {
        var source = new List<int> { 1, 2, 3 };
        var filled = source.Fill(0, 3, 1).ToList();

        Assert.Equal([1, 2, 3], filled);
    }

    [Fact]
    public void Fill_CountExceedsSize_ChangesElements()
    {
        var source = new List<int> { 1, 2, 3 };
        var filled = source.Fill(0, 1, 10).ToList();

        Assert.Equal([1, 0, 0], filled);
    }

    [Fact]
    public void Fill_NoCount_ChangesAllFromStart()
    {
        var source = new List<int> { 1, 2, 3 };
        var filled = source.Fill(0, 1).ToList();

        Assert.Equal([1, 0, 0], filled);
    }

    [Fact]
    public void Fill_EmptySource_NoChange()
    {
        var source = new List<int>();
        var filled = source.Fill(0, 1, 3).ToList();

        Assert.Empty(filled);
    }

    [Fact]
    public void Fill_NullSource_ThrowsArgumentNullException()
    {
        List<int> source = null;

        var exception = Assert.Throws<ArgumentNullException>(() => source.Fill(0, 1, 3).ToList());
        Assert.Equal("source", exception.ParamName);
    }

    [Fact]
    public void Fill_NegativeStart_ThrowsArgumentOutOfRangeException()
    {
        var source = new List<int> { 1, 2, 3 };

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => source.Fill(0, -1, 3).ToList());
        Assert.Equal("start", exception.ParamName);
    }
}