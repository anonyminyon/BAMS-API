namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class RegexConstant
    {
        /*
            ^           : Bắt đầu chuỗi
            [^@\s]+     : Ít nhất một ký tự bất kỳ, ngoại trừ dấu '@' và khoảng trắng (phần tên email)
            @           : Ký tự '@' bắt buộc
            [^@\s]+     : Ít nhất một ký tự bất kỳ, ngoại trừ dấu '@' và khoảng trắng (tên miền)
            \.          : Dấu chấm bắt buộc (.)
            [^@\s]+     : Ít nhất một ký tự bất kỳ, ngoại trừ dấu '@' và khoảng trắng (tên miền cấp cao, ví dụ: com, net, org)
            $           : Kết thúc chuỗi
        */
        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        /*
            ^                 : Bắt đầu chuỗi
            (?=.*[A-Z])       : Đảm bảo có ít nhất một chữ cái viết hoa (A-Z)
            (?=.*[a-z])       : Đảm bảo có ít nhất một chữ cái viết thường (a-z)
            (?=.*\d)          : Đảm bảo có ít nhất một chữ số (0-9)
            (?=.*[\W_])       : Đảm bảo có ít nhất một ký tự đặc biệt (bất kỳ ký tự không phải chữ hoặc số, bao gồm _)
            .{8,}             : Đảm bảo mật khẩu có ít nhất 8 ký tự
            $                 : Kết thúc chuỗi
        */
        public const string PasswordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$";

        /*
            ^                     : Bắt đầu chuỗi
            (\+\d{1,3}\s?)?       : Mã quốc gia (tùy chọn), ví dụ: +84, +84 
            \d{2}                 : 2 chữ số đầu (có thể là 03, 05, 07, 08, 09, 01)
            [\s\.-]?              : Dấu phân cách tùy chọn (khoảng trắng, dấu chấm hoặc gạch ngang)
            \d{3}                 : 3 chữ số tiếp theo
            [\s\.-]?              : Dấu phân cách tùy chọn
            \d{3}                 : 3 chữ số cuối
            [\s\.-]?              : Dấu phân cách tùy chọn
            \d{3}                 : 3 chữ số cuối (cho số 11 chữ số)
            $                     : Kết thúc chuỗi
            
            Hỗ trợ các định dạng:
            - 0912345678
            - 09-123-45678
            - 09.123.45678
            - 09 123 45678
            - +84 912345678
            - 0123456789
            - 84123456789 (không khuyên dùng)
        */
        public const string VietnamPhoneRegex = @"^(\+\d{1,3}\s?)?(0\d{2}[\s\.-]?\d{3}[\s\.-]?\d{3,4}|1\d{9}|\d{2}[\s\.-]?\d{3}[\s\.-]?\d{3,4})$";

        /*
            ^                     : Bắt đầu chuỗi
            (0\d{9,10})           : Số điện thoại bắt đầu bằng 0 và có 10-11 chữ số
            |                     : Hoặc
            (\+\d{1,3}\s?0\d{9})  : Mã quốc gia (+84) + số điện thoại 10 chữ số
            $                     : Kết thúc chuỗi
            
            Hỗ trợ:
            - 0912345678 (10 số)
            - 01234567890 (11 số - một số nhà mạng)
            - +840912345678
        */
        public const string StrictVietnamPhoneRegex = @"^(0\d{9,10}|(\+\d{1,3}\s?0\d{9}))$";
    }
}