using System;
using System.Collections.Generic;
using System.Linq;
using SquidLabs.Linq.Extensions;
using Xunit;

namespace Tentacles.Extensions.Test;

public class CompactsTests
{
    [Fact]
    public void Compact_RemovesFalsyValues()
    {
        var source = new List<object> { 1, null, "test", "", 0, " ", "hello", false, true };
        var compacted = source.Compact().ToList();

        var expected = new List<object> { 1, "test", "hello", true };
        Assert.Equal(expected, compacted);
    }

    [Fact]
    public void Compact_AllTruthyValues_NoChange()
    {
        var source = new List<int> { 1, 2, 3 };
        var compacted = source.Compact().ToList();

        Assert.Equal(source, compacted);
    }

    [Fact]
    public void Compact_AllFalsyValues_EmptyResult()
    {
        var source = new List<object> { null, "", 0, false };
        var compacted = source.Compact().ToList();

        Assert.Empty(compacted);
    }

    [Fact]
    public void Compact_EmptySource_EmptyResult()
    {
        var source = new List<object>();
        var compacted = source.Compact().ToList();

        Assert.Empty(compacted);
    }

    [Fact]
    public void Compact_NullSource_ThrowsArgumentNullException()
    {
        List<object> source = null;

        var exception = Assert.Throws<ArgumentNullException>(() => source.Compact().ToList());
        Assert.Equal("source", exception.ParamName);
    }
}