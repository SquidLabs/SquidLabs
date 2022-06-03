using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class StoreDuplicateIdentifierException : StoreException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public StoreDuplicateIdentifierException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public StoreDuplicateIdentifierException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public StoreDuplicateIdentifierException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}