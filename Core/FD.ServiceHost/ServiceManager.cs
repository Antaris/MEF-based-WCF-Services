namespace FD.ServiceHost
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    using Composition;
    using Hosting;

    /// <summary>
    /// Defines a service manager.
    /// </summary>
    public class ServiceManager
    {
        #region Fields
        private readonly CompositionContainer _container;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ServiceManager"/>.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public ServiceManager(ICompositionContainerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var provider = new ServiceHostExportProvider();
            _container = factory.CreateCompositionContainer(provider);
            _container.ComposeExportedValue(this);

            provider.SourceContainer = _container;
            Initialise();
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceManager"/>.
        /// </summary>
        /// <param name="factory">The delegate used to create a container.</param>
        public ServiceManager(Func<ExportProvider[], CompositionContainer> factory)
            : this(new DelegateCompositionContainerFactory(factory)) { }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the set of service hosts.
        /// </summary>
        public IEnumerable<ExportServiceHost> Services { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initialises the service manager.
        /// </summary>
        private void Initialise()
        {
            Services = _container.GetExportedValues<ExportServiceHost>();
        }
        #endregion
    }
}