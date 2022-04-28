namespace SquidLabs.Tentacles.Infrastructure.Aws;

/// <summary>
/// </summary>
public interface IAwsCredentials
{
    /// <summary>
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// </summary>
    public string Token { get; set; }
}