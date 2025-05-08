using System.Text;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public static class CurrencyHelper
    {
        public static string GetFormattedMoney(decimal money)
        {
            string amount = money.ToString();
            StringBuilder formattedAmount = new StringBuilder();
            int endPoint = amount.IndexOf(".");
            if (endPoint < 0)
            {
                endPoint = amount.Length;
            }
            int count = 0;
            for (int i = endPoint - 1; i >= 0; i--)
            {
                formattedAmount.Insert(0, amount[i]);
                count++;

                if (count % 3 == 0 && i > 0)
                {
                    formattedAmount.Insert(0, ",");
                }
            }
            if (endPoint == amount.Length)
            {
                return formattedAmount.ToString();
            }
            else
            {
                return formattedAmount.ToString() + amount.Substring(endPoint) + " VNĐ";
            }
        }

    }
}
