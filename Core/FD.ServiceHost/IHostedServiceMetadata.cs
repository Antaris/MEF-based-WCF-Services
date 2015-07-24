namespace FD.ServiceHost
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing hosted service metadata.
    /// </summary>
    public interface IHostedServiceMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        Type ServiceType { get; }
        #endregion
    }
}