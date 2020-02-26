using Xunit;
using chequewriter.src;
using System.IO;
using Moq;
using Xunit.Abstractions;

namespace chequewriter.test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper stdOutput;

        public UnitTest1(ITestOutputHelper output)
        {
            this.stdOutput = output;
        }

        [Fact]
        public void Validate_Digit_ReturnsTrue()
        {
            string input = "1";
            bool output = Function.isValid(input);
            Assert.True(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", true);
        }

        [Fact]
        public void Validate_RandomChars_ReturnsFalse()
        {
            string input = Path.GetRandomFileName();
            input = input.Replace(".", "");
            bool output = Function.isValid(input);
            Assert.False(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", false);
        }

        [Theory]
        [InlineData("1,1")]
        [InlineData("1,23")]
        [InlineData("1,023")]
        public void Validate_Cents_ReturnsTrue(string input)
        {
            bool output = Function.isValid(input);
            Assert.True(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", true);
        }

        [Theory]
        [InlineData("1,23.45")]
        [InlineData("1.23,45")]
        public void Validate_More_Than_Two_CommaOrDot_ReturnsFalse(string input)
        {
            bool output = Function.isValid(input);
            Assert.False(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", false);
        }

        [Fact]
        public void Validate_Cents_NonDigit_ReturnFalse()
        {
            string input = "12345,ab";
            bool output = Function.isValid(input);
            Assert.False(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", false);
        }

        [Fact]
        public void Validate_Input_Negative_Value_ReturnFalse()
        {
            string input = "-12345";
            bool output = Function.isValid(input);
            Assert.False(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", false);
        }

        [Fact]
        public void Validate_Input_GreaterThan_Quintillion_ReturnFalse()
        {
            string input = "1e+21";
            bool output = Function.isValid(input);
            Assert.False(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", false);
        }

        [Fact]
        public void Validate_Input_LessThan_Sextillion_ReturnTrue()
        {
            string input = "1e+20";
            bool output = Function.isValid(input);
            Assert.True(output);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", true);
        }

        [Theory]
        [InlineData("123", "ONE HUNDRED AND TWENTY THREE DOLLARS")]
        [InlineData("12", "TWELVE DOLLARS")]
        [InlineData("1", "ONE DOLLAR")]
        public void Convert_Hundreds(string input, string expected)
        {
            Function function = new Function();
            string output = function.ChequeWriting(input);
            Assert.Equal(output, expected);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", expected);
        }

        [Theory]
        [InlineData("1000000000000000000", "ONE QUINTILLION DOLLARS")]
        [InlineData("1000000000000", "ONE TRILLION DOLLARS")]
        [InlineData("1000000000", "ONE BILLION DOLLARS")]
        [InlineData("1000000", "ONE MILLION DOLLARS")]
        [InlineData("1000", "ONE THOUSAND DOLLARS")]
        [InlineData("100", "ONE HUNDRED DOLLARS")]
        public void Convert_All_TenPowers(string input, string expected)
        {
            Function function = new Function();
            string output = function.ChequeWriting(input);
            Assert.Equal(output, expected);
            stdOutput.WriteLine("Input : {0}", input);
            stdOutput.WriteLine("Output : {0}", output);
            stdOutput.WriteLine("Expected : {0}", expected);
        }
    }
}
