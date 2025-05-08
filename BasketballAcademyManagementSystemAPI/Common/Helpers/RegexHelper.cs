using System.Text.RegularExpressions;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public static class RegexHelper
    {
        public static bool IsMatchRegex(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return false;

            return Regex.IsMatch(input, pattern);
        }

		//Kiểm tra số điện thoại vietnam hợp lệ
		public static bool IsValidVietnamesePhoneNumber(string phoneNumber)
		{
			var regex = new Regex(@"^(0|\+84)[3|5|7|8|9][0-9]{8}$");
			return regex.IsMatch(phoneNumber);
		}


		//Kiểm tra định dạng email hợp lệ
		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			// Regex pattern kiểm tra định dạng email chuẩn
			var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

			return regex.IsMatch(email);
		}

	}
}
