using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
  public  class Pair
    {
      public   String name;
      private Decimal _price;
      public   Decimal price {
            get { return _price; }
            set {
                History hist = new History();
                hist.price = value;
                hist.time = DateTime.Now;
                histories.Add(hist);
                time = DateTime.Now;
                _price = value;
                Uti uti = new Uti();
                
                uti.OnChange += uti.onLimitChange;
                if (this.histories.Count>1)
                uti.analyse(this);
                
            } 
        }
       public DateTime time;
       private Changes _change;
       public Changes change { get { return _change; }set { _change = value;changes.Add(value); } }
       public readonly List<Changes> changes = new List<Changes>();
       public readonly PairList<History> histories=new PairList<History>();
       public marketCondition markcond = new marketCondition();
       public MarketPrint marketPrint = new MarketPrint();
    }

    public class History {
        public DateTime time;
        public Decimal price;
    }
    
}
