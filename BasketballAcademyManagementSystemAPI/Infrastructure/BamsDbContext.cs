using System;
using System.Collections.Generic;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure;

public partial class BamsDbContext : DbContext
{
	public BamsDbContext()
	{
	}

	public BamsDbContext(DbContextOptions<BamsDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Attendance> Attendances { get; set; }

	public virtual DbSet<CachedQuestion> CachedQuestions { get; set; }

	public virtual DbSet<ClubContact> ClubContacts { get; set; }

	public virtual DbSet<Coach> Coaches { get; set; }

	public virtual DbSet<Court> Courts { get; set; }

	public virtual DbSet<EmailVerification> EmailVerifications { get; set; }

	public virtual DbSet<Exercise> Exercises { get; set; }

	public virtual DbSet<Expenditure> Expenditures { get; set; }

	public virtual DbSet<ExpenditurePlayer> ExpenditurePlayers { get; set; }

	public virtual DbSet<FileHash> FileHashes { get; set; }

	public virtual DbSet<MailTemplate> MailTemplates { get; set; }

	public virtual DbSet<Manager> Managers { get; set; }

	public virtual DbSet<ManagerRegistration> ManagerRegistrations { get; set; }

	public virtual DbSet<Match> Matches { get; set; }

	public virtual DbSet<MatchArticle> MatchArticles { get; set; }

	public virtual DbSet<MatchLineup> MatchLineups { get; set; }

	public virtual DbSet<MemberRegistrationSession> MemberRegistrationSessions { get; set; }

	public virtual DbSet<Parent> Parents { get; set; }

	public virtual DbSet<Payment> Payments { get; set; }

	public virtual DbSet<PaymentItem> PaymentItems { get; set; }

	public virtual DbSet<Player> Players { get; set; }

	public virtual DbSet<PlayerRegistration> PlayerRegistrations { get; set; }

	public virtual DbSet<President> Presidents { get; set; }

	public virtual DbSet<Team> Teams { get; set; }

	public virtual DbSet<TeamFund> TeamFunds { get; set; }

	public virtual DbSet<TrainingSession> TrainingSessions { get; set; }

	public virtual DbSet<TrainingSessionStatusChangeRequest> TrainingSessionStatusChangeRequests { get; set; }

	public virtual DbSet<TryOutMeasurementScale> TryOutMeasurementScales { get; set; }

	public virtual DbSet<TryOutScoreCriterion> TryOutScoreCriteria { get; set; }

	public virtual DbSet<TryOutScoreLevel> TryOutScoreLevels { get; set; }

	public virtual DbSet<TryOutScorecard> TryOutScorecards { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<UserFace> UserFaces { get; set; }

	public virtual DbSet<UserForgotPasswordToken> UserForgotPasswordTokens { get; set; }

	public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

	public virtual DbSet<UserTeamHistory> UserTeamHistories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Attendance>(entity =>
		{
			entity.HasKey(e => e.AttendanceId).HasName("Attendance_pk");

			entity.ToTable("Attendance");

			entity.Property(e => e.AttendanceId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.ManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Note).HasMaxLength(100);
			entity.Property(e => e.TrainingSessionId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.Manager).WithMany(p => p.Attendances)
				.HasForeignKey(d => d.ManagerId)
				.HasConstraintName("Attendance_Manager");

			entity.HasOne(d => d.TrainingSession).WithMany(p => p.Attendances)
				.HasForeignKey(d => d.TrainingSessionId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Attendance_TrainingSession");

			entity.HasOne(d => d.User).WithMany(p => p.Attendances)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Attendance_User");
		});

		modelBuilder.Entity<CachedQuestion>(entity =>
		{
			entity.HasKey(e => e.CachedQuestionId).HasName("CachedQuestion_pk");

			entity.ToTable("CachedQuestion");

			entity.Property(e => e.Question).HasMaxLength(255);
		});

		modelBuilder.Entity<ClubContact>(entity =>
		{
			entity.HasKey(e => e.ContactMethodId).HasName("ClubContact_pk");

			entity.ToTable("ClubContact");

			entity.Property(e => e.ContactMethodName).HasMaxLength(50);
			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.IconUrl).HasColumnName("IconURL");
			entity.Property(e => e.MethodValue).HasMaxLength(100);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
		});

		modelBuilder.Entity<Coach>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("Coach_pk");

			entity.ToTable("Coach");

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Bio).HasMaxLength(255);
			entity.Property(e => e.CreatedByPresidentId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.CreatedByPresident).WithMany(p => p.Coaches)
				.HasForeignKey(d => d.CreatedByPresidentId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Coach_President");

			entity.HasOne(d => d.Team).WithMany(p => p.Coaches)
				.HasForeignKey(d => d.TeamId)
				.HasConstraintName("Coach_Team");

			entity.HasOne(d => d.User).WithOne(p => p.Coach)
				.HasForeignKey<Coach>(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Coach_User");
		});

		modelBuilder.Entity<Court>(entity =>
		{
			entity.HasKey(e => e.CourtId).HasName("Court_pk");

			entity.ToTable("Court");

			entity.Property(e => e.CourtId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Address).HasMaxLength(255);
			entity.Property(e => e.Contact)
				.HasMaxLength(12)
				.IsUnicode(false);
			entity.Property(e => e.CourtName).HasMaxLength(100);
			entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
			entity.Property(e => e.Kind)
				.HasMaxLength(10)
				.IsUnicode(false);
			entity.Property(e => e.RentPricePerHour).HasColumnType("decimal(10, 0)");
			entity.Property(e => e.Type)
				.HasMaxLength(10)
				.IsUnicode(false);
		});

		modelBuilder.Entity<EmailVerification>(entity =>
		{
			entity.HasKey(e => e.EmailVerificationId).HasName("EmailVerification_pk");

			entity.ToTable("EmailVerification");

			entity.Property(e => e.Code)
				.HasMaxLength(10)
				.IsUnicode(false);
			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.Email)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
			entity.Property(e => e.Purpose)
				.HasMaxLength(100)
				.IsUnicode(false);
		});

