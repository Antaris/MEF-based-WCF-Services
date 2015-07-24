namespace FD.Logger
{
    using System;
    using System.ComponentModel.Composition;

    using Contracts;

    /// <summary>
    /// Defines a logger that writes to the <see cref="Console"/>.
    /// </summary>
    [Export(typeof(ILogger))]
    public class ConsoleLogger : ILogger
    {
        #region Methods
        /// <summary>
        /// Logs the specified object.
        /// </summary>
        /// <param name="obj">The object to log.</param>
        public void Log(object obj)
        {
            Console.WriteLine(obj);
        }
        #endregion
    }
}