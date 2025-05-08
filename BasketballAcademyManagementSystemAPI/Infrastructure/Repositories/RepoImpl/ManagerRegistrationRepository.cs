using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class ManagerRegistrationRepository : IManagerRegistrationRepository
    {
        private readonly BamsDbContext _dbContext;

        public ManagerRegistrationRepository(BamsDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task AddNewManagerRegistrationAsync(ManagerRegistration managerEntity)
        {
            await _dbContext.ManagerRegistrations.AddAsync(managerEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ChangeStatusByManagerRegistrationID(int managerRegistrationId, string status)
        {
            var mri = await _dbContext.ManagerRegistrations
                .FirstOrDefaultAsync(mri => mri.ManagerRegistrationId == managerRegistrationId);

            if (mri != null)
            {
                mri.Status = status;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ManagerRegistration> GetManagerRegistrationByID(int id)
        {
            return await _dbContext.ManagerRegistrations.FindAsync(id);
        }

		public async Task<PagedResponseDto<ManagerRegistrationDto>> GetAllManagerRegistrations(ManagerRegistrationFilterDto filter)
        {
            var query = _dbContext.ManagerRegistrations.AsQueryable();

            if (filter.ManagerRegistrationId != null && filter.ManagerRegistrationId != 0)
                query = query.Where(m => m.ManagerRegistrationId == filter.ManagerRegistrationId);
            if (filter.MemberRegistrationSessionId != null && filter.MemberRegistrationSessionId != 0)
                query = query.Where(m => m.MemberRegistrationSessionId == filter.MemberRegistrationSessionId);
            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(m => m.FullName.Contains(filter.FullName));
            if (!string.IsNullOrEmpty(filter.GenerationAndSchoolName))
                query = query.Where(m => m.GenerationAndSchoolName.Contains(filter.GenerationAndSchoolName));
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(m => m.PhoneNumber.Contains(filter.PhoneNumber));
            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(m => m.Email.Contains(filter.Email));
            if (!string.IsNullOrEmpty(filter.FacebookProfileUrl))
                query = query.Where(m => m.FacebookProfileUrl.Contains(filter.FacebookProfileUrl));
            if (!string.IsNullOrEmpty(filter.KnowledgeAboutAcademy))
                query = query.Where(m => m.KnowledgeAboutAcademy.Contains(filter.KnowledgeAboutAcademy));
            if (!string.IsNullOrEmpty(filter.ReasonToChooseUs))
                query = query.Where(m => m.ReasonToChooseUs.Contains(filter.ReasonToChooseUs));
            if (!string.IsNullOrEmpty(filter.KnowledgeAboutAmanager))
                query = query.Where(m => m.KnowledgeAboutAmanager.Contains(filter.KnowledgeAboutAmanager));
            if (!string.IsNullOrEmpty(filter.ExperienceAsAmanager))
                query = query.Where(m => m.ExperienceAsAmanager.Contains(filter.ExperienceAsAmanager));
            if (!string.IsNullOrEmpty(filter.Strength))
                query = query.Where(m => m.Strength.Contains(filter.Strength));
            if (!string.IsNullOrEmpty(filter.WeaknessAndItSolution))
                query = query.Where(m => m.WeaknessAndItSolution.Contains(filter.WeaknessAndItSolution));
            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(m => m.Status == filter.Status);
            else
                query = query.Where(m => m.Status == RegistrationStatusConstant.PENDING);
            if (filter.SubmitedDate.HasValue)
                query = query.Where(m => m.SubmitedDate.Date == filter.SubmitedDate.Value.Date);
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                query = query.Where(m => m.SubmitedDate.Date >= filter.StartDate.Value.Date && m.SubmitedDate.Date <= filter.EndDate.Value.Date);

            int totalRecords = await query.CountAsync();
            var managers = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(m => new ManagerRegistrationDto
                {
                    ManagerRegistrationId = m.ManagerRegistrationId,
                    MemberRegistrationSessionId = m.MemberRegistrationSessionId,
                    FullName = m.FullName,
                    GenerationAndSchoolName = m.GenerationAndSchoolName,
                    PhoneNumber = m.PhoneNumber,
                    Email = m.Email,
                    FacebookProfileUrl = m.FacebookProfileUrl,
                    KnowledgeAboutAcademy = m.KnowledgeAboutAcademy,
                    ReasonToChooseUs = m.ReasonToChooseUs,
                    KnowledgeAboutAmanager = m.KnowledgeAboutAmanager,
                    ExperienceAsAmanager = m.ExperienceAsAmanager,
                    Strength = m.Strength,
                    WeaknessAndItSolution = m.WeaknessAndItSolution,
                    Status = m.Status,
                    SubmitedDate = m.SubmitedDate.ToString("yyyy-MM-dd")
                }).ToListAsync();

            return new PagedResponseDto<ManagerRegistrationDto>
            {
                Items = managers,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        public async Task DeleteAsync(ManagerRegistration managerRegistration)
        {
            _dbContext.ManagerRegistrations.Remove(managerRegistration);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(ICollection<ManagerRegistration> managerRegistrations)
        {
            _dbContext.ManagerRegistrations.RemoveRange(managerRegistrations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateManagerRegistrationAsync(ManagerRegistration managerEntity)
        {
            try
            {
                // 1. Kiểm tra đơn đăng ký có tồn tại không
                var existingRegistration = await _dbContext.ManagerRegistrations
                    .FirstOrDefaultAsync(m => m.ManagerRegistrationId == managerEntity.ManagerRegistrationId);

                if (existingRegistration == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy đơn đăng ký Manager");
                }

                // 2. Validate dữ liệu
                if (string.IsNullOrWhiteSpace(managerEntity.FullName))
                {
                    throw new ArgumentException("Họ và tên không được để trống");
                }

                if (string.IsNullOrWhiteSpace(managerEntity.Email))
                {
                    throw new ArgumentException("Email không được để trống");
                }

                // Có thể thêm các validate khác tùy nghiệp vụ

                // 3. Kiểm tra email có thay đổi không (nếu không cho phép thay đổi email)
                if (existingRegistration.Email != managerEntity.Email)
                {
                    throw new InvalidOperationException("Không được phép thay đổi email");
                }

                // 4. Cập nhật thông tin
                existingRegistration.FullName = managerEntity.FullName;
                existingRegistration.GenerationAndSchoolName = managerEntity.GenerationAndSchoolName;
                existingRegistration.PhoneNumber = managerEntity.PhoneNumber;
                existingRegistration.FacebookProfileUrl = managerEntity.FacebookProfileUrl;
                existingRegistration.KnowledgeAboutAcademy = managerEntity.KnowledgeAboutAcademy;
                existingRegistration.ReasonToChooseUs = managerEntity.ReasonToChooseUs;
                existingRegistration.KnowledgeAboutAmanager = managerEntity.KnowledgeAboutAmanager;
                existingRegistration.ExperienceAsAmanager = managerEntity.ExperienceAsAmanager;
                existingRegistration.Strength = managerEntity.Strength;
                existingRegistration.WeaknessAndItSolution = managerEntity.WeaknessAndItSolution;
                existingRegistration.Status = managerEntity.Status;

                // 5. Lưu thay đổi
                _dbContext.ManagerRegistrations.Update(existingRegistration);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("Lỗi đồng thời khi cập nhật dữ liệu", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật đơn đăng ký Manager", ex);
            }
        }

        public async Task<bool> IsEmailExistsAndPendingAndNotInMemberRegistrationSessionAsync(string? email, int memberRegistrationSessionId)
        {
            return await _dbContext.ManagerRegistrations
                .AnyAsync(mr => 
                (email != null && mr.Email == email)
                && (mr.Status == RegistrationStatusConstant.PENDING)
                && mr.MemberRegistrationSessionId != memberRegistrationSessionId);
        }

        public async Task<bool> IsEmailExistsAndRejectAndInMemberRegistrationSessionAsync(string email, int memberRegistrationSessionId)
        {
            return await _dbContext.ManagerRegistrations
                .AnyAsync(mr =>
                (email != null && mr.Email == email)
                && (mr.Status == RegistrationStatusConstant.REJECTED)
                && mr.MemberRegistrationSessionId == memberRegistrationSessionId);
        }

        public async Task<ManagerRegistration> IsEmailExistsAndPendingAndInMemberRegistrationSessionAsync(string? email, int memberRegistrationSessionId)
        {
            return await _dbContext.ManagerRegistrations.Where(mr =>
                (email != null && mr.Email == email)
                && (mr.Status == RegistrationStatusConstant.PENDING)
                && mr.MemberRegistrationSessionId == memberRegistrationSessionId).FirstOrDefaultAsync();
        }

    }
}
