using System;
using System.Collections.Generic;
using System.Linq;
using SquidLabs.Linq.Extensions;
using Xunit;

namespace Tentacles.Extensions.Test;

public class PartitionTests
{
    [Fact]
    public void Partition_WithValidPredicate_PartitionsCorrectly()
    {
        var source = new List<int> { 1, 2, 3, 4, 5 };
        var (truthy, falsy) = source.Partition(n => n % 2 == 0);

        Assert.Equal([2, 4], truthy.ToList());
        Assert.Equal([1, 3, 5], falsy.ToList());
    }

    [Fact]
    public void Partition_EmptySource_ReturnsEmptyPartitions()
    {
        var source = new List<int>();
        var (truthy, falsy) = source.Partition(n => n % 2 == 0);

        Assert.Empty(truthy);
        Assert.Empty(falsy);
    }

    [Fact]
    public void Partition_AllTruthy_ReturnsAllTruthy()
    {
        var source = new List<int> { 2, 4, 6 };
        var (truthy, falsy) = source.Partition(n => n % 2 == 0);

        Assert.Equal(source, truthy.ToList());
        Assert.Empty(falsy);
    }

    [Fact]
    public void Partition_AllFalsy_ReturnsAllFalsy()
    {
        var source = new List<int> { 1, 3, 5 };
        var (truthy, falsy) = source.Partition(n => n % 2 == 0);

        Assert.Empty(truthy);
        Assert.Equal(source, falsy.ToList());
    }

    [Fact]
    public void Partition_NullSource_ThrowsArgumentNullException()
    {
        List<int> source = null;

        var exception = Assert.Throws<ArgumentNullException>(() => source.Partition(n => n % 2 == 0));
        Assert.Equal("source", exception.ParamName);
    }

    [Fact]
    public void Partition_NullPredicate_ThrowsArgumentNullException()
    {
        var source = new List<int> { 1, 2, 3 };

        var exception = Assert.Throws<ArgumentNullException>(() => source.Partition(null));
        Assert.Equal("predicate", exception.ParamName);
    }
}