using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
     class PairChangeEventArgs: EventArgs
    {
      public  Changes changes { set; get; }
    }
}
