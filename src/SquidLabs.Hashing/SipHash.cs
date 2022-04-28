using System.Numerics;
using System.Runtime.CompilerServices;

namespace SquidLabs.Hashing;

public static class SipHash
{
    public static byte[] ComputeHash128(byte[] input, byte[] key, int compressionRounds = 2, int finalizeRounds = 4)
    {
        var k0 = BitConverter.ToUInt64(key, 0);
        var k1 = BitConverter.ToUInt64(key, sizeof(ulong));
        var v0 = 0x736f6d6570736575 ^ k0;
        var v1 = 0x646f72616e646f6d ^ k1 ^ 0xee; //bitwise and of 0xee is specific to 128bit output algorithm 
        var v2 = 0x6c7967656e657261 ^ k0;
        var v3 = 0x7465646279746573 ^ k1;

        ulong m;

        for (var i = 0; i < input.Length; i++)
        {
            m = Convert.ToUInt64(input[i]);
            v3 ^= m;
            for (i = 0; i < compressionRounds; i++) SipRound(ref v0, ref v1, ref v2, ref v3);

            v0 ^= m;
        }

        var leftover = Convert.ToUInt64(input.Length) << 56;

        switch (input.Length & 7)
        {
            case 7:
                leftover |= Convert.ToUInt64(input[6]) << 48;
                goto case 6;
            case 6:
                leftover |= Convert.ToUInt64(input[5]) << 40;
                goto case 5;
            case 5:
                leftover |= Convert.ToUInt64(input[4]) << 32;
                goto case 4;
            case 4:
                leftover |= Convert.ToUInt64(input[3]) << 24;
                goto case 3;
            case 3:
                leftover |= Convert.ToUInt64(input[2]) << 16;
                goto case 2;
            case 2:
                leftover |= Convert.ToUInt64(input[1]) << 8;
                goto case 1;
            case 1:
                leftover |= Convert.ToUInt64(input[0]);
                break;
            case 0:
                break;
        }


        v3 ^= leftover;

        for (var i = 0; i < compressionRounds; i++)
            SipRound(ref v0, ref v1, ref v2, ref v3);

        v0 ^= leftover;
        v2 ^= 0xee;

        for (var i = 0; i < finalizeRounds; i++)
            SipRound(ref v0, ref v1, ref v2, ref v3);

        leftover = v0 ^ v1 ^ v2 ^ v3;
        var output = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(leftover), 0, output, 0, 8);

        v1 ^= 0xdd;

        for (var i = 0; i < finalizeRounds; i++)
            SipRound(ref v0, ref v1, ref v2, ref v3);

        leftover = v0 ^ v1 ^ v2 ^ v3;

        Buffer.BlockCopy(BitConverter.GetBytes(leftover), 8, output, 8, 8);

        return output;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SipRound(ref ulong v0, ref ulong v1, ref ulong v2, ref ulong v3)
    {
        v0 += v1;
        v1 = BitOperations.RotateLeft(v1, 13);
        v1 ^= v0;
        v0 = BitOperations.RotateLeft(v0, 32);
        v2 += v3;
        v3 = BitOperations.RotateLeft(v3, 16);
        v3 ^= v2;
        v0 += v3;
        v3 = BitOperations.RotateLeft(v3, 21);
        v3 ^= v0;
        v2 += v1;
        v1 = BitOperations.RotateLeft(v1, 17);
        v1 ^= v2;
        v2 = BitOperations.RotateLeft(v2, 32);
    }
}