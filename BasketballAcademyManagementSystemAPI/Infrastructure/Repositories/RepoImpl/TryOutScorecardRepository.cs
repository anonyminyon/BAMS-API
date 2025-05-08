using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class TryOutScorecardRepository : ITryOutScorecardRepository
    {
        private readonly BamsDbContext _dbContext;
        public TryOutScorecardRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TryOutScorecard?> GetByPlayerAndMeasurementAsync(int playerRegistrationId, string measurementScaleCode)
        {
            return await _dbContext.TryOutScorecards
                .FirstOrDefaultAsync(s => s.PlayerRegistrationId == playerRegistrationId && s.MeasurementScaleCode == measurementScaleCode);
        }

        public async Task AddOrUpdateAsync(TryOutScorecard scorecard)
        {
            var existing = await GetByPlayerAndMeasurementAsync(scorecard.PlayerRegistrationId, scorecard.MeasurementScaleCode);
            if (existing != null)
            {
                existing.Score = scorecard.Score;
                existing.UpdatedAt = DateTime.Now;
            }
            else
            {
                await _dbContext.TryOutScorecards.AddAsync(scorecard);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TryOutScorecard?> GetAsync(int tryOutScorecardId)
        {
            return await _dbContext.TryOutScorecards.FirstOrDefaultAsync(s => s.TryOutScorecardId == tryOutScorecardId);
        }

        public async Task<List<TryOutScorecard>> GetScoresByPlayerIdAsync(int playerRegistrationId)
        {
            return await _dbContext.TryOutScorecards
                .Where(s => s.PlayerRegistrationId == playerRegistrationId)
                .Include(s => s.MeasurementScaleCodeNavigation)
                .Include(s => s.ScoredByNavigation)
                .ToListAsync();
        }

        public async Task<List<PlayerRegistration>> GetFilteredPlayersAsync(int sessionId, PlayerRegistrationScoresFilterDto filter)
        {
            var query = _dbContext.PlayerRegistrations
                .Where(p => p.MemberRegistrationSessionId == sessionId)
                .Include(p => p.TryOutScorecards).ThenInclude(s => s.MeasurementScaleCodeNavigation)
                .Include(p => p.TryOutScorecards).ThenInclude(s => s.ScoredByNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchValue))
            {
                filter.SearchValue = filter.SearchValue.ToLower();
                query = query.Where(p =>
                    p.FullName.ToLower().Contains(filter.SearchValue) ||
                    p.CandidateNumber.ToString().Contains(filter.SearchValue) ||
                    p.PlayerRegistrationId.ToString().Contains(filter.SearchValue));
            }

            if (filter.Gender.HasValue)
            {
                query = query.Where(p => p.Gender == filter.Gender.Value);
            }

            var players = await query.ToListAsync();

            return players;
        }

        public async Task DeleteAsync(TryOutScorecard scorecard)
        {
            _dbContext.TryOutScorecards.Remove(scorecard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(ICollection<TryOutScorecard> scorecards)
        {
            _dbContext.TryOutScorecards.RemoveRange(scorecards);
            await _dbContext.SaveChangesAsync();
        }
    }
}
