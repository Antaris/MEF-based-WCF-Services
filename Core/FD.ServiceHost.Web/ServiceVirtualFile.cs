namespace FD.ServiceHost.Web
{
    using System.IO;
    using System.Web.Hosting;

    /// <summary>
    /// Defines a virtual file for a dynamic service.
    /// </summary>
    public class ServiceVirtualFile : VirtualFile
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ServiceVirtualFile"/>.
        /// </summary>
        /// <param name="virtualFile">The virtual file path.</param>
        public ServiceVirtualFile(string virtualFile)
            : base(virtualFile) { }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the name of the service from the virtual file path.
        /// </summary>
        /// <param name="virtualFile">The virtual file path.</param>
        /// <returns>The service name.</returns>
        private static string GetName(string virtualFile)
        {
            string name = virtualFile.Substring(
                virtualFile.LastIndexOf("/") + 1);
            name = name.Substring(0, name.LastIndexOf("."));

            return name;
        }

        /// <summary>
        /// Opens the stream containing the virtual file content.
        /// </summary>
        /// <returns>The stream.</returns>
        public override Stream Open()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(string.Format("<%@ ServiceHost Language=\"C#\" " +
                              "Debug=\"true\" Service=\"{0}\" " + 
                              "Factory=\"FD.ServiceHost.Web.WebServiceHostFactory, FD.ServiceHost.Web\" %>", 
                              GetName(VirtualPath)));

            writer.Flush();

            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}