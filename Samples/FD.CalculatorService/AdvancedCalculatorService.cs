namespace FD.CalculatorService
{
    using System;
    using System.ComponentModel.Composition;

    using Logger.Contracts;
    using ServiceHost;
    using ServiceHost.Endpoints;

    /// <summary>
    /// Defines a simple calculator service.
    /// </summary>
    [//ExportService("AdvancedCalculator", typeof(AdvancedCalculatorService)),
     HttpEndpoint, TcpEndpoint]
    public class AdvancedCalculatorService : ICalculatorService
    {
        #region Fields
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="AdvancedCalculatorService"/>
        /// </summary>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public AdvancedCalculatorService(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            _logger = logger;
            _logger.Log("Created instance of AdvancedCalculatorService");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sums the two operand values.
        /// </summary>
        /// <param name="operandA">The value of the left operand.</param>
        /// <param name="operandB">The value of the right operand.</param>
        /// <returns>The sum of the operand values.</returns>
        public int Add(int operandA, int operandB)
        {
            int result = (operandA + operandB);

            _logger.Log("Computing result: " + operandA + " + " + operandB + ": " + result);
            return result;
        }
        #endregion
    }
}