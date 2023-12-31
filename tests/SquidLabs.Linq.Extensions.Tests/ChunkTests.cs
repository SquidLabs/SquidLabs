using System.Collections.Generic;
using System.Linq;
using SquidLabs.Linq.Extensions;
using Xunit;

namespace Tentacles.Extensions.Test;

public class ChunkTests
{
    [Fact]
    public void Split_ListIntoTwoParts_EvenCount()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4 };
        var parts = 2;

        // Act
        var result = Linq.Chunk(list, parts).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new[] { 1, 3 }, result[0]);
        Assert.Equal(new[] { 2, 4 }, result[1]);
    }

    [Fact]
    public void Split_ListIntoThreeParts_UnevenCount()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };
        var parts = 3;

        // Act
        var result = Linq.Chunk(list, parts).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(new[] { 1, 4 }, result[0]);
        Assert.Equal(new[] { 2, 5 }, result[1]);
        Assert.Equal(new[] { 3 }, result[2]);
    }

    [Fact]
    public void Split_EmptyList()
    {
        // Arrange
        var list = new List<int>();
        var parts = 3;

        // Act
        var result = Linq.Chunk(list, parts).ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Split_SingleElementList()
    {
        // Arrange
        var list = new List<int> { 1 };
        var parts = 3;

        // Act
        var result = Linq.Chunk(list, parts).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(new[] { 1 }, result[0]);
    }
}