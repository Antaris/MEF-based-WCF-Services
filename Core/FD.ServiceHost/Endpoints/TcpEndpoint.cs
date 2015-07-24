namespace FD.ServiceHost.Endpoints
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    /// <summary>
    /// Describes an endpoint that supports the Tcp scheme.
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TcpEndpointAttribute : EndpointAttribute
    {
        #region Fields
        private const int DefaultPort = 40001;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="TcpEndpointAttribute"/>.
        /// </summary>
        public TcpEndpointAttribute() : base(DefaultPort) { }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of a <see cref="ServiceEndpoint"/> that represents the endpoint.
        /// </summary>
        /// <param name="description">The contract description.</param>
        /// <param name="meta">The hosted service metadata.</param>
        /// <returns>An instance of <see cref="ServiceEndpoint"/></returns>
        internal override ServiceEndpoint CreateEndpoint(ContractDescription description, IHostedServiceMetadata meta)
        {
            var uri = CreateUri("net.tcp", meta);
            var address = new EndpointAddress(uri);

            var binding = new NetTcpBinding();
            return new ServiceEndpoint(description, binding, address);
        }
        #endregion
    }
}
