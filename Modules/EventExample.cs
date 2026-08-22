using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates events in C# with a practical BankAccount example.
    /// Events are encapsulated delegates that allow objects to notify subscribers
    /// about state changes or significant occurrences.
    ///
    /// Covers:
    /// - Custom EventArgs (TransactionEventArgs, LowBalanceEventArgs)
    /// - Event declaration with EventHandler pattern
    /// - Publishing events (raising events)
    /// - Subscribing to events with += 
    /// - Unsubscribing from events with -=
    /// - Event naming conventions (OnEventName)
    /// - Weak event pattern benefits (encapsulation, one-way communication)
    /// </summary>
    public class EventExample : ITopicModule
    {
        public string Name => "Events & EventArgs";
        public string Description => "Demonstrates event declaration, publishing, and subscription patterns.";

        public void Run()
        {
            Console.WriteLine("Events demo with BankAccount:\n");

            // Create a bank account with initial balance
            var account = new BankAccount("Alice", 1000m);

            // Create event subscribers (handlers)
            var logger = new TransactionLogger();
            var notifier = new LowBalanceNotifier();

            // Subscribe to events
            Console.WriteLine("Subscribing handlers to events...\n");
            account.OnTransactionCompleted += logger.LogTransaction;
            account.OnLowBalance += notifier.NotifyLowBalance;

            // Perform transactions that will trigger events
            Console.WriteLine("=== Performing Deposit ===");
            account.Deposit(500m);

            Console.WriteLine("\n=== Performing Withdrawal ===");
            account.Withdraw(400m);

            Console.WriteLine("\n=== Performing Large Withdrawal (triggers low balance) ===");
            account.Withdraw(1050m); // Balance will be negative, triggers low balance event

            // Unsubscribe from one event
            Console.WriteLine("\n=== Unsubscribing logger from OnTransactionCompleted ===");
            account.OnTransactionCompleted -= logger.LogTransaction;

            Console.WriteLine("\n=== Performing Deposit (only notifier receives event) ===");
            account.Deposit(200m);

            Console.WriteLine("\n=== Example of exception in event handler ===");
            account.OnTransactionCompleted += (s, e) => throw new InvalidOperationException("Simulated handler error");
            try
            {
                account.Deposit(100m);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"   Exception caught: {ex.Message}");
            }

            Console.WriteLine("\nEvent highlights:");
            Console.WriteLine("- Events encapsulate and protect delegate access (one-way communication).");
            Console.WriteLine("- Subscribers use += to subscribe and -= to unsubscribe.");
            Console.WriteLine("- Publishers raise events with custom EventArgs containing data for subscribers.");
            Console.WriteLine("- Follow naming convention: OnEventName for event members.");
            Console.WriteLine("- EventArgs are typically derived from EventArgs base class.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // ==================== Custom EventArgs ====================

    /// <summary>
    /// Custom EventArgs for transaction events. Carries transaction details to subscribers.
    /// </summary>
    public class TransactionEventArgs : EventArgs
    {
        public string AccountHolder { get; set; }
        public string TransactionType { get; set; } // "Deposit" or "Withdrawal"
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Custom EventArgs for low balance events.
    /// </summary>
    public class LowBalanceEventArgs : EventArgs
    {
        public string AccountHolder { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Threshold { get; set; }
        public DateTime AlertTime { get; set; }
    }

    // ==================== BankAccount (Event Publisher) ====================

    /// <summary>
    /// BankAccount publishes events when transactions occur or when balance is low.
    /// This is the event publisher.
    /// </summary>
    public class BankAccount
    {
        private decimal balance;
        private const decimal LowBalanceThreshold = 100m;

        public string AccountHolder { get; }

        // Event declarations following the standard pattern: EventHandler<TEventArgs>
        // OnTransactionCompleted event: raised when a deposit or withdrawal completes
        public event EventHandler<TransactionEventArgs> OnTransactionCompleted;

        // OnLowBalance event: raised when balance falls below threshold
        public event EventHandler<LowBalanceEventArgs> OnLowBalance;

        public BankAccount(string accountHolder, decimal initialBalance)
        {
            AccountHolder = accountHolder;
            balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }

            balance += amount;
            Console.WriteLine($"[BankAccount] Deposited {amount:C}, new balance: {balance:C}");

            // Publish the transaction event
            RaiseTransactionCompleted("Deposit", amount, balance);

            // Check if balance is low
            if (balance < LowBalanceThreshold)
            {
                RaiseLowBalance(balance);
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return;
            }

            balance -= amount;
            Console.WriteLine($"[BankAccount] Withdrew {amount:C}, new balance: {balance:C}");

            // Publish the transaction event
            RaiseTransactionCompleted("Withdrawal", amount, balance);

            // Check if balance is low
            if (balance < LowBalanceThreshold)
            {
                RaiseLowBalance(balance);
            }
        }

        // Protected method to raise (publish) the OnTransactionCompleted event
        protected virtual void RaiseTransactionCompleted(string type, decimal amount, decimal newBalance)
        {
            var args = new TransactionEventArgs
            {
                AccountHolder = AccountHolder,
                TransactionType = type,
                Amount = amount,
                NewBalance = newBalance,
                Timestamp = DateTime.Now
            };

            // Invoke all subscribed event handlers
            OnTransactionCompleted?.Invoke(this, args);
        }

        // Protected method to raise (publish) the OnLowBalance event
        protected virtual void RaiseLowBalance(decimal currentBalance)
        {
            var args = new LowBalanceEventArgs
            {
                AccountHolder = AccountHolder,
                CurrentBalance = currentBalance,
                Threshold = LowBalanceThreshold,
                AlertTime = DateTime.Now
            };

            // Invoke all subscribed event handlers
            OnLowBalance?.Invoke(this, args);
        }
    }

    // ==================== Event Subscribers ====================

    /// <summary>
    /// TransactionLogger subscribes to OnTransactionCompleted and logs each transaction.
    /// </summary>
    public class TransactionLogger
    {
        public void LogTransaction(object sender, TransactionEventArgs e)
        {
            Console.WriteLine($"[TransactionLogger] Logged: {e.TransactionType} of {e.Amount:C} by {e.AccountHolder} at {e.Timestamp:HH:mm:ss}");
        }
    }

    /// <summary>
    /// LowBalanceNotifier subscribes to OnLowBalance and sends alerts.
    /// </summary>
    public class LowBalanceNotifier
    {
        public void NotifyLowBalance(object sender, LowBalanceEventArgs e)
        {
            Console.WriteLine($"[LowBalanceNotifier] ALERT: {e.AccountHolder}'s balance ({e.CurrentBalance:C}) is below threshold ({e.Threshold:C})");
        }
    }
}
