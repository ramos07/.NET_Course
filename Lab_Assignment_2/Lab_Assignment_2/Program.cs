
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lab_Assignment_3
{
    public class Program
    {
        static void Main(string[] args)
        {
            Header();
            Stock stock1 = new Stock("Technology", 160, 5, 15);
            Stock stock2 = new Stock("Retail", 30, 2, 6);
            Stock stock3 = new Stock("Banking", 90, 4, 10);
            Stock stock4 = new Stock("Commodity", 500, 20, 50);

            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockBroker b2 = new StockBroker("Broker 2");
            b2.AddStock(stock1);
            b2.AddStock(stock3);
            b2.AddStock(stock4);

            StockBroker b3 = new StockBroker("Broker 3");
            b3.AddStock(stock1);
            b3.AddStock(stock3);

            StockBroker b4 = new StockBroker("Broker 4");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);

            Console.ReadKey(true);
        }

        /// <summary>
        /// Header for the console output.
        /// </summary>
        static void Header()
        {
            Console.WriteLine("Broker".PadRight(12) + "Stock".PadRight(12) + "Value".PadRight(12) + "Changes".PadRight(12));
            Console.WriteLine();
        }
    }
    /// <summary>
    /// Class Stock that represents a stock. 
    /// </summary>
    class Stock
    {
        private string name; //name of the stock
        private int initial_Value; //the starting value of the Stock
        private int maxChange; //the maximum value a stock can change
        private int threshold; //value to hold maximum a stock can change 

        public int currentValue; //will hold the current value as the stock changes
        public int changes = 0; //variable to hold the amount of times a stock changes

        /// <summary>
        /// Default constructor
        /// </summary>
        public Stock() { }

        /// <summary>
        /// Initialized constructor
        /// </summary>
        /// <param name="StockName"></param>
        /// <param name="InitialValue"></param>
        /// <param name="MaxChange"></param>
        /// <param name="Threshold"></param>
        public Stock(string StockName, int InitialValue, int MaxChange, int Threshold)
        {
            name = StockName;
            initial_Value = InitialValue;
            currentValue = initial_Value; //set the current value to the initial value of the stock
            maxChange = MaxChange;
            threshold = Threshold;
            Thread thread = new Thread(new ThreadStart(Activate)); //For each stock create a thread
            thread.Start();
        }

        /// <summary>
        /// This method will begin once the thread has been started.
        /// It determines the frequency a stock will change its value.
        /// It also calls another method ChangeStockValue(), which will
        /// change the price of the stock.
        /// </summary>
        public void Activate()
        {
            for (; ; )
            {
                Thread.Sleep(500); //every 50ms change Stock(thread)
                ChangeStockValue(); //call method
            }
        }

        /// <summary>
        /// This method will change the stock value. Inside this method 
        /// the OnStockEvent method is called which will raise the Stock-
        /// Notification event. Passing the name of the stock, current value
        /// of the stock, amount of price changes to stock, the initial value 
        /// of the stock, and lastly the time and date the price changed for
        /// the stock.
        /// </summary>
        public void ChangeStockValue()
        {
            changes++; //everytime ChangeStockValue method is called increment the changes variable
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, maxChange); //generating a random number to add to the current value within the maximum price change allowed
            currentValue += randomNumber; //adding a random value to the current value
            string date = DateTime.Now.ToString()+"\t"; //storing the time and date for each value change of stock

            //if the current value changes past a threshold value, the broker will be notified by raising the event
            if ((currentValue - initial_Value) > threshold)
            {
                //raise StockNotification Event
                OnStockEvent(name, currentValue, changes, initial_Value, date);
            }
        }

        /// <summary>
        /// Method to handle raising the event
        /// </summary>
        /// <param name="name"></param>
        /// <param name="currentValue"></param>
        /// <param name="changes"></param>
        /// <param name="initial_Value"></param>
        /// <param name="date"></param>
        public void OnStockEvent(string name, int currentValue, int changes, int initial_Value, string date)
        {
            stockEvent?.Invoke(this, new StockNotification(name, currentValue, changes, initial_Value, date));
        }

        /// <summary>
        /// Declaring the stock event
        /// </summary>
        public event EventHandler<StockNotification> stockEvent;

    }//end of Stock class

    /// <summary>
    /// This class is of event type StockNotication which will get the necessary values
    /// to notify the broker.
    /// </summary>
    public class StockNotification : EventArgs
    {
        /// <summary>
        /// Initialized constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="currentValue"></param>
        /// <param name="changes"></param>
        /// <param name="initial_Value"></param>
        /// <param name="date"></param>
        public StockNotification(string name, int currentValue, int changes, int initial_Value, string date)
        {
            Date = date;
            StockName = name;
            CurrentValue = currentValue;
            Changes = changes;
            InitialValue = initial_Value;
        }
        public string Date { get; set; }
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int Changes { get; set; }
        public int InitialValue { get; set; }

    }

    /// <summary>
    /// Class StockBroker which represents a stock broker.
    /// </summary>
    class StockBroker
    {
        private string brokerName; // name of the broker 
        List<Stock> stocks; //list to hold the stocks being added to a broker
        static object ToLock = new object(); //lock object variable to handle synchronization

        /// <summary>
        /// Initialized constructor
        /// </summary>
        /// <param name="BrokerName"></param>
        public StockBroker(string BrokerName)
        {
            brokerName = BrokerName;
            stocks = new List<Stock>();
        }

        /// <summary>
        /// This method will add a stock to the list of stocks.
        /// It will also subscribe the broker to the Notify method
        /// so that it can be notified of prices changes to a stock.
        /// </summary>
        /// <param name="s"></param>
        public void AddStock(Stock s)
        {
            s.stockEvent += Notify; //subcribing to stockEvent
            stocks.Add(s); //adding stock to list stocks
        }

        /// <summary>
        /// Event handler method Notify will ouput price changes to a stock according to the 
        /// brokers that hold those current stocks. The lock object is utilized to handle the 
        /// synchronization of certain data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Notify(object sender, StockNotification e)
        {
            
            lock (ToLock)
            {
                Console.WriteLine(brokerName.PadRight(12) + e.StockName.PadRight(12) + e.CurrentValue.ToString().PadRight(12) + e.Changes.ToString().PadRight(12));
            }
            lock (ToLock)
            {

                string directory = "C:\\Users\\ramos\\Desktop\\CSULB\\Spring 2019\\CECS_475\\Lab2\\";
                string filename = "stocks.txt";
                string path = directory + filename;

                bool FileExists = File.Exists(path);

                if (!File.Exists(path))
                {
                    using (StreamWriter writer = File.CreateText(path))
                    {
                        writer.WriteLine("Date".PadRight(12) + "\t\tStock".PadRight(12) + "Initial".PadRight(12) + "Current".PadRight(12));
                        writer.WriteLine(e.Date.PadRight(12) + e.StockName.PadRight(12) + e.InitialValue.ToString().PadRight(12) + e.CurrentValue.ToString().PadRight(12));
                        writer.Flush();
                    }
                }
                else if (File.Exists(path))
                {
                    using (StreamWriter writer = File.AppendText(path))
                    {
                        writer.WriteLine(e.Date.PadRight(12) + e.StockName.PadRight(12) + e.InitialValue.ToString().PadRight(12) + e.CurrentValue.ToString().PadRight(12));
                        writer.Flush();
                    }
                }

            }
        }//end of Notify method
    }//end of StockBroker class
}//end of Namespace

