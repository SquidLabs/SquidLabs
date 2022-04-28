using System.Runtime.CompilerServices;
using System.Text;

namespace SquidLabs.Hashing;

/// <summary>
/// </summary>
public static class NormalizedConverter
{
    /// <summary>
    ///     Convert DateTime to bytes
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static byte[] GetBytes(DateTime dt)
    {
        return NormalizeBytesByEndianness(BitConverter.GetBytes(dt.ToUniversalTime().ToBinary()));
    }

    /// <summary>
    ///     Convert int to bytes
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static byte[] GetBytes(int i)
    {
        return NormalizeBytesByEndianness(BitConverter.GetBytes(i));
    }

    /// <summary>
    ///     Convert long to bytes
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static byte[] GetBytes(long l)
    {
        return NormalizeBytesByEndianness(BitConverter.GetBytes(l));
    }

    /// <summary>
    ///     Convert ulong to bytes
    /// </summary>
    /// <param name="ul"></param>
    /// <returns></returns>
    public static byte[] GetBytes(ulong ul)
    {
        return NormalizeBytesByEndianness(BitConverter.GetBytes(ul));
    }

    /// <summary>
    ///     Convert string to bytes
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string? s, Encoding? encoding = null)
    {
        if (s is null) return new byte[] { };
        encoding ??= Encoding.Default;
        // TODO: check if Endianness flip is necessary 
        return encoding.GetBytes(s);
    }

    /// <summary>
    ///     Convert URI to bytes
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static byte[] GetBytes(Uri uri)
    {
        return NormalizeBytesByEndianness(Encoding.UTF8.GetBytes(uri.ToString()));
    }

    /// <summary>
    ///     Normalize bit order for different system Endianess
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] NormalizeBytesByEndianness(byte[] bytes)
    {
        if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
        return bytes;
    }
}