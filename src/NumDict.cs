using System;
using System.Collections.Generic;
using System.Text;

namespace codingtest
{
    public class NumDict
    {
        public static readonly List<string> UnitsMap = new List<string>()
        {
            "", "ONE", "TWO",
            "THREE", "FOUR", "FIVE",
            "SIX", "SEVEN", "EIGHT",
            "NINE", "TEN", "ELEVEN",
            "TWELVE", "THIRTEEN", "FOURTEEN",
            "FIFTEEN", "SIXTEEN", "SEVENTEEN",
            "EIGHTEEN", "NINETEEN"
        };

        public static readonly List<string> TensMap = new List<string>()
        {
            "", "TEN", "TWENTY",
            "THIRTY", "FORTY", "FIFTY",
            "SIXTY", "SEVENTY", "EIGHTY",
            "NINETY"
        };

        public static readonly Dictionary<double, string> TenPowerMaps = new Dictionary<double, string>()
        {
            {(double)Math.Pow(10, 21), "SEXTILLION"},
            {(double)Math.Pow(10, 18), "QUINTILLION"},
            {(double)Math.Pow(10, 15), "QUADRILLION"},
            {(double)Math.Pow(10, 12), "TRILLION"},
            {(double)Math.Pow(10, 9), "BILLION"},
            {(double)Math.Pow(10, 6), "MILLION"},
            {(double)Math.Pow(10, 3), "THOUSAND"},
            {(double)Math.Pow(10, 2), "HUNDRED"},
            {(double)Math.Pow(10, 1), "TEN"}
        };
    }
}
