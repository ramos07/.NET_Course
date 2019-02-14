using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public class StockNotification : EventArgs
        {
            /// <summary>
            /// Constructor for the 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="currentValue"></param>
            /// <param name="stockChanges"></param>
            //public StockNotification(string name, int currentValue, int stockChanges, int val)
            public StockNotification(string name, int currentValue, int stockChanges)
            {
                Name = name;
                CurrentValue = currentValue;
                StockChanges = stockChanges;
                //Val = val;
            }
            public string Name { get; }
            public int CurrentValue { get; }
            public int StockChanges { get; }
            //public int Val { get; }
        }


        class Stock
        {
            //Declare event of type delegate
            public event EventHandler<StockNotification> stockEvent; //EVENT HANDLER DELEGATE

            string name;//stock name
            int initialValue;//stock price
            int maxChange;//stock price change
            int notificationThreshold;//the value to measure the change of the stock for raising the event
            int currentValue;//value to the stock currently
            int stockChanges = 0;//how many changes the stock has had
            int done = 0;//stop condition for the threads to pause

            public Stock() { }//default constructor
            public Stock(string Name, int startingValue, int maxchange, int threshold)
            {
                name = Name;
                initialValue = startingValue;
                currentValue = initialValue;
                notificationThreshold = threshold;
                Thread thread = new Thread(new ThreadStart(Activate));
                thread.Start();
            }

            public void Activate()
            {
                //infinity loop
                for (; ; )
                {
                    Thread.Sleep(500); //1/2 second
                    ChangeStockValue();
                }
            }

            public void ChangeStockValue()
            {
                stockChanges++; //incrementing the number of changes to the stock
                int randomVal; //declaring the random number
                Random rand = new Random(); //random object to get random number
                randomVal = rand.Next(1, maxChange + 1); //random number will between 1 to maxChange
                currentValue += randomVal;//changing the currentVaule by the randomVal

                if ((Math.Abs(currentValue - initialValue)) > notificationThreshold)
                {
                    //Raise the event //send to the file and to the console
                    //EventArgs? //have to create and send it (stock, currentValue, #changes)

                    StockEvent(name, currentValue, stockChanges);
                    //StockEvent(name, currentValue, stockChanges, initialValue);
                }
            }

            //public void StockEvent(string name, int currentValue, int stockChanges, int initialValue)
            public void StockEvent(string name, int currentValue, int stockChanges)
            {
                stockEvent?.Invoke(this, new StockNotification(name, currentValue, stockChanges));
                //stockEvent?.Invoke(this, new StockNotification(name, currentValue, stockChanges, initialValue));
            }
        }
        class StockBroker
        {
            int headerCount = 0;
            string broker;
            List<Stock> stocks;
            static object tolock1 = new object(); // this is for synchronization
            static object tolock2 = new object(); // this is for synchronization
            public StockBroker() { }//default constructor

            //Constructor for broker's name and a new list of stocks for the broker
            public StockBroker(string name)
            {
                broker = name;
                stocks = new List<Stock>();
            }

            public void AddStock(Stock s)
            {
                //stk.event1 += ConsoleOutput //class stock needs the event - send to the console window and file
                stocks.Add(s);
                s.stockEvent += Notify;
            }

            //stockEvent handler with event message
            public void Notify(object sender, StockNotification e)
            {
                if (headerCount < 15)
                {
                    lock (tolock1)
                    {
                        headerCount++;
                        Console.WriteLine(broker.PadRight(14) + e.Name.PadRight(14) + e.CurrentValue.ToString().PadRight(14) + e.StockChanges.ToString().PadRight(14));
                    }
                    lock (tolock2)
                    {
                        //send data to the file // how to send the file is also in the lecture notes//
                        string dir = "C:\\Users\\ramos\\Desktop\\CSULB\\Spring 2019\\CECS_475\\Lab2";
                        string path = dir + "stock.txt";

                        bool exists = Directory.Exists(dir);
                        if (!exists)
                        {
                            Directory.CreateDirectory(dir);
                        }

                        StreamWriter s = new StreamWriter(path, true, );
                        DateTime localDate = DateTime.Now;
                        //s.WriteLine(localDate.ToString().PadRight(25) + e.Name.PadRight(14) + e.Val.ToString().PadRight(14) + e.CurrentValue.ToString().PadRight(14));
                        s.WriteLine(broker.PadRight(14) + e.Name.PadRight(14) + e.CurrentValue.ToString().PadRight(14) + e.StockChanges.ToString().PadRight(14));
                        s.Close();
                    }
                }
            }
        }

        static public void fileStarter()
        {
            Console.WriteLine("Broker".PadRight(14) + "Stock".PadRight(14) + "Value".PadRight(14) + "Changes".PadRight(14));
            string dir = "C:\\Users\\ramos\\Desktop\\CSULB\\Spring 2019\\CECS_475\\Lab2";
            string path = dir + "stock.txt";

            bool exists = Directory.Exists(dir);
            if (!exists)
            {
                Directory.CreateDirectory(dir);
            }
            StreamWriter s = new StreamWriter(path, true);
            s.WriteLine("Broker".PadRight(14) + "Stock".PadRight(14) + "Value".PadRight(14) + "Changes".PadRight(14));
            s.Close();
        }

        static void Main(string[] args)
        {
            fileStarter();
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
        }
    }
}
