using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
    class MarketManager
    {

        public void buy(BinanceClient binanceClient) {
            try
            {
                var buyOrder = binanceClient.PostNewOrder("ancbusd", 151m, 0.135m, OrderSide.BUY).Result;
                //var accountInfo = program.binanceClient.GetAccountInfo().Result;
                Console.WriteLine(buyOrder.OrderId);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
