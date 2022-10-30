using System;

namespace BinanceApi1
{
    public class TradeOrder
    {
      public int Id { get; set; }
      public string pair { get; set; }
      public decimal quantity { get; set; }
      public decimal price { get; set; }
      public TradeType tradeType { get; set; }

      public int BinanceOrderId { get; internal set; }
      public string ClientOrderId { get; internal set; }
      public bool IsOpen { get; internal set; }
        
      public DateTime OrderTime { get; set; }
    }
   public enum TradeType { buy, sell };
}