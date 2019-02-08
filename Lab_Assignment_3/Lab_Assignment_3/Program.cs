
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab_Assignment_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stock stock1 = new Stock("Technology", 160, 5, 16);
            Stock stock2 = new Stock("Retail", 30, 2, 6);
            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);
            Console.ReadKey(true);
        }

    }

    //Stock notification event
    class StockNotification : EventArgs
    {
        public StockNotification(string stockName, int currentValue, int changes)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            Changes = changes;
        }
        public string StockName { get; }
        public int CurrentValue { get; }
        public int Changes { get; }
    }

    class Stock
    {
        private string stockName;
        private int initialValue;
        private int maxChange;
        private int threshold;

        public event EventHandler<StockNotification> stockEvent;

        public Stock() { } //default constructor

        public Stock(string stockName, int initialValue, int maxChange, int threshold)
        {
            StockName = stockName;
            InitialValue = initialValue;
            MaxChange = maxChange;
            notificationThreshold = threshold;
            ThreadStart stockref = new ThreadStart(Activate);
            Thread stockThread = new Thread(stockref);
            stockThread.Start();
        }
        public string StockName { get; }
        public int InitialValue { get; }
        public int MaxChange { get; }
        public int notificationThreshold { get; }

        public void Activate()
        {
            for (; ; )
            {
                Thread.Sleep(500);
                Random random = new Random();
                int randomValue = random.Next(1, maxChange);
                int currentValue = initialValue;
                int changes = 0;
                currentValue += randomValue; //add a random value to the current value
                changes++;
                int priceChange = currentValue - initialValue;
                if (priceChange > threshold)
                {
                    //raise event stock Event
                    stockEvent?.Invoke(this, new StockNotification(stockName, currentValue, changes));
                }

            }
        }

        public void stockNotificationHandler(string stockName, int currentValue, int changes)
        {
            stockEvent?.Invoke(this, new StockNotification(stockName, currentValue, changes));
        }
    }//end of Stock class

    class StockBroker
    {
        private string brokerName;
        private Stock _stock;
        List<Stock> stocks;

        public StockBroker(string brokerName)
        {
            _name = brokerName;
            _stock = new Stock();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void AddStock(Stock _stock)
        {
            //stocks.Add(s);
            _stock.stockEvent += ConsoleOutput; //event handler

        }

        public void ConsoleOutput(object sender, StockNotification e)
        {
            //throw new NotImplementedException();
            Console.WriteLine(e.StockName + e.CurrentValue + e.Changes);
        }
    }
}
