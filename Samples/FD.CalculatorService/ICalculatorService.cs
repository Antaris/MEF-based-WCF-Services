namespace FD.CalculatorService
{
    using System.ServiceModel;

    using ServiceHost;

    /// <summary>
    /// Defines the requried contract for implementing a calculator service.
    /// </summary>
    [ServiceContract]
    public interface ICalculatorService : IHostedService
    {
        #region Methods
        /// <summary>
        /// Sums the two operand values.
        /// </summary>
        /// <param name="operandA">The value of the left operand.</param>
        /// <param name="operandB">The value of the right operand.</param>
        /// <returns>The sum of the operand values.</returns>
        [OperationContract]
        int Add(int operandA, int operandB);
        #endregion
    }
}