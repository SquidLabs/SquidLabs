using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class DataStoreException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public DataStoreException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public DataStoreException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public DataStoreException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}