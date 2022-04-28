using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class ClientFactoryArgumentNullException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ClientFactoryArgumentNullException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ClientFactoryArgumentNullException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ClientFactoryArgumentNullException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public ClientFactoryArgumentNullException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}