using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
   public class Changes
    {
       public enum Change {min,max}
      public  Change change;
      public  decimal margin;
      public  DateTime time;
      public String pairName;
      public Decimal price;
    }
}
