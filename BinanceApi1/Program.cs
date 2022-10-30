using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BinanceApi1
{
    public delegate void ThreadDelegate(Pair pair);
    class Program
    {

        ApiClient apiClient = new ApiClient("voRpJRj95roiIhfKkejMDuTCpZYFnxVE5MzuAuxmEIYwwUZYprpgimBCQ0n3HEGc", "7cPte5J4X3iG0A5SwVZUHAAPkfa52MllYIUEDSALt2NlzmYgVJPBxoQF4LVP7pCZ");
        static BinanceClient binanceClient;
        static Dictionary<String, Pair> dictionary = new Dictionary<string, Pair>();
        static Dictionary<String, Balance> AccountBalance;
        private ThreadDelegate threadDelegate;
        string watchPair = "";
        static readonly object pblock = new object();
        static Program program = new Program();
        static void Main(string[] args)
        {
            
            binanceClient = new BinanceClient(program.apiClient);
            var test = binanceClient.TestConnectivity().Result;
            writbalanceinfo = true;
            program.BalanceInfo(binanceClient);
            writbalanceinfo = false;
            program.RestoreFromDb();
            program.orderProcess();
            program.buyProcess();
            var process = new ThreadStart(() =>
            {
                
                do
                {
                    lock (pblock)
                    {
                        int dir = 0;
input:            Console.WriteLine("which operation do you want to run");
                        Console.WriteLine("1. market entry \n2. Add interested pair\n3. See market data\n4. See buying process\n5. Remove interested pair");
                        var read = Console.ReadKey();
                        //Console.WriteLine("You entered " + read.Key);
                        try
                        {
                            ordering = true;
                            buying = true;
                            program.startMarketData(program);
                            Thread.Sleep(4000);

                            program.ClearOldData();
                            switch (read.Key)
                            {
                                case System.ConsoleKey.D1:
                                    // Console.WriteLine("you entered 1");
                                    Program.ordering = true;
                                    program.EntryMarket();
                                    break;
                                case System.ConsoleKey.D2:
                                    //Console.WriteLine("you entered 1");
                                    program.EnterInterestedPair();
                                    break;
                                case System.ConsoleKey.D3:
                                    Program.ordering = false;
                                    Thread.Sleep(30000);
                                    Program.ordering = true;
                                    break;
                                case System.ConsoleKey.D4:
                                    Program.ordering = true;
                                    Program.writbalanceinfo = true;
                                    Program.buying = false;
                                    Thread.Sleep(60000);
                                    Program.buying = true;
                                    Program.writbalanceinfo = false;
                                    break;
                                case System.ConsoleKey.D5:
                                    /* Program.ordering = true;
                                     Program.buying = true;
                                    // Thread.Sleep(60000);
                                     Console.WriteLine("Enter number to test");
                                     Uti uti = new Uti();
                                     Console.WriteLine(uti.roundToSignificantDigits(decimal.Parse(Console.ReadLine()), 3));
                                    // Program.buying = true;*/
                                    program.RemoveInterestedPair();
                                    break;
                                default:
                                    Console.WriteLine("you entered neither 1 or 2");
                                    break;
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message +ex.StackTrace + "Line 93 Main");
                            goto input;
                        }
                        Console.WriteLine(read);
                        
                    }
                    
                    Thread.Sleep(3000);
                } while (true);
            });
            var t = new Thread(process);
            t.Start();
            //Console.ReadLine();
            /*
            

            var Proces2 = new ThreadStart(() =>
            {
                do
                {
                    Thread.Sleep(5000);
                    program.monitorPair(dictionary["ETHBUSD"]);
                } while (true);
            });
            Thread p2 = new Thread(Proces2);
           */


        }

        private void orderProcess()
        {
            var Proces3 = new ThreadStart(() =>
            {
                do
                {
                    program.monitorOrdering();
                    Thread.Sleep(40000);
                } while (true);
              
              
            });
            Thread p3 = new Thread(Proces3);
            p3.Start();
        }
        private void startMarketData(Program program)
        {
            // throw new NotImplementedException();
            var Proces = new ThreadStart(() =>
            {
                int i = 0;
                do
                {
                    // Console.WriteLine("Thread 1 is runing");
                    try
                    {
                        //if (!ordering)
                        program.MonitorAllPair();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + ex.StackTrace+ "line 154 startMarketData");
                    }
                    Thread.Sleep(4000);
                } while (true);
            });
            Thread p = new Thread(Proces);
            p.Start();
            var Proces1 = new ThreadStart(() =>
            {
                do
                {
                    Thread.Sleep(720000);
                    //if (!ordering)
                     program.SaveMarketPrint();
                    //Thread.Sleep(720000);
                    Thread.Sleep(10000);
                    break;
                } while (true);
            });

            Thread p1 = new Thread(Proces1);
            p1.Start();
        }

        public void monitorOrdering()
        {
           
            DataContext dataContext = new DataContext();
            try
            {

            
            foreach (var item in dataContext.tradeOrders)
            {

                var itm = binanceClient.TestConnectivity().Result;
                var order = binanceClient.GetOrder(item.pair, item.BinanceOrderId).Result;
                if (order.Status == "FILLED" && item.IsOpen)
                {
                    Console.WriteLine($"order {order.Symbol} is now FILLED");
                    //   TradingPair tradingPair = new TradingPair();
                    item.IsOpen = false;
                    RunningOrder runningOrder = new RunningOrder();
                    runningOrder.initialRate = item.price;
                    runningOrder.pair = item.pair;
                    runningOrder.initialWorth = item.quantity;
                    dataContext.runningOrders.Add(runningOrder);
                }
                else if(order.Status != "FILLED" && item.IsOpen)
                {
                    // Console.WriteLine($"order {order.Symbol} is now FILLED");
                    //  item.IsOpen = false;
                    if ((DateTime.Now - item.OrderTime).Hours > 6)
                    {

                        var canceledOrder = binanceClient.CancelOrder(item.pair, item.BinanceOrderId).Result;
                        item.IsOpen = false; ;
                    }
                }
                // ordering = true;
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace + "line 218 monitorOrdering");
               
            }
            dataContext.SaveChanges();
        }
        public static void threadStop()
        {
            Console.WriteLine("Thread stop");
        }
        public void RemoveInterestedPair()
        {
            DataContext dataContext = new DataContext();
            
            Console.WriteLine("Select Pair to remove");
            foreach (var item in dataContext.tradingPairs)
            {
                Console.WriteLine($"{item.Id}"+item.name);
            }
            int selectedIndex = 100;
            try
            {
                selectedIndex = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace + "line 243 RemoveInterestedPair");
            }
            try
            {
                if (selectedIndex != 100) {
                    dataContext.tradingPairs.Remove(dataContext.tradingPairs.Single(e => e.Id == selectedIndex));
                    dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace + "line 254 RemoveInterestedPair");
            }

        }
            public void EnterInterestedPair() {
            DataContext dataContext = new DataContext();
            foreach (var item in dataContext.tradingPairs)
            {
                Console.WriteLine(item.name);
            }
            Boolean condition = true;
            do
            {
pairLing:       Console.WriteLine("# Enter Pair");
                var pairval = Console.ReadLine();
                TradingPair tradingPair = new TradingPair();

                if (pairval != "")
                    tradingPair.name = pairval;
                else goto pairLing;



                if (dataContext.tradingPairs.Any(tradingP => tradingP.name == pairval))
                    dataContext.tradingPairs.Remove(dataContext.tradingPairs.Single(t => t.name == pairval));
                else
                    dataContext.tradingPairs.Add(tradingPair);

finishLine:     Console.Write("Are you done y / n");
                switch (Console.ReadKey().Key)
                {
                    case System.ConsoleKey.Y:
                        dataContext.SaveChangesAsync();
                        condition = false;
                        break;
                    case System.ConsoleKey.N:
                         
                        break;
                    default:
                        Console.WriteLine("you entered wrong key");
                        goto finishLine;
                }
                
            } while (condition);
        }
        public void tradeManager()
        {
            DataContext dataContext = new DataContext();
            foreach (var item in dataContext.runningOrders)
            {
                var par = dictionary[item.pair];
                var currentWorth = item.initialWorth * par.price;
                var initialworth = item.initialRate * item.initialWorth;
                var profit = currentWorth - initialworth;
                var profitTreshold = initialworth * (2M / 100);
                var lossTreshold = currentWorth - (currentWorth * 0.00025M);
                var valPrice = (profitTreshold + initialworth) / item.initialWorth;
                Console.WriteLine($"current worth of {item.pair} is {currentWorth} and ini {initialworth}");
                Console.WriteLine($"profit: {profit}");
                Console.WriteLine($"profitTreshold: {profitTreshold} valPrice{valPrice}");
                Console.WriteLine($"selling loss {lossTreshold} at {lossTreshold / item.initialWorth}");

            }
            ordering = true;
        }
        public void BalanceInfo(BinanceClient binanceClient)
        {
            try
            {
                var test = binanceClient.TestConnectivity().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "line 327 BalanceInfo");
                return;
            }
            
            var accountInfo = binanceClient.GetAccountInfo().Result;
            AccountBalance = new Dictionary<string, Balance>();
            foreach (var item in accountInfo.Balances)
            {
                Balance balance = new Balance();
                if (item.Free > 0.00000001M)
                    try
                    {
                        AccountBalance[item.Asset] = new Balance { Coin = item.Asset, Free = item.Free, Locked = item.Locked };
                        
                    }
                    catch (Exception)
                    {
                        AccountBalance[item.Asset].Free = item.Free;
                        AccountBalance[item.Asset].Locked = item.Locked;
                        //throw;
                    }
            }
            foreach (var item in AccountBalance.Values)
            {
                if(writbalanceinfo)
                Console.WriteLine($"Assest: {item.Coin}, Free: {item.Free}, Locked: {item.Locked}");
            }
        }

        public void MonitorAllPair()
        {
            // Console.WriteLine("running monitorAllPair");
            var tickerPrices = from dat in binanceClient.GetAllPrices().Result
                               where dat.Symbol.Contains("BUSD")
                               select dat;

            foreach (var item in tickerPrices)
            {
                // Console.WriteLine(item.Symbol + "  " + item.Price);
                if (dictionary.ContainsKey(item.Symbol))
                {
                    // dictionary[item.Symbol].name = item.Symbol;
                    dictionary[item.Symbol].price = item.Price;
                }
                else
                {
                    Pair pair = new Pair();
                    pair.name = item.Symbol;
                    pair.price = item.Price;
                    dictionary[item.Symbol] = pair;
                }
            }
            if (watchPair != "" && threadDelegate != null)
                threadDelegate(dictionary[watchPair]);
        }

        System.Threading.Timer timer;
        System.Threading.Timer timer1;

        private bool isRestore;
        private Timer timer3;

        public void Test()
        {
            timer = new System.Threading.Timer(OnTimedEvent, null, 1000, Timeout.Infinite);
        }
        public void Test1()
        {
            timer1 = new System.Threading.Timer(OnTimedEvent1, null, 120000, Timeout.Infinite);
        }
        public void Test2() { }
        public void Test3()
        {
            Console.WriteLine("Timer3 is running");
           
            timer3 = new System.Threading.Timer(OnTimedEvent3, null, 130000, Timeout.Infinite);
        }
        private void OnTimedEvent3(object state)
        {
            //throw new NotImplementedException();
            timer3.Change(70000, Timeout.Infinite);
        }

        private void OnTimedEvent1(object state)
        {
            Console.WriteLine("Timer1 is running");
            //Console.WriteLine("Saving MarketPrint Data");
            SaveMarketPrint();
            timer1.Change(120000, Timeout.Infinite);
        }

        private void SaveMarketPrint()
        {
            summary();
        }

        public void OnTimedEvent(Object state)
        {
            Console.WriteLine("timer is running");
            MonitorAllPair();
            timer.Change(30000, Timeout.Infinite);
        }

        public void summary()
        {
            if (!isRestore)
            {
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
                    dataContext.marketPrints.Add(marketPrint);
                    // Console.WriteLine($"{marketPrint.pair}  Price: {marketPrint.price} MaxPrice: {marketPrint.max} MinPrice: {marketPrint.min} Price: {marketPrint.price}   Percent Change position: {marketPrint.pricePercentPosition}");
                }
                dataContext.SaveChanges();
            }
        }

        public void buy(String pair)
        {
            Console.WriteLine($"buying  {pair}");
        }

        public void selling(String pair)
        {
            Console.WriteLine($"Selling  {pair}");
        }

        public void ClearOldData() {
            DataContext dataContext = new DataContext();
            var datad = from dt in dataContext.marketPrints
                        where dt.time < DateTime.Now.AddDays(-10) && dt.pair != null
                        select dt;
            foreach (var item in datad)
            {
                dataContext.Remove(item);
            }
            dataContext.SaveChangesAsync();
        }
        public void RestoreFromDb()
        {
            DataContext dataContext = new DataContext();
            var datad = from dt in dataContext.marketPrints
                        where dt.pair == null
                        select dt;
            foreach (var item in datad)
            {
                dataContext.Remove(item);
            }
            dataContext.SaveChanges();
            var data = from dt in dataContext.marketPrints
                       where dt.time > DateTime.Now.AddHours(-24) && dt.pair != null
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

        public void monitorPair(Pair pair)
        {
            //  Console.WriteLine($"monitoring {pair.name}");
            //  Console.WriteLine($"Price: {pair.price}");
            // Console.WriteLine($"Max: {pair.histories.Max(t=>t.price)}");
            // Console.WriteLine($"Min: {pair.histories.Min(t=>t.price)}");
            //  Console.WriteLine($"count: {pair.histories.Count()}");
        }
        List<String> interestPair = new List<string>() { "" };
        decimal interestAmount = 0;
        Uti uti = new Uti();
        public void EntryMarket()
        {
            
            DataContext dataContext = new DataContext();
            if (dataContext.tradingPairs.ToList().Count < 1)
            {
                Console.WriteLine("Pls indicate at least one pair of interest for trading");
                return;
            }
selectPair: Console.WriteLine("Select a pair to enter buy condition for");
            var i = 0;
            foreach (var item in dataContext.tradingPairs.ToList())
            {
                Console.WriteLine($"{item.Id}. {item.name}");
            }

            try
            {
                var selection = int.Parse(Console.ReadLine());
                TradingPair tradingP = dataContext.tradingPairs.Single(e=>e.Id==selection);
percentEquity:  Console.WriteLine("Enter percent of equity to commit (maximum of 1 and greater than 0)");
                
                var pEquity = float.Parse(Console.ReadLine());
                if (pEquity > 1 || pEquity <= 0) goto percentEquity;
                Console.WriteLine("Enter max equity size to commit");
                var mEquity = float.Parse(Console.ReadLine());
                BuyCondition buyCondition1 = new BuyCondition() { tradingPair = tradingP, percentBalance = pEquity, maxInvestment = mEquity };
                //checking for existing buy condition to edit or add new buy condition
                if (dataContext.buyConditions.Select(e => e.tradingPair).Select(e => e.name).Contains(buyCondition1.tradingPair.name))
                {
                    var data = from a in dataContext.buyConditions
                               where a.tradingPair.name == buyCondition1.tradingPair.name
                               select a;
                    data.ToList()[0].tradingPair = tradingP;
                    data.ToList()[0].maxInvestment = mEquity;
                    data.ToList()[0].percentBalance = pEquity;
                    dataContext.SaveChangesAsync();
                   
                }
                else 
                {
                    dataContext.buyConditions.Add(buyCondition1);
                    dataContext.SaveChangesAsync();
                }
                Console.WriteLine("Thanks you have entered a buy condition, I will take it from here");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Entring");
                goto selectPair;
                //throw;
            }
                 // string read = "";
           
        }
        public void entringMarketStage1A(BuyCondition buyItem, decimal interestAmount) {
            DataContext dataContext = new DataContext();
            interestPair.Clear();
            var item =buyItem.tradingPair.name;
                    if (item != "")
                    {

                        var data = from dt in dataContext.marketPrints
                                   where dt.time > DateTime.Now.AddHours(-216) && dt.pair.Contains(item)
                                   select dt;
                        MinMax limit = new MinMax();
                        limit.max = 0;
                        limit.min = 100000;
                        foreach (var ite in data)
                            if (ite.max > limit.max)
                                limit.max = ite.max;
                        foreach (var ite in data)
                            if (ite.min < limit.min)
                                limit.min = ite.min;
                        Pair pair = dictionary[item];
                        var pricePosition = ((pair.price - limit.min) * 100 / (limit.max - limit.min));
                        if (pricePosition < 2)
                        {
                        isTryingToBuy = true;
                            uti.consoleInterface("watching for best condition");
                            prices = new watchList<decimal>();
                            threadDelegate = new ThreadDelegate(watchEntry);
                            prices.Clear();
                        }
                        else uti.consoleInterface($"Condition not optimal {pricePosition} max:{limit.max}  min:{limit.min} pair:{pair.price}");
                    }
        }
        internal void Mprocess() {
            var Proces3 = new ThreadStart(() =>
            {
                do
                {
                        program.monitorOrdering();
                    Thread.Sleep(30000);
                } while (true);
            });
            Thread p3 = new Thread(Proces3);
            p3.Start();
        }
        public void buyProcess()
        {
            var Proces3 = new ThreadStart(() =>
            {
                do
                {
                    if(!isTryingToBuy)
                    program.entringMarketStage1();
                    Thread.Sleep(30000);
                } while (true);
            });
            Thread p3 = new Thread(Proces3);
            p3.Start(); 
        }

        public void entringMarketStage1() {
            program.BalanceInfo(binanceClient);
            DataContext dataContext = new DataContext();

            foreach (var item in dataContext.buyConditions.ToList())
            {

                var tradingP_ = dataContext.tradingPairs.Where(e => e.Id == item.tradingPairId).Select(e => e).ToList()[0];
                item.tradingPair = tradingP_;
                AccountBalance[item.tradingPair.name.Replace("BUSD", "")].setWorth(dictionary[item.tradingPair.name]);
                uti.consoleInterface($"Current {item.tradingPair.name} worth is {AccountBalance[item.tradingPair.name.Replace("BUSD", "")].worth}");
                watchPair = item.tradingPair.name;
                var watchPairHaveAmount = 0M;
                var busdbalance = 0M;
                
                interestAmount = interestAmount < (decimal)item.maxInvestment ? interestAmount : (decimal)item.maxInvestment;
                try
                {
                    busdbalance= AccountBalance["BUSD"].Free;
                    interestAmount =Math.Round(busdbalance * (decimal)item.percentBalance,0);
                    watchPairHaveAmount =  AccountBalance[watchPair.Replace("BUSD", "")].worth;
                }
                catch (Exception)
                {
                    watchPairHaveAmount = 0;
                   // throw;
                }
                // checking running order and trading order
                var isRunning = dataContext.runningOrders.Where(e => e.pair == item.tradingPair.name).Select(e => e.pair).Contains(item.tradingPair.name);
                var isPending = false;
                try
                {
                  isPending  = dataContext.tradeOrders.Where(e => e.pair == item.tradingPair.name).Select(e => e.IsOpen).ToList()[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace + "line 669 entringMarketStage1");
                    //throw;
                }
               
                if (watchPairHaveAmount > 15M || interestAmount<10M)
                {
                    continue;
                }
                if (isRunning || isPending)
                {

                    continue;
                }
                entringMarketStage1A(item,interestAmount);
            }
        }

      
        watchList<decimal> prices = new watchList<decimal>();
        int score = 0; watchList<int> scores = new watchList<int>();
        internal static bool ordering = false;
        internal static bool buying;
        private bool isTryingToBuy;
        private bool isM;
        private static bool writbalanceinfo=false;

        public void watchEntry(Pair pair)
        {
            //Boolean condition = true;
            /*do
            {
                Console.WriteLine($"{pair.change.change}  {pair.change.margin}")
            } while (condition);
            */
            int i = prices.Count() - 1;
            if (prices.Count() != 0)
                if (pair.price > prices[i])
                {
                    score = 1;
                    Console.WriteLine($"price is going up from {prices[i]} to {pair.price} and score : {score}");
                    scores.Add(score);
                    Console.WriteLine($"Sum score: {scores.Sum()}, Count: {scores.Count()}");
                    if (scores.Sum() > 5)
                    {
                        var pr = uti.roundToSignificantDigits(prices.Min() - (prices.Min() * 0.01M), 3);
                        Console.WriteLine($"Buy trade for pair {pair.name}, Quantity {interestAmount} at price: {pr}");
                        
                        placeOrderBuy(binanceClient, pair, pr);
                        threadDelegate = null;
                    }
                }
                else
                {
                    if (pair.price < prices[i])
                    {
                        score = 0;
                        scores.Add(score);
                    }
                    Console.WriteLine($"price is not yet improving for {pair.name} {prices[i]}  {pair.price}");
                }
            prices.Add(pair.price);
        }
        private void placeOrderBuy(BinanceClient binanceClient, Pair pair, decimal price)
        {
            // throw new NotImplementedException();
            Console.WriteLine("Implementing coin purchase for pair " + pair.name);
            var quantity = Math.Round(interestAmount / price, 0);
            interestAmount = quantity;
            var buyOrder = binanceClient.PostNewOrder(pair.name, interestAmount, price, OrderSide.BUY).Result;
            TradeOrder tradeOrder = new TradeOrder();
            tradeOrder.BinanceOrderId = buyOrder.OrderId;
            tradeOrder.ClientOrderId = buyOrder.ClientOrderId;
            tradeOrder.pair = pair.name;
            tradeOrder.price = price;
            tradeOrder.quantity = interestAmount;
            tradeOrder.tradeType = TradeType.buy;
            tradeOrder.IsOpen = true;
            tradeOrder.OrderTime = DateTime.UtcNow;
            DataContext dataContext = new DataContext();
            dataContext.tradeOrders.Add(tradeOrder);
           // dataContext.tradingPairs.Add(new TradingPair() { name = tradeOrder.pair });
            dataContext.SaveChangesAsync();
        }

        private void placeOrderSell(BinanceClient binanceClient, Pair pair, decimal price)
        {
            // throw new NotImplementedException();
            var buyOrder = binanceClient.PostNewOrder(pair.name, interestAmount, price, OrderSide.BUY).Result;
            TradeOrder tradeOrder = new TradeOrder();
            tradeOrder.BinanceOrderId = buyOrder.OrderId;
            tradeOrder.ClientOrderId = buyOrder.ClientOrderId;
            tradeOrder.pair = pair.name;
            tradeOrder.price = price;
            tradeOrder.quantity = interestAmount;
            tradeOrder.tradeType = TradeType.sell;
            tradeOrder.IsOpen = true;
            DataContext dataContext = new DataContext();
            dataContext.tradeOrders.Add(tradeOrder);
            dataContext.SaveChangesAsync();
        }

    }
}
