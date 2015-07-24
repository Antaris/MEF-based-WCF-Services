namespace FD.ServiceHost.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;

    /// <summary>
    /// Defines a container factory that supporst delegated container creation.
    /// </summary>
    public class DelegateCompositionContainerFactory : ICompositionContainerFactory
    {
        #region Fields
        private Func<ExportProvider[], CompositionContainer> _factory;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="DelegateCompositionContainerFactory"/>.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public DelegateCompositionContainerFactory(Func<ExportProvider[], CompositionContainer> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _factory = factory;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a container used for composition.
        /// </summary>
        /// <param name="providers">The set of export providers.</param>
        /// <returns>An instance of <see cref="CompositionContainer"/>.</returns>
        public CompositionContainer CreateCompositionContainer(params ExportProvider[] providers)
        {
            return _factory(providers);
        }
        #endregion
    }
}