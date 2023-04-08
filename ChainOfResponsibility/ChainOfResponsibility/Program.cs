using System;

namespace ChainOfResponsibility
{
    using System;
    using System;

    interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
        void SetNextProcessor(IPaymentProcessor processor);
    }

    class CreditCardPaymentProcessor : IPaymentProcessor
    {
        private IPaymentProcessor _nextProcessor;

        public void SetNextProcessor(IPaymentProcessor processor)
        {
            _nextProcessor = processor;
        }

        public void ProcessPayment(decimal amount)
        {
            if (amount <= 5000m)
            {
                Console.WriteLine("Credit Card Payment Processor processed the payment of {0:C}", amount);
            }
            else if (_nextProcessor != null)
            {
                Console.WriteLine("Credit Card Payment Processor cannot process the payment. Forwarding the payment to the next processor.");
                _nextProcessor.ProcessPayment(amount);
            }
            else
            {
                Console.WriteLine("None of the payment processors can process the payment.");
            }
        }
    }

    class PayPalPaymentProcessor : IPaymentProcessor
    {
        private IPaymentProcessor _nextProcessor;

        public void SetNextProcessor(IPaymentProcessor processor)
        {
            _nextProcessor = processor;
        }

        public void ProcessPayment(decimal amount)
        {
            if (amount <= 10000m)
            {
                Console.WriteLine("PayPal Payment Processor processed the payment of {0:C}", amount);
            }
            else if (_nextProcessor != null)
            {
                Console.WriteLine("PayPal Payment Processor cannot process the payment. Forwarding the payment to the next processor.");
                _nextProcessor.ProcessPayment(amount);
            }
            else
            {
                Console.WriteLine("None of the payment processors can process the payment.");
            }
        }
    }

    class BankTransferPaymentProcessor : IPaymentProcessor
    {
        private IPaymentProcessor _nextProcessor;

        public void SetNextProcessor(IPaymentProcessor processor)
        {
            _nextProcessor = processor;
        }

        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine("Bank Transfer Payment Processor processed the payment of {0:C}", amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Ödeme işlemi için ödeme işlemcileri oluşturuluyor
            IPaymentProcessor creditCardProcessor = new CreditCardPaymentProcessor();
            IPaymentProcessor payPalProcessor = new PayPalPaymentProcessor();
            IPaymentProcessor bankTransferProcessor = new BankTransferPaymentProcessor();

            // Zincir oluşturuluyor
            creditCardProcessor.SetNextProcessor(payPalProcessor);
            payPalProcessor.SetNextProcessor(bankTransferProcessor);

            // Ödeme işlemleri gerçekleştiriliyor
            creditCardProcessor.ProcessPayment(8000m);
            creditCardProcessor.ProcessPayment(3000m);
            creditCardProcessor.ProcessPayment(20000m);
        }
    }

}
