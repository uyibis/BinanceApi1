using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BinanceApi1
{
    class Program
    {
        ApiClient apiClient = new ApiClient("v8Kxt3DtznnJpwUAsJIy68LI9VnB959Rpjd3peSxvd59juhTAGWQIWa0fwCwiink", "IqLDzGVDpsLp6A8hPRwqSLyZxcDvsWXrABt8nCYvf46M5EntgZ4hN7FROxBe1FDK");
        BinanceClient binanceClient;
        Dictionary<String, Pair> dictionary = new Dictionary<string, Pair>();

        static void Main(string[] args)
        {
            Program program = new Program();
            program.binanceClient = new BinanceClient(program.apiClient);

            var test = program.binanceClient.TestConnectivity().Result;
            //var sellOrder = binanceClient.PostNewOrder("ethbtc", 1m, 0.04m, OrderSide.SELL).Result;
            /*var openOrders = program.binanceClient.GetCurrentOpenOrders("ancbusd").Result;
            foreach (var item in openOrders)
            {
                Console.WriteLine("name: " + item.Symbol + "order id: " + item.OrderId + " status:" + item.Status + " price:" + item.Price);
            }*/
            // program.Test();
            // program.Test1();
            /* try
             {
                  var buyOrder = program.binanceClient.PostNewOrder("ancbusd", 151m, 0.135m, OrderSide.BUY).Result;
                  //var accountInfo = program.binanceClient.GetAccountInfo().Result;
                  Console.WriteLine(buyOrder.OrderId);
             }
             catch (Exception e)
             {

                 Console.WriteLine(e.Message);
             }*/

            //var sellOrder = program.binanceClient.PostNewOrder("epxbusd", 1m, 0.04m, OrderSide.SELL).Result;
            //  Console.WriteLine("Order id: "+buyOrder.OrderId);
            program.RestoreFromDb();
            Console.ReadLine();
        }



        public void MonitorAllPair() {
            var tickerPrices = from dat in binanceClient.GetAllPrices().Result
                               where dat.Symbol.Contains("BUSD")
                               select dat;
            foreach (var item in tickerPrices)
            {
                //Console.WriteLine(item.Symbol + "  " + item.Price);
                if (dictionary.ContainsKey(item.Symbol))
                {
                    dictionary[item.Symbol].name = item.Symbol;
                    dictionary[item.Symbol].price = item.Price;
                }
                else {
                    Pair pair = new Pair();
                    pair.name = item.Symbol;
                    pair.price = item.Price;
                    dictionary[item.Symbol] = pair;
                }
            }

        }
        System.Threading.Timer timer;
        System.Threading.Timer timer1;
        private bool isRestore;

        public void Test()
        {
            timer = new System.Threading.Timer(OnTimedEvent, null, 1000, Timeout.Infinite);
        }
        public void Test1()
        {
            timer1 = new System.Threading.Timer(OnTimedEvent1, null, 120000, Timeout.Infinite);
        }

        private void OnTimedEvent1(object state)
        {
            //Console.WriteLine("Calling");
            Console.WriteLine("Saving MarketPrint Data");
            SaveMarketPrint();
            timer1.Change(120000, Timeout.Infinite);
        }

        private void SaveMarketPrint()
        {

            summary();

        }

        public void OnTimedEvent(Object state)
        {
            Console.WriteLine("Calling");
            MonitorAllPair();
            timer.Change(30000, Timeout.Infinite);
        }

        public void summary() {
            DataContext dataContext = new DataContext();
            foreach (var item in dictionary.Values)
            {
                MarketPrint marketPrint = new MarketPrint();
                marketPrint.max = item.marketPrint.max;
                marketPrint.min = item.marketPrint.min;
                marketPrint.pair = item.marketPrint.pair;
                marketPrint.price = item.marketPrint.price;
                marketPrint.pricePercentPosition = item.marketPrint.pricePercentPosition;
                marketPrint.time = DateTime.Now;
                if (!isRestore)
                    dataContext.marketPrints.Add(marketPrint);
                Console.WriteLine($"{marketPrint.pair}  Price: {marketPrint.price} MaxPrice: {marketPrint.max} MinPrice: {marketPrint.min} Price: {marketPrint.price}   Percent Change position: {marketPrint.pricePercentPosition}");
            }
            if (!isRestore)
                dataContext.SaveChanges();
        }

        public void buy(String pair) {
            Console.WriteLine($"buying  {pair}");

        }
        public void selling(String pair)
        {
            Console.WriteLine($"Selling  {pair}");
        }
        public void RestoreFromDb() {
            DataContext dataContext = new DataContext();
            var data = from dt in dataContext.marketPrints
                       where dt.time > DateTime.Now.AddHours(-24)
                       select dt;
            foreach (var item in data)
            {
                //Console.WriteLine(item.Symbol + "  " + item.Price);
                if (dictionary.ContainsKey(item.pair))
                {
                    dictionary[item.pair].name = item.pair;
                    dictionary[item.pair].price = item.price;
                }
                else
                {
                    Pair pair = new Pair();
                    pair.name = item.pair;
                    pair.price = item.price;
                    dictionary[item.pair] = pair;
                }
            }
            isRestore = true;
            summary();
            isRestore = false;
        }
    }
}
