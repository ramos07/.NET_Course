
using System;

namespace NET_DelegateArgs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Number myNumber = new Number(100000);
            myNumber.PrintMoney();
            myNumber.PrintNumber();
            Console.ReadKey(true);
        }
    }

    //SUBSCRIBER CLASS
    class Number
    {
        private PrintHelper _printHelper;

        public Number(int val)
        {
            _value = val;
            _printHelper = new PrintHelper();

            //Subscribes to the beforePrintEvent
            _printHelper.beforePrintEvent += printHelper_beforePrintEvent;
        }

        //Handler function that will be called when the publisher raises an event.
        public void printHelper_beforePrintEvent(object sender, MessageArgs e)
        {
            Console.WriteLine("BeforPrintEventHandler fires from {0}", e.Message);
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void PrintMoney()
        {
            _printHelper.PrintMoney(_value);
        }

        public void PrintNumber()
        {
            _printHelper.PrintNumber(_value);
        }
    }

    //EVENT 
    class MessageArgs : EventArgs
    {
        public MessageArgs(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }

    /// <summary>
    /// PrintHelper is the PUBLISHER class.
    /// It offers subscriptions based on the events.
    /// </summary>
    class PrintHelper
    {
        public PrintHelper() { }
        public event EventHandler<MessageArgs> beforePrintEvent; //EVENT HANDLER DELEGATE

        /*public void messageHandler(string message)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(message));
        }*/

        public void PrintNumber(int num)
        {
            //messageHandler("Print Number...");
            beforePrintEvent?.Invoke(this, new MessageArgs("Print Number"));
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintDecimal(int dec)
        {
            //messageHandler("Print Decimal...");
            beforePrintEvent?.Invoke(this, new MessageArgs("Print Decimal"));
            Console.WriteLine("Decimal: {0:G}", dec);
        }

        public void PrintMoney(int money)
        {
            //messageHandler("Print Money...");
            beforePrintEvent?.Invoke(this, new MessageArgs("Print Money"));
            Console.WriteLine("Money: {0:C}", money);
        }

        public void PrintTemperature(int num)
        {
            // messageHandler("Print Temperature...");
            beforePrintEvent?.Invoke(this, new MessageArgs("Print Temperature"));
            Console.WriteLine("Temperature: {0,4:N1} F", num);
        }
        public void PrintHexadecimal(int dec)
        {
            //messageHandler("Printing Hexadecimal...");
            beforePrintEvent?.Invoke(this, new MessageArgs("Print Hexadecimal"));
            Console.WriteLine("Hexadecimal: {0:X}", dec);
        }
    }


}

