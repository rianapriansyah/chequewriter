using System;
using Xunit;
using chequewriter.src;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace chequewriter.test
{
    public class UnitTest1
    {
        static String[] unitsMap = new[]{
                "ONE DOLLAR", "TWO",
                "THREE", "FOUR", "FIVE",
                "SIX", "SEVEN", "EIGHT",
                "NINE", "TEN", "ELEVEN",
                "TWELVE", "THIRTEEN", "FOURTEEN",
                "FIFTEEN", "SIXTEEN", "SEVENTEEN",
                "EIGHTEEN", "NINETEEN" };


        static Dictionary<int, String> tenMaps = new Dictionary<int, string>(){
                {10, "TEN"},
                {11, "ELEVEN"},
                {12, "TWELVE"},
                {13, "THIRTEEN"},
                {14, "FOURTEEN"},
                {15, "FIFTEEN"},
                {16, "SIXTEEN"},
                {17, "SEVENTEEN"},
                {18, "EIGHTEEN"},
                {19, "NINETEEN"}
            };

        static Dictionary<Double, String> tenPowerMaps = new Dictionary<Double, string>(){
                {(Double)Math.Pow(10, 18), "QUINTILLION"},
                {(Double)Math.Pow(10, 15), "QUADRILLION"},
                {(Double)Math.Pow(10, 12), "TRILLION"},
                {(Double)Math.Pow(10, 9), "BILLION"},
                {(Double)Math.Pow(10, 6), "MILLION"},
                {(Double)Math.Pow(10, 3), "THOUSAND"},
                {(Double)Math.Pow(10, 2), "HUNDRED"},
                {(Double)Math.Pow(10, 1), "TEN"}
            };

        [Fact]
        public void TestValidateDigit()
        {
            for (int i = 0; i < 10; i++)
            {
                bool output = Function.isValid(i.ToString());
                Assert.True(output);
            }
        }

        [Fact]
        public void TestValidateDigitFalse()
        {
            for (int i = 0; i < 10; i++)
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                bool output = Function.isValid(i + path);
                Assert.True(output, "input : " + path + " is not acceptable");
            }
        }

        [Fact]
        public void TestValidateCents()
        {
            for (int i = 0; i < 101; i++)
            {
                string x = "1,";
                bool output = Function.isValid(x+i.ToString());
                Assert.True(output, "input : " + i + " is not acceptable as cents");
            }
        }

        [Fact]
        public void TestGetTeensMaps()
        {
            foreach (var pair in tenMaps)
            {
                String expected = pair.Value;
                String output = Function.getUnits(pair.Key);
                Assert.Equal(expected, output);
            }
        }

        [Fact]
        public void TestGetCents()
        {
            string expected = "ONE CENT";
            string output = Function.getCents(1);
            Assert.Equal(expected, output);
        }

        [Fact]
        public void TestRecurrenceUnit()
        {
            foreach (var pair in tenPowerMaps)
            {
                String expectedToContain = pair.Value;
                String output = Program.ChequeWriting(pair.Key.ToString());

                Assert.True(output.Contains(expectedToContain));
            }
        }

        [Fact]
        public void TestRecurrenceUnitFalse()
        {
            foreach (var pair in tenPowerMaps)
            {
                String expectedToContain = pair.Value;
                String output = Program.ChequeWriting(pair.Key + 1.ToString());

                Assert.False(output.Contains(expectedToContain), "input is bigger than the max limit");
            }
        }
    }
}
