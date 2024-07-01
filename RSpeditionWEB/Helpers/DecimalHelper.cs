namespace RSpeditionWEB.Helpers
{
    public static class DecimalHelper
    {
        public static string FormatingDecimalToString(this decimal num)
        => string.Format("{0:f2}", num);

        public static string FormatingDecimalToString5(this decimal num)
        => string.Format("{0:f5}", num);

        public static decimal FormatingObjToDecimal(this object num)
        {
            var decimalData = 0M;

            var temp = num?.ToString() ?? string.Empty;

            if (Decimal.TryParse(temp, out decimal resPars1))
                decimalData = resPars1;
            else
            {
                if (temp.Contains(','))
                {
                    temp = temp.Replace(',', '.');
                    if (Decimal.TryParse(temp, out decimal resPars2))
                        decimalData = resPars2;
                }
                else if (temp.Contains('.'))
                {
                    temp = temp.Replace('.', ',');
                    if (Decimal.TryParse(temp, out decimal resPars3))
                        decimalData = resPars3;
                }
            }

            return decimalData;
        }

        public static decimal? FormatingObjToDecimalNullable(this object num)
        {
            decimal? decimalData = null;

            var temp = num?.ToString() ?? string.Empty;

            if (Decimal.TryParse(temp, out decimal resPars1))
                decimalData = resPars1;
            else
            {
                if (temp.Contains(','))
                {
                    temp = temp.Replace(',', '.');
                    if (Decimal.TryParse(temp, out decimal resPars2))
                        decimalData = resPars2;
                }
                else if (temp.Contains('.'))
                {
                    temp = temp.Replace('.', ',');
                    if (Decimal.TryParse(temp, out decimal resPars3))
                        decimalData = resPars3;
                }
            }

            return decimalData;
        }

        public static decimal RoundingDecimal(this decimal num, int digitsNum)
        {
            decimal res = Math.Round(num, digitsNum, MidpointRounding.AwayFromZero);
            var resStr = res.ToString();
            var resStrLenth = resStr.Length;

            var indPoint = resStr.LastIndexOf('.');

            if (indPoint == -1)
                indPoint = resStr.LastIndexOf(',');

            if (indPoint != -1)
            {
                var resStrDigNum = resStrLenth - indPoint - 1; // 12,4567
                var diff = resStrDigNum - digitsNum;
                if (diff > 0)
                {
                    var trimmedResStr = resStr.Substring(0, resStrLenth - diff);
                    res = FormatingObjToDecimalNullable(trimmedResStr) ?? num;
                }
            }

            return res;
        }
    }
}