		modelBuilder.Entity<Exercise>(entity =>
		{
			entity.HasKey(e => e.ExerciseId).HasName("Exercise_pk");

			entity.ToTable("Exercise");

			entity.Property(e => e.ExerciseId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CoachId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.Duration).HasColumnType("decimal(10, 1)");
			entity.Property(e => e.ExerciseName).HasMaxLength(100);
			entity.Property(e => e.TrainingSessionId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

			entity.HasOne(d => d.Coach).WithMany(p => p.Exercises)
				.HasForeignKey(d => d.CoachId)
				.HasConstraintName("Exercise_Coach");

			entity.HasOne(d => d.TrainingSession).WithMany(p => p.Exercises)
				.HasForeignKey(d => d.TrainingSessionId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Exercise_TrainingSession");
		});

		modelBuilder.Entity<Expenditure>(entity =>
		{
			entity.HasKey(e => e.ExpenditureId).HasName("Expenditure_pk");

			entity.ToTable("Expenditure");

			entity.Property(e => e.ExpenditureId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Amount).HasColumnType("decimal(10, 0)");
			entity.Property(e => e.ByManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Name).HasMaxLength(255);
			entity.Property(e => e.PayoutDate).HasColumnType("datetime");
			entity.Property(e => e.TeamFundId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UsedByUserId)
				.HasMaxLength(1000)
				.IsUnicode(false);

			entity.HasOne(d => d.ByManager).WithMany(p => p.Expenditures)
				.HasForeignKey(d => d.ByManagerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Expenditure_Manager");

			entity.HasOne(d => d.TeamFund).WithMany(p => p.Expenditures)
				.HasForeignKey(d => d.TeamFundId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Expenditure_TeamFund");
		});

		modelBuilder.Entity<ExpenditurePlayer>(entity =>
		{
			entity.HasKey(e => e.ExpenditurePlayerId).HasName("ExpenditurePlayer_pk");

			entity.ToTable("ExpenditurePlayer");

			entity.Property(e => e.ExpenditureId)
				.HasMaxLength(36)
				.IsUnicode(false)
				.HasColumnName("ExpenditureId ");
			entity.Property(e => e.PlayerId)
				.HasMaxLength(36)
				.IsUnicode(false);
		});

		modelBuilder.Entity<FileHash>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("FileHash_pk");

			entity.ToTable("FileHash");

			entity.Property(e => e.Id).HasColumnName("Id ");
			entity.Property(e => e.CreatedDate)
				.HasColumnType("datetime")
				.HasColumnName("CreatedDate ");
			entity.Property(e => e.FilePath).HasColumnName("FilePath ");
			entity.Property(e => e.FileSize).HasColumnName("FileSize ");
			entity.Property(e => e.FileType)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("FileType ");
			entity.Property(e => e.Hash)
				.HasMaxLength(64)
				.HasColumnName("Hash ");
		});

		modelBuilder.Entity<MailTemplate>(entity =>
		{
			entity.HasKey(e => e.MailTemplateId).HasName("MailTemplate_pk");

			entity.ToTable("MailTemplate");

			entity.Property(e => e.MailTemplateId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CreatedDate).HasColumnType("datetime");
			entity.Property(e => e.TemplateTitle).HasMaxLength(255);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
		});

		modelBuilder.Entity<Manager>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("Manager_pk");

			entity.ToTable("Manager");

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.BankAccountNumber)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.BankBinId)
				.HasMaxLength(10)
				.IsUnicode(false);
			entity.Property(e => e.BankName).HasMaxLength(100);
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.Team).WithMany(p => p.Managers)
				.HasForeignKey(d => d.TeamId)
				.HasConstraintName("Manager_Team");

			entity.HasOne(d => d.User).WithOne(p => p.Manager)
				.HasForeignKey<Manager>(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Manager_User");
		});

		modelBuilder.Entity<ManagerRegistration>(entity =>
		{
			entity.HasKey(e => e.ManagerRegistrationId).HasName("ManagerRegistration_pk");

			entity.ToTable("ManagerRegistration");

			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.ExperienceAsAmanager).HasColumnName("ExperienceAsAManager");
			entity.Property(e => e.FacebookProfileUrl).HasColumnName("FacebookProfileURL");
			entity.Property(e => e.FullName).HasMaxLength(100);
			entity.Property(e => e.GenerationAndSchoolName).HasMaxLength(100);
			entity.Property(e => e.KnowledgeAboutAmanager).HasColumnName("KnowledgeAboutAManager");
			entity.Property(e => e.PhoneNumber)
				.HasMaxLength(12)
				.IsUnicode(false);
			entity.Property(e => e.Status)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.SubmitedDate).HasColumnType("datetime");

			entity.HasOne(d => d.MemberRegistrationSession).WithMany(p => p.ManagerRegistrations)
				.HasForeignKey(d => d.MemberRegistrationSessionId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("ManagerRegistration_MemberRegistrationSession");
		});

		modelBuilder.Entity<Match>(entity =>
		{
			entity.HasKey(e => e.MatchId).HasName("Match_pk");

			entity.ToTable("Match");

			entity.Property(e => e.AwayTeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CourtId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CreatedByCoachId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.HomeTeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.MatchDate).HasColumnType("datetime");
			entity.Property(e => e.MatchName).HasMaxLength(255);
			entity.Property(e => e.OpponentTeamName).HasMaxLength(50);

			entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
				.HasForeignKey(d => d.AwayTeamId)
				.HasConstraintName("Match_AwayTeam");

			entity.HasOne(d => d.Court).WithMany(p => p.Matches)
				.HasForeignKey(d => d.CourtId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Match_Court");

			entity.HasOne(d => d.CreatedByCoach).WithMany(p => p.Matches)
				.HasForeignKey(d => d.CreatedByCoachId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Match_Coach");

			entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
				.HasForeignKey(d => d.HomeTeamId)
				.HasConstraintName("Match_HomeTeam");
		});

		modelBuilder.Entity<MatchArticle>(entity =>
		{
			entity.HasKey(e => e.ArticleId).HasName("MatchArticle_pk");

			entity.ToTable("MatchArticle");

			entity.Property(e => e.ArticleType)
				.HasMaxLength(10)
				.IsUnicode(false);
			entity.Property(e => e.Title).HasMaxLength(255);
			entity.Property(e => e.Url).HasColumnName("URL");

			entity.HasOne(d => d.Match).WithMany(p => p.MatchArticles)
				.HasForeignKey(d => d.MatchId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("MatchArticle_Match");
		});

		modelBuilder.Entity<MatchLineup>(entity =>
		{
			entity.HasKey(e => e.LineupId).HasName("MatchLineup_pk");

			entity.ToTable("MatchLineup");

			entity.Property(e => e.PlayerId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.Match).WithMany(p => p.MatchLineups)
				.HasForeignKey(d => d.MatchId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("MatchLineup_Match");

			entity.HasOne(d => d.Player).WithMany(p => p.MatchLineups)
				.HasForeignKey(d => d.PlayerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("MatchLineup_Player");
		});

		modelBuilder.Entity<MemberRegistrationSession>(entity =>
		{
			entity.HasKey(e => e.MemberRegistrationSessionId).HasName("MemberRegistrationSession_pk");

			entity.ToTable("MemberRegistrationSession");

			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.EndDate).HasColumnType("datetime");
			entity.Property(e => e.RegistrationName).HasMaxLength(255);
			entity.Property(e => e.StartDate).HasColumnType("datetime");
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
		});

		modelBuilder.Entity<Parent>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("Parent_pk");

			entity.ToTable("Parent");

			entity.HasIndex(e => e.CitizenId, "Parent_ak_CitizenId").IsUnique();

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CitizenId)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.CreatedByManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.CreatedByManager).WithMany(p => p.Parents)
				.HasForeignKey(d => d.CreatedByManagerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Parent_Manager");

			entity.HasOne(d => d.User).WithOne(p => p.Parent)
				.HasForeignKey<Parent>(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Parent_User");
		});

		modelBuilder.Entity<Payment>(entity =>
		{
			entity.HasKey(e => e.PaymentId).HasName("Payment_pk");

			entity.ToTable("Payment");

			entity.Property(e => e.PaymentId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.DueDate).HasColumnType("datetime");
			entity.Property(e => e.Note).HasMaxLength(255);
			entity.Property(e => e.PaidDate).HasColumnType("datetime");
			entity.Property(e => e.TeamFundId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.TeamFund).WithMany(p => p.Payments)
				.HasForeignKey(d => d.TeamFundId)
				.HasConstraintName("Payment_TeamFund");

			entity.HasOne(d => d.User).WithMany(p => p.Payments)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Payment_Player");
		});

		modelBuilder.Entity<PaymentItem>(entity =>
		{
			entity.HasKey(e => e.PaymentItemId).HasName("PaymentItem_pk");

			entity.ToTable("PaymentItem");

			entity.Property(e => e.Amount).HasColumnType("decimal(10, 0)");
			entity.Property(e => e.Note).HasMaxLength(255);
			entity.Property(e => e.PaidItemName).HasMaxLength(255);
			entity.Property(e => e.PaymentId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.Payment).WithMany(p => p.PaymentItems)
				.HasForeignKey(d => d.PaymentId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("PaymentDetail_Payment");
		});

		modelBuilder.Entity<Player>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("Player_pk");

			entity.ToTable("Player");

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Height).HasColumnType("decimal(10, 2)");
			entity.Property(e => e.ParentId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Position).HasMaxLength(10);
			entity.Property(e => e.RelationshipWithParent).HasMaxLength(50);
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

			entity.HasOne(d => d.Parent).WithMany(p => p.Players)
				.HasForeignKey(d => d.ParentId)
				.HasConstraintName("Player_Parent");

			entity.HasOne(d => d.Team).WithMany(p => p.Players)
				.HasForeignKey(d => d.TeamId)
				.HasConstraintName("Player_Team");

			entity.HasOne(d => d.User).WithOne(p => p.Player)
				.HasForeignKey<Player>(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Player_User");
		});

		modelBuilder.Entity<PlayerRegistration>(entity =>
		{
			entity.HasKey(e => e.PlayerRegistrationId).HasName("PlayerRegistration_pk");

			entity.ToTable("PlayerRegistration");

			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.FacebookProfileUrl).HasColumnName("FacebookProfileURL");
			entity.Property(e => e.FormReviewedBy)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.FullName).HasMaxLength(100);
			entity.Property(e => e.GenerationAndSchoolName).HasMaxLength(100);
			entity.Property(e => e.Height).HasColumnType("decimal(10, 2)");
			entity.Property(e => e.ParentCitizenId)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.ParentEmail).HasMaxLength(100);
			entity.Property(e => e.ParentName).HasMaxLength(100);
			entity.Property(e => e.ParentPhoneNumber)
				.HasMaxLength(12)
				.IsUnicode(false);
			entity.Property(e => e.PhoneNumber)
				.HasMaxLength(12)
				.IsUnicode(false);
			entity.Property(e => e.Position).HasMaxLength(255);
			entity.Property(e => e.RelationshipWithParent).HasMaxLength(50);
			entity.Property(e => e.ReviewedDate).HasColumnType("datetime");
			entity.Property(e => e.Status)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.SubmitedDate).HasColumnType("datetime");
			entity.Property(e => e.TryOutDate).HasColumnType("datetime");
			entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

			entity.HasOne(d => d.FormReviewedByNavigation).WithMany(p => p.PlayerRegistrations)
				.HasForeignKey(d => d.FormReviewedBy)
				.HasConstraintName("PlayerRegistration_User");

			entity.HasOne(d => d.MemberRegistrationSession).WithMany(p => p.PlayerRegistrations)
				.HasForeignKey(d => d.MemberRegistrationSessionId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("PlayerRegistration_MemberRegistrationSession");
		});

		modelBuilder.Entity<President>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("President_pk");

			entity.ToTable("President");

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.User).WithOne(p => p.President)
				.HasForeignKey<President>(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("President_User");
		});

		modelBuilder.Entity<Team>(entity =>
		{
			entity.HasKey(e => e.TeamId).HasName("Team_pk");

			entity.ToTable("Team");

			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CreateAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnType("datetime");
			entity.Property(e => e.FundManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.TeamName).HasMaxLength(50);

			entity.HasOne(d => d.FundManager).WithMany(p => p.Teams)
				.HasForeignKey(d => d.FundManagerId)
				.HasConstraintName("FK_Team_FundManager");
		});

		modelBuilder.Entity<TeamFund>(entity =>
		{
			entity.HasKey(e => e.TeamFundId).HasName("TeamFund_pk");

			entity.ToTable("TeamFund");

			entity.Property(e => e.TeamFundId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.ApprovedAt).HasColumnType("datetime");
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.Team).WithMany(p => p.TeamFunds)
				.HasForeignKey(d => d.TeamId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TeamFund_Team");
		});

		modelBuilder.Entity<TrainingSession>(entity =>
		{
			entity.HasKey(e => e.TrainingSessionId).HasName("TrainingSession_pk");

			entity.ToTable("TrainingSession");

			entity.Property(e => e.TrainingSessionId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CourtId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CourtPrice).HasColumnType("decimal(10, 0)");
			entity.Property(e => e.CreateRejectedReason).HasMaxLength(255);
			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.CreatedByUserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.CreatedDecisionAt).HasColumnType("datetime");
			entity.Property(e => e.CreatedDecisionByManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.EndTime).HasPrecision(0);
			entity.Property(e => e.StartTime).HasPrecision(0);
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

			entity.HasOne(d => d.Court).WithMany(p => p.TrainingSessions)
				.HasForeignKey(d => d.CourtId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Schedule_Court");

			entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TrainingSessions)
				.HasForeignKey(d => d.CreatedByUserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TrainingSession_Coach");

			entity.HasOne(d => d.CreatedDecisionByManager).WithMany(p => p.TrainingSessions)
				.HasForeignKey(d => d.CreatedDecisionByManagerId)
				.HasConstraintName("TrainingSession_Manager");

			entity.HasOne(d => d.Team).WithMany(p => p.TrainingSessions)
				.HasForeignKey(d => d.TeamId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TraningSession_Team");
		});

		modelBuilder.Entity<TrainingSessionStatusChangeRequest>(entity =>
		{
			entity.HasKey(e => e.StatusChangeRequestId).HasName("TrainingSessionStatusChangeRequest_pk");

			entity.ToTable("TrainingSessionStatusChangeRequest");

			entity.Property(e => e.DecisionAt).HasColumnType("datetime");
			entity.Property(e => e.DecisionByManagerId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.NewCourtId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.NewEndTime).HasPrecision(0);
			entity.Property(e => e.NewStartTime).HasPrecision(0);
			entity.Property(e => e.RejectedReason).HasMaxLength(255);
			entity.Property(e => e.RequestReason).HasMaxLength(255);
			entity.Property(e => e.RequestedAt).HasColumnType("datetime");
			entity.Property(e => e.RequestedByCoachId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.TrainingSessionId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.DecisionByManager).WithMany(p => p.TrainingSessionStatusChangeRequests)
				.HasForeignKey(d => d.DecisionByManagerId)
				.HasConstraintName("TrainingSessionUpdateRequest_Manager");

			entity.HasOne(d => d.NewCourt).WithMany(p => p.TrainingSessionStatusChangeRequests)
				.HasForeignKey(d => d.NewCourtId)
				.HasConstraintName("TrainingSessionUpdateRequest_Court");

			entity.HasOne(d => d.RequestedByCoach).WithMany(p => p.TrainingSessionStatusChangeRequests)
				.HasForeignKey(d => d.RequestedByCoachId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TrainingSessionUpdateRequest_Coach");

			entity.HasOne(d => d.TrainingSession).WithMany(p => p.TrainingSessionStatusChangeRequests)
				.HasForeignKey(d => d.TrainingSessionId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TrainingSessionUpdateRequest_TrainingSession");
		});

		modelBuilder.Entity<TryOutMeasurementScale>(entity =>
		{
			entity.HasKey(e => e.MeasurementScaleCode).HasName("TryOutMeasurementScale_pk");

			entity.ToTable("TryOutMeasurementScale");

			entity.Property(e => e.MeasurementScaleCode)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.Location).HasMaxLength(100);
			entity.Property(e => e.MeasurementName).HasMaxLength(255);
			entity.Property(e => e.ParentMeasurementScaleCode)
				.HasMaxLength(50)
				.IsUnicode(false);

			entity.HasOne(d => d.ParentMeasurementScaleCodeNavigation).WithMany(p => p.InverseParentMeasurementScaleCodeNavigation)
				.HasForeignKey(d => d.ParentMeasurementScaleCode)
				.HasConstraintName("TryOutMeasurementScale_TryOutMeasurementScale");
		});

		modelBuilder.Entity<TryOutScoreCriterion>(entity =>
		{
			entity.HasKey(e => e.ScoreCriteriaId).HasName("TryOutScoreCriteria_pk");

			entity.Property(e => e.CriteriaName).HasMaxLength(255);
			entity.Property(e => e.MeasurementScaleCode)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.Unit).HasMaxLength(50);

			entity.HasOne(d => d.MeasurementScaleCodeNavigation).WithMany(p => p.TryOutScoreCriteria)
				.HasForeignKey(d => d.MeasurementScaleCode)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TryOutScoreCriteria_TryOutMeasurementScale");
		});

		modelBuilder.Entity<TryOutScoreLevel>(entity =>
		{
			entity.HasKey(e => e.ScoreLevelId).HasName("TryOutScoreLevel_pk");

			entity.ToTable("TryOutScoreLevel");

			entity.Property(e => e.FivePointScaleScore).HasColumnType("decimal(10, 1)");
			entity.Property(e => e.MaxValue).HasColumnType("decimal(10, 2)");
			entity.Property(e => e.MinValue).HasColumnType("decimal(10, 2)");
			entity.Property(e => e.ScoreLevel).HasMaxLength(50);

			entity.HasOne(d => d.ScoreCriteria).WithMany(p => p.TryOutScoreLevels)
				.HasForeignKey(d => d.ScoreCriteriaId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TryOutScoreLevel_TryOutScoreCriteria");
		});

		modelBuilder.Entity<TryOutScorecard>(entity =>
		{
			entity.HasKey(e => e.TryOutScorecardId).HasName("TryOutScorecard_pk");

			entity.ToTable("TryOutScorecard");

			entity.Property(e => e.MeasurementScaleCode)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.Note).HasMaxLength(500);
			entity.Property(e => e.Score).HasMaxLength(255);
			entity.Property(e => e.ScoredAt).HasColumnType("datetime");
			entity.Property(e => e.ScoredBy)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

			entity.HasOne(d => d.MeasurementScaleCodeNavigation).WithMany(p => p.TryOutScorecards)
				.HasForeignKey(d => d.MeasurementScaleCode)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TryOutScorecard_TryOutMeasurementScale");

			entity.HasOne(d => d.PlayerRegistration).WithMany(p => p.TryOutScorecards)
				.HasForeignKey(d => d.PlayerRegistrationId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TryOutScorecard_PlayerRegistration");

			entity.HasOne(d => d.ScoredByNavigation).WithMany(p => p.TryOutScorecards)
				.HasForeignKey(d => d.ScoredBy)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("TryOutScorecard_User");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("User_pk");

			entity.ToTable("User");

			entity.HasIndex(e => e.Email, "User_ak_Email").IsUnique();

			entity.HasIndex(e => e.Username, "User_ak_Username").IsUnique();

			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.Address).HasMaxLength(100);
			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.Fullname).HasMaxLength(100);
			entity.Property(e => e.Phone)
				.HasMaxLength(12)
				.IsUnicode(false);
			entity.Property(e => e.RoleCode)
				.HasMaxLength(20)
				.IsUnicode(false);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
			entity.Property(e => e.Username)
				.HasMaxLength(50)
				.IsUnicode(false);
		});

		modelBuilder.Entity<UserFace>(entity =>
		{
			entity.HasKey(e => e.UserFaceId).HasName("UserFace_pk");

			entity.ToTable("UserFace");

			entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
			entity.Property(e => e.RegisteredFaceId)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.UserFaces)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("UserFace_User");
		});

		modelBuilder.Entity<UserForgotPasswordToken>(entity =>
		{
			entity.HasKey(e => e.ForgotPasswordTokenId).HasName("UserForgotPasswordToken_pk");

			entity.ToTable("UserForgotPasswordToken");

			entity.Property(e => e.CreatedAt).HasColumnType("datetime");
			entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
			entity.Property(e => e.ForgotPasswordToken)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.RevokedAt).HasColumnType("datetime");
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.UserForgotPasswordTokens)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("UserForgotPasswordToken_User");
		});

		modelBuilder.Entity<UserRefreshToken>(entity =>
		{
			entity.HasKey(e => e.UserRefreshTokenId).HasName("UserRefreshToken_pk");

			entity.ToTable("UserRefreshToken");

			entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
			entity.Property(e => e.RefreshToken)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.UserRefreshTokens)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("UserRefreshToken_User");
		});

		modelBuilder.Entity<UserTeamHistory>(entity =>
		{
			entity.HasKey(e => e.UserTeamHistoryId).HasName("UserTeamHistory_pk");

			entity.ToTable("UserTeamHistory");

			entity.Property(e => e.JoinDate).HasColumnType("datetime");
			entity.Property(e => e.LeftDate).HasColumnType("datetime");
			entity.Property(e => e.Note).HasMaxLength(255);
			entity.Property(e => e.RemovedByUserId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.TeamId)
				.HasMaxLength(36)
				.IsUnicode(false);
			entity.Property(e => e.UserId)
				.HasMaxLength(36)
				.IsUnicode(false);

			entity.HasOne(d => d.RemovedByUser).WithMany(p => p.UserTeamHistoryRemovedByUsers)
				.HasForeignKey(d => d.RemovedByUserId)
				.HasConstraintName("UserTeamHistory_User_RemovedBy");

			entity.HasOne(d => d.Team).WithMany(p => p.UserTeamHistories)
				.HasForeignKey(d => d.TeamId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("PlayerTeamHistory_Team");

			entity.HasOne(d => d.User).WithMany(p => p.UserTeamHistoryUsers)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("UserTeamHistory_User_UserId");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
