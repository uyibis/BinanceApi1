using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
  public  class BuyCondition
    {
        public int Id { get; set; }
        public TradingPair tradingPair { set; get; }
        public int tradingPairId { set; get; }
        public float percentBalance { get; set; }
        public float maxInvestment { get; set; }
    }
}
