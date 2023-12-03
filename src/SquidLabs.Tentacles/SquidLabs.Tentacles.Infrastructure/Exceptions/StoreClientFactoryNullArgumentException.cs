using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class StoreClientFactoryArgumentNullException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public StoreClientFactoryArgumentNullException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public StoreClientFactoryArgumentNullException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public StoreClientFactoryArgumentNullException(SerializationInfo info, StreamingContext context)
    {
    }
}