using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static SquidLabs.Hashing.SipHash;

namespace Hashing.Tests.SipHash;

public class HashTest
{
    [Fact]
    public void SipHash_OutPut_Should_Match()
    {
        // Arrange
        var key = Enumerable.Range(0, 16).Select(Convert.ToByte).ToArray();
        var message = new byte[Vectors.TestValues.Length];
        var results = new bool[Vectors.TestValues.Length];
        
        // Act
        for (var i = 0; i < Vectors.TestValues.Length; i++)
        {
            message[i] = Convert.ToByte(i);

            // Compute the tag
            var tag = ComputeHash128(message[..i], key);
            
            // Get the target tag
            var targetTag = Vectors.TestValues[i].Select(x => (byte)x).ToArray();
     
            // Store the result of the assertion
            results[i] = tag.SequenceEqual(targetTag);
        }
        
        // Assert
        Assert.DoesNotContain(false, results);
    }
}