using System.Runtime.Serialization;

namespace SquidLabs.Tentacles.Infrastructure.Exceptions;

[Serializable]
public class DataStoreEntryNotFound : DataStoreOperationException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public DataStoreEntryNotFound(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public DataStoreEntryNotFound(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public DataStoreEntryNotFound(string message, Exception innerException, DataStoreOperationTypeEnum operationType)
        : base(message, innerException)
    {
        OperationType = operationType;
    }
    
    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public DataStoreEntryNotFound(string message, Exception innerException, DataStoreOperationTypeEnum operationType, object data)
        : base(message, innerException)
    {
        OperationType = operationType;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="operationType">Type of operation that error resulted from</param>
    public DataStoreEntryNotFound(string message, DataStoreOperationTypeEnum operationType)
        : base(message)
    {
        OperationType = operationType;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataStoreException" /> class.
    /// </summary>
    /// <param name="info">The SerializationInfo.</param>
    /// <param name="context">The StreamingContext.</param>
    public DataStoreEntryNotFound(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}