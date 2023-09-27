namespace simple
{
    internal class Helpful
    {
        public class NaturalStringComparer : IComparer<string>
        {
            private readonly StringComparer _comparisonType;

            public NaturalStringComparer(StringComparer comparisonType)
            {
                _comparisonType = comparisonType;
            }
            public int Compare(string x, string y) => NaturalStringCompare(x, y, _comparisonType);
            private static int NaturalStringCompare(string x, string y, StringComparer comparisonType)
            {
                int num1 = 0;
                int num2 = 0;
                int result;

                while (num1 < x.Length && num2 < y.Length)
                {
                    if (char.IsDigit(x[num1]) && char.IsDigit(y[num2]))
                    {
                        string numStr1 = "";
                        string numStr2 = "";

                        while (num1 < x.Length && char.IsDigit(x[num1]))
                        {
                            numStr1 += x[num1];
                            num1++;
                        }

                        while (num2 < y.Length && char.IsDigit(y[num2]))
                        {
                            numStr2 += y[num2];
                            num2++;
                        }

                        int numInt1 = int.Parse(numStr1);
                        int numInt2 = int.Parse(numStr2);

                        result = numInt1.CompareTo(numInt2);
                        if (result != 0)
                            return result;
                    }
                    else
                    {
                        result = comparisonType.Compare(x[num1].ToString(), y[num2].ToString());
                        if (result != 0)
                            return result;
                        num1++;
                        num2++;
                    }
                }

                return x.Length - y.Length;
            }
        }
    }
}
