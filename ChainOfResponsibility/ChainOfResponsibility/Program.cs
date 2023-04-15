using System;

namespace ChainOfResponsibility
{
    using System;


    //IPaymentProcessor: An interface that defines the behavior of payment processors to be used for payment processing.
    //This interface will be implemented by the classes that form the links in the payment processing chain
    interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
        void SetNextProcessor(IPaymentProcessor processor);
    }

    //Represents the payment processor to be used for processing credit card payments. 
    //This class implements the IPaymentProcessor interface and forms a link in the payment processing chain. 
    //It can process transactions that are below the amount that can be handled.
    //If the transaction amount is above the limit, it will forward the transaction to the processor in the next link in the chain.
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
    //Represents the payment processor to be used for processing PayPal payments.
    //This class implements the IPaymentProcessor interface and forms a link in the payment processing chain.
    //It can process transactions that are below the amount that can be handled.
    //If the transaction amount is above the limit, it will forward the transaction to the processor in the next link in the chain.
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
    //Represents the payment processor to be used for processing bank transfer payments.
    //This class implements the IPaymentProcessor interface and forms the last link in the payment processing chain.
    //Therefore, it handles the payment processing in cases where all previous links in the chain cannot process the transaction.
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
    //Represents the main application that includes how the chain will be created and how payment processing will be performed.
    //Different payment processors are created for payment processing, and the chain is created by connecting these processors to each other.
    //The processor in the first link of the chain is called the ProcessPayment method for payment processing, and cases where other processors
    //in the chain try to process the transaction are checked.
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
