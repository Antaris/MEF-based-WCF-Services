namespace FD.ServiceHost.ConsoleClient
{
    using System;

    using SimpleClient = SimpleCalculatorService.CalculatorServiceClient;
    using AdvancedClient = AdvancedCalculatorService.CalculatorServiceClient;

    class Program
    {
        static void Main(string[] args)
        {
            var client = new SimpleClient();
            int result = client.Add(1, 2);

            Console.WriteLine("Service Response: " + result);
            Console.ReadKey();

            var client2 = new AdvancedClient("BasicHttpBinding_ICalculatorService1");
            int result2 = client2.Add(1, 2);

            Console.WriteLine("Service Response: " + result2);
            Console.ReadKey();
        }
    }
}
