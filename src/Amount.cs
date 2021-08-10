using System;
using System.Collections.Generic;
using System.Text;

namespace codingtest
{
    public class Amount
    {
        public Dollar Dollar { get; set; }
        public Cent Cent { get; set; }
        public bool IsValid { get; set; }
    }

    public class Dollar
    {
        public int Dollars { get; set; }
    }

    public class Cent
    {
        public int Cents { get; set; }
    }
}
