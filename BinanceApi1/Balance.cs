using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
    class Balance
    {
      public  String Coin;
      public  decimal Free;
      public  decimal Locked;
      public decimal worth;
      public void setWorth(Pair pair) {
           this. worth= pair.price * Free;
      }
    }
}
