using System.Linq;
using Xunit;
using static SquidLabs.Hashing.SipHash;

namespace Hashing.Tests.SipHash;

public class HashTest
{
    [Fact]
    public void test()
    {
        var key = Enumerable.Range(0, 16).Select(x => (byte)x).ToArray();
        var message = new byte[Vectors.TestValues.Length];
        for (var i = 0; i < Vectors.TestValues.Length; i++)
        {
            message[i] = (byte)i;

            // Compute the tag
            var tag = ComputeHash128(message, key);
            // Get the target tag
            var targetTag = Vectors.TestValues[i].Select(x => (byte)x).ToArray();

            Assert.True(tag.SequenceEqual(targetTag));
        }
    }
}