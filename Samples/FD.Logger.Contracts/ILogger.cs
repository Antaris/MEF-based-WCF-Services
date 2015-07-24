namespace FD.Logger.Contracts
{
    /// <summary>
    /// Defines the required contract for implementing a logger.
    /// </summary>
    public interface ILogger
    {
        #region Methods
        /// <summary>
        /// Logs the specified object.
        /// </summary>
        /// <param name="obj">The object to log.</param>
        void Log(object obj);
        #endregion
    }
}