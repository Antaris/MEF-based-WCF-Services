namespace FD.ServiceHost.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    using Composition;
    using Hosting;

    /// <summary>
    /// Defines a service host factory for dynamic web services.
    /// </summary>
    public class WebServiceHostFactory : ServiceHostFactory
    {
        #region Fields
        private static CompositionContainer _container;
        private static readonly object sync = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the composition container.
        /// </summary>
        public CompositionContainer Container
        {
            get
            {
                lock (sync)
                {
                    return _container;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the service host that will handle web service requests.
        /// </summary>
        /// <param name="constructorString">The constructor string used to select the service.</param>
        /// <param name="baseAddresses">The set of base address for the service.</param>
        /// <returns>An instance of <see cref="ServiceHostBase"/> for the service.</returns>
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var meta = Container
                .GetExports<IHostedService, IHostedServiceMetadata>()
                .Where(e => e.Metadata.Name.Equals(constructorString, StringComparison.OrdinalIgnoreCase))
                .Select(e => e.Metadata)
                .SingleOrDefault();

            if (meta == null)
                return null;

            var host = new ExportServiceHost(meta, baseAddresses);
            host.Description.Behaviors.Add(new ExportServiceBehavior(Container, meta.Name));

            var contracts = meta.ServiceType
                .GetInterfaces()
                .Where(t => t.IsDefined(typeof(ServiceContractAttribute), true));

            EnsureHttpBinding(host, contracts);

            return host;
        }

        /// <summary>
        /// Ensures that the Http binding has been created.
        /// </summary>
        /// <param name="host">The Http binding.</param>
        /// <param name="contracts">The set of contracts</param>
        private static void EnsureHttpBinding(ExportServiceHost host, IEnumerable<Type> contracts)
        {
            var binding = new BasicHttpBinding();

            host.Description.Endpoints.Clear();

            foreach (var contract in contracts)
                host.AddServiceEndpoint(contract.FullName, binding, "");
        }

        /// <summary>
        /// Sets the composition container factory.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public static void SetCompositionContainerFactory(ICompositionContainerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            lock (sync)
            {
                var provider = new ServiceHostExportProvider();
                _container = factory.CreateCompositionContainer(provider);

                provider.SourceContainer = _container;
            }
        }

        /// <summary>
        /// Sets the composition container factory.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public static void SetCompositionContainerFactory(Func<ExportProvider[], CompositionContainer> factory)
        {
            SetCompositionContainerFactory(new DelegateCompositionContainerFactory(factory));
        }
        #endregion
    }
}