using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
    public class PairList<T> : List<T>
    {
        public new void Add(T item)
        {
            if (Count > 2880)
            {
                Remove(this[0]);
            }
            base.Add(item);
        }
    }
}
