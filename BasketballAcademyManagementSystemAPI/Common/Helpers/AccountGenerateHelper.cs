using System;
using System.Linq;
using System.Text;
using BasketballAcademyManagementSystemAPI.Infrastructure;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
	public class AccountGenerateHelper
	{
		private readonly BamsDbContext _bamsDbContext;
		private static readonly Random _random = new Random();
		public AccountGenerateHelper(BamsDbContext context)
		{
			_bamsDbContext = context;
		}
		#region Tạo username theo fullname
		public static string[] SplitFullName(string fullName)
		{
			if (string.IsNullOrEmpty(fullName))
			{
				return null;
			}

			string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (nameParts.Length == 1)
			{
				return new string[] { nameParts[0], "", "" }; // Chỉ có tên
			}
			else if (nameParts.Length == 2)
			{
				return new string[] { nameParts[0], "", nameParts[1] }; // Họ + tên
			}
			else if (nameParts.Length == 3)
			{
				return new string[] { nameParts[0], nameParts[1], nameParts[2] }; // Họ + tên đệm + tên
			}
			else if (nameParts.Length > 3)
			{
				return new string[] { nameParts[0], string.Join(" ", nameParts.Skip(1).Take(nameParts.Length - 2)), nameParts[^1] }; // Họ + tên đệm + tên
			}

			return null;
		}


		public static string ToNonAccentVietnamese(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return "";
			}

			// Mapping Vietnamese characters to their non-accented versions
			Dictionary<char, char> vietnameseMap = new Dictionary<char, char>
		{
			{ 'á', 'a' }, { 'à', 'a' }, { 'ả', 'a' }, { 'ã', 'a' }, { 'ạ', 'a' },
			{ 'ă', 'a' }, { 'ắ', 'a' }, { 'ằ', 'a' }, { 'ẳ', 'a' }, { 'ẵ', 'a' }, { 'ặ', 'a' },
			{ 'â', 'a' }, { 'ấ', 'a' }, { 'ầ', 'a' }, { 'ẩ', 'a' }, { 'ẫ', 'a' }, { 'ậ', 'a' },

			{ 'é', 'e' }, { 'è', 'e' }, { 'ẻ', 'e' }, { 'ẽ', 'e' }, { 'ẹ', 'e' },
			{ 'ê', 'e' }, { 'ế', 'e' }, { 'ề', 'e' }, { 'ể', 'e' }, { 'ễ', 'e' }, { 'ệ', 'e' },

			{ 'í', 'i' }, { 'ì', 'i' }, { 'ỉ', 'i' }, { 'ĩ', 'i' }, { 'ị', 'i' },

			{ 'ó', 'o' }, { 'ò', 'o' }, { 'ỏ', 'o' }, { 'õ', 'o' }, { 'ọ', 'o' },
			{ 'ô', 'o' }, { 'ố', 'o' }, { 'ồ', 'o' }, { 'ổ', 'o' }, { 'ỗ', 'o' }, { 'ộ', 'o' },
			{ 'ơ', 'o' }, { 'ớ', 'o' }, { 'ờ', 'o' }, { 'ở', 'o' }, { 'ỡ', 'o' }, { 'ợ', 'o' },

			{ 'ú', 'u' }, { 'ù', 'u' }, { 'ủ', 'u' }, { 'ũ', 'u' }, { 'ụ', 'u' },
			{ 'ư', 'u' }, { 'ứ', 'u' }, { 'ừ', 'u' }, { 'ử', 'u' }, { 'ữ', 'u' }, { 'ự', 'u' },

			{ 'ý', 'y' }, { 'ỳ', 'y' }, { 'ỷ', 'y' }, { 'ỹ', 'y' }, { 'ỵ', 'y' },

			{ 'Á', 'A' }, { 'À', 'A' }, { 'Ả', 'A' }, { 'Ã', 'A' }, { 'Ạ', 'A' },
			{ 'Ă', 'A' }, { 'Ắ', 'A' }, { 'Ằ', 'A' }, { 'Ẳ', 'A' }, { 'Ẵ', 'A' }, { 'Ặ', 'A' },
			{ 'Â', 'A' }, { 'Ấ', 'A' }, { 'Ầ', 'A' }, { 'Ẩ', 'A' }, { 'Ẫ', 'A' }, { 'Ậ', 'A' },

			{ 'É', 'E' }, { 'È', 'E' }, { 'Ẻ', 'E' }, { 'Ẽ', 'E' }, { 'Ẹ', 'E' },
			{ 'Ê', 'E' }, { 'Ế', 'E' }, { 'Ề', 'E' }, { 'Ể', 'E' }, { 'Ễ', 'E' }, { 'Ệ', 'E' },

			{ 'Í', 'I' }, { 'Ì', 'I' }, { 'Ỉ', 'I' }, { 'Ĩ', 'I' }, { 'Ị', 'I' },

			{ 'Ó', 'O' }, { 'Ò', 'O' }, { 'Ỏ', 'O' }, { 'Õ', 'O' }, { 'Ọ', 'O' },
			{ 'Ô', 'O' }, { 'Ố', 'O' }, { 'Ồ', 'O' }, { 'Ổ', 'O' }, { 'Ỗ', 'O' }, { 'Ộ', 'O' },
			{ 'Ơ', 'O' }, { 'Ớ', 'O' }, { 'Ờ', 'O' }, { 'Ở', 'O' }, { 'Ỡ', 'O' }, { 'Ợ', 'O' },

			{ 'Ú', 'U' }, { 'Ù', 'U' }, { 'Ủ', 'U' }, { 'Ũ', 'U' }, { 'Ụ', 'U' },
			{ 'Ư', 'U' }, { 'Ứ', 'U' }, { 'Ừ', 'U' }, { 'Ử', 'U' }, { 'Ữ', 'U' }, { 'Ự', 'U' },

			{ 'Ý', 'Y' }, { 'Ỳ', 'Y' }, { 'Ỷ', 'Y' }, { 'Ỹ', 'Y' }, { 'Ỵ', 'Y' }
		};

			StringBuilder sb = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				sb.Append(vietnameseMap.ContainsKey(c) ? vietnameseMap[c] : c);
			}

			return sb.ToString();
		}

		// Viết thường và bỏ dấu
		public static string ToLowerCaseNonAccentVietnamese(string input)
		{
			return ToNonAccentVietnamese(input).ToLower();
		}

		// Lấy chữ cái đầu tiên của mỗi từ
		public static string ExtractFirstCharacter(string str)
		{
			var result = new System.Text.StringBuilder();
			foreach (var word in str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				result.Append(word[0]);
			}
			return result.ToString().ToLower();
		}

		// Tạo tên người dùng
		public static string GenerateUseName(string fullName)
		{
			string lowerNonAcc = ToLowerCaseNonAccentVietnamese(fullName);
			string[] nameParts = SplitFullName(lowerNonAcc);

			if (nameParts == null) return "";

			string username;

			if (string.IsNullOrEmpty(nameParts[1])) // Không có tên đệm
			{
				username = nameParts[2] + ExtractFirstCharacter(nameParts[0]); // Tên + Họ
			}
			else
			{
				username = nameParts[2] + ExtractFirstCharacter(nameParts[0]) + ExtractFirstCharacter(nameParts[1]); // Tên + Họ + Đệm
			}

			// Bổ sung logic đảm bảo username luôn >=5 ký tự
			if (username.Length < 5)
			{
				var random = new Random();
				while (username.Length < 5)
				{
					username += random.Next(0, 9).ToString(); // thêm số ngẫu nhiên (0-9)
				}
			}

			return username;

		}

		// Lấy username duy nhất từ DB
		public string GetUniqueUsername(string fullName)
		{

			string useName = GenerateUseName(fullName);
			string finalUserName;
			string fullNameInput = fullName.Trim().ToLower();

			int count = _bamsDbContext.Users.Count(u => u.Fullname.Trim().ToLower() == fullNameInput);

			if (count > 0)
			{
				Console.WriteLine("Tên đã tồn tại trong DB!");
				finalUserName = useName + (count + 1);
			}
			else
			{
				finalUserName = useName;
			}

			Console.WriteLine("Generated Username: " + finalUserName);
			return finalUserName;
		}
		#endregion

		#region tạo  pasword 8-10 kí tự, chỉ có 1 kí tự đặc biệt, chứa số , chữ hoa chữ thường
		public string GeneratePassword(int minLength = 8, int maxLength = 10)
		{
			// Đảm bảo mật khẩu có độ dài hợp lệ
			if (minLength < 8) minLength = 8;
			if (maxLength < 8) maxLength = 8;
			if (maxLength > 10) maxLength = 10;
			if (maxLength < minLength) maxLength = minLength;

			// Các bộ ký tự cho mật khẩu
			const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
			const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			const string digits = "0123456789";
			const string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

			// Ghép tất cả các bộ ký tự lại với nhau
			string allChars = lowerCase + upperCase + digits;

			// Tạo mật khẩu ngẫu nhiên
			var passwordLength = _random.Next(minLength, maxLength + 1); // Độ dài mật khẩu ngẫu nhiên
			var password = new StringBuilder(passwordLength);

			// Đảm bảo có ít nhất 1 ký tự trong mỗi loại (chữ thường, chữ hoa, số)
			password.Append(lowerCase[_random.Next(lowerCase.Length)]);
			password.Append(upperCase[_random.Next(upperCase.Length)]);
			password.Append(digits[_random.Next(digits.Length)]);

			// Thêm 1 ký tự đặc biệt
			password.Append(specialChars[_random.Next(specialChars.Length)]);

			// Điền các ký tự còn lại từ các bộ ký tự thông thường
			for (int i = password.Length; i < passwordLength - 1; i++) // -1 vì đã có 1 ký tự đặc biệt
			{
				password.Append(allChars[_random.Next(allChars.Length)]);
			}

			// Xáo trộn mật khẩu
			var result = new string(password.ToString().OrderBy(c => _random.Next()).ToArray());
			return result;
		}

		#endregion
	}

}

