using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

namespace lw
{
    public static class Parse
    {
        public static bool boolval(dynamic value)
        {
            if (value is int || value is double || value is float)
            {
                return Convert.ToDouble(value) != 0;
            }
            if (value is string)
            {
                return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value) && value != "0";
            }
            if (value is ICollection<object> collection)
            {
                return collection.Count > 0;
            }
            if (value is ICollection)
            {
                return value.Count > 0;
            }
            if (value is object)
            {
                return true;
            }
            return false;
        }

        public static int intval(dynamic value, int? baseValue = null)
        {
            if (value == null) return 0;
            if (value is bool boolValue) return boolValue ? 1 : 0;
            if (value is int intValue) return intValue;

            string? stringValue = value.ToString().ToLower().TrimStart('0');

            if (baseValue.HasValue && int.TryParse(stringValue, System.Globalization.NumberStyles.AllowHexSpecifier, null, out int hexValue))
            {
                return hexValue;
            }
            if (int.TryParse(stringValue, out int longValue))
            {
                return longValue;
            }
            return 0;
        }

        public static int doubleval(dynamic value, int? baseValue = null)
        {
            if (value == null) return 0;
            if (value is bool boolValue) return boolValue ? 1 : 0;
            if (value is int intValue) return intValue;

            string? stringValue = value.ToString().ToLower().TrimStart('0');

            if (baseValue.HasValue && int.TryParse(stringValue, NumberStyles.AllowHexSpecifier, null, out int hexValue))
            {
                return hexValue;
            }
            if (double.TryParse(stringValue, out double doubleValue))
            {
                return (int)Math.Floor(doubleValue);
            }
            return 0;
        }

        public static float floatval(dynamic value)
        {
            string input = Convert.ToString(value).Replace(',', '.');
            Regex regex = new(@"[-+]?[0-9]*[.]?[0-9]+");
            Match match = regex.Match(input);

            if (match.Success)
            {
                float floatVal = float.Parse(match.Value, CultureInfo.InvariantCulture);
                return floatVal;
            }
            else
            {
                return 0.0f;
            }
        }
    }
}
