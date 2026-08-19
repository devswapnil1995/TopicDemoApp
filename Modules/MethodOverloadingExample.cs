
using System;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates method overloading in C# by providing multiple ProcessPayment
    /// methods with different parameter lists. Callers can use the same method
    /// name with different arguments to invoke different behavior.
    /// </summary>
    public class MethodOverloadingExample : ITopicModule
    {
        public string Name => "Method Overloading";
        public string Description => "Learn about method overloading in C#.";

        /// <summary>
        /// Runs the demo by calling the various overloaded ProcessPayment methods.
        /// Each call shows a different overload being selected based on the
        /// number and types of arguments provided.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Running Method Overloading Demo...");

            // Create the payment processor that exposes overloaded methods
            PaymentProcessing paymentProcessor = new PaymentProcessing();

            // Call the simplest overload: only amount. This uses the default
            // processing behavior and demonstrates that the runtime selects
            // the overload that matches the provided arguments.
            paymentProcessor.ProcessPayment(49.99m);
            Console.ReadLine(); // Wait so user can see output

            // Call the overload with amount and currency. This shows a different
            // signature is chosen when additional parameters are supplied.
            paymentProcessor.ProcessPayment(99.50m, "USD");
            Console.ReadLine();

            // Call the most specific overload: amount, currency and payment method.
            paymentProcessor.ProcessPayment(15.00m, "EUR", "Credit Card");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Example payment processor that exposes three overloaded ProcessPayment methods.
    /// Overloading allows multiple methods with the same name but different
    /// parameter lists (different signatures).
    /// </summary>
    public class PaymentProcessing
    {
        /// <summary>
        /// Processes a payment using a default method. This overload accepts only
        /// the amount and formats it using the current culture currency format.
        /// </summary>
        /// <param name="amount">Amount to process</param>
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing payment of {amount:C} using default method.");
        }

        /// <summary>
        /// Processes a payment and displays the currency code provided.
        /// Demonstrates a different overload with an extra string parameter.
        /// </summary>
        /// <param name="amount">Amount to process</param>
        /// <param name="currency">Currency code (e.g., "USD")</param>
        public void ProcessPayment(decimal amount, string currency)
        {
            // Use invariant formatting for clarity; show currency explicitly
            Console.WriteLine($"Processing payment of {amount} in {currency}.");
        }

        /// <summary>
        /// Processes a payment specifying amount, currency and the payment method.
        /// This is the most specific overload and will be selected when all three
        /// arguments are provided.
        /// </summary>
        /// <param name="amount">Amount to process</param>
        /// <param name="currency">Currency code</param>
        /// <param name="paymentMethod">Payment method description</param>
        public void ProcessPayment(decimal amount, string currency, string paymentMethod)
        {
            Console.WriteLine($"Processing payment of {amount} in {currency} using {paymentMethod}.");
        }
    }
}
