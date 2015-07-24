namespace FD.CalculatorService
{
    using ServiceHost;

    /// <summary>
    /// Defines a simple calculator service.
    /// </summary>
    [ExportService("SimpleCalculator", typeof(SimpleCalculatorService))]
    public class SimpleCalculatorService : ICalculatorService
    {
        #region Methods
        /// <summary>
        /// Sums the two operand values.
        /// </summary>
        /// <param name="operandA">The value of the left operand.</param>
        /// <param name="operandB">The value of the right operand.</param>
        /// <returns>The sum of the operand values.</returns>
        public int Add(int operandA, int operandB)
        {
            return (operandA + operandB);
        }
        #endregion
    }
}