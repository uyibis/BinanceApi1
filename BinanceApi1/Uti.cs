using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinanceApi1
{
    class Uti
    {
        public event EventHandler<PairChangeEventArgs> OnChange;
        public Decimal max(List<History> ticker24s)
        {

            return (from ti in ticker24s
                    select ti.price).Max();

        }
        public Decimal min(List<History> ticker24s)
        {
            return (from ti in ticker24s
                    select ti.price).Min();
        }
        public decimal average(List<History> ticker24s)
        {
            return (from ti in ticker24s
                    select ti.price).Average();
        }
        public decimal sum(List<History> ticker24s)
        {
            return (from ti in ticker24s
                    select ti.price).Sum();
        }
        public decimal lastPrice(List<History> ticker24s)
        {
            return ticker24s[ticker24s.Count - 1].price;
        }
        public void analyse(Pair pair)
        {
            pair.markcond.aveOld = pair.markcond.aveNew;
            pair.markcond.maxOld = pair.markcond.maxNew;
            pair.markcond.minOld = pair.markcond.minNew;
            decimal oldval = pair.histories[pair.histories.Count - 2].price;
            decimal newval = pair.histories[pair.histories.Count - 1].price;
           // pair.histories.RemoveAt(pair.histories.Count - 1);
            pair.markcond.aveNew = average(pair.histories);
            pair.markcond.maxNew = max(pair.histories);
            pair.markcond.minNew = min(pair.histories);
            pair.markcond.def = newval - oldval;
            pair.markcond.defs.Add(pair.markcond.def);

            if(pair.markcond.aveOld!=0)
            pair.markcond.percentChange = Math.Round((pair.price - pair.markcond.aveOld)*100/pair.markcond.aveOld,2);
            pair.markcond.maxDef =  pair.price- pair.markcond.maxNew;
            pair.markcond.minDef = pair.price - pair.markcond.minNew;
            if (pair.markcond.minOld > pair.markcond.minNew) {
               // Console.WriteLine("Going down");
                Changes changes = new Changes();
                changes.change = Changes.Change.min;
                changes.pairName = pair.name;
                changes.margin=pair.markcond.minOld-pair.markcond.minNew;
                changes.time = DateTime.Now;
                pair.change = changes;
                PairChangeEventArgs pairChangeEventArgs = new PairChangeEventArgs();
                pairChangeEventArgs.changes = changes;
                RaiseChanageEvent(pairChangeEventArgs);
                // fire event;

            }
            if (pair.markcond.maxOld < pair.markcond.maxNew)
            {
               // Console.WriteLine("Going up");
                Changes changes = new Changes();
                changes.change = Changes.Change.max;
                changes.pairName = pair.name;
                changes.margin = pair.markcond.maxNew - pair.markcond.maxOld;
                changes.time = DateTime.Now;
                changes.price = pair.price;
                pair.change = changes;
                PairChangeEventArgs pairChangeEventArgs = new PairChangeEventArgs();
                pairChangeEventArgs.changes = changes;
                RaiseChanageEvent(pairChangeEventArgs);
                //fire event
            }
            pair.marketPrint.time = pair.time;
            pair.marketPrint.price = pair.price;
            pair.marketPrint.max = pair.markcond.maxNew;
            pair.marketPrint.min = pair.markcond.minNew;
            pair.marketPrint.pair = pair.name;
            decimal spread = pair.marketPrint.max - pair.marketPrint.min;
            if (spread == 0)
                spread = 100;
            pair.marketPrint.pricePercentPosition = ((pair.price - pair.marketPrint.min) * 100) / spread;
        }

        protected void RaiseChanageEvent(PairChangeEventArgs e)
        {
            EventHandler<PairChangeEventArgs> handler = OnChange;
            handler?.Invoke(this, e);
        }
        public void onLimitChange(object sender, PairChangeEventArgs e)
        {
            string read;
            consoleInterface($"Pair: {e.changes.pairName} ----- Limit change {e.changes.change}  -----   Margin {e.changes.margin} ---- Price {e.changes.price}", 0,out read);
        }
        public MinMax minMaxTime48(int duration)
        {
            MinMax minMax = new MinMax();
            DataContext dataContext = new DataContext();
            minMax.min = (from dt in dataContext.marketPrints
                          where dt.time > DateTime.Now.AddHours(duration)
                          select dt.min).Min();

            minMax.max = (from dt in dataContext.marketPrints
                          where dt.time > DateTime.Now.AddHours(duration)
                          select dt.max).Max();
            return minMax;
        }

        public void consoleInterface(String statement, int cas,out string read)
        {
           
            //lock (this)
            
                switch (cas)
                {
                    case 0:
                    if(!Program.ordering)
                        Console.WriteLine(statement);
                        read = "";
                        break;
                case 1:
                    if (!Program.buying)
                        Console.WriteLine(statement);
                    read = "";
                    break;
                default:
                        read = Console.ReadLine();
                        break;
                }
            
        }
        public void consoleInterface(String statement)
        {
                    if (!Program.buying)
                        Console.WriteLine(statement);
        }

        public decimal roundToSignificantDigits(decimal d, int digits)
        {
            if (d == 0.0M || Double.IsNaN((double)d) || Double.IsInfinity((double)d))
            {
                return d;
            }
            decimal scale = (decimal)Math.Pow(10, Math.Floor(Math.Log10((double)Math.Abs(d))) + 1);
            return (scale * Math.Round((decimal)d / scale, digits, MidpointRounding.AwayFromZero));
        }
    }
}
