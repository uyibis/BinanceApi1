using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
    class MarketPrint
    {
        public int Id { set; get; }
        public decimal max { set; get; }
        public decimal price { set; get; }
        public decimal min { set; get; }
        public DateTime time { set; get; }
        public string pair { set; get; }
        public decimal pricePercentPosition { set; get; }
    }
}
