namespace SquidLabs.Hashing;

public static class Reducers
{
    public static byte[] ReduceBytesToSize(this byte[] bytes, int size)
    {
        var output = new byte[size];

        Buffer.BlockCopy(bytes, 0, output, 0, size);

        if (bytes.Length <= size) return output;

        for (var i = size; i < bytes.Length; i++)
            output[i % size] ^= bytes[i];

        return output;
    }
}