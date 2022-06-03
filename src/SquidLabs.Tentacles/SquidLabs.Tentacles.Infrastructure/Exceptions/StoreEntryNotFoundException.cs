using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class StoreEntryNotFoundException : StoreOperationException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public StoreEntryNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public StoreEntryNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public StoreEntryNotFoundException(string message, Exception innerException, StoreOperationTypeEnum operationType)
        : base(message, innerException)
    {
        OperationType = operationType;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public StoreEntryNotFoundException(string message, Exception innerException, StoreOperationTypeEnum operationType,
        object data)
        : base(message, innerException)
    {
        OperationType = operationType;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public StoreEntryNotFoundException(string message, StoreOperationTypeEnum operationType)
        : base(message)
    {
        OperationType = operationType;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StoreException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public StoreEntryNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}