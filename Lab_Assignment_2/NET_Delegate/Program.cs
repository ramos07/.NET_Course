
using System;

namespace NET_Delegate
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
            Console.WriteLine("BeforPrintEventHandler: PrintHelper is going to print a value");
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

    //Event with empty parameter
    class MessageArgs : EventArgs
    {
        public MessageArgs(EventArgs empty) { }
    }

    /// <summary>
    /// PrintHelper is the PUBLISHER class.
    /// It offers subscriptions based on the events.
    /// </summary>
    class PrintHelper
    {
        public PrintHelper() { }

        public event EventHandler<MessageArgs> beforePrintEvent;


        public void PrintNumber(int num)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(EventArgs.Empty));
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintDecimal(int dec)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(EventArgs.Empty));
            Console.WriteLine("Decimal: {0:G}", dec);
        }

        public void PrintMoney(int money)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(EventArgs.Empty));
            Console.WriteLine("Money: {0:C}", money);
        }

        public void PrintTemperature(int num)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(EventArgs.Empty));
            Console.WriteLine("Temperature: {0,4:N1} F", num);
        }
        public void PrintHexadecimal(int dec)
        {
            beforePrintEvent?.Invoke(this, new MessageArgs(EventArgs.Empty));
            Console.WriteLine("Hexadecimal: {0:X}", dec);
        }
    }


}

