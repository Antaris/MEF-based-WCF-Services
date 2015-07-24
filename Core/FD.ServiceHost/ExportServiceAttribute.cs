namespace FD.ServiceHost
{
    using System;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Allows the export of a hosted service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true), MetadataAttribute]
    public class ExportServiceAttribute : ExportAttribute, IHostedServiceMetadata
    {
        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="ExportServiceAttribute" />.
        /// </summary>
        /// <param name="name">The name of the service.</param>
        /// <param name="serviceType">The service type.</param>
        public ExportServiceAttribute(string name, Type serviceType)
            : base(typeof(IHostedService))
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is a required parameter.", "name");

            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            Name = name;
            ServiceType = serviceType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        public Type ServiceType { get; private set; }
        #endregion
    }
}