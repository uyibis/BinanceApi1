using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
 public   class marketCondition
    {
        public decimal maxOld;
        public decimal minOld;
        public decimal aveOld;
        public decimal def;
        public PairList<decimal> defs=new PairList<decimal>();
        public decimal maxNew;
        public decimal minNew;
        public decimal aveNew;
        //public int upCount;
       //public int downCount;
        public decimal percentChange;
        public decimal minDef;
        public decimal maxDef;
        
    }
}
