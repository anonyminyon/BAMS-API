using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly BamsDbContext _dbContext;
        public EmailVerificationRepository(BamsDbContext context)
        {
            _dbContext = context;
        }

        //Hàm lưu code vào database
        public async Task SaveVerificationCodeWithOtpAsync(string email, string otp, string purpose)
        {
            var existingRecord = await _dbContext.EmailVerifications.FirstOrDefaultAsync(e => e.Email == email);
            //nếu tìm được thì update
            if (existingRecord != null)
            {
                existingRecord.Email = email;
                existingRecord.Code = otp;
                existingRecord.ExpiresAt = DateTime.Now.AddMinutes(10);//để thời gian hết hạn của code là 10p
                existingRecord.Purpose = purpose;
                existingRecord.IsUsed = false;
                existingRecord.CreatedAt = DateTime.Now;
            }
            else
            {
                //tạo dữ liệu mới
                _dbContext.EmailVerifications.Add(new Models.EmailVerification
                {
                    Email = email,
                    Code = otp,
                    ExpiresAt = DateTime.Now.AddMinutes(10),
                    Purpose = purpose,
                    IsUsed = false,
                    CreatedAt = DateTime.Now
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyCodeAsync(string email, string code, string purpose)
        {
            var record = await _dbContext.EmailVerifications.FirstOrDefaultAsync(
               e => e.Email == email
            && e.Code.Equals(code)
            && e.Purpose.Equals(purpose));

            if (record == null || record.ExpiresAt < DateTime.Now)
                return false;

            record.IsUsed = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEmailVerifiedOtp(string email)
        {
            //lấy ra email đã được lưu trong bảng EmailVerification
            var emailVerify = await _dbContext.EmailVerifications.FirstOrDefaultAsync(e => e.Email == email);
            //nếu lấy được tức là email này đã verify otp trong quá khứ rồi nên chỉ việc gửi lại
            if (emailVerify != null)
            {
                //nếu email đấy đã verify thành công
                if (emailVerify.IsUsed)
                {
                    //nếu email hết hạn rồi thì gửi lại otp cho nó
                    if (emailVerify.ExpiresAt < DateTime.Now)
                    {
                        //SaveVerificationCodeAsync(email);
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsEmailVerified(string email)
        {
            //Email đã tồn tại và đã xác minh
            return await _dbContext.EmailVerifications.AnyAsync(e => e.Email == email && e.IsUsed);
        }

        
    }
}
