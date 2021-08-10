using System.Linq;

namespace codingtest
{
    public class Validator
    {
        public static bool Validate(string stdin)
        {
            if (stdin.Contains('.') || stdin.Contains(','))
            {
                char[] delimiterChars = { ',', '.' };
                string[] input = stdin.Split(delimiterChars);

                for (int i = 0; i < input.Length; i++)
                {
                    if (!ValidateInput(input[i], i == (input.Length - 1)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return ValidateInput(stdin, false);
            }
        }

        static bool ValidateInput(string stdin, bool cent)
        {
            double input = 0d;
            if (!double.TryParse(stdin, out input))
            {
                return false;
            }
            else
            {
                if (cent)
                {
                    return ValidateCents(input, 99);
                }
                else
                {
                    return ValidateDollar(input, NumDict.TenPowerMaps.ElementAt(0).Key);
                }
            }
        }

        static bool ValidateDollar(double dollar, double max)
        {
            if (dollar > max)
            {
                return false;
            }

            return true;
        }

        static bool ValidateCents(double cents, double max)
        {
            if (cents > max)
            {
                return false;
            }

            return true;
        }
    }
}
