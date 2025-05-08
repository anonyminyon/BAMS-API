using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class SendMailService : ISendMailService
    {
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly EmailHelper _emailHelper;
        public SendMailService(IMailTemplateRepository mailTemplateRepository, EmailHelper emailHelper)
        {
            _mailTemplateRepository = mailTemplateRepository;
            _emailHelper = emailHelper;
        }
        public async Task SendMailAssignToTeamAsync(User user, Team team, string mailTemplateId)
        {
            // Kiểm tra null cho các tham số đầu vào
            if (user == null || team == null || string.IsNullOrEmpty(mailTemplateId))
            {
                throw new ArgumentNullException("User, team, or mailTemplateId cannot be null or empty.");
            }

            // Lấy mẫu email từ repository
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(mailTemplateId);
            if (mailTemplate == null)
            {
                throw new InvalidOperationException("Mail template not found.");
            }

            // Thay thế các placeholder trong nội dung email
            mailTemplate.Content = mailTemplate.Content
                .Replace("{{USER_NAME}}", user.Fullname)
                .Replace("{{TEAM_NAME}}", team.TeamName)
                .Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS)
                .Replace("{{DAY_ASSIGN_TO_TEAM}}", DateTime.Now.ToString("dd/MM/yyyy"))
                .Replace("{{ROLES_CODE}}", user.RoleCode);

            // Gửi email bất đồng bộ
            await _emailHelper.SendEmailAsync(user.Email, mailTemplate.TemplateTitle, mailTemplate.Content);
        }

        //send email to guest relate approve, reject, send form regis success
        public async Task SendMailByMailTemplateIdAsync(string mailTemplateId, string email, dynamic data)
        {
            if (string.IsNullOrEmpty(mailTemplateId)) return;

            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(mailTemplateId);
            if (mailTemplate != null)
            {
                switch (mailTemplateId)
                {
                    //sửa nội dung approve manager registration đã được vào clb và cung cấp mật khẩu tài khoản
                    case string id when id == MailTemplateConstant.ApproveManagerRegistration:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{FULLNAME}}", data?.Fullname ?? "Bạn")
                            .Replace("{{USERNAME}}", data?.Username ?? "Chưa có")
                            .Replace("{{PASSWORD}}", data?.Password ?? "Chưa có")
                            .Replace("{{LOGIN_LINK}}", MailTemplateConstant.URL_TO_BAMS);
                        break;
                    //gửi email thông báo huấn luyện viên đã được vào clb và cung cấp mật khẩu tài khoản
                    case string id when id == MailTemplateConstant.CoachRegistrationSuccess:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{FULLNAME}}", data?.Fullname ?? "Bạn")
                            .Replace("{{USERNAME}}", data?.Username ?? "Chưa có")
                            .Replace("{{PASSWORD}}", data?.Password ?? "Chưa có")
                            .Replace("{{LOGIN_LINK}}", MailTemplateConstant.URL_TO_BAMS);
                        break;
                    //sửa nội dung reject registration
                    case string id when id == MailTemplateConstant.RejectManagerRegistration:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{USER_NAME}}", data?.FullName ?? "Bạn");
                        break;
                    //sửa nội dung gửi registration thành công 
                    case string id when id == MailTemplateConstant.SendFormRegistrationSuccess:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{USER_NAME}}", data?.FullName ?? "Bạn")
                            .Replace("{{ROLE_CODE}}", data?.RoleCode ?? "Học Viên")
                            .Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
                        break;
                    // nội dung approved player
					case string id when id == MailTemplateConstant.ApprovedPlayerEmailRegistration:
						mailTemplate.Content = mailTemplate.Content
							  .Replace("{{USER_NAME}}", data?.Username)
							.Replace("{{PASSWORD}}", data?.Password)
							.Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
						break;
					//sửa nội dung reject player registration
					case string id when id == MailTemplateConstant.RejectPlayerRegistration:
						mailTemplate.Content = mailTemplate.Content
							.Replace("{{USER_NAME}}", data?.FullName ?? "Bạn");
                        break;
					//sửa nội dung call to try out
					case string id when id == MailTemplateConstant.CallToTryOut:
						mailTemplate.Content = mailTemplate.Content
					        .Replace("{{LOCATION}}", data?.TryOutLocation ?? "N/A")
					        .Replace("{{TIME}}", data?.TryOutDate?.ToString("dd-MM-yyyy, HH:mm") ?? "N/A")
					        .Replace("{{SBD}}", data?.CandidateNumber.ToString())
					        .Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
						break;
                    //sửa nội dung gửi otp
					case string id when id == MailTemplateConstant.VerifyEmailRegistration:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{OTP_CODE}}", data?.Otp)
                            .Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
                        break;
					case string id when id == MailTemplateConstant.ApprovedParentEmailRegistration:
						mailTemplate.Content = mailTemplate.Content
                            .Replace("{{PARENT_NAME}}", data?.Fullname)
							.Replace("{{USER_NAME}}", data?.Username)
							.Replace("{{PASSWORD}}", data?.Password)
							.Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
						break;
                    //gửi email yêu cầu phụ huynh thanh toán tiền
					case string id when id == MailTemplateConstant.RequireParentPayment:
						mailTemplate.Content = mailTemplate.Content
							.Replace("{{TEN_HOC_VIEN}}", data?.PlayerName)
							.Replace("{{SO_TIEN}}", data?.Amount)
							.Replace("{{HAN_THANH_TOAN}}", DateTime.Now.AddDays(15).ToString("dd/MM/yyyy")) // hạn thanh toán 15 ngày
							.Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
						break;
					//gửi email yêu cầu học viên thanh toán tiền
					case string id when id == MailTemplateConstant.RequirePlayerPayment:
						mailTemplate.Content = mailTemplate.Content
							.Replace("{{TEN_HOC_VIEN}}", data?.Username)
							.Replace("{{SO_TIEN}}", data?.Amount)
							.Replace("{{HAN_THANH_TOAN}}", DateTime.Now.AddDays(15).ToString("dd/MM/yyyy")) // hạn thanh toán 15 ngày
							.Replace("{{URL_GO_TO_PAGE}}", MailTemplateConstant.URL_TO_BAMS);
						break;
                    //gửi email yêu cầu học viên thanh toán tiền khi chỉ còn 5 3 1 ngày
                    case string id when id == MailTemplateConstant.ReminderPayment:
						mailTemplate.Content = mailTemplate.Content
							.Replace("{{FULLNAME}},", data?.FullName)
                            .Replace("{{DAY_LEFT}}", data?.DayLeft)
                            .Replace("{{TEAMFUND_DESCRIPTION}}", data?.TeamFundDescription)
                            .Replace("{{DUEDATE}}", data?.DueDate)
                            .Replace("{{URL_GO_TO}}", MailTemplateConstant.URL_TO_BAMS);
						break;
                    //gửi email report điểm danh cho phụ huynh
                    case string id when id == MailTemplateConstant.ReportAttendanceToParent:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{PLAYER_NAME}},", data?.PlayerName)
                            .Replace("{{TRAINING_DATE}},", data?.TrainingSessionInfo.TrainingDate)
                            .Replace("{{START_TIME}},", data?.TrainingSessionInfo.StartTime)
                            .Replace("{{COURT_NAME}},", data?.TrainingSessionInfo.CourtName)
                            .Replace("{{COURT_ADDRESS}},", data?.TrainingSessionInfo.CourtAddress)
                            .Replace("{{ATTENDANCE_STATUS}},", data?.Status);
						break;
                    //gửi email đính chính thông tin điểm danh của con cho phụ huynh
                    case string id when id == MailTemplateConstant.CorrectionAttendanceToParent:
                        mailTemplate.Content = mailTemplate.Content
                            .Replace("{{PLAYER_NAME}},", data?.PlayerName)
                            .Replace("{{TRAINING_DATE}},", data?.TrainingSessionInfo.TrainingDate)
                            .Replace("{{START_TIME}},", data?.TrainingSessionInfo.StartTime)
                            .Replace("{{COURT_NAME}},", data?.TrainingSessionInfo.CourtName)
                            .Replace("{{COURT_ADDRESS}},", data?.TrainingSessionInfo.CourtAddress)
                            .Replace("{{COURT_ADDRESS}},", data?.Status);
						break;
					//gửi email từ chối teamfund
					case string id when id == MailTemplateConstant.CorrectionAttendanceToParent:
						mailTemplate.Content = mailTemplate.Content
							.Replace("{{PLAYER_NAME}},", data?.PlayerName)
							.Replace("{{TRAINING_DATE}},", data?.TrainingSessionInfo.TrainingDate)
							.Replace("{{START_TIME}},", data?.TrainingSessionInfo.StartTime)
							.Replace("{{COURT_NAME}},", data?.TrainingSessionInfo.CourtName)
							.Replace("{{COURT_ADDRESS}},", data?.TrainingSessionInfo.CourtAddress)
							.Replace("{{COURT_ADDRESS}},", data?.Status);
						break;
					case null:
                        throw new ArgumentNullException(nameof(mailTemplateId), "mailTemplateId cannot be null.");

                    default:
                        throw new ArgumentException("Invalid mailTemplateId", nameof(mailTemplateId));
                }

                var t = new Thread(() => _emailHelper.SendEmailMultiThread(email, mailTemplate.TemplateTitle, mailTemplate.Content));
                t.Start();
            }
        }
    }
}
