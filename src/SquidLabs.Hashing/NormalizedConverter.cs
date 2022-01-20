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
        return NormalizeBytesByEndianess(BitConverter.GetBytes(dt.ToUniversalTime().ToBinary()));
    }

    /// <summary>
    ///     Convert int to bytes
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static byte[] GetBytes(int i)
    {
        return NormalizeBytesByEndianess(BitConverter.GetBytes(i));
    }

    /// <summary>
    ///     Convert long to bytes
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static byte[] GetBytes(long l)
    {
        return NormalizeBytesByEndianess(BitConverter.GetBytes(l));
    }

    /// <summary>
    ///     Convert ulong to bytes
    /// </summary>
    /// <param name="ul"></param>
    /// <returns></returns>
    public static byte[] GetBytes(ulong ul)
    {
        return NormalizeBytesByEndianess(BitConverter.GetBytes(ul));
    }

    /// <summary>
    ///     Convert string to bytes
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string s, Encoding encoding = null)
    {
        encoding ??= Encoding.Unicode;
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
        return NormalizeBytesByEndianess(Encoding.UTF8.GetBytes(uri.ToString()));
    }

    /// <summary>
    ///     Normalize bit order for different system Endianess
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private static byte[] NormalizeBytesByEndianess(byte[] bytes)
    {
        if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
        return bytes;
    }
}