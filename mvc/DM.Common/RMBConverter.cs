using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common
{
    public static class RMBConverter
    {
        public static string MoneyToChinese(string pLowerMoney)
        {
            bool isNegative = false;
            string lowerMoney = pLowerMoney;
            if (pLowerMoney.Trim().Substring(0, 1) == "-")
            {
                lowerMoney = lowerMoney.Trim().Remove(0, 1);
                isNegative = true;
            }
            lowerMoney = Math.Round(double.Parse(lowerMoney), 2).ToString();
            if (lowerMoney.IndexOf(".") > 0)
            {
                if (lowerMoney.IndexOf(".") == lowerMoney.Length - 2)
                {
                    lowerMoney = lowerMoney + "0";
                }
            }
            else
            {
                lowerMoney = lowerMoney + ".00";
            }
            string upperMoney = "";
            string unitPart = "";
            int i = 1;
            while (i <= lowerMoney.Length)
            {
                switch (lowerMoney.Substring(lowerMoney.Length - i, 1))
                {
                    case ".":
                        unitPart = "圆";
                        break;
                    case "0":
                        unitPart = "零";
                        break;
                    case "1":
                        unitPart = "壹";
                        break;
                    case "2":
                        unitPart = "贰";
                        break;
                    case "3":
                        unitPart = "叁";
                        break;
                    case "4":
                        unitPart = "肆";
                        break;
                    case "5":
                        unitPart = "伍";
                        break;
                    case "6":
                        unitPart = "陆";
                        break;
                    case "7":
                        unitPart = "柒";
                        break;
                    case "8":
                        unitPart = "捌";
                        break;
                    case "9":
                        unitPart = "玖";
                        break;
                }

                switch (i)
                {
                    case 1:
                        unitPart = unitPart + "分";
                        break;
                    case 2:
                        unitPart = unitPart + "角";
                        break;
                    case 3:
                        unitPart = unitPart + "";
                        break;
                    case 4:
                        unitPart = unitPart + "";
                        break;
                    case 5:
                        unitPart = unitPart + "拾";
                        break;
                    case 6:
                        unitPart = unitPart + "佰";
                        break;
                    case 7:
                        unitPart = unitPart + "仟";
                        break;
                    case 8:
                        unitPart = unitPart + "万";
                        break;
                    case 9:
                        unitPart = unitPart + "拾";
                        break;
                    case 10:
                        unitPart = unitPart + "佰";
                        break;
                    case 11:
                        unitPart = unitPart + "仟";
                        break;
                    case 12:
                        unitPart = unitPart + "亿";
                        break;
                    case 13:
                        unitPart = unitPart + "拾";
                        break;
                    case 14:
                        unitPart = unitPart + "佰";
                        break;
                    case 15:
                        unitPart = unitPart + "仟";
                        break;
                    case 16:
                        unitPart = unitPart + "万";
                        break;
                    default:
                        unitPart = unitPart + "";
                        break;
                }

                upperMoney = unitPart + upperMoney;
                i = i + 1;
            }

            upperMoney = upperMoney.Replace("零拾", "零");
            upperMoney = upperMoney.Replace("零佰", "零");
            upperMoney = upperMoney.Replace("零仟", "零");
            upperMoney = upperMoney.Replace("零零零", "零");
            upperMoney = upperMoney.Replace("零零", "零");
            upperMoney = upperMoney.Replace("零角零分", "整");
            upperMoney = upperMoney.Replace("零分", "整");
            upperMoney = upperMoney.Replace("零角", "零");
            upperMoney = upperMoney.Replace("零亿零万零圆", "亿圆");
            upperMoney = upperMoney.Replace("亿零万零圆", "亿圆");
            upperMoney = upperMoney.Replace("零亿零万", "亿");
            upperMoney = upperMoney.Replace("零万零圆", "万圆");
            upperMoney = upperMoney.Replace("零亿", "亿");
            upperMoney = upperMoney.Replace("零万", "万");
            upperMoney = upperMoney.Replace("零圆", "圆");
            upperMoney = upperMoney.Replace("零零", "零");

            if (upperMoney.Substring(0, 1) == "圆")
            {
                upperMoney = upperMoney.Substring(1, upperMoney.Length - 1);
            }
            if (upperMoney.Substring(0, 1) == "零")
            {
                upperMoney = upperMoney.Substring(1, upperMoney.Length - 1);
            }
            if (upperMoney.Substring(0, 1) == "角")
            {
                upperMoney = upperMoney.Substring(1, upperMoney.Length - 1);
            }
            if (upperMoney.Substring(0, 1) == "分")
            {
                upperMoney = upperMoney.Substring(1, upperMoney.Length - 1);
            }
            if (upperMoney.Substring(0, 1) == "整")
            {
                upperMoney = "零圆整";
            }
            if (isNegative == true)
            {
                return "负" + upperMoney;
            }
            else
            {
                return upperMoney;
            }
        }
    }
}
