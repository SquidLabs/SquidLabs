namespace SquidLabs.Hashing;

public static class SipHash
{
    private static readonly ulong V0 = 0x736f6d6570736575;
    private static readonly ulong V1 = 0x646f72616e646f6d;
    private static readonly ulong V2 = 0x6c7967656e657261;
    private static readonly ulong V3 = 0x7465646279746573;

    private static Guid ComputeHash128(ReadOnlySpan<byte> data, ulong seed)
    {
        return new Guid();
    }
}