USE [BAMS_DB]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 5/8/2025 11:59:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[AttendanceId] [varchar](36) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[ManagerId] [varchar](36) NULL,
	[TrainingSessionId] [varchar](36) NOT NULL,
	[Status] [int] NOT NULL,
	[Note] [nvarchar](100) NULL,
 CONSTRAINT [Attendance_pk] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CachedQuestion]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CachedQuestion](
	[CachedQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](255) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
	[IsForGuest] [bit] NOT NULL,
 CONSTRAINT [CachedQuestion_pk] PRIMARY KEY CLUSTERED 
(
	[CachedQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClubContact]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClubContact](
	[ContactMethodId] [int] IDENTITY(1,1) NOT NULL,
	[ContactMethodName] [nvarchar](50) NOT NULL,
	[MethodValue] [nvarchar](100) NOT NULL,
	[IconURL] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [ClubContact_pk] PRIMARY KEY CLUSTERED 
(
	[ContactMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coach]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach](
	[UserId] [varchar](36) NOT NULL,
	[TeamId] [varchar](36) NULL,
	[CreatedByPresidentId] [varchar](36) NOT NULL,
	[Bio] [nvarchar](255) NULL,
	[ContractStartDate] [date] NOT NULL,
	[ContractEndDate] [date] NOT NULL,
 CONSTRAINT [Coach_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Court]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Court](
	[CourtId] [varchar](36) NOT NULL,
	[CourtName] [nvarchar](100) NOT NULL,
	[RentPricePerHour] [decimal](10, 0) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Contact] [varchar](12) NULL,
	[Status] [int] NOT NULL,
	[Type] [varchar](10) NULL,
	[Kind] [varchar](10) NULL,
	[ImageURL] [nvarchar](max) NULL,
	[UsagePurpose] [int] NOT NULL,
 CONSTRAINT [Court_pk] PRIMARY KEY CLUSTERED 
(
	[CourtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailVerification]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailVerification](
	[EmailVerificationId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Purpose] [varchar](100) NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [EmailVerification_pk] PRIMARY KEY CLUSTERED 
(
	[EmailVerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exercise]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exercise](
	[ExerciseId] [varchar](36) NOT NULL,
	[TrainingSessionId] [varchar](36) NOT NULL,
	[CoachId] [varchar](36) NULL,
	[ExerciseName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Duration] [decimal](10, 1) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [Exercise_pk] PRIMARY KEY CLUSTERED 
(
	[ExerciseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenditure]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenditure](
	[ExpenditureId] [varchar](36) NOT NULL,
	[TeamFundId] [varchar](36) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Amount] [decimal](10, 0) NOT NULL,
	[Date] [date] NOT NULL,
	[ByManagerId] [varchar](36) NOT NULL,
	[PayoutDate] [datetime] NULL,
	[UsedByUserId] [varchar](1000) NULL,
 CONSTRAINT [Expenditure_pk] PRIMARY KEY CLUSTERED 
(
	[ExpenditureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenditurePlayer]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenditurePlayer](
	[PlayerId] [varchar](36) NULL,
	[ExpenditureId ] [varchar](36) NULL,
	[ExpenditurePlayerId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [ExpenditurePlayer_pk] PRIMARY KEY CLUSTERED 
(
	[ExpenditurePlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileHash]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileHash](
	[Id ] [int] IDENTITY(1,1) NOT NULL,
	[Hash ] [nvarchar](64) NOT NULL,
	[FilePath ] [nvarchar](max) NOT NULL,
	[CreatedDate ] [datetime] NULL,
	[FileType ] [varchar](50) NULL,
	[FileSize ] [bigint] NULL,
 CONSTRAINT [FileHash_pk] PRIMARY KEY CLUSTERED 
(
	[Id ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailTemplate]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailTemplate](
	[MailTemplateId] [varchar](36) NOT NULL,
	[TemplateTitle] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [MailTemplate_pk] PRIMARY KEY CLUSTERED 
(
	[MailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[UserId] [varchar](36) NOT NULL,
	[TeamId] [varchar](36) NULL,
	[BankName] [nvarchar](100) NULL,
	[BankAccountNumber] [varchar](100) NULL,
	[PaymentMethod] [int] NULL,
	[BankBinId] [varchar](10) NULL,
 CONSTRAINT [Manager_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManagerRegistration]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagerRegistration](
	[ManagerRegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[MemberRegistrationSessionId] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[GenerationAndSchoolName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [varchar](12) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FacebookProfileURL] [nvarchar](max) NOT NULL,
	[KnowledgeAboutAcademy] [nvarchar](max) NOT NULL,
	[ReasonToChooseUs] [nvarchar](max) NOT NULL,
	[KnowledgeAboutAManager] [nvarchar](max) NOT NULL,
	[ExperienceAsAManager] [nvarchar](max) NOT NULL,
	[Strength] [nvarchar](max) NOT NULL,
	[WeaknessAndItSolution] [nvarchar](max) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[SubmitedDate] [datetime] NOT NULL,
 CONSTRAINT [ManagerRegistration_pk] PRIMARY KEY CLUSTERED 
(
	[ManagerRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Match]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Match](
	[MatchId] [int] IDENTITY(1,1) NOT NULL,
	[MatchName] [nvarchar](255) NOT NULL,
	[MatchDate] [datetime] NOT NULL,
	[HomeTeamId] [varchar](36) NULL,
	[AwayTeamId] [varchar](36) NULL,
	[OpponentTeamName] [nvarchar](50) NULL,
	[CourtId] [varchar](36) NOT NULL,
	[ScoreHome] [int] NOT NULL,
	[ScoreAway] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedByCoachId] [varchar](36) NOT NULL,
 CONSTRAINT [Match_pk] PRIMARY KEY CLUSTERED 
(
	[MatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatchArticle]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchArticle](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[MatchId] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[ArticleType] [varchar](10) NOT NULL,
 CONSTRAINT [MatchArticle_pk] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatchLineup]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchLineup](
	[LineupId] [int] IDENTITY(1,1) NOT NULL,
	[MatchId] [int] NOT NULL,
	[IsStarting] [bit] NOT NULL,
	[PlayerId] [varchar](36) NOT NULL,
 CONSTRAINT [MatchLineup_pk] PRIMARY KEY CLUSTERED 
(
	[LineupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberRegistrationSession]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRegistrationSession](
	[MemberRegistrationSessionId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationName] [nvarchar](255) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsAllowPlayerRecruit] [bit] NOT NULL,
	[IsAllowManagerRecruit] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[IsEnable] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [MemberRegistrationSession_pk] PRIMARY KEY CLUSTERED 
(
	[MemberRegistrationSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parent]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parent](
	[UserId] [varchar](36) NOT NULL,
	[CitizenId] [varchar](20) NULL,
	[CreatedByManagerId] [varchar](36) NOT NULL,
 CONSTRAINT [Parent_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [varchar](36) NOT NULL,
	[TeamFundId] [varchar](36) NULL,
	[UserId] [varchar](36) NOT NULL,
	[Status] [int] NOT NULL,
	[PaidDate] [datetime] NULL,
	[Note] [nvarchar](255) NOT NULL,
	[DueDate] [datetime] NULL,
	[PaymentMethod] [int] NULL,
 CONSTRAINT [Payment_pk] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentItem]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentItem](
	[PaymentItemId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentId] [varchar](36) NOT NULL,
	[PaidItemName] [nvarchar](255) NOT NULL,
	[Amount] [decimal](10, 0) NOT NULL,
	[Note] [nvarchar](255) NOT NULL,
 CONSTRAINT [PaymentItem_pk] PRIMARY KEY CLUSTERED 
(
	[PaymentItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[UserId] [varchar](36) NOT NULL,
	[ParentId] [varchar](36) NULL,
	[TeamId] [varchar](36) NULL,
	[RelationshipWithParent] [nvarchar](50) NULL,
	[Weight] [decimal](10, 2) NULL,
	[Height] [decimal](10, 2) NULL,
	[Position] [nvarchar](10) NULL,
	[ShirtNumber] [int] NULL,
	[ClubJoinDate] [date] NOT NULL,
 CONSTRAINT [Player_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerRegistration]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerRegistration](
	[PlayerRegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[MemberRegistrationSessionId] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[GenerationAndSchoolName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [varchar](12) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Gender] [bit] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Height] [decimal](10, 2) NOT NULL,
	[Weight] [decimal](10, 2) NOT NULL,
	[FacebookProfileURL] [nvarchar](max) NOT NULL,
	[KnowledgeAboutAcademy] [nvarchar](max) NOT NULL,
	[ReasonToChooseUs] [nvarchar](max) NOT NULL,
	[Position] [nvarchar](255) NOT NULL,
	[Experience] [nvarchar](max) NOT NULL,
	[Achievement] [nvarchar](max) NOT NULL,
	[ParentName] [nvarchar](100) NULL,
	[ParentPhoneNumber] [varchar](12) NULL,
	[ParentEmail] [nvarchar](100) NULL,
	[RelationshipWithParent] [nvarchar](50) NULL,
	[ParentCitizenId] [varchar](20) NULL,
	[CandidateNumber] [int] NULL,
	[TryOutNote] [nvarchar](max) NULL,
	[FormReviewedBy] [varchar](36) NULL,
	[ReviewedDate] [datetime] NULL,
	[Status] [varchar](50) NOT NULL,
	[SubmitedDate] [datetime] NOT NULL,
	[TryOutDate] [datetime] NULL,
	[TryOutLocation] [nvarchar](max) NULL,
 CONSTRAINT [PlayerRegistration_pk] PRIMARY KEY CLUSTERED 
(
	[PlayerRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[President]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[President](
	[UserId] [varchar](36) NOT NULL,
	[Generation] [int] NULL,
 CONSTRAINT [President_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[TeamId] [varchar](36) NOT NULL,
	[TeamName] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[FundManagerId] [varchar](36) NULL,
 CONSTRAINT [Team_pk] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamFund]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamFund](
	[TeamFundId] [varchar](36) NOT NULL,
	[TeamId] [varchar](36) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ApprovedAt] [datetime] NULL,
 CONSTRAINT [TeamFund_pk] PRIMARY KEY CLUSTERED 
(
	[TeamFundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingSession]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingSession](
	[TrainingSessionId] [varchar](36) NOT NULL,
	[TeamId] [varchar](36) NOT NULL,
	[CourtId] [varchar](36) NOT NULL,
	[Status] [int] NOT NULL,
	[ScheduledDate] [date] NOT NULL,
	[StartTime] [time](0) NOT NULL,
	[EndTime] [time](0) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[CreatedByUserId] [varchar](36) NOT NULL,
	[CreatedDecisionByManagerId] [varchar](36) NULL,
	[CreatedDecisionAt] [datetime] NULL,
	[CreateRejectedReason] [nvarchar](255) NULL,
	[CourtPrice] [decimal](10, 0) NULL,
 CONSTRAINT [TrainingSession_pk] PRIMARY KEY CLUSTERED 
(
	[TrainingSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingSessionStatusChangeRequest]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingSessionStatusChangeRequest](
	[StatusChangeRequestId] [int] IDENTITY(1,1) NOT NULL,
	[TrainingSessionId] [varchar](36) NOT NULL,
	[RequestedByCoachId] [varchar](36) NOT NULL,
	[RequestType] [int] NOT NULL,
	[RequestReason] [nvarchar](255) NOT NULL,
	[NewCourtId] [varchar](36) NULL,
	[NewScheduledDate] [date] NULL,
	[NewStartTime] [time](0) NULL,
	[NewEndTime] [time](0) NULL,
	[RequestedAt] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[RejectedReason] [nvarchar](255) NULL,
	[DecisionByManagerId] [varchar](36) NULL,
	[DecisionAt] [datetime] NULL,
 CONSTRAINT [TrainingSessionStatusChangeRequest_pk] PRIMARY KEY CLUSTERED 
(
	[StatusChangeRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TryOutMeasurementScale]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TryOutMeasurementScale](
	[MeasurementScaleCode] [varchar](50) NOT NULL,
	[MeasurementName] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Duration] [nvarchar](max) NULL,
	[Location] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Equipment] [nvarchar](max) NULL,
	[MeasurementScale] [nvarchar](max) NULL,
	[ParentMeasurementScaleCode] [varchar](50) NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [TryOutMeasurementScale_pk] PRIMARY KEY CLUSTERED 
(
	[MeasurementScaleCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TryOutScorecard]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TryOutScorecard](
	[TryOutScorecardId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerRegistrationId] [int] NOT NULL,
	[MeasurementScaleCode] [varchar](50) NOT NULL,
	[Score] [nvarchar](255) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[ScoredBy] [varchar](36) NOT NULL,
	[ScoredAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [TryOutScorecard_pk] PRIMARY KEY CLUSTERED 
(
	[TryOutScorecardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TryOutScoreCriteria]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TryOutScoreCriteria](
	[ScoreCriteriaId] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementScaleCode] [varchar](50) NOT NULL,
	[CriteriaName] [nvarchar](255) NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
 CONSTRAINT [TryOutScoreCriteria_pk] PRIMARY KEY CLUSTERED 
(
	[ScoreCriteriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TryOutScoreLevel]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TryOutScoreLevel](
	[ScoreLevelId] [int] IDENTITY(1,1) NOT NULL,
	[ScoreCriteriaId] [int] NOT NULL,
	[MinValue] [decimal](10, 2) NULL,
	[MaxValue] [decimal](10, 2) NULL,
	[ScoreLevel] [nvarchar](50) NOT NULL,
	[FivePointScaleScore] [decimal](10, 1) NOT NULL,
 CONSTRAINT [TryOutScoreLevel_pk] PRIMARY KEY CLUSTERED 
(
	[ScoreLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [varchar](36) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[Phone] [varchar](12) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NULL,
	[RoleCode] [varchar](20) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[IsEnable] [bit] NOT NULL,
	[Gender] [bit] NULL,
 CONSTRAINT [User_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFace]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFace](
	[UserFaceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[RegisteredFaceId] [varchar](50) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[RegisteredAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [UserFace_pk] PRIMARY KEY CLUSTERED 
(
	[UserFaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserForgotPasswordToken]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserForgotPasswordToken](
	[ForgotPasswordTokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[ForgotPasswordToken] [varchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[RevokedAt] [datetime] NULL,
 CONSTRAINT [UserForgotPasswordToken_pk] PRIMARY KEY CLUSTERED 
(
	[ForgotPasswordTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRefreshToken]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRefreshToken](
	[UserRefreshTokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[RefreshToken] [varchar](100) NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
 CONSTRAINT [UserRefreshToken_pk] PRIMARY KEY CLUSTERED 
(
	[UserRefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTeamHistory]    Script Date: 5/8/2025 11:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTeamHistory](
	[UserId] [varchar](36) NOT NULL,
	[UserTeamHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [varchar](36) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[LeftDate] [datetime] NULL,
	[Note] [nvarchar](255) NULL,
	[RemovedByUserId] [varchar](36) NULL,
 CONSTRAINT [UserTeamHistory_pk] PRIMARY KEY CLUSTERED 
(
	[UserTeamHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'108eb9b2-1664-4113-958d-ee372d2ee4be', N'5f989406-657b-4920-b27e-91d465f648d3', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'8d6a425a-7626-40c6-acaf-7992cd45e67e', 0, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'1a8e2429-c9b8-4c4f-863c-994917d3b26a', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'8d6a425a-7626-40c6-acaf-7992cd45e67e', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'294f407a-66dd-437a-9410-a9b84102802b', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'49482a2e-7b9f-422d-8fc7-effbe6894553', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'56d64e39-c0e4-48bc-a1eb-ebe37f4d400a', N'10aaa2ec-ed9b-4c41-99b6-1341ba482431', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'6bd1650a-c621-4bd7-b6c4-9f73e45be643', N'1f0e5627-988a-4313-8714-58081655488a', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'8d6a425a-7626-40c6-acaf-7992cd45e67e', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'6d23849c-1767-4992-a399-6112b6c672de', N'1f0e5627-988a-4313-8714-58081655488a', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'49482a2e-7b9f-422d-8fc7-effbe6894553', 1, N'yêu')
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'70449e48-b684-4be1-bb70-7163a5b7145d', N'5f989406-657b-4920-b27e-91d465f648d3', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'49482a2e-7b9f-422d-8fc7-effbe6894553', 0, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'9ed9afbe-084a-4257-96c2-4d8aae7c621a', N'5f989406-657b-4920-b27e-91d465f648d3', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'b67b980b-845d-472f-b0d1-f9c0c978863d', N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'd0b0c4c8-9117-4caa-8007-f5f7d1f97bc4', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', 1, NULL)
INSERT [dbo].[Attendance] ([AttendanceId], [UserId], [ManagerId], [TrainingSessionId], [Status], [Note]) VALUES (N'f71e009a-77d0-467d-aefb-017ac5219131', N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[CachedQuestion] ON 

INSERT [dbo].[CachedQuestion] ([CachedQuestionId], [Question], [Answer], [IsForGuest]) VALUES (13, N'cho tôi biết thông tin về định lý pytago', N'Xin lỗi, tôi không tìm thấy thông tin về định lý Pytago trong nội dung cho trước.', 1)
INSERT [dbo].[CachedQuestion] ([CachedQuestionId], [Question], [Answer], [IsForGuest]) VALUES (14, N'chào bạn', N'Chào bạn, hân hạnh được phục vụ.', 1)
INSERT [dbo].[CachedQuestion] ([CachedQuestionId], [Question], [Answer], [IsForGuest]) VALUES (15, N'Trường Sa Hoàng Sa là của ai', N'Xin lỗi, tôi không tìm thấy thông tin nào trong nội dung cho biết về Trường Sa Hoàng Sa thuộc về ai. Nội dung chỉ nói về lịch sử hình thành và thành tích của CLB Bóng rổ Yên Hòa.', 1)
INSERT [dbo].[CachedQuestion] ([CachedQuestionId], [Question], [Answer], [IsForGuest]) VALUES (16, N'clb yên hoà là ai', N'CLB Bóng rổ Yên Hòa.', 1)
INSERT [dbo].[CachedQuestion] ([CachedQuestionId], [Question], [Answer], [IsForGuest]) VALUES (17, N'clb yên hoà thành lập năm bao nhiêu', N'CLB Bóng rổ Yên Hòa thành lập vào tháng 9 năm 2015.', 1)
SET IDENTITY_INSERT [dbo].[CachedQuestion] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubContact] ON 

INSERT [dbo].[ClubContact] ([ContactMethodId], [ContactMethodName], [MethodValue], [IconURL], [CreatedAt], [UpdatedAt]) VALUES (1, N'Số điện thoại', N'083435555', N'https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.iconfinder.com%2Ficons%2F1807538%2Fphone_icon&psig=AOvVaw2rg9xMkCdqB7jddbPJqh3r&ust=1740586205818000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCNjap4eb34sDFQAAAAAdAAAAABAT', CAST(N'2025-02-25T00:00:00.000' AS DateTime), CAST(N'2025-02-25T00:00:00.000' AS DateTime))
INSERT [dbo].[ClubContact] ([ContactMethodId], [ContactMethodName], [MethodValue], [IconURL], [CreatedAt], [UpdatedAt]) VALUES (2, N'Email', N'mail@mail.com', N'https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.cleanpng.com%2Fpng-iphone-computer-icons-email-email-964430%2F&psig=AOvVaw1I4KEiUV3SvwP_Pdk52Qxm&ust=1740586261705000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCLid5p-b34sDFQAAAAAdAAAAABAT', CAST(N'2025-02-25T00:00:00.000' AS DateTime), CAST(N'2025-04-03T15:31:40.237' AS DateTime))
INSERT [dbo].[ClubContact] ([ContactMethodId], [ContactMethodName], [MethodValue], [IconURL], [CreatedAt], [UpdatedAt]) VALUES (3, N'Phone', N'123-456-7890', N'https://example.com/icon1.png', CAST(N'2025-03-01T22:25:37.447' AS DateTime), CAST(N'2025-03-01T22:25:37.447' AS DateTime))
INSERT [dbo].[ClubContact] ([ContactMethodId], [ContactMethodName], [MethodValue], [IconURL], [CreatedAt], [UpdatedAt]) VALUES (4, N'Email', N'contact@club.com', N'https://example.com/icon2.png', CAST(N'2025-03-01T22:25:37.447' AS DateTime), CAST(N'2025-03-01T22:25:37.447' AS DateTime))
SET IDENTITY_INSERT [dbo].[ClubContact] OFF
GO
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'18c33107-e68f-46cd-b540-c123ed3069b3', NULL, N'0', NULL, CAST(N'2025-05-19' AS Date), CAST(N'2025-05-19' AS Date))
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'1cdc0035-9d5b-4230-961d-24a88d17daf2', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'0', NULL, CAST(N'2025-05-05' AS Date), CAST(N'2025-05-05' AS Date))
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'0', NULL, CAST(N'2025-05-04' AS Date), CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'5f989406-657b-4920-b27e-91d465f648d3', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'0', NULL, CAST(N'0202-11-11' AS Date), CAST(N'2025-11-12' AS Date))
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'c0959f14-9298-447c-80c7-289d864ddf33', NULL, N'0', NULL, CAST(N'2025-05-19' AS Date), CAST(N'2025-05-19' AS Date))
INSERT [dbo].[Coach] ([UserId], [TeamId], [CreatedByPresidentId], [Bio], [ContractStartDate], [ContractEndDate]) VALUES (N'ef1ae1ac-096f-4f6b-8b67-bb6128fdffdc', NULL, N'0', NULL, CAST(N'2025-05-07' AS Date), CAST(N'2025-05-07' AS Date))
GO
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'1de0b70e-41c4-4899-a017-fdb12dbdc274', N'ĐH Thương Mại', CAST(250000 AS Decimal(10, 0)), N'số 2 ngõ 206 Thịnh Quang (ngõ 67 Thái Thịnh), Đống Đa, tp. Hà Nội.', N'0965638824', 1, N'Outdoor', N'5x5', N'uploads/court/images/fb11f902-b182-4d6b-88e3-948489a1c129.jpg', 1)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'2252a202-4a49-4974-801c-fc00c9895698', N'Ngôi Sao', CAST(300000 AS Decimal(10, 0)), N'Lô T1 khu Đô thị Trung Hòa Nhân Chính, Thanh Xuân', N'0967611984', 1, N'Outdoor', N'5x5', N'uploads/court/images/1dfc9f49-1e41-4ff4-9e90-f056beee768d.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'27644042-489c-43a4-9500-85ed95bc811a', N'Lợi Bôn', CAST(150000 AS Decimal(10, 0)), N'Nhà văn hóa phường Mỹ Đình 1, cuối ngõ 180 Đình Thôn, Nam Từ Liêm, Hà Nội', N'0982 288 312', 1, N'Outdoor', N'3x3', N'uploads/court/images/abf93ad7-cb82-4f51-92ab-3f1ecc434d9f.png', 2)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'781131ed-2b33-4be3-8252-d3fad87b8df4', N'Hồ Đền Lừ', CAST(200000 AS Decimal(10, 0)), N'Đối diện số 32 Hồ Đền Lừ', N'0932419669', 1, N'Outdoor', N'5x5', N'uploads/court/images/a554e290-67e9-45fb-8925-9f1f5e37369c.jpg', 2)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'7d6ef955-d2a6-4090-8fed-59555a934a59', N'Zone Six7', CAST(250000 AS Decimal(10, 0)), N'số 2 ngõ 206 Thịnh Quang (ngõ 67 Thái Thịnh), Đống Đa, tp. Hà Nội.', N'0971936372', 1, N'Outdoor', N'5x5', N'uploads/court/images/eea01adf-29e7-455a-8f3b-d3471544bb4f.jpg', 1)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'88ec0f27-f00f-4061-a87c-c18bf2d98638', N'An Dương', CAST(300000 AS Decimal(10, 0)), N'70 An Dương, quận Tây Hồ', N'0932233413', 1, N'Outdoor', N'5x5', N'uploads/court/images/5c19167c-10b2-4407-9894-37a17c3d058b.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'991f75c7-60ff-4364-92bb-d87f7062fe6a', N'THPT Yên Hòa - sân 1', CAST(0 AS Decimal(10, 0)), N'251 Đ. Nguyễn Khang, Yên Hoà, Cầu Giấy, Hà Nội', N'0923476969', 1, N'Outdoor', N'5x5', N'uploads/court/images/2c7478ce-1b2c-413b-b931-9fba3ad6c3d9.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'ad9531be-cc48-46d9-a412-a2577ec839e7', N'Sky Trap', CAST(500000 AS Decimal(10, 0)), N'AMDI Group, đường Trịnh Văn Bô, phường Phương Canh, quận Nam Từ Liêm', N'033 980 9222', 1, N'Indoor', N'5x5', N'uploads/court/images/6ddc9c41-962b-4b4e-932e-54c4b15ca68a.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', N'Phòng không không quân', CAST(400000 AS Decimal(10, 0)), N'03 Lê Trọng Tấn, Thanh Xuân', N'0903277780', 1, N'Indoor', N'5x5', N'uploads/court/images/8499d445-1f97-4f1e-a1f7-f3bff7543893.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'b7bee54c-be4f-4d51-a5f5-ff5d8c0b6cc8', N'THPT Yên Hòa - sân 2', CAST(0 AS Decimal(10, 0)), N'251 Đ. Nguyễn Khang, Yên Hoà, Cầu Giấy, Hà Nội', N'0923476969', 1, N'Outdoor', N'3x3', N'uploads/court/images/a1f2dd02-411e-442f-9720-fa5bfd7d285b.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'ef7348f8-92b3-43c0-83db-6d49aaf366bb', N'NTĐ Quận Thanh Xuân', CAST(250000 AS Decimal(10, 0)), N'166 Khuất Duy Tiến, Nhân Chính, Thanh Xuân', N'0384858169', 1, N'Outdoor', N'5x5', N'uploads/court/images/140ee03a-126e-4bb0-b072-d5f27c596fd0.jpg', 3)
INSERT [dbo].[Court] ([CourtId], [CourtName], [RentPricePerHour], [Address], [Contact], [Status], [Type], [Kind], [ImageURL], [UsagePurpose]) VALUES (N'f6f2e75f-6a19-49ed-b13b-3c14cbc64b83', N'KTX Mễ Trì', CAST(250000 AS Decimal(10, 0)), N'182 Lương Thế Vinh', N'0903299623', 1, N'Outdoor', N'5x5', N'uploads/court/images/652d9864-e345-4d0c-b969-0b39f4164597.jpg', 3)
GO
SET IDENTITY_INSERT [dbo].[EmailVerification] ON 

INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (1, N'hieptvhe173252@fpt.edu.vn', N'612089', N'PlayerRegistrationForm', CAST(N'2025-05-04T21:24:12.797' AS DateTime), 1, CAST(N'2025-05-04T21:14:12.797' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (2, N'umt89618@toaik.com', N'560761', N'ManagerRegistrationForm', CAST(N'2025-05-04T21:32:15.450' AS DateTime), 1, CAST(N'2025-05-04T21:22:15.450' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (3, N'swpgroup4se1742@gmail.com', N'561780', N'PlayerRegistrationForm', CAST(N'2025-05-05T01:11:58.463' AS DateTime), 1, CAST(N'2025-05-05T01:01:58.463' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (4, N'tainghe171475@fpt.edu.vn', N'695425', N'PlayerRegistrationForm', CAST(N'2025-05-04T21:51:34.457' AS DateTime), 1, CAST(N'2025-05-04T21:41:34.457' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (6, N'hgwfkjyx@tempmail.id.vn', N'535467', N'PlayerRegistrationForm', CAST(N'2025-05-05T01:56:36.547' AS DateTime), 1, CAST(N'2025-05-05T01:46:36.547' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (7, N'vomow83973@javbing.com', N'459239', N'PlayerRegistrationForm', CAST(N'2025-05-05T17:14:51.973' AS DateTime), 1, CAST(N'2025-05-05T17:04:51.973' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (8, N'conghieu8899@gmail.com', N'767986', N'PlayerRegistrationForm', CAST(N'2025-05-06T04:16:19.167' AS DateTime), 1, CAST(N'2025-05-06T04:06:19.167' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (9, N'hieptran.pa@gmail.com', N'540234', N'PlayerRegistrationForm', CAST(N'2025-05-07T11:34:12.757' AS DateTime), 1, CAST(N'2025-05-07T11:24:12.757' AS DateTime))
INSERT [dbo].[EmailVerification] ([EmailVerificationId], [Email], [Code], [Purpose], [ExpiresAt], [IsUsed], [CreatedAt]) VALUES (10, N'hieunche173333@fpt.edu.vn', N'333583', N'PlayerRegistrationForm', CAST(N'2025-05-07T01:52:40.577' AS DateTime), 0, CAST(N'2025-05-07T01:42:40.577' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmailVerification] OFF
GO
INSERT [dbo].[Expenditure] ([ExpenditureId], [TeamFundId], [Name], [Amount], [Date], [ByManagerId], [PayoutDate], [UsedByUserId]) VALUES (N'08a147a6-679d-4fe2-b984-d29987c5dab8', N'936eaf99-0470-4909-9080-ecccd525d582', N'TIền test 2', CAST(20000 AS Decimal(10, 0)), CAST(N'2025-05-07' AS Date), N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-09T23:59:59.000' AS DateTime), N'0(02dbf8d2-982d-40d3-893e-15b8fe85a90a,133a6f47-cef4-4da3-ae24-e44b1e477b6e)')
INSERT [dbo].[Expenditure] ([ExpenditureId], [TeamFundId], [Name], [Amount], [Date], [ByManagerId], [PayoutDate], [UsedByUserId]) VALUES (N'4199e12a-3177-4555-9bbc-be08f63d8a21', N'553cf450-6534b-44ab-9c32-444493fdc71', N'tiền test', CAST(1232312 AS Decimal(10, 0)), CAST(N'2025-05-07' AS Date), N'04c82cb5-d43a-4e6a-b610-aacbb50d2297', CAST(N'2025-05-07T00:00:00.000' AS DateTime), N'0(00b4a569-ad4a-41d2-97bf-16d25a736023)')
INSERT [dbo].[Expenditure] ([ExpenditureId], [TeamFundId], [Name], [Amount], [Date], [ByManagerId], [PayoutDate], [UsedByUserId]) VALUES (N'426a9e67-fb49-4213-8691-1c6b8f09323e', N'936eaf99-0470-4909-9080-ecccd525d582', N'tiền test 1', CAST(10000 AS Decimal(10, 0)), CAST(N'2025-05-07' AS Date), N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-08T23:59:59.000' AS DateTime), N'0(02dbf8d2-982d-40d3-893e-15b8fe85a90a)')
INSERT [dbo].[Expenditure] ([ExpenditureId], [TeamFundId], [Name], [Amount], [Date], [ByManagerId], [PayoutDate], [UsedByUserId]) VALUES (N'8b31e751-cd79-4325-9c78-150dd1205963', N'553cf450-6534b-44ab-9c32-444493fdc71', N'tiền ăn của tất', CAST(3324 AS Decimal(10, 0)), CAST(N'2025-05-07' AS Date), N'04c82cb5-d43a-4e6a-b610-aacbb50d2297', CAST(N'2025-05-08T00:00:00.000' AS DateTime), N'0(0f751cc0-81b4-4549-9155-26bf20bce4f2,00b4a569-ad4a-41d2-97bf-16d25a736023)')
GO
SET IDENTITY_INSERT [dbo].[FileHash] ON 

INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (1, N'2c70b57f00dc4c7d280aa484744f70f896b3235d55371c870f66e793b4daf2f7', N'uploads/court/images/652d9864-e345-4d0c-b969-0b39f4164597.jpg', CAST(N'2025-05-04T20:49:20.387' AS DateTime), N'.jpg', 470877)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (2, N'027d85dbfffa4c1bf205ae2a53e496d972589512b79c04f36c66b8fcb3ce0a41', N'uploads/court/images/1dfc9f49-1e41-4ff4-9e90-f056beee768d.jpg', CAST(N'2025-05-04T20:50:30.000' AS DateTime), N'.jpg', 177837)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (3, N'd86be280709fdb4d775d757c55cd3d8e408e8b21734333a9814132520fdb5952', N'uploads/court/images/140ee03a-126e-4bb0-b072-d5f27c596fd0.jpg', CAST(N'2025-05-04T20:52:51.953' AS DateTime), N'.jpg', 835941)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (4, N'54ccbfd18dd3218d2b74adff01bca10e771afbfb973632e4bfa09b9bf81f9842', N'uploads/court/images/8499d445-1f97-4f1e-a1f7-f3bff7543893.jpg', CAST(N'2025-05-04T20:54:41.603' AS DateTime), N'.jpg', 444928)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (5, N'd461521bf6885c20511057cbf432af0aaf0e96ef2b25ffe7cf80eb3481ae3824', N'uploads/court/images/5c19167c-10b2-4407-9894-37a17c3d058b.jpg', CAST(N'2025-05-04T20:55:58.977' AS DateTime), N'.jpg', 39711)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (6, N'30dbfbf5ba4b4587609a0c1b98cd9abd0a0a64f60c77cd6a20a58b7f0c8ef77b', N'uploads/court/images/eea01adf-29e7-455a-8f3b-d3471544bb4f.jpg', CAST(N'2025-05-04T21:01:37.280' AS DateTime), N'.jpg', 316130)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (7, N'47abe83c66461a810f93cae1e0dbebba768a3791645ae78f36088028b9d41036', N'uploads/court/images/fb11f902-b182-4d6b-88e3-948489a1c129.jpg', CAST(N'2025-05-04T21:03:40.253' AS DateTime), N'.jpg', 96138)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (8, N'8de2b4ae8399fa89106daebbe6dbc19d5862f662899d6476630254fbb6f1a363', N'uploads/court/images/a554e290-67e9-45fb-8925-9f1f5e37369c.jpg', CAST(N'2025-05-04T21:07:26.920' AS DateTime), N'.jpg', 66958)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (9, N'926a6b2446b53a856d09435093ba52fb6cf350c5878732f2a40a455b797c1f44', N'uploads/court/images/2c7478ce-1b2c-413b-b931-9fba3ad6c3d9.jpg', CAST(N'2025-05-04T21:12:15.977' AS DateTime), N'.jpg', 152372)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (10, N'926a6b2446b53a856d09435093ba52fb6cf350c5878732f2a40a455b797c1f44', N'uploads/court/images/f4e80a45-ba03-4836-acde-a159500013a3.jpg', CAST(N'2025-05-04T21:13:51.643' AS DateTime), N'.jpg', 152372)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (11, N'7fd8d7c760fddda6c6e032adf584f7b1dfbdc47bc742929e10ca9518918fc880', N'uploads/court/images/abf93ad7-cb82-4f51-92ab-3f1ecc434d9f.png', CAST(N'2025-05-04T21:16:30.287' AS DateTime), N'.png', 278482)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (12, N'f698169e3a620301ce033af660d99643098ac0e9bdeca170a9c8f1f8ded65259', N'uploads/court/images/6ddc9c41-962b-4b4e-932e-54c4b15ca68a.jpg', CAST(N'2025-05-04T21:18:47.757' AS DateTime), N'.jpg', 213243)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (13, N'926a6b2446b53a856d09435093ba52fb6cf350c5878732f2a40a455b797c1f44', N'uploads/court/images/156724d1-ffe8-474e-9867-11f25cff8c07.jpg', CAST(N'2025-05-04T21:22:03.057' AS DateTime), N'.jpg', 152372)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (14, N'926a6b2446b53a856d09435093ba52fb6cf350c5878732f2a40a455b797c1f44', N'uploads/court/images/a1f2dd02-411e-442f-9720-fa5bfd7d285b.jpg', CAST(N'2025-05-04T21:22:57.760' AS DateTime), N'.jpg', 152372)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (15, N'cfc73ff885db2ea9bab89be058114bb916468a1e189caa108e4f4357e8ea6696', N'uploads/face-recognition/d4e24341-22d1-4a12-b2a9-c217301bb97b.png', CAST(N'2025-05-04T21:34:11.907' AS DateTime), N'.png', 1266121)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (19, N'3575560f8a1b55bf19a96b5c32f013e6b80bf44f676cc305812eaf2e3245aefe', N'uploads/face-recognition/d62b920a-8b62-402b-9118-83a3a58f0b61.png', CAST(N'2025-05-05T20:52:58.043' AS DateTime), N'.png', 285967)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (46, N'3354e1373107ccd39fb3ab15bf923bf33ffeb78f4e367edb87e4eea99ab7f664', N'uploads/face-recognition/c6f73f2d-07d7-4e04-a244-b335bb5e30f8.png', CAST(N'2025-05-07T08:11:56.040' AS DateTime), N'.png', 1140009)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (47, N'37e7b69f9004adaac84b7585003ef9b6140581401a7fec72bbb4dd0bdffab036', N'uploads/face-recognition/0a8e779f-f0f5-4b36-9442-a1c336a2c438.png', CAST(N'2025-05-07T11:40:31.763' AS DateTime), N'.png', 1069718)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (48, N'5830c9b32b02273df16c67d16039423314b0ae55a76c15e3849ede6530ba0150', N'uploads/face-recognition/e64db7b1-0776-4693-8241-77333ab1c69c.jpg', CAST(N'2025-05-07T11:41:52.690' AS DateTime), N'.jpg', 13791)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (52, N'119fb36a3742acfa4f0839a41cea9ce727d43bd11a06c44883f9d915397d0029', N'uploads/face-recognition/e894f22d-ed85-4f62-8f00-b99e5c2d4030.png', CAST(N'2025-05-07T12:12:03.463' AS DateTime), N'.png', 1194709)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (54, N'd816907f4a3aa89835740f4850d59570e6375b5720b1f506bf69902f4aaa19ce', N'uploads/face-recognition/da9fca75-27f3-47da-ae5c-bd7b5a0ab596.png', CAST(N'2025-05-07T12:13:24.023' AS DateTime), N'.png', 1143149)
INSERT [dbo].[FileHash] ([Id ], [Hash ], [FilePath ], [CreatedDate ], [FileType ], [FileSize ]) VALUES (55, N'a3a290de52db3ff1caca0db14e6d873c2f5f1b0f9c1c97dc1ee80265b3139388', N'uploads/face-recognition/fcbde0c0-4972-460e-8e16-43a7ff66e71d.png', CAST(N'2025-05-07T12:14:47.750' AS DateTime), N'.png', 1114976)
SET IDENTITY_INSERT [dbo].[FileHash] OFF
GO
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'ADDITIONAL_TRAINING_SESSION_PENDING', N'[YHBT] Thông báo: Vui lòng đặt sân tập luyện cho đội ngày', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px;"><em>(Nếu bạn không phải là quản lý có nhiệm vụ đặt sân, vui lòng bỏ qua email này)</em></p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Xin chào các quản lý của <strong>{{TEAM_NAME}}</strong>,</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nhằm nâng cao hiệu quả tập luyện, huấn luyện viên vừa thêm một buổi tập bổ sung vào lịch tập luyện của đội. Cụ thể lịch tập luyện bổ sung như sau:</p>
              <ul style="line-height: 28px;">
                <li>Thời gian: <strong>{{SCHEDULED_DATE}}</strong> từ <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong>.</li>
                <li>Địa điểm: <strong>{{COURT_NAME}}</strong>.</li>
              </ul>
              <p style="margin: 0px;">Vui lòng đặt <strong>{{COURT_NAME}}</strong> trong khoảng thời gian này. Dưới đây là thông tin liên hệ chủ sân bóng:</p>
              <ul style="line-height: 28px;">
                <li>Địa chỉ: <strong>{{COURT_ADDRESS}}</strong>.</li>
                <li>Liên hệ: <strong>{{COURT_CONTACT}}</strong>.</li>
                <li>Giá thuê: <strong>{{COURT_PRICE}}</strong>.</li>
              </ul>
              <p style="margin: 0px;">Sau khi đặt sân, vui lòng xác nhận trên hệ thống <a rel="noopener" href="https://yhbt.vn/training-session/{{TRAINING_SESSION_ID}}" target="_blank">tại đây</a>.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nếu có vấn đề gì cần trao đổi, vui lòng liên hệ huấn luyện viên <strong>{{CREATED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-14T10:31:13.000' AS DateTime), CAST(N'2025-05-04T11:18:59.430' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'APPROVED_MANAGER_REGISTRATION', N'[YHBT] Thông báo: Chào mừng bạn gia nhập Câu Lạc Bộ Bóng Rổ Yên Hòa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>
                        
                        <tr>
                          <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
                                  <!-- Dynamic Body Section -->
                                  <p style="margin: 0px; font-size: 18px; text-align: center;">Kính gửi Đỗ Đức Hùng,</p>
                                  <p style="margin: 0px;">Thay mặt câu lạc bộ bóng rổ <strong>Yên Hòa BasketBall</strong>, chúng tôi xin gửi lời chúc mừng nồng nhiệt nhất đến bạn!</p>
                                  <p style="margin: 0px;">Sau buổi phỏng vấn đầy ấn tượng, chúng tôi rất vui mừng thông báo bạn đã được chọn để gia nhập đội với vị trí <strong>Quản Lý Đội.</strong></p>
                                  <p style="margin: 0px;">Chúng tôi đánh giá cao tinh thần đồng đội của bạn và tin rằng bạn sẽ đóng góp tích cực vào sự thành công của đội.</p>
                                  <p style="margin: 0px;">Thông tin về buổi tập đầu tiên và các hoạt động của câu lạc bộ sẽ được gửi đến bạn trong thời gian sớm nhất.</p>
                                  <p style="margin: 0px;">Nếu bạn có bất kỳ câu hỏi nào, xin đừng ngần ngại liên hệ với chúng tôi.</p>
                                  <p style="margin: 0px;">Chào mừng bạn đến với gia đình <strong>Yên Hòa BasketBall</strong>!</p>
                                  <hr style="margin: 20px 0;">
                                  <p style="margin: 0px;"><strong>Thông tin đăng nhập:</strong></p>
                                  <p style="margin: 0px;">Tên đăng nhập: <strong>hungđđ</strong></p>
                                  <p style="margin: 0px;">Mật khẩu: <strong>$2a$11$FXZH9K8CjroUnCs4cx2ZFuUusMzCgHMxm36rTntR3RrPfPVeReOlS</strong></p>
                                  <p style="margin: 0px;">Bạn có thể đăng nhập hệ thống tại: <a href="https://localhost:5000/api/auth/login" target="_blank">https://localhost:5000/api/auth/login</a></p>
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                        


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>
                      
                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                      
                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-01T20:30:23.000' AS DateTime), CAST(N'2025-05-04T11:18:59.587' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'APPROVED_PARENT_EMAIL_REGISTRATION', N'[YHBT] Thông báo: Chúc mừng bạn đã gia nhập Câu Lạc Bộ Bóng Rổ Yên Hòa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px; font-size: 18px;"><strong>Xin chào {{FULLNAME}},</strong></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi xin thông báo rằng con bạn đã được đăng ký thành công vào <strong>CLB Bóng Rổ Yên Hòa</strong>. Dưới đây là thông tin tài khoản và mật khẩu để bạn có thể quản lý hoạt động học tập và tham gia các buổi tập của con:</p>
              <p style="margin: 20px 0 0 0;"><strong>Tài khoản:</strong> <span style="font-size: 16px; font-weight: bold; color: #333; background-color: #f8f8f8; padding: 5px 10px; border-radius: 6px; display: inline-block;">nguyenh</span></p>
              <p style="margin: 20px 0 0 0;"><strong>Mật khẩu:</strong> <span style="font-size: 16px; font-weight: bold; color: #333; background-color: #f8f8f8; padding: 5px 10px; border-radius: 6px; display: inline-block;">PwhqC[4SY</span></p>
              <p style="margin: 10px 0 0 0;">Với tài khoản này, bạn sẽ có thể theo dõi lịch tập, kết quả và các hoạt động của con tại CLB. Chúng tôi hy vọng bạn và con bạn sẽ có một trải nghiệm tuyệt vời cùng với đội bóng đầy nhiệt huyết và đam mê này.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="https://localhost:5000/api/auth/login" target="_blank" style="color: #333333; text-decoration: none;">https://localhost:5000/api/auth/login</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-08T01:33:50.000' AS DateTime), CAST(N'2025-05-04T11:18:59.717' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'APPROVED_PLAYER_EMAIL_REGISTRATION', N'[YHBT] Thông báo: Chúc mừng bạn đã gia nhập Câu Lạc Bộ Bóng Rổ Yên Hòa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px; font-size: 22px; text-align: center;"><strong>Chúc mừng bạn đã gia nhập CLB Bóng Rổ Yên Hòa!</strong></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi rất vui mừng thông báo rằng bạn đã gia nhập thành công <strong>CLB Bóng Rổ Yên Hòa</strong>. Chúc mừng bạn đã trở thành thành viên của đội bóng đầy nhiệt huyết và đam mê này. Bạn sẽ nhận được thông tin tài khoản và mật khẩu của mình dưới đây:</p>
              <p style="margin: 20px 0 0 0;"><strong>Tài khoản:</strong> <span style="font-size: 18px; font-weight: bold; color: #333; background-color: #f8f8f8; padding: 5px 10px; border-radius: 6px; display: inline-block;">gs08012</span></p>
              <p style="margin: 20px 0 0 0;"><strong>Mật khẩu:</strong> <span style="font-size: 18px; font-weight: bold; color: #333; background-color: #f8f8f8; padding: 5px 10px; border-radius: 6px; display: inline-block;">DY$!nw}&9z</span></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi hy vọng bạn sẽ có một trải nghiệm tuyệt vời tại CLB. Hãy chuẩn bị sẵn sàng để phát triển kỹ năng và tham gia các hoạt động thú vị với đội bóng của chúng tôi.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="https://localhost:5000/api/auth/login" target="_blank" style="color: #333333; text-decoration: none;">https://localhost:5000/api/auth/login</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-06T21:05:01.000' AS DateTime), CAST(N'2025-05-04T11:18:59.853' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'CALL_TRY_OUT', N'[YHBT] Thông báo: Vòng kiểm tra đầu vào Câu Lạc Bộ Yên Hòa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px; font-size: 22px; text-align: center;"><strong>Thông báo tham dự buổi kiểm tra đầu vào</strong></p>
              <p style="margin: 10px 0 0 0;">Chúc mừng bạn đã đăng ký thành công tham gia buổi kiểm tra đầu vào của <strong>CLB Yên Hòa BasketBall</strong>. Dưới đây là thông tin chi tiết:</p>
              <p style="margin: 20px 0 0 0; text-align: center;"><strong>- Địa điểm:</strong> {{LOCATION}}</p>
              <p style="margin: 10px 0 0 0; text-align: center;"><strong>- Thời gian:</strong> {{TIME}}</p>
              <p style="margin: 20px 0 0 0; text-align: center;"><strong>- Số báo danh:</strong> {{SBD}}</p>
              <p style="margin: 10px 0 0 0;"><strong>Lưu ý:</strong> Vui lòng có mặt trước thời gian diễn ra ít nhất <strong>30 phút</strong> để hoàn tất thủ tục.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-09T20:37:10.000' AS DateTime), CAST(N'2025-05-04T11:18:59.993' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'CANCEL_TRAINING_SESSION', N'[YHBT] Thông báo: Huỷ buổi tập ngày', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px;">Xin chào các thành viên của <strong>{{TEAM_NAME}}</strong>,</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Do một vài lý do bất khả kháng, buổi tập dự kiến vào <strong>{{SCHEDULED_DATE}}</strong> từ <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong> tại <strong>{{COURT_NAME}}</strong> sẽ bị <strong>huỷ</strong>.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Chúng tôi rất tiếc về sự thay đổi này và mong mọi người thông cảm. Nếu có lịch tập thay thế, chúng tôi sẽ thông báo sớm nhất có thể.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nếu có bất kỳ thắc mắc nào, vui lòng liên hệ <strong>{{CANCELED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-14T20:52:12.000' AS DateTime), CAST(N'2025-05-04T11:19:00.107' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'CHANGE_PASSWORD_SUCCESS', N'[YHBT] Thông báo: Đổi mật khẩu thành công', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px; font-size: 20px;"><strong>Xin chào {{USER_FULLNAME}},</strong></p>
              <p style="margin: 10px 0 0 0;"></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi xin thông báo rằng mật khẩu cho tài khoản của bạn tại hệ thống quản lý học viện bóng rổ YHBT đã được thay đổi thành công vào lúc <strong>{{CHANGE_DATE}}</strong>. Nếu bạn không thực hiện yêu cầu này, vui lòng liên hệ ngay với chúng tôi để đảm bảo an toàn cho tài khoản của bạn.</p>
              <p style="margin: 10px 0 0 0;">Nếu bạn đã đổi mật khẩu, không cần thực hiện thêm bước nào. Hãy nhớ sử dụng mật khẩu mới khi đăng nhập lần sau.</p>
              <p style="margin: 10px 0 0 0;">Nếu cần hỗ trợ, vui lòng liên hệ quản lý của bạn.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-01T20:29:59.000' AS DateTime), CAST(N'2025-05-04T11:19:00.210' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'COACH_ASSIGN_TO_TEAM_SUCCESS', N'[YHBT] Thông báo: Bạn đã trở thành Huấn Luyện Viên của đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>
                        
<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px;"><strong>XIN CHÀO {{USER_NAME}},</strong></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi trân trọng thông báo bạn đã được phân công vào vai trò <strong>{{ROLES_CODE}} (Huấn Luyện Viên)</strong> đội bóng rổ <strong>{{TEAM_NAME}}</strong>, kể từ ngày <strong>{{DAY_ASSIGN_TO_TEAM}}</strong>. Với kinh nghiệm và năng lực của mình, chúng tôi tin tưởng bạn sẽ giúp các cầu thủ phát triển kỹ năng và đạt được những thành tích xuất sắc trong thời gian tới.</p>
              <p style="margin: 10px 0 0 0;"><strong>Nhiệm vụ cụ thể:</strong></p>
              <ul style="line-height: 28px;">
                <li>Xây dựng và thực hiện các chương trình huấn luyện kỹ năng cho các cầu thủ.</li>
                <li>Theo dõi và đánh giá sự tiến bộ của từng cầu thủ trong quá trình tập luyện.</li>
                <li>Phối hợp với ban huấn luyện để xây dựng chiến lược phát triển kỹ năng cho đội bóng.</li>
                <li>Tổ chức các buổi tập luyện chuyên sâu và các bài tập phù hợp với từng vị trí thi đấu.</li>
                <li>Đảm bảo tinh thần đoàn kết và kỷ luật trong đội bóng.</li>
              </ul>
              <p style="margin: 0px;"><strong>Hỗ trợ từ phía tổ chức:</strong></p>
              <ul style="line-height: 28px;">
                <li>Đội ngũ huấn luyện viên và nhân viên y tế sẽ đồng hành cùng đội.</li>
                <li>Cung cấp đầy đủ trang thiết bị và ngân sách theo kế hoạch đã phê duyệt.</li>
              </ul>
              <p style="margin: 0px;">Chúng tôi đánh giá cao sự đóng góp của bạn và tin rằng đội bóng sẽ có một mùa giải thành công dưới sự dẫn dắt này. Hãy liên hệ nếu có bất kỳ thắc mắc nào.</p>
              <p style="margin: 10px 0 0 0;">Chào mừng bạn đến với gia đình <strong>Yên Hòa BasketBall</strong>!</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>
                      
                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                      
                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-19T23:48:46.000' AS DateTime), CAST(N'2025-05-04T11:19:00.290' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'COACH_REGISTRATION_SUCCESS', N'[YHBT] Thông báo: Chào mừng bạn gia nhập Câu Lạc Bộ Bóng Rổ Yên Hòa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>
                        
                        <tr>
                          <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
                                  <!-- Dynamic Body Section -->
                                  <p style="margin: 0px; text-align: center; font-size: 24px;"><strong>Kính gửi {{FULLNAME}},</strong></p>
                                  <p style="margin: 0px;">Thay mặt câu lạc bộ bóng rổ <strong>Yên Hòa BasketBall</strong>, chúng tôi xin gửi lời chúc mừng nồng nhiệt nhất đến bạn! Sau buổi phỏng vấn đầy ấn tượng, chúng tôi rất vui mừng thông báo bạn đã được chọn để gia nhập đội với vị trí <strong>Huấn Luyện Viên</strong>.</p>
                                  <p style="margin: 0px;">Chúng tôi đánh giá cao kinh nghiệm và kỹ năng của bạn, và tin rằng bạn sẽ đóng góp tích cực vào sự thành công của đội.</p>
                                  <p style="margin: 0px;">Thông tin về buổi tập đầu tiên và các hoạt động của câu lạc bộ sẽ được gửi đến bạn trong thời gian sớm nhất.</p>
                                  <p style="margin: 0px;">Nếu bạn có bất kỳ câu hỏi nào, xin đừng ngần ngại liên hệ với chúng tôi.</p>
                                  <p style="margin: 0px;">Chào mừng bạn đến với gia đình <strong>Yên Hòa BasketBall</strong>!</p>
                                  <hr style="margin: 20px 0;">
                                  <p style="margin: 0px;"><strong>Thông tin đăng nhập:</strong></p>
                                  <p style="margin: 0px;">Tên đăng nhập: <strong>{{USERNAME}}</strong></p>
                                  <p style="margin: 0px;">Mật khẩu: <strong>{{PASSWORD}}</strong></p>
                                  <p style="margin: 0px;">Bạn có thể đăng nhập hệ thống tại: <a href="{{LOGIN_LINK}}" target="_blank">{{LOGIN_LINK}}</a></p>
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>
                      
                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                      
                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-23T03:00:30.000' AS DateTime), CAST(N'2025-05-04T11:19:00.397' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'CORRECTION_ATTENDANCE_TO_PARENT', N'[YHBT] Thông báo: Đính chính trạng thái điểm danh của con ', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0; font-weight: bold;">Đính chính tình trạng điểm danh của con bạn</p>
              <p style="margin: 0;">Xin chào,</p>
              <p style="margin: 10px 0 0 0;">Chúng tôi xin đính chính thông tin về tình trạng điểm danh của con {{PLAYER_NAME}} tại buổi tập của <strong>CLB Bóng Rổ Yên Hòa</strong> như sau:</p>
              <p style="margin: 10px 0 0 0;">- <strong>Ngày tập:</strong> {{TRAINING_DATE}}</p>
              <p style="margin: 0;">- <strong>Thời gian:</strong> Từ {{START_TIME}} đến {{END_TIME}}</p>
              <p style="margin: 0;">- <strong>Địa điểm:</strong> {{COURT_NAME}}</p>
              <p style="margin: 0;">- <strong>Tình trạng đính chính:</strong> <span style="color: #bd2426; font-weight: bold;">{{ATTENDANCE_STATUS}}</span></p>
              <p style="margin: 10px 0 0 0;">Chúng tôi xin lỗi vì sự nhầm lẫn này và rất mong nhận được sự thông cảm từ quý phụ huynh. Nếu có bất kỳ thắc mắc nào về tình trạng điểm danh hoặc lịch tập, vui lòng liên hệ với chúng tôi qua email hoặc hotline để được hỗ trợ.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-05-04T09:12:53.000' AS DateTime), CAST(N'2025-05-04T11:19:00.523' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'CREATE_ADDITIONAL_TRAINING_SESSION', N'[YHBT] Thông báo: Lịch tập bổ sung ngày', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0px;">Xin chào các thành viên của <strong>{{TEAM_NAME}}</strong>,</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nhằm nâng cao hiệu quả tập luyện, huấn luyện viên vừa thêm một buổi tập bổ sung vào lịch tập luyện của đội. Cụ thể lịch tập luyện bổ sung như sau:</p>
              <ul style="line-height: 28px;">
                <li>Thời gian: <strong>{{SCHEDULED_DATE}}</strong> từ <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong>.</li>
                <li>Địa điểm: <strong>{{COURT_NAME}}</strong>.</li>
              </ul>
              <p style="margin: 0px;">Sự có mặt đầy đủ và đúng giờ của các thành viên là rất quan trọng để đảm bảo chất lượng buổi tập. Nếu có vấn đề gì cần trao đổi, vui lòng liên hệ huấn luyện viên <strong>{{CREATED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-14T16:12:17.000' AS DateTime), CAST(N'2025-05-04T11:19:00.693' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'DISABLE_ACCOUNT', N'[YHBT] Thông báo: Tài khoản của bạn đã bị vô hiệu hóa', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Content Section -->
              <p style="margin: 0px;"><strong>THÔNG BÁO KHÓA TÀI KHOẢN</strong></p>
              <p style="margin: 0px;">Xin chào <strong>{{USERNAME}}</strong>,</p>
              <p style="margin: 0px;">Chúng tôi xin thông báo rằng tài khoản của bạn đã bị khóa vào lúc <strong>{{DISABLE_ACCOUNT_TIME}}</strong> do vi phạm các điều khoản sử dụng của hệ thống.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;"><strong>Lý do khóa tài khoản:</strong></p>
              <ul style="line-height: 28px;">
                <li>Vi phạm quy định về hành vi ứng xử trên hệ thống.</li>
                <li>Sử dụng tài khoản cho mục đích không hợp lệ.</li>
                <li>Không tuân thủ các quy định bảo mật và an toàn thông tin.</li>
              </ul>
              <p style="margin: 0px;">Nếu bạn cho rằng đây là sự nhầm lẫn hoặc cần thêm thông tin, vui lòng liên hệ với chúng tôi.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-20T22:22:35.000' AS DateTime), CAST(N'2025-05-04T11:19:00.790' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'ENABLE_ACCOUNT', N'[YHBT] Thông báo: Tài khoản của bạn đã được kích hoạt trở lại', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Content Section -->
              <p style="margin: 0px;"><strong>THÔNG BÁO KÍCH HOẠT LẠI TÀI KHOẢN</strong></p>
              <p style="margin: 0px;">Xin chào <strong>{{USERNAME}}</strong>,</p>
              <p style="margin: 0px;">Chúng tôi xin thông báo rằng tài khoản của bạn đã được kích hoạt lại thành công vào lúc <strong>{{DISABLE_ACCOUNT_TIME}}</strong>. Bạn có thể tiếp tục sử dụng tài khoản của mình như bình thường.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;"><strong>Lưu ý:</strong></p>
              <ul style="line-height: 28px;">
                <li>Vui lòng tuân thủ các quy định và điều khoản sử dụng của hệ thống để tránh việc tài khoản bị khóa lại.</li>
                <li>Nếu bạn gặp bất kỳ vấn đề nào khi đăng nhập, vui lòng liên hệ với chúng tôi qua email hỗ trợ: <strong>support@bams.com</strong>.</li>
              </ul>
              <p style="margin: 0px;">Trân trọng,<br /><strong>Đội ngũ BAMS</strong></p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-20T22:49:50.000' AS DateTime), CAST(N'2025-05-04T11:19:00.867' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'FORGOT_PASSWORD_TOKEN', N'[YHBT] Thông báo: Đặt lại mật khẩu tài khoản', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{USER_FULLNAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn. Vui lòng nhấn vào nút bên dưới để tạo mật khẩu mới:</p>

              <!-- Button Section -->
              <div align="center">
                <a href="{{CHANGE_PASSWORD_LINK}}" target="_blank" style="box-sizing: border-box; display: inline-block; text-decoration: none; text-align: center; color: rgb(255, 255, 255); background: rgb(189, 36, 38); border-radius: 4px; width: 45%; max-width: 100%; word-break: break-word; overflow-wrap: break-word; font-family: Arial, sans-serif; font-size: 20px; font-weight: 400;">
                  <span style="display:block;padding:10px 20px;">
                    <span style="font-size: 14px; line-height: 14px;">TẠO MẬT KHẨU MỚI</span>
                  </span>
                </a>
              </div>

              <!-- Additional Instructions -->
              <p style="margin: 10px 0 0 0;">Trong trường hợp nút bên trên không hoạt động, vui lòng truy cập theo đường dẫn này: <a href="{{CHANGE_PASSWORD_LINK}}" target="_blank">{{CHANGE_PASSWORD_LINK}}</a>.</p>
              <p style="margin: 0px;">Liên kết này sẽ hết hạn sau <strong>{{TOKEN_LIVE_TIME}} phút</strong>. Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
              <p style="margin: 0px;">Nếu cần hỗ trợ, vui lòng liên hệ với quản lý của bạn.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-01T20:30:23.000' AS DateTime), CAST(N'2025-05-04T11:19:00.947' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'MANAGER_ASSIGN_TO_TEAM_SUCCESS', N'[YHBT] Thông báo: Bạn đã trở thành Quản Lý của đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting and Team Role Assignment -->
              <p style="margin: 0px;"><strong>Xin chào {{USER_NAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi trân trọng thông báo bạn đã được phân công vào vai trò <strong>{{ROLES_CODE}}</strong> đội bóng rổ <strong>{{TEAM_NAME}}</strong>, kể từ ngày <strong>{{DAY_ASSIGN_TO_TEAM}}</strong>. Với kinh nghiệm và năng lực của mình, chúng tôi tin tưởng bạn sẽ hỗ trợ đội bóng đạt được những thành tích xuất sắc trong thời gian tới.</p>
              <p style="margin: 0px;"> </p>

              <!-- Responsibilities Section -->
              <p style="margin: 0px;"><strong>Nhiệm vụ cụ thể:</strong></p>
              <ul style="line-height: 28px;">
                <li>Giám sát lịch tập luyện, thi đấu và các hoạt động liên quan của đội.</li>
                <li>Quản lý thành viên đội bóng, đảm bảo tinh thần đoàn kết và kỷ luật.</li>
                <li>Phối hợp với huấn luyện viên, ban tổ chức và các bộ phận liên quan để xây dựng chiến lược phát triển.</li>
                <li>Xử lý các công việc hành chính (đăng ký thi đấu, quản lý ngân sách, báo cáo định kỳ, v.v.).</li>
                <li>Đại diện đội bóng trong các sự kiện và giao dịch với đối tác.</li>
              </ul>

              <!-- Support from Organization Section -->
              <p style="margin: 0px;"><strong>Hỗ trợ từ phía tổ chức:</strong></p>
              <ul style="line-height: 28px;">
                <li>Đội ngũ huấn luyện viên và nhân viên y tế sẽ đồng hành cùng đội.</li>
                <li>Cung cấp đầy đủ trang thiết bị và ngân sách theo kế hoạch đã phê duyệt.</li>
              </ul>

              <!-- Closing Remarks -->
              <p style="margin: 0px;">Chúng tôi đánh giá cao sự đóng góp của bạn và tin rằng đội bóng sẽ có một mùa giải thành công dưới sự dẫn dắt này. Hãy liên hệ nếu có bất kỳ thắc mắc nào.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-01T20:30:23.000' AS DateTime), CAST(N'2025-05-04T11:19:01.017' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'PLAYER_ASSIGN_TO_TEAM_SUCCESS', N'[YHBT] Thông báo: Bạn đã trở thành cầu thủ của đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{USER_NAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi trân trọng thông báo bạn đã chính thức trở thành thành viên của đội bóng rổ <strong>{{TEAM_NAME}}</strong>, kể từ ngày <strong>{{DAY_ASSIGN_TO_TEAM}}</strong>. Chúng tôi rất vui mừng được chào đón bạn và tin rằng bạn sẽ đóng góp tích cực vào sự thành công của đội bóng.</p>
              <p style="margin: 0px;"> </p>

              <!-- Responsibilities Section -->
              <p style="margin: 0px;"><strong>Trách nhiệm của bạn:</strong></p>
              <ul style="line-height: 28px;">
                <li>Tham gia đầy đủ các buổi tập luyện và thi đấu theo lịch trình của đội.</li>
                <li>Tuân thủ các quy định và kỷ luật của đội bóng.</li>
                <li>Nỗ lực cải thiện kỹ năng cá nhân và phối hợp với đồng đội.</li>
                <li>Thể hiện tinh thần đồng đội và tôn trọng mọi thành viên trong đội.</li>
              </ul>

              <!-- Support from Organization Section -->
              <p style="margin: 0px;"><strong>Hỗ trợ từ phía tổ chức:</strong></p>
              <ul style="line-height: 28px;">
                <li>Đội ngũ huấn luyện viên sẽ hỗ trợ bạn trong quá trình tập luyện và thi đấu.</li>
                <li>Cung cấp đầy đủ trang thiết bị và dụng cụ tập luyện.</li>
              </ul>

              <!-- Closing Remarks Section -->
              <p style="margin: 0px;">Chúng tôi đánh giá cao sự tham gia của bạn và tin rằng đội bóng sẽ đạt được nhiều thành tích xuất sắc trong thời gian tới. Hãy liên hệ nếu bạn có bất kỳ thắc mắc nào.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Chào mừng bạn đến với gia đình Yên Hòa BasketBall!</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-20T10:15:30.000' AS DateTime), CAST(N'2025-05-04T11:19:01.093' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_CANCEL_TSRQ', N'[YHBT] Thông báo: Yêu cầu huỷ buổi tập luyện ngày {{DATE}} đã bị từ chối', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{COACH_NAME}},</strong></p>
              <p style="margin: 0px;">Yêu cầu huỷ buổi tập luyện ngày <strong>{{DATE}}</strong> lúc <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong> tại <strong>{{COURT_NAME}}</strong> đã bị từ chối bởi quản lý <strong>{{REJECTED_NAME}}</strong> với lý do: <strong>{{REASON}}</strong>.</p>
              <p style="margin: 0px;"> </p>

              <!-- Closing Remarks Section -->
              <p style="margin: 0px;">Nếu có vấn đề gì cần trao đổi, vui lòng liên hệ quản lý <strong>{{REJECTED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-15T09:54:01.000' AS DateTime), CAST(N'2025-05-04T11:19:01.150' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_MANAGER_REGISTRATION', N'[YHBT] Thông báo: Kết quả đơn đăng ký Quản Lý CLB', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{USER_NAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi rất tiếc phải thông báo rằng đơn đăng ký tham gia CLB Bóng Rổ Yên Hòa của bạn đã bị từ chối. Chúng tôi đã xem xét kỹ lưỡng và đưa ra quyết định này dựa trên các tiêu chí xét duyệt của CLB.</p>
              <p style="margin: 0px;"> </p>

              <!-- Closing Remarks Section -->
              <p style="margin: 0px;">Nếu bạn có bất kỳ câu hỏi nào về quyết định này, vui lòng liên hệ với chúng tôi.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-10T22:22:02.000' AS DateTime), CAST(N'2025-05-04T11:19:01.193' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_PENDING_TRAINING_SESSION', N'[YHBT] Thông báo: Yêu cầu tạo buổi tập luyện ngày {{DATE}} đã bị từ chối', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{COACH_NAME}},</strong></p>
              <p style="margin: 0px;">Yêu cầu tạo trận đấu bổ sung ngày <strong>{{DATE}}</strong> lúc <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong> tại <strong>{{COURT_NAME}}</strong> đã bị từ chối bởi quản lý <strong>{{REJECTED_NAME}}</strong> với lý do: <strong>{{REASON}}</strong>.</p>
              <p style="margin: 0px;"> </p>

              <!-- Closing Remarks Section -->
              <p style="margin: 0px;">Nếu có vấn đề gì cần trao đổi, vui lòng liên hệ quản lý <strong>{{REJECTED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-14T16:42:23.000' AS DateTime), CAST(N'2025-05-04T11:19:01.240' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_PLAYER_EMAIL_REGISTRATION', N'[YHBT] Thông báo: Đơn đăng ký CLB không được chấp thuận', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{USER_NAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi rất tiếc phải thông báo rằng đơn đăng ký tham gia CLB Bóng Rổ Yên Hòa của bạn đã không được chấp nhận. Sau khi xem xét kỹ lưỡng và căn cứ vào các tiêu chí xét duyệt của CLB, chúng tôi đã đưa ra quyết định này. Hi vọng bạn sẽ tiếp tục luyện tập và nộp đơn vào mùa giải tiếp theo. Chúng tôi luôn hoan nghênh sự tham gia của bạn và mong muốn có cơ hội làm việc cùng bạn trong tương lai.</p>
              <p style="margin: 0px;"> </p>

              <!-- Closing Remark Section -->
              <p style="margin: 0px;">Nếu bạn có bất kỳ câu hỏi nào về quyết định này, vui lòng liên hệ với chúng tôi.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-06T09:49:24.000' AS DateTime), CAST(N'2025-05-04T11:19:01.290' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_TEAMFUND', N'[YHBT] Thông báo: Từ chối quỹ đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }

      .u-row .u-col-100 {
        width: 600px !important;
      }
    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }

      .u-row .u-col img {
        max-width: 100% !important;
      }
    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }

    @media (max-width: 480px) {
      .hide-mobile {
        max-height: 0px;
        overflow: hidden;
        display: none !important;
      }
    }

    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span> </span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span> </span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
                                    <p style="margin: 0; font-weight: bold;">Thông báo từ chối yêu cầu Teamfund</p>
                                    <p style="margin: 0;">Rất tiếc, chúng tôi không thể chấp thuận yêu cầu của bạn vào thời điểm này. </p>
                                    <p style="margin: 0;">Lý do từ chối : {{REJECT_REASON}}</p>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong>Trân trọng,</strong></span><br /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"> </p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đây
                                      là email được gửi tự động bởi hệ thống, vui lòng không trả lời
                                      email này.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span> </span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span> </span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-06T01:03:57.000' AS DateTime), CAST(N'2025-05-04T11:19:01.760' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REJECT_UPDATE_TSRQ', N'[YHBT] Thông báo: Yêu cầu cập nhật buổi tập luyện ngày {{DATE}} đã bị từ chối', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>Xin chào {{COACH_NAME}},</strong></p>
              <p style="margin: 0px;">Yêu cầu cập nhật buổi tập luyện ngày <strong>{{DATE}}</strong> lúc <strong>{{START_TIME}}</strong> đến <strong>{{END_TIME}}</strong> tại <strong>{{COURT_NAME}}</strong> đã bị từ chối bởi quản lý <strong>{{REJECTED_NAME}}</strong> với lý do: <strong>{{REASON}}</strong>.</p>
              <p style="margin: 0px;"> </p>

              <!-- Closing Remark Section -->
              <p style="margin: 0px;">Nếu có vấn đề gì cần trao đổi, vui lòng liên hệ quản lý <strong>{{REJECTED_NAME}}</strong>.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-15T10:03:23.000' AS DateTime), CAST(N'2025-05-04T11:19:01.347' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REMINDER_PAYMENT', N'[YHBT] Thông báo: Nhắc nhở đóng quỹ đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>
                        
<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <p style="margin: 0;"><strong>Kính gửi {{FULLNAME}},</strong></p>
        <p style="color: #BD2426; font-weight: bold; margin: 0;">
          {{DAY_LEFT}} ngày để hoàn tất khoản đóng quỹ đội.
        </p>
        
        <p><strong>Tên quỹ:</strong> {{TEAMFUND_DESCRIPTION}}</p>
        <p>Hạn cuối để đóng khoản quỹ này là vào ngày <strong>{{DUEDATE}}</strong>.</p>
        <p>Vui lòng thực hiện thanh toán đúng hạn để đảm bảo quyền lợi và hoạt động của đội bóng.</p>
        <p>Nếu bạn đã thanh toán, vui lòng bỏ qua email này.</p>
        <div style="text-align: center; margin: 20px 0;">
          <a href="{{URL_GO_TO}}" target="_blank" style="display: inline-block; background-color: #BD2426; color: white; padding: 12px 24px; border-radius: 6px; font-weight: bold; text-decoration: none;">Thanh toán ngay</a>
        </div>
        <p>Trân trọng,</p>
        <p><strong>Ban điều hành CLB Bóng rổ Yên Hòa</strong></p>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>
                      
                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                      
                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-05-03T21:33:01.000' AS DateTime), CAST(N'2025-05-04T11:19:01.390' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REPORT_ATTENDANCE_TO_PARENT', N'[YHBT] Thông báo: Trạng thái điểm danh của con ', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Dynamic Body Section -->
              <p style="margin: 0; font-weight: bold;">Thông báo tình trạng điểm danh của con bạn</p>
              <p style="margin: 0;">Xin chào,</p>
              <p style="margin: 10px 0 0 0;">Chúng tôi xin thông báo về tình trạng điểm danh của con bạn tại buổi tập của <strong>CLB Bóng Rổ Yên Hòa</strong> như sau:</p>
              <p style="margin: 10px 0 0 0;">- <strong>Ngày tập:</strong> {{TRAINING_DATE}}</p>
              <p style="margin: 0;">- <strong>Thời gian:</strong> Từ {{START_TIME}} đến {{END_TIME}}</p>
              <p style="margin: 0;">- <strong>Địa điểm:</strong> {{COURT_NAME}} - {{COURT_ADDRESS}}</p>
              <p style="margin: 10px 0 0 0;">- <strong>Tình trạng:</strong> <span style="color: #bd2426; font-weight: bold;">{{ATTENDANCE_STATUS}}</span></p>
              <p style="margin: 10px 0 0 0;">Nếu có bất kỳ thắc mắc nào về tình trạng điểm danh hoặc lịch tập, vui lòng liên hệ với chúng tôi qua email hoặc hotline. Chúng tôi rất mong nhận được sự hợp tác từ quý phụ huynh để đảm bảo con bạn tham gia đầy đủ các buổi tập.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-05-04T09:12:53.000' AS DateTime), CAST(N'2025-05-04T11:19:01.440' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REQUIRE_PARENT_PAYMENT', N'[YHBT] Thông báo: Yêu cầu thanh toán quỹ đội cho con', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;"><strong>THÔNG BÁO ĐÓNG QUỸ ĐỘI BÓNG RỔ CHO HỌC VIÊN</strong></p>
              <p style="margin: 0px;">Kính gửi Quý phụ huynh <strong>{{TEN_PHU_HUYNH}},</strong></p>
              <p style="margin: 0px;">Câu lạc bộ Bóng rổ YHBT trân trọng thông báo về khoản quỹ hoạt động dành cho học viên <strong>{{TEN_HOC_VIEN}}</strong> - Đội <strong>{{LOP_HOC}}</strong> tháng <strong>{{THANG_NAM}}</strong>.</p>

              <!-- Payment Information Section -->
              <p style="margin: 0px;"><strong>THÔNG TIN THANH TOÁN</strong></p>
              <ul style="line-height: 28px;">
                <li>Học viên: <strong>{{TEN_HOC_VIEN}}</strong></li>
                <li>Số tiền: <strong>{{SO_TIEN}} VNĐ</strong></li>
                <li>Hạn thanh toán: <strong>{{HAN_THANH_TOAN}}</strong></li>
                <li>Nội dung: Quỹ CLB tháng <strong>{{THANG_NAM}}</strong></li>
                <li>Trạng thái: <strong>CHƯA THANH TOÁN</strong></li>
              </ul>
              <p style="margin: 0px;">Vui lòng thực hiện thanh toán qua một trong các hình thức sau:</p>

              <!-- Online Payment Button Section -->
              <div align="center">
                <a href="{{ONLINE_PAYMENT_LINK}}" target="_blank" style="box-sizing: border-box; display: inline-block; text-decoration: none; text-align: center; color: rgb(255, 255, 255); background: rgb(189, 36, 38); border-radius: 4px; width: 45%; max-width: 100%; word-break: break-word; overflow-wrap: break-word; font-family: Arial, sans-serif; font-size: 20px; font-weight: 400;">
                  <span style="display:block;padding:10px 20px;">
                    <span style="font-size: 14px; line-height: 14px;">THANH TOÁN TRỰC TUYẾN</span>
                  </span>
                </a>
              </div>

              <!-- Important Notes Section -->
              <p style="margin: 10px 0 0 0;"><strong>LƯU Ý QUAN TRỌNG:</strong></p>
              <ul style="line-height: 28px;">
                <li>Vui lòng hoàn tất thanh toán trước ngày <strong>{{HAN_THANH_TOAN}}</strong></li>
                <li>Học viên sẽ tạm ngừng tập luyện nếu chậm thanh toán quá 7 ngày</li>
                <li>Liên hệ ngay với chúng tôi nếu cần gia hạn thời gian thanh toán</li>
              </ul>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-12T02:43:14.000' AS DateTime), CAST(N'2025-05-04T11:19:01.500' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'REQUIRE_PLAYER_PAYMENT', N'[YHBT] Thông báo: Yêu cầu thanh toán quỹ đội', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <!-- Greeting Section -->
              <p style="margin: 0px;">Kính gửi thành viên <strong>{{TEN_HOC_VIEN}},</strong></p>
              <p style="margin: 0px;">Câu lạc bộ Bóng rổ YHBT thông báo về khoản quỹ hoạt động tháng <strong>{{THANG_NAM}}</strong> cần được đóng góp.</p>

              <!-- Payment Information Section -->
              <p style="margin: 0px;"><strong>Thông tin thanh toán:</strong></p>
              <ul style="line-height: 28px;">
                <li>Số tiền: <strong>{{SO_TIEN}} VNĐ</strong></li>
                <li>Hạn thanh toán: <strong>{{HAN_THANH_TOAN}}</strong></li>
                <li>Trạng thái: <strong>Chưa thanh toán</strong></li>
              </ul>
              <p style="margin: 0px;">Hãy truy cập nút bên dưới để:</p>
              <ul style="line-height: 28px;">
                <li>Xem chi tiết khoản quỹ đội</li>
                <li>Kiểm tra trạng thái thanh toán của bạn</li>
                <li>Thực hiện thanh toán trực tuyến</li>
              </ul>

              <!-- Online Payment Button Section -->
              <div align="center">
                <a href="{{ONLINE_PAYMENT_LINK}}" target="_blank" style="box-sizing: border-box; display: inline-block; text-decoration: none; text-align: center; color: rgb(255, 255, 255); background: rgb(189, 36, 38); border-radius: 4px; width: 45%; max-width: 100%; word-break: break-word; overflow-wrap: break-word; font-family: Arial, sans-serif; font-size: 20px; font-weight: 400;">
                  <span style="display:block;padding:10px 20px;">
                    <span style="font-size: 14px; line-height: 14px;">TRUY CẬP TRANG THANH TOÁN</span>
                  </span>
                </a>
              </div>

              <!-- Important Notes Section -->
              <p style="margin: 10px 0 0 0;"><strong>Lưu ý quan trọng:</strong></p>
              <ul style="line-height: 28px;">
                <li>Vui lòng hoàn tất thanh toán trước ngày <strong>{{HAN_THANH_TOAN}}</strong></li>
              </ul>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-11T21:51:31.000' AS DateTime), CAST(N'2025-05-04T11:19:01.553' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'RESET_PASSWORD_SUCCESS', N'[YHBT] Thông báo: Mật khẩu đã được đặt lại thành công', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <p style="margin: 0px;"><strong>Xin chào {{USER_FULLNAME}},</strong></p>
              <p style="margin: 0px;">Chúng tôi xin thông báo rằng mật khẩu cho tài khoản của bạn tại hệ thống quản lý học viện bóng rổ YHBT đã được đặt lại thành công vào lúc <strong>{{CHANGE_DATE}}</strong> bởi <strong>{{ACTION_USERNAME}}</strong> ({{ACTION_FULLNAME}}).</p>
              <p style="margin: 0px;">Đây là mật khẩu mới của bạn: <strong>{{NEW_PASSWORD}}</strong></p>
              <p style="margin: 0px;">Hãy nhớ sử dụng mật khẩu mới khi đăng nhập lần sau.</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nếu cần hỗ trợ, vui lòng liên hệ quản lý của bạn.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-07T10:38:51.000' AS DateTime), CAST(N'2025-05-04T11:19:01.610' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'SEND_FORM_REGISTRATION_SUCCESS', N'[YHBT] Thông báo: Đơn đăng ký đã được gửi thành công', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <p style="margin: 0px;">Kính gửi {{USER_NAME}},</p>
              <p style="margin: 0px;">Thay mặt câu lạc bộ bóng rổ Yên Hòa BasketBall, chúng tôi xin chân thành cảm ơn bạn đã gửi đơn đăng ký tham gia đội với vị trí <strong>{{ROLE_CODE}}</strong>.</p>
              <p style="margin: 0px; font-weight: bold; color: #bd2426;">QUAN TRỌNG: Nhớ tham dự buổi phỏng vấn.</p>
              <p style="margin: 0px;">Đơn đăng ký của bạn đã được tiếp nhận thành công. Vui lòng chuẩn bị tham dự buổi phỏng vấn theo thông tin đã được gửi đến email của bạn.</p>
              <p style="margin: 0px;">Chúng tôi đánh giá cao sự quan tâm của bạn và mong muốn được gặp bạn tại buổi phỏng vấn để tìm hiểu thêm về tinh thần đồng đội và kỹ năng của bạn.</p>
              <p style="margin: 0px;">Nếu bạn có bất kỳ câu hỏi nào, xin đừng ngần ngại liên hệ với chúng tôi qua email hoặc số điện thoại đã cung cấp.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-11T12:00:31.000' AS DateTime), CAST(N'2025-05-04T11:19:01.657' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'UPDATE_TRAINING_SESSION', N'[YHBT] Thông báo: Thay đổi lịch tập ngày', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <p style="margin: 0px;">Xin chào các thành viên của <strong>{{TEAM_NAME}}</strong>,</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px;">Nhằm đảm bảo kế hoạch tập luyện phù hợp hơn, chúng tôi xin thông báo về <strong>sự thay đổi lịch tập</strong> của buổi tập<strong> {{OLD_DATE}} </strong>từ<strong> {{OLD_START_TIME}} </strong>đến<strong> {{OLD_END_TIME}} </strong>được đổi thành như sau:</p>
              <ul>
                <li style="line-height: 19.6px;">
                  <p style="margin: 0px;"><strong>Thời gian: {{NEW_DATE}} </strong>từ <strong>{{NEW_START_TIME}} </strong>đến <strong>{{NEW_END_TIME}}</strong>.</p>
                </li>
                <li style="line-height: 19.6px;">
                  <p style="margin: 0px;"><strong>Địa điểm:</strong> <strong>{{NEW_COURT}}</strong>.</p>
                </li>
              </ul>
              <p style="margin: 0px;">Rất mong mọi người sắp xếp thời gian và tham gia đầy đủ. Nếu có thắc mắc hoặc không thể tham gia, vui lòng liên hệ <strong>{{UPDATED_BY}}</strong>.</p>
              <p style="margin: 0px;"><strong>Trân trọng,</strong><br />YHBT</p>
              <p style="margin: 0px;"> </p>
              <p style="margin: 0px; color: #8d8c8c;"><em>*Đây là email được gửi tự động bởi hệ thống, vui lòng không trả lời email này.</em></p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-04-15T09:48:48.000' AS DateTime), CAST(N'2025-05-04T11:19:01.710' AS DateTime))
INSERT [dbo].[MailTemplate] ([MailTemplateId], [TemplateTitle], [Content], [CreatedDate], [UpdatedAt]) VALUES (N'VERIFY_EMAIL_REGISTRATION', N'[YHBT] Thông báo: Mã xác minh email đăng ký', N'<!DOCTYPE HTML
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
  xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
  <!--[if gte mso 9]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="x-apple-disable-message-reformatting">
  <!--[if !mso]><!-->
  <meta http-equiv="X-UA-Compatible" content="IE=edge"><!--<![endif]-->
  <title></title>

  <style type="text/css">
    @media only screen and (min-width: 620px) {
      .u-row {
        width: 600px !important;
      }

      .u-row .u-col {
        vertical-align: top;
      }


      .u-row .u-col-100 {
        width: 600px !important;
      }

    }

    @media only screen and (max-width: 620px) {
      .u-row-container {
        max-width: 100% !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
      }

      .u-row {
        width: 100% !important;
      }

      .u-row .u-col {
        display: block !important;
        width: 100% !important;
        min-width: 320px !important;
        max-width: 100% !important;
      }

      .u-row .u-col>div {
        margin: 0 auto;
      }


      .u-row .u-col img {
        max-width: 100% !important;
      }

    }

    body {
      margin: 0;
      padding: 0
    }

    table,
    td,
    tr {
      border-collapse: collapse;
      vertical-align: top
    }

    .ie-container table,
    .mso-container table {
      table-layout: fixed
    }

    * {
      line-height: inherit
    }

    a[x-apple-data-detectors=true] {
      color: inherit !important;
      text-decoration: none !important
    }


    @media (max-width: 480px) {
      .hide-mobile {

        max-height: 0px;
        overflow: hidden;

        display: none !important;
      }
    }


    table,
    td {
      color: #000000;
    }

    #u_body a {
      color: #e67e23;
      text-decoration: underline;
    }

    @media (max-width: 480px) {
      #u_content_divider_2 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-container-padding-padding {
        padding: 10px !important;
      }

      #u_content_image_12 .v-src-width {
        width: 30% !important;
      }

      #u_content_image_12 .v-src-max-width {
        max-width: 30% !important;
      }

      #u_content_button_2 .v-size-width {
        width: 70% !important;
      }
    }
  </style>

  <link href="https://fonts.googleapis.com/css?family=Cabin:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Lato:400,700&display=swap" rel="stylesheet" type="text/css">
  <!--<![endif]-->
</head>

<body class="clean-body u_body"
  style="margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #bd2426;color: #000000">
  <table id="u_body"
    style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #bd2426;width:100%"
    cellpadding="0" cellspacing="0">
    <tbody>
      <tr style="vertical-align: top">
        <td style="word-break: break-word;border-collapse: collapse !important;vertical-align: top">
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <!--<![endif]-->
                      <table id="u_content_divider_2" class="hide-mobile" style="font-family:''Lato'',sans-serif;"
                        role="presentation" cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="background-color: #ffffff;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table id="u_content_image_12" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td style="padding-right: 0px;padding-left: 0px;" align="center">
                                    <img align="center" border="0"
                                      src="https://res.cloudinary.com/duzrv35z5/image/upload/v1740731989/BAMS/yjy2e2iisztqjsjlva4a.png"
                                      alt="" title=""
                                      style="outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 20%;max-width: 116px;"
                                      width="116" class="v-src-width v-src-max-width" />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">

                              <table role="presentation" height="0px" align="center" border="0" cellpadding="0"
                                cellspacing="0" width="49%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>

                            </td>
                          </tr>
                        </tbody>
                      </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                      width="100%" border="0">
                      <tbody>

<tr>
      <td style="padding: 25px 35px; font-size: 16px; line-height: 1.6; background-color: #ffffff;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="padding: 10px 0; font-family: Arial, sans-serif; line-height: 28px; text-align: justify;">
              <p style="margin: 0; font-weight: bold;">Xác thực email đăng ký của bạn</p>
              <p style="margin: 0;">Bạn vừa yêu cầu một mã OTP để xác thực email đăng ký Học viện Bóng rổ YHBT. Vui lòng sử dụng mã OTP bên dưới để tiếp tục:</p>
              <p style="margin: 0; font-size: 18px; font-weight: bold; color: #bd2426;">{{OTP_CODE}}</p>
              <p style="margin: 0;">Lưu ý: Mã OTP có hiệu lực trong 10 phút. Vui lòng nhập mã trước khi hết hạn để hoàn tất quá trình xác thực.</p>
              <p style="margin: 0;">Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email này.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>


                        <!-- =========================================================================== -->
                      </tbody>
                    </table>

                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:40px 40px 30px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <div
                                style="font-size: 14px; color: #333333; letter-spacing: 0px; line-height: 200%; text-align: left; word-wrap: break-word;">
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;"><strong data-end="503"
                                      data-start="488">Tr&acirc;n trọng,</strong></span><br data-end="506"
                                    data-start="503" /><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px;">YHBT</span></p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;">&nbsp;</p>
                                <p style="font-size: 14px; line-height: 200%; margin: 0px;"><span
                                    style="font-family: Cabin, sans-serif; line-height: 28px; color: #8d8c8c;"><em>*Đ&acirc;y
                                      l&agrave; email được gửi tự động bởi hệ thống, vui l&ograve;ng kh&ocirc;ng trả lời
                                      email n&agrave;y.</em></span></p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <table style="font-family:''Lato'',sans-serif;" role="presentation" cellpadding="0" cellspacing="0"
                        width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:15px 10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="55%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td style="background-color: #d3d3d3; padding: 20px; font-size: 12px; color: #666666; text-align: center;">
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- Support Info -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Thông tin hỗ trợ học viên</p>
                                    <p style="margin: 5px 0;">Hotline: <a href="tel:0987654321" style="color: #666666; text-decoration: none;">0987 654 321</a></p>
                                    <p style="margin: 5px 0;">Email: <a href="mailto:yhbasketballteam@gmail.com" style="color: #666666; text-decoration: none;">yhbasketballteam@gmail.com</a></p>
                                    <p style="margin: 5px 0;">Website: <a href="{{URL_GO_TO_PAGE}}" target="_blank" style="color: #333333; text-decoration: none;">{{URL_GO_TO_PAGE}}</a></p>
                                    <p style="margin: 5px 0;">Địa chỉ: THPT Yên Hòa, 251 Đ. Nguyễn Khang, Yên Hòa, Cầu Giấy, Hà Nội</p>
                                  </td>
                                </tr>

                                <!-- Social Media -->
                                <tr>
                                  <td style="padding: 10px; text-align: left;">
                                    <p style="margin: 0; font-weight: bold;">Kết nối với chúng tôi</p>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                      <tr>
                                        <td style="padding: 5px; text-align: left;">
                                          <a href="https://fb.com/yenhoabasketballteam" target="_blank" style="text-decoration: none; color: #3b5998;">
                                            <img src="https://cdn-icons-png.flaticon.com/512/733/733547.png" alt="Facebook Icon" style="width: 20px; height: 20px; vertical-align: middle; margin-right: 5px;">
                                            Fanpage Facebook
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>

                                <!-- Footer Notes -->
                                <tr>
                                  <td style="padding: 10px; text-align: center; font-size: 11px; color: #999999;">
                                    Liên hệ với chúng tôi nếu bạn cần bất kỳ hỗ trợ nào liên quan đến quá trình học tập và hoạt động tại CLB.
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="u-row-container" style="padding: 0px;background-color: transparent">
            <div class="u-row"
              style="margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;">
              <div
                style="border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;">
                <div class="u-col u-col-100"
                  style="max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;">
                  <div
                    style="height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                    <div
                      style="box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;">
                      <table class="hide-mobile" style="font-family:''Lato'',sans-serif;" role="presentation"
                        cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tbody>
                          <tr>
                            <td class="v-container-padding-padding"
                              style="overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:''Lato'',sans-serif;"
                              align="left">
                              <table height="0px" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                                style="border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 0px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                <tbody>
                                  <tr style="vertical-align: top">
                                    <td
                                      style="word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%">
                                      <span>&#160;</span>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</body>

</html>', CAST(N'2025-03-06T01:03:57.000' AS DateTime), CAST(N'2025-05-04T11:19:01.760' AS DateTime))
GO
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'04c82cb5-d43a-4e6a-b610-aacbb50d2297', N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', N'TPBank', N'0344665098', 0, N'970423')
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'093ef81d-ea11-459a-9fc7-11e77e32f3c6', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'2a066892-c0e4-4544-aed7-ced3111f9dd4', N'9fedde57-97a7-4c26-a5c5-423c0edf13be', N'TP Bank', N'0344665098', 0, N'970423')
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'Ngân hàng TMCP Tiên Phong', N'0344665098', 0, N'970423')
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'3a78d638-a0ce-445f-b392-24c175807f5d', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'54d10dff-edcd-4646-a8c8-5ff7c2796f45', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'59058d3d-d96b-4cba-a31c-ec6199c1c148', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'95019a52-79ee-42bd-92b9-c9dfae4c9a0d', N'9fedde57-97a7-4c26-a5c5-423c0edf13be', NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'abba1a67-d0ed-4d5b-8be3-013b3af6b75c', N'9fedde57-97a7-4c26-a5c5-423c0edf13be', NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'd82df62c-a230-48cb-ac68-9cc8d3482325', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'e9c24726-f3ca-48bf-98fc-26ffe0caf3be', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'eb398293-ac6f-4586-a6a1-1174df259f9a', N'30517058-1cb6-4ede-8818-bfe559e479cc', NULL, NULL, NULL, NULL)
INSERT [dbo].[Manager] ([UserId], [TeamId], [BankName], [BankAccountNumber], [PaymentMethod], [BankBinId]) VALUES (N'ecf50c37-3464-42e2-8a1b-bb2052895b69', NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ManagerRegistration] ON 

INSERT [dbo].[ManagerRegistration] ([ManagerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [KnowledgeAboutAManager], [ExperienceAsAManager], [Strength], [WeaknessAndItSolution], [Status], [SubmitedDate]) VALUES (1, 1, N'Đỗ Đức Hùng', N'THPT YÊN HÒA', N'0987983018', N'anw10907@toaik.com', N'https://www.facebook.com/tranhiwp?__tn__=%3C1', N'<p>qua bạn bè1</p>', N'<p>tôi muốn chơi thể thao và quản lý đội1</p>', N'<p>tôi giỏi làm quản lý1</p>', N'<p>tôi rất có kinh nghiệp1</p>', N'<p>ham ăn1</p>', N'<p>hơi lười nhưng sẽ cố khắc phục1</p>', N'Pending', CAST(N'2025-05-04T17:33:24.130' AS DateTime))
SET IDENTITY_INSERT [dbo].[ManagerRegistration] OFF
GO
SET IDENTITY_INSERT [dbo].[Match] ON 

INSERT [dbo].[Match] ([MatchId], [MatchName], [MatchDate], [HomeTeamId], [AwayTeamId], [OpponentTeamName], [CourtId], [ScoreHome], [ScoreAway], [Status], [CreatedByCoachId]) VALUES (1, N'Giao hữu đầu mùa', CAST(N'2025-05-06T14:02:00.000' AS DateTime), N'30517058-1cb6-4ede-8818-bfe559e479cc', N'9fedde57-97a7-4c26-a5c5-423c0edf13be', NULL, N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 20, 50, 2, N'37d46080-f527-43a8-8d33-cc9902f9b5e0')
INSERT [dbo].[Match] ([MatchId], [MatchName], [MatchDate], [HomeTeamId], [AwayTeamId], [OpponentTeamName], [CourtId], [ScoreHome], [ScoreAway], [Status], [CreatedByCoachId]) VALUES (2, N'Giao hữu', CAST(N'2025-05-09T17:00:00.000' AS DateTime), NULL, N'30517058-1cb6-4ede-8818-bfe559e479cc', N'FPT', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', 0, 0, 0, N'37d46080-f527-43a8-8d33-cc9902f9b5e0')
SET IDENTITY_INSERT [dbo].[Match] OFF
GO
SET IDENTITY_INSERT [dbo].[MatchLineup] ON 

INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (1, 1, 1, N'02dbf8d2-982d-40d3-893e-15b8fe85a90a')
INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (3, 1, 1, N'10aaa2ec-ed9b-4c41-99b6-1341ba482431')
INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (4, 1, 1, N'11bf8993-8b96-47a4-a8be-6621f12c9d18')
INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (5, 1, 1, N'0b952eb5-3ce9-4a7b-b11c-9c03ea4e5045')
INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (6, 1, 1, N'151c8f06-3d12-4590-b5c1-fa786aa8f2b1')
INSERT [dbo].[MatchLineup] ([LineupId], [MatchId], [IsStarting], [PlayerId]) VALUES (8, 1, 0, N'1e318d37-02e2-468d-b1bb-e94d6cb7bd3c')
SET IDENTITY_INSERT [dbo].[MatchLineup] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberRegistrationSession] ON 

INSERT [dbo].[MemberRegistrationSession] ([MemberRegistrationSessionId], [RegistrationName], [StartDate], [EndDate], [IsAllowPlayerRecruit], [IsAllowManagerRecruit], [CreatedAt], [UpdatedAt], [IsEnable], [Description]) VALUES (1, N'Đợt tuyển quân tháng 5 năm 2025', CAST(N'2025-05-01T00:00:00.000' AS DateTime), CAST(N'2025-05-31T00:00:00.000' AS DateTime), 1, 1, CAST(N'2025-05-04T21:11:41.630' AS DateTime), NULL, 1, N'CLB Yên Hoà cần tuyển quân.')
SET IDENTITY_INSERT [dbo].[MemberRegistrationSession] OFF
GO
INSERT [dbo].[Parent] ([UserId], [CitizenId], [CreatedByManagerId]) VALUES (N'12cc1e69-c4e4-474c-9eb3-d3a740203f29', N'0402040603534', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
INSERT [dbo].[Parent] ([UserId], [CitizenId], [CreatedByManagerId]) VALUES (N'1db84fa4-807c-45d4-8a6e-6bd3adf42961', N'00123456789', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
INSERT [dbo].[Parent] ([UserId], [CitizenId], [CreatedByManagerId]) VALUES (N'62845780-4fb6-403c-b54a-2cc2f42c2b34', N'001987654321', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
INSERT [dbo].[Parent] ([UserId], [CitizenId], [CreatedByManagerId]) VALUES (N'ab396bb3-d8e0-4012-9c08-404577f5e7d4', N'035204000572', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
INSERT [dbo].[Parent] ([UserId], [CitizenId], [CreatedByManagerId]) VALUES (N'fe05c691-4649-4c4c-b0f1-e8eca761ed0c', N'001333555777', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
GO
INSERT [dbo].[Payment] ([PaymentId], [TeamFundId], [UserId], [Status], [PaidDate], [Note], [DueDate], [PaymentMethod]) VALUES (N'YHB129', N'9aa5cbd7-78cf-4c10-b564-7295e5f67161', N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', 0, NULL, N'Tiền ăn', CAST(N'2025-05-11T00:09:41.000' AS DateTime), NULL)
INSERT [dbo].[Payment] ([PaymentId], [TeamFundId], [UserId], [Status], [PaidDate], [Note], [DueDate], [PaymentMethod]) VALUES (N'YHBT0NLRD', N'936eaf99-0470-4909-9080-ecccd525d582', N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', 0, NULL, N'', CAST(N'2025-05-17T08:01:25.067' AS DateTime), NULL)
INSERT [dbo].[Payment] ([PaymentId], [TeamFundId], [UserId], [Status], [PaidDate], [Note], [DueDate], [PaymentMethod]) VALUES (N'YHBT2TGGK', N'553cf450-6534b-44ab-9c32-444493fdc71', N'00b4a569-ad4a-41d2-97bf-16d25a736023', 1, CAST(N'2025-05-07T03:35:00.110' AS DateTime), N'', CAST(N'2025-05-17T02:37:26.747' AS DateTime), 2)
INSERT [dbo].[Payment] ([PaymentId], [TeamFundId], [UserId], [Status], [PaidDate], [Note], [DueDate], [PaymentMethod]) VALUES (N'YHBT4UN5K', N'936eaf99-0470-4909-9080-ecccd525d582', N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', 0, NULL, N'', CAST(N'2025-05-17T08:01:25.070' AS DateTime), NULL)
INSERT [dbo].[Payment] ([PaymentId], [TeamFundId], [UserId], [Status], [PaidDate], [Note], [DueDate], [PaymentMethod]) VALUES (N'YHBTZH4AU', N'553cf450-6534b-44ab-9c32-444493fdc71', N'0f751cc0-81b4-4549-9155-26bf20bce4f2', 1, CAST(N'2025-05-07T02:46:07.623' AS DateTime), N'', CAST(N'2025-05-17T02:37:26.750' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[PaymentItem] ON 

INSERT [dbo].[PaymentItem] ([PaymentItemId], [PaymentId], [PaidItemName], [Amount], [Note]) VALUES (130, N'YHBT0NLRD', N'TIền test 2', CAST(10000 AS Decimal(10, 0)), N'Thanh toán ngày 09/05/2025 - tổng 20,000 VND')
INSERT [dbo].[PaymentItem] ([PaymentItemId], [PaymentId], [PaidItemName], [Amount], [Note]) VALUES (131, N'YHBT0NLRD', N'tiền test 1', CAST(10000 AS Decimal(10, 0)), N'Thanh toán ngày 08/05/2025 - tổng 10,000 VND')
INSERT [dbo].[PaymentItem] ([PaymentItemId], [PaymentId], [PaidItemName], [Amount], [Note]) VALUES (132, N'YHBT4UN5K', N'TIền test 2', CAST(10000 AS Decimal(10, 0)), N'Thanh toán ngày 09/05/2025 - tổng 20,000 VND')
SET IDENTITY_INSERT [dbo].[PaymentItem] OFF
GO
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'00b4a569-ad4a-41d2-97bf-16d25a736023', NULL, N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', NULL, CAST(62.57 AS Decimal(10, 2)), CAST(167.50 AS Decimal(10, 2)), N'PG', NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'00e54246-00f7-436f-abc0-c5b9f8e9df68', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', NULL, N'30517058-1cb6-4ede-8818-bfe559e479cc', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'0b952eb5-3ce9-4a7b-b11c-9c03ea4e5045', NULL, N'9fedde57-97a7-4c26-a5c5-423c0edf13be', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'0f751cc0-81b4-4549-9155-26bf20bce4f2', NULL, N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'10aaa2ec-ed9b-4c41-99b6-1341ba482431', NULL, N'30517058-1cb6-4ede-8818-bfe559e479cc', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'11bf8993-8b96-47a4-a8be-6621f12c9d18', N'ab396bb3-d8e0-4012-9c08-404577f5e7d4', NULL, N'con trai', CAST(65.00 AS Decimal(10, 2)), CAST(175.00 AS Decimal(10, 2)), N'C', NULL, CAST(N'2025-05-05' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', NULL, N'30517058-1cb6-4ede-8818-bfe559e479cc', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'151c8f06-3d12-4590-b5c1-fa786aa8f2b1', NULL, N'9fedde57-97a7-4c26-a5c5-423c0edf13be', NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1bd94322-b192-4bf9-bc15-b91fe5461ee3', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1c528fb0-ddf3-4648-bef2-8b8f78712cc6', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1d35fd6f-4ed0-479d-8aa4-1750a0973a53', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1e318d37-02e2-468d-b1bb-e94d6cb7bd3c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1f0e5627-988a-4313-8714-58081655488a', N'1db84fa4-807c-45d4-8a6e-6bd3adf42961', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'1f7425c5-8f4c-49d1-b00e-e5b3ca2a3721', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'24ddf986-343a-405e-a7a7-e6d8034407e7', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'25d9eb39-dc99-46cf-9555-c9551e19341a', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'276f2611-0c1e-4961-81e3-d38a85236e33', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'39f54802-86ea-4f90-b3fb-a7431447363f', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'3bfdf73f-ad7c-42f6-9290-b134a93fbc3b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'3cbd6e5b-a81d-415c-b331-2e3be3c0a204', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'3da537d5-9d3e-4b8c-ba8b-adce4158f6fc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'4dc7efdd-8d0e-4d97-952e-c3338adbe7eb', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'4debbdf1-a48c-4be1-8bd4-b38b6f4e891b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'5447a7a3-94ee-4270-9dc4-2737e98a3848', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'55990e2c-6b6e-4f88-a063-7dd4aa8455be', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'55cd4ff1-793d-4f03-a0af-d5927f4c7f5b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'5c2832ff-2962-4f83-a0eb-d6048cceeab8', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'6207b009-8f03-4190-843b-bcbba1766b97', N'12cc1e69-c4e4-474c-9eb3-d3a740203f29', NULL, N'Con', CAST(63.00 AS Decimal(10, 2)), CAST(163.00 AS Decimal(10, 2)), N'SG', NULL, CAST(N'2025-05-07' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'62aa1aa0-cf83-4f01-9809-a7915623ec5e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'64121129-6cbe-4cba-8802-39329c321b46', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'656c54b4-0fb2-446c-97d3-4b879c199438', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'65ee1f78-9f75-4453-9bf5-cf3558b5173a', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'673d0367-25b4-41b3-b3ac-043848279be1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'681e3bdf-22d2-4223-b464-5df60516c51c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'72793f52-a593-49a4-a339-6ad490cbc067', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'78799558-9b36-4bce-89a2-39298efff3d3', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'78acc318-5b69-4d23-a1d8-d7176f36da2c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'7a46cc87-750e-431a-8d3f-6a7ba099e5c0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'7d8fb40b-a4d3-49ef-a1d8-f6bd130a192d', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8064ed30-50c5-4c43-b56e-fb32c74c2261', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8589820a-b1ef-4377-8f61-544f398bf237', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'85f6e82a-4eca-4fe1-934a-08c1e5c0b33b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8ad8b8ca-8808-4d5e-9500-0189aa3ac6ad', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8b3ebe4a-ee51-4f4e-ba19-8d2c25ad3719', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8bfd5166-79e5-4f8d-ae40-cc681cb4bb64', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'8d542ecd-4068-466f-bf3c-ff0f27a2d48b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'9400440d-4e1e-4666-ada7-31544a4eacf9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'9d7378c6-032c-4bd3-808a-9773cbab1b24', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'9fc6f8d3-615c-4b1f-acb9-333aa3916a9e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'a5238a01-fde9-4a73-88c3-7d6e007bdd9c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'a94b4871-5711-4ed2-a20a-365b70735763', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'ab605003-b80c-49ae-988b-185151ad6113', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'ae5be2af-83b0-47e8-baac-0e5b69cd24f7', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'b02fbf06-8564-4c52-b024-b581b500f2f4', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'c74b4513-0ec3-4101-a813-cd9aab10f49f', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'cb36552e-1446-45b4-a76a-2a20b0603728', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'cb7c3cb1-16fe-4579-9793-b51864303f39', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'd98b60ca-9293-4909-883b-83ed2d4f7906', N'fe05c691-4649-4c4c-b0f1-e8eca761ed0c', NULL, N'Con trai', CAST(60.00 AS Decimal(10, 2)), CAST(180.00 AS Decimal(10, 2)), N'SG', NULL, CAST(N'2025-05-06' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'dd6eec27-fe40-45cc-b3e6-99e2cc19a6a9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'dfc79fe2-5d67-4698-8189-c087b6400bce', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'eb8b08e9-ce2b-402d-8b26-f1d640e8e764', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'f7744d89-9622-415d-b0c3-0ec0739b9af1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
INSERT [dbo].[Player] ([UserId], [ParentId], [TeamId], [RelationshipWithParent], [Weight], [Height], [Position], [ShirtNumber], [ClubJoinDate]) VALUES (N'fefd3ff9-cd81-459f-b417-ea3eab683887', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2025-05-04' AS Date))
GO
SET IDENTITY_INSERT [dbo].[PlayerRegistration] ON 

INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (1, 1, N'Trần Văn Hiệp', N'K17 - ĐH FPT', N'0839699073', N'hieptvhe173252@fpt.edu.vn', 1, CAST(N'2003-06-23' AS Date), CAST(163.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'https://www.facebook.com/tranhiwp', N'<p>Tôi yêu YHBT</p>', N'<p>Tôi yêu YHBT</p>', N'SF', N'<p>Tôi yêu YHBT</p>', N'<p>Không có</p>', N'Trần Hiệp PH', N'0853743846', N'hieptv24@gmail.com', N'Con', N'0402040603534', 2, NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:41.017' AS DateTime), N'Scored', CAST(N'2025-05-04T21:16:35.707' AS DateTime), CAST(N'2025-05-07T02:00:00.000' AS DateTime), N'THPT Yên Hoà')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (2, 1, N'Nguyễn Huy Hoàng', N'K59 - THPT Yên Hòa', N'091234567', N'tainghe171475@fpt.edu.vn', 1, CAST(N'2004-06-01' AS Date), CAST(180.00 AS Decimal(10, 2)), CAST(70.00 AS Decimal(10, 2)), N'fb.com', N'<p>thích chơi bóng</p>', N'<p>thích chơi bóng</p>', N'PG', N'thích chơi bóng', N'<p>thích chơi bóng</p>', N'Huy Nguyên', N'0912365498', N'phuhuynh@gmail.com', N'Con', N'00123456789', NULL, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:53:33.213' AS DateTime), N'Approved', CAST(N'2025-05-04T21:44:23.877' AS DateTime), CAST(N'2025-05-13T02:00:00.000' AS DateTime), N'THPT Yên Hòa')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (3, 1, N'Minh Anh', N'anh', N'0334934332', N'hgwfkjyx@tempmail.id.vn', 1, CAST(N'1986-06-16' AS Date), CAST(150.00 AS Decimal(10, 2)), CAST(45.00 AS Decimal(10, 2)), N'', N'm', N'nh', N'SG', N'<p>hj</p>', N'<p>l</p>', N'anh', N'0334934331', N'mtdgbom0@tempmail.id.vn', N'anh', N'111111111111', 3, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T12:48:40.863' AS DateTime), N'Checked-in', CAST(N'2025-05-05T01:49:49.197' AS DateTime), CAST(N'2025-05-06T02:00:00.000' AS DateTime), N'THPT Yên Hòa')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (4, 1, N'Lương Hữu Tài', N'K12-THPT Yên Hòa', N'0344858585', N'swpgroup4se1742@gmail.com', 0, CAST(N'2011-05-16' AS Date), CAST(180.00 AS Decimal(10, 2)), CAST(65.00 AS Decimal(10, 2)), N'https://www.facebook.com/groups/1849366168772823', N'<p>fb</p>', N'<p>thích</p>', N'SG', N'<p>10 năm</p>', N'<p>vô địch U13</p>', N'Lương Bằng Quang', N'0334934335', N'caddee@gmail.com', N'con ', N'035204000738', 4, NULL, NULL, NULL, N'Called', CAST(N'2025-05-05T01:52:27.083' AS DateTime), CAST(N'2025-05-15T09:00:00.000' AS DateTime), N'THPT Yên Hòa')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (5, 1, N'Hoàng Trung Anh', N'K7-YenHoa', N'0876245125', N'vomow83973@javbing.com', 0, CAST(N'2009-07-08' AS Date), CAST(175.00 AS Decimal(10, 2)), CAST(65.00 AS Decimal(10, 2)), N'https://www.facebook.com/kin.hoangvan.5', N'<p>bạn bè giới thiệu</p>', N'<p>muốn phát triển bản thân</p>', N'C', N'<p>chơi bóng rổ từ bé</p>', N'<p>vô địch U13</p>', N'Hoàng Hữu Bắc', N'0768925763', N'bangHH@gmail.com', N'con trai', N'035204000572', NULL, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:30:31.850' AS DateTime), N'Approved', CAST(N'2025-05-05T17:11:09.623' AS DateTime), CAST(N'2025-05-10T09:00:00.000' AS DateTime), N'THPT Yên Hòa')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (6, 1, N'Hoàng Trung Anh', N'K7-YenHoa', N'0876245128', N'vomows83973@javbing.com', 0, CAST(N'2009-07-08' AS Date), CAST(175.00 AS Decimal(10, 2)), CAST(65.00 AS Decimal(10, 2)), N'https://www.facebook.com/kin.hoangvan.5', N'<p>bạn bè giới thiệu</p>', N'<p>muốn phát triển bản thân</p>', N'C', N'<p>chơi bóng rổ từ bé</p>', N'<p>vô địch U13</p>', N'Hoàng Hữu Bắc', N'0768929763', N'baxngHH@gmail.com', N'con trai', N'0352040005728', NULL, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:25:33.793' AS DateTime), N'Pending', CAST(N'2025-05-05T17:11:09.623' AS DateTime), NULL, N'string')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (7, 1, N'Nguyễn Hiếu', N'K56', N'0912345678', N'conghieu8899@gmail.com', 1, CAST(N'2025-04-09' AS Date), CAST(180.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'fb.com', N'<p><strong>hello</strong></p>', N'<p><strong>hello</strong></p>', N'SG', N'<p>không có</p>', N'<p>không có</p>', N'Nguyễn Phụ', N'0912365498', N'tainghe280703@gmail.com', N'Con trai', N'001333555777', NULL, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:14:56.487' AS DateTime), N'Approved', CAST(N'2025-05-06T04:08:51.243' AS DateTime), CAST(N'2025-05-08T15:00:00.000' AS DateTime), N'THPT Yên Hòa')
INSERT [dbo].[PlayerRegistration] ([PlayerRegistrationId], [MemberRegistrationSessionId], [FullName], [GenerationAndSchoolName], [PhoneNumber], [Email], [Gender], [DateOfBirth], [Height], [Weight], [FacebookProfileURL], [KnowledgeAboutAcademy], [ReasonToChooseUs], [Position], [Experience], [Achievement], [ParentName], [ParentPhoneNumber], [ParentEmail], [RelationshipWithParent], [ParentCitizenId], [CandidateNumber], [TryOutNote], [FormReviewedBy], [ReviewedDate], [Status], [SubmitedDate], [TryOutDate], [TryOutLocation]) VALUES (8, 1, N'Trần Hiệp DEmo', N'K17 - ĐH FPT', N'0839699073', N'hieptran.pa@gmail.com', 1, CAST(N'2009-05-27' AS Date), CAST(163.00 AS Decimal(10, 2)), CAST(63.00 AS Decimal(10, 2)), N'https://www.facebook.com/tranhiwp', N'<p>Tôi yêu YHBT</p>', N'<p>Tôi yêu YHBT</p>', N'SG', N'<p>Tôi yêu YHBT</p>', N'<p>Tôi yêu YHBT</p>', N'Trần Hiệp PH', N'0837866782', N'hieptv24a@gmail.com', N'Con', N'0402040603534', 5, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:29:52.227' AS DateTime), N'Approved', CAST(N'2025-05-07T11:25:30.157' AS DateTime), CAST(N'2025-05-16T07:00:00.000' AS DateTime), N'THPT Yên Hoà')
SET IDENTITY_INSERT [dbo].[PlayerRegistration] OFF
GO
INSERT [dbo].[President] ([UserId], [Generation]) VALUES (N'0', 0)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [Status], [CreateAt], [FundManagerId]) VALUES (N'20fd3ce1-4ca9-4681-9ed4-bf0afb5e6a8d', N'Nam Cựu', 2, CAST(N'2025-05-04T23:39:05.620' AS DateTime), NULL)
INSERT [dbo].[Team] ([TeamId], [TeamName], [Status], [CreateAt], [FundManagerId]) VALUES (N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', N'Nữ A', 1, CAST(N'2025-05-04T19:00:41.287' AS DateTime), N'04c82cb5-d43a-4e6a-b610-aacbb50d2297')
INSERT [dbo].[Team] ([TeamId], [TeamName], [Status], [CreateAt], [FundManagerId]) VALUES (N'30517058-1cb6-4ede-8818-bfe559e479cc', N'Nam A', 1, CAST(N'2025-05-04T19:00:31.210' AS DateTime), N'35cca793-2166-45fe-a865-9b2c6ea2f8b6')
INSERT [dbo].[Team] ([TeamId], [TeamName], [Status], [CreateAt], [FundManagerId]) VALUES (N'9fedde57-97a7-4c26-a5c5-423c0edf13be', N'Nam B', 1, CAST(N'2025-05-04T19:00:37.540' AS DateTime), NULL)
INSERT [dbo].[Team] ([TeamId], [TeamName], [Status], [CreateAt], [FundManagerId]) VALUES (N'e3bbae96-5677-4920-b044-a7751a870230', N'Nam Nhi Đồng', 2, CAST(N'2025-05-04T23:48:35.343' AS DateTime), NULL)
GO
INSERT [dbo].[TeamFund] ([TeamFundId], [TeamId], [StartDate], [EndDate], [Status], [Description], [ApprovedAt]) VALUES (N'553cf450-6534b-44ab-9c32-444493fdc71', N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', CAST(N'2025-05-01' AS Date), CAST(N'2025-05-31' AS Date), 0, N'Tiền Quỹ Tháng 5', NULL)
INSERT [dbo].[TeamFund] ([TeamFundId], [TeamId], [StartDate], [EndDate], [Status], [Description], [ApprovedAt]) VALUES (N'936eaf99-0470-4909-9080-ecccd525d582', N'30517058-1cb6-4ede-8818-bfe559e479cc', CAST(N'2025-05-01' AS Date), CAST(N'2025-05-31' AS Date), 1, N'Tiền Quỹ Tháng 5', CAST(N'2025-05-07T07:56:05.180' AS DateTime))
INSERT [dbo].[TeamFund] ([TeamFundId], [TeamId], [StartDate], [EndDate], [Status], [Description], [ApprovedAt]) VALUES (N'9aa5cbd7-78cf-4c10-b564-7295e5f67161', N'30517058-1cb6-4ede-8818-bfe559e479cc', CAST(N'2025-04-01' AS Date), CAST(N'2025-04-30' AS Date), 0, N'Tiền Quỹ Tháng 4', NULL)
INSERT [dbo].[TeamFund] ([TeamFundId], [TeamId], [StartDate], [EndDate], [Status], [Description], [ApprovedAt]) VALUES (N'e83cf450-486b-44ab-9c32-599d193fdc71', N'9fedde57-97a7-4c26-a5c5-423c0edf13be', CAST(N'2025-05-01' AS Date), CAST(N'2025-05-31' AS Date), 0, N'Tiền Quỹ Tháng 5', NULL)
GO
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'000f7042-ff2d-4dcd-89aa-1cfeacffa29f', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-05-31' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.777' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.403' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'01eca457-286c-4b15-b724-dd99c8c9e702', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-06-02' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T16:05:23.537' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T16:05:23.607' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'020bcc20-da96-4c7b-aab1-3c295754c0f2', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-07-14' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.637' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.400' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'05aa29bf-76c7-4cde-8caf-da1757ca9169', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'b7bee54c-be4f-4d51-a5f5-ff5d8c0b6cc8', 0, CAST(N'2025-05-06' AS Date), CAST(N'15:00:00' AS Time), CAST(N'23:59:17' AS Time), CAST(N'2025-05-05T20:34:26.517' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T01:24:24.570' AS DateTime), NULL, CAST(100000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'07eea66a-96c0-47f8-8190-fd098bb352b4', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-06-28' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.890' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:07.920' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'08f1236c-cb07-4771-9c6d-bdd33502b363', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-05-12' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.473' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.560' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'10703c91-b12a-4f2d-aaeb-f37060701920', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', 0, CAST(N'2025-05-22' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:19:19.930' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:19:19.957' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'12ba0609-ebef-4122-9bde-21eb44af9ce7', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-06-21' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.877' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.403' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'1ee8206b-e377-47f0-9c16-d288634b6fc2', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-06-04' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.767' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.620' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'2359df80-3352-4407-b193-1a3ba12951f9', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-05-21' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.717' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.677' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'26288db3-6a1a-4762-b480-b3fd136b5d09', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-06-14' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.857' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.403' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'2653cf1a-bc24-4b5b-8515-7e7f6a19283c', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-06-18' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.797' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.737' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'2b830d2d-5e5d-437f-a086-24a969c311ff', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-08-06' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.873' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.787' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'2ca5430e-3c69-43bf-9c2e-325c62268f96', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-07-21' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.653' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.847' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'426262b0-37ad-4e3c-a0f2-25fe5ab0af17', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-06-25' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.807' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.907' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'27644042-489c-43a4-9500-85ed95bc811a', 1, CAST(N'2025-05-08' AS Date), CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time), CAST(N'2025-05-06T01:21:44.873' AS DateTime), CAST(N'2025-05-06T23:47:45.067' AS DateTime), N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T01:33:04.160' AS DateTime), NULL, CAST(900000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'49482a2e-7b9f-422d-8fc7-effbe6894553', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 1, CAST(N'2025-05-05' AS Date), CAST(N'01:20:00' AS Time), CAST(N'23:30:00' AS Time), CAST(N'2025-05-04T21:58:40.193' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T22:01:01.700' AS DateTime), NULL, CAST(750000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'4fe18b68-3e00-4448-bb88-2e3695c2ca29', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', 0, CAST(N'2025-05-29' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:19:19.943' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:19:19.983' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'506d4075-6702-40f8-be4d-98e404c90ac0', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-05-14' AS Date), CAST(N'02:00:00' AS Time), CAST(N'04:30:00' AS Time), CAST(N'2025-05-06T04:20:19.963' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:20:19.997' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'52da2d32-bb86-4805-be21-7a8c8badc7ad', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-06-04' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.010' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:07.053' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'55da766b-924e-4ddd-a5b2-3a48c7559871', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'f6f2e75f-6a19-49ed-b13b-3c14cbc64b83', 0, CAST(N'2025-05-17' AS Date), CAST(N'16:00:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'2025-05-07T11:44:05.420' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-07T23:44:05.423' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'56335eeb-318a-47b6-a4e6-cafd2f136a45', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-05-19' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.497' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:31.963' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'577ea1eb-4aa2-44d5-b1d0-651748d03f1f', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-07-28' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.667' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.030' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'6b2452ca-0c1e-496b-8d72-c25db4cbccac', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-06-16' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.563' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.087' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'6ee21c43-0ec1-4c20-95e9-5d324fc253be', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-05-10' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.107' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.403' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'70ba4930-f639-4dba-9733-a23e47057b17', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-08-04' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.683' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.140' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'7d7b26f7-0231-4c72-830d-ee12cecacc74', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-06-23' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.590' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.213' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'7e11bf5e-fe80-44df-a452-a142bcf6f247', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-05-28' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.750' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.273' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'806610c3-e734-484e-b536-497229b44e6e', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-28' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:06.993' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.147' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'82f1516c-2901-4688-aac8-f263ff429fa2', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'27644042-489c-43a4-9500-85ed95bc811a', 0, CAST(N'2025-05-15' AS Date), CAST(N'16:00:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'2025-05-06T23:36:44.050' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T23:36:54.330' AS DateTime), NULL, CAST(225000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'8954d4dd-7f32-437a-b424-ac54716e1814', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', 0, CAST(N'2025-05-15' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:19:19.920' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T15:24:05.277' AS DateTime), N'aaa', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'8a6b47b2-1895-4047-a21b-3f54c1dd79b7', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-07-09' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.830' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.367' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'8bda3660-3ecf-489b-8a81-69fe096435cf', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-07-02' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.820' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.423' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'8c955dbe-a809-4b62-91d5-7cc3d77209a4', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'ad9531be-cc48-46d9-a412-a2577ec839e7', 0, CAST(N'2025-05-12' AS Date), CAST(N'04:00:00' AS Time), CAST(N'06:30:00' AS Time), CAST(N'2025-05-04T21:59:32.437' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.483' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'8d6a425a-7626-40c6-acaf-7992cd45e67e', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'27644042-489c-43a4-9500-85ed95bc811a', 1, CAST(N'2025-05-04' AS Date), CAST(N'20:00:00' AS Time), CAST(N'22:30:00' AS Time), CAST(N'2025-05-04T21:44:18.200' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:45:17.510' AS DateTime), NULL, CAST(500000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'92cfe6b8-0759-4d1d-a09b-102d2b620216', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-06-11' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.783' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.543' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'986ea973-bd33-4f52-ae47-7077bb67ca17', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-07-30' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.863' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.600' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'a2eedf28-4a2b-4ee5-8798-090df1c9484e', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-06-30' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.603' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.657' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'a307fcfe-05b0-482c-bc8d-ef0d05ecb48a', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-06-18' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.050' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.277' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'ab22a2d6-4742-47d6-9f5e-a0090f409289', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-14' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:06.903' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:06.953' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'af7dbf33-6af1-447d-8380-4b88713d24bb', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-22' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:58:40.257' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.713' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'b36360da-4b74-4d35-8d63-d78bfc5f0006', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-13' AS Date), CAST(N'16:00:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'2025-05-07T11:43:54.303' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-07T23:43:54.537' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'b647a34f-e19f-4e91-aab0-aedba4555542', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-06-09' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.547' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.777' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'b80ccd17-5720-4dae-b81c-f1bf1a302a42', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-20' AS Date), CAST(N'16:00:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'2025-05-07T11:43:54.323' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-07T23:43:54.560' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'be84e094-1429-45a8-a599-134698cf19f1', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-07-16' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.840' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.833' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'c08b3472-42e4-4520-9fed-4262f8cc3784', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-07-07' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.620' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.887' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'cc6acaf1-6b46-4348-ab11-f7ea14db78c6', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-05-24' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.753' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:07.787' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 1, CAST(N'2025-05-07' AS Date), CAST(N'08:00:00' AS Time), CAST(N'12:30:00' AS Time), CAST(N'2025-05-05T04:17:07.120' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:18:24.737' AS DateTime), NULL, CAST(500000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'd0d75fa8-c38b-4266-8852-9fe5bc8fa46a', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-21' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:06.967' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.147' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'd4b5f849-8887-4d0d-904c-a4aeee81d136', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-05-14' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.703' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.940' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'd5d9db2f-1504-4d87-b870-9d0baea2eba6', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-06-25' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.067' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.277' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'da0d11a0-7a69-4df7-9af6-90935d06a118', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-06-11' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.033' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:08.277' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'e0209110-df5a-41ac-a4cd-a23984af785a', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'27644042-489c-43a4-9500-85ed95bc811a', 0, CAST(N'2025-05-07' AS Date), CAST(N'08:00:00' AS Time), CAST(N'11:30:00' AS Time), CAST(N'2025-05-05T16:22:26.933' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:38:57.220' AS DateTime), NULL, CAST(525000 AS Decimal(10, 0)))
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'e366cd05-6bd1-4dff-94ff-2e45f127a3c0', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'781131ed-2b33-4be3-8252-d3fad87b8df4', 0, CAST(N'2025-06-07' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.833' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:07.867' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'e559601c-3cc9-4ed6-b1b1-df4f283b7a8b', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-15' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:58:40.217' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:32.993' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'ebe5d152-a3be-47fc-9660-ad385ccac973', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-09-04' AS Date), CAST(N'16:00:00' AS Time), CAST(N'19:30:00' AS Time), CAST(N'2025-05-06T15:13:29.537' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T21:25:37.083' AS DateTime), N'can be happen', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'ec0f64ca-e08f-4528-a693-b3167ab721d1', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-07-02' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:17:07.080' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:17:07.113' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'f118de6e-513e-424d-a108-ee80d488a78f', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', 0, CAST(N'2025-06-05' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-06T04:19:19.953' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-06T16:19:19.987' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'f5c95f1b-7e13-4351-97bc-72007e6dca1d', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'88ec0f27-f00f-4061-a87c-c18bf2d98638', 0, CAST(N'2025-07-23' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.853' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:33.053' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'f651b397-9e29-4a82-8585-f560d37db140', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-05-29' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:58:40.283' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:33.103' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'f66d0373-a447-4cc9-b5ad-fe9d586dc04f', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'991f75c7-60ff-4364-92bb-d87f7062fe6a', 0, CAST(N'2025-05-26' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:57:23.513' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:33.167' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'fa9cae05-211a-4a37-a8b3-2fb0ce88d5eb', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'2252a202-4a49-4974-801c-fc00c9895698', 0, CAST(N'2025-06-05' AS Date), CAST(N'16:00:00' AS Time), CAST(N'18:30:00' AS Time), CAST(N'2025-05-04T21:58:40.303' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-05T17:34:33.220' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
INSERT [dbo].[TrainingSession] ([TrainingSessionId], [TeamId], [CourtId], [Status], [ScheduledDate], [StartTime], [EndTime], [CreatedAt], [UpdatedAt], [CreatedByUserId], [CreatedDecisionByManagerId], [CreatedDecisionAt], [CreateRejectedReason], [CourtPrice]) VALUES (N'fde7a241-a17f-4fd0-b3bc-f2eac0546fde', N'30517058-1cb6-4ede-8818-bfe559e479cc', N'f6f2e75f-6a19-49ed-b13b-3c14cbc64b83', 0, CAST(N'2025-05-24' AS Date), CAST(N'16:00:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'2025-05-07T11:44:05.433' AS DateTime), NULL, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', NULL, CAST(N'2025-05-07T23:44:05.440' AS DateTime), N'Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL)
GO
SET IDENTITY_INSERT [dbo].[TrainingSessionStatusChangeRequest] ON 

INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (1, N'e0209110-df5a-41ac-a4cd-a23984af785a', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 0, N'trời mưa to', NULL, NULL, NULL, NULL, CAST(N'2025-05-05T20:20:19.600' AS DateTime), 1, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T20:30:46.940' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (2, N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 0, N'FPT bawts', NULL, NULL, NULL, NULL, CAST(N'2025-05-06T15:12:46.977' AS DateTime), 0, N'Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL, CAST(N'2025-05-06T23:14:07.443' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (3, N'cf01d0fe-1caf-49ac-9861-cad53f4328f3', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 1, N'Thích đổi', N'781131ed-2b33-4be3-8252-d3fad87b8df4', CAST(N'2025-05-17' AS Date), CAST(N'04:00:00' AS Time), CAST(N'07:30:00' AS Time), CAST(N'2025-05-06T15:18:56.133' AS DateTime), 0, N'Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL, CAST(N'2025-05-06T23:33:59.183' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (4, N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 1, N'Sân cũ bảo trì', N'aed58716-3e97-4bf5-a98a-f50eb2c0a5b8', CAST(N'2025-05-08' AS Date), CAST(N'08:00:00' AS Time), CAST(N'10:30:00' AS Time), CAST(N'2025-05-06T23:35:05.153' AS DateTime), 1, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T23:38:22.427' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (5, N'82f1516c-2901-4688-aac8-f263ff429fa2', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 0, N'Bận rồi', NULL, NULL, NULL, NULL, CAST(N'2025-05-06T23:37:51.990' AS DateTime), 0, N'Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL, CAST(N'2025-05-07T07:37:52.083' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (6, N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 1, N'Sân cũ bảo trì', N'ad9531be-cc48-46d9-a412-a2577ec839e7', CAST(N'2025-05-08' AS Date), CAST(N'08:00:00' AS Time), CAST(N'10:30:00' AS Time), CAST(N'2025-05-06T23:40:16.497' AS DateTime), 1, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T23:46:25.363' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (7, N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 1, N'Sân cũ bảo trì', N'27644042-489c-43a4-9500-85ed95bc811a', CAST(N'2025-05-08' AS Date), CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time), CAST(N'2025-05-06T23:47:18.217' AS DateTime), 1, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T23:47:45.073' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (8, N'4563e476-7b98-4294-a77b-bf5bf150ef61', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 1, N'Sân cũ bảo trì', N'ad9531be-cc48-46d9-a412-a2577ec839e7', CAST(N'2025-05-08' AS Date), CAST(N'03:00:00' AS Time), CAST(N'05:00:00' AS Time), CAST(N'2025-05-07T00:42:47.287' AS DateTime), 0, N'Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.', NULL, CAST(N'2025-05-07T08:42:47.307' AS DateTime))
INSERT [dbo].[TrainingSessionStatusChangeRequest] ([StatusChangeRequestId], [TrainingSessionId], [RequestedByCoachId], [RequestType], [RequestReason], [NewCourtId], [NewScheduledDate], [NewStartTime], [NewEndTime], [RequestedAt], [Status], [RejectedReason], [DecisionByManagerId], [DecisionAt]) VALUES (9, N'82f1516c-2901-4688-aac8-f263ff429fa2', N'37d46080-f527-43a8-8d33-cc9902f9b5e0', 0, N'HLV Ốm', NULL, NULL, NULL, NULL, CAST(N'2025-05-07T08:15:29.873' AS DateTime), 1, NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T08:16:23.497' AS DateTime))
SET IDENTITY_INSERT [dbo].[TrainingSessionStatusChangeRequest] OFF
GO
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'AgilityTest', N'Nhanh nhẹn', N'<p>Agility Test 5-10-5 ( t&iacute;nh Gi&acirc;y)</p>
<p>Thực hiện: 01 lần.</p>', NULL, N'<p>sân 3-3</p>', N'<p>Khả năng di chuyển</p>', N'<p>Đồng hồ, 3 nấm cọc</p>', NULL, N'PhysicalFitness', 18)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Attitude', N'Thái độ (TĐ)', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', NULL, NULL, NULL, NULL, N'Scrimmage', 7)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'BasketballIQ', N'Tư duy (IQ)', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', NULL, NULL, NULL, NULL, N'Scrimmage', 11)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'BasketballSkill', N'Kỹ năng bóng rổ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Dribble', N'Dẫn bóng', N'<ul><li>Stationary ( Pound; Pound + cross/double cross/ BTL/ BTB); 2 ball.</li>
  <li>Moving: Luồn cọc; zig zag.</li>
  <li>Reaction with tennis ball.</li>
  <li>Tag dribbling 1on1; 2on2.</li>
</ul>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', N'<p>Góc sân</p>', N'<ul>
  <li>Stationary ( dẫn b&oacute;ng tại chỗ) thực hiện c&aacute;c động t&aacute;c trong 30s/ t&iacute;nh số lần nhanh nhất thực hiện được ở mỗi động t&aacute;c ( người đếm l&agrave; c&aacute;c bạn ở lượt tiếp theo)</li>
  <li>Moving ( thực hiện dẫn b&oacute;ng di chuyển luồn cọc) với c&aacute;c kĩ thuật giống dẫn b&oacute;ng tại chỗ ( thực hiện mỗi kĩ thuật 2 lượt tr&aacute;i-phải)</li>
  <li>Reaction tennis ball ( dẫn b&oacute;ng kết hợp tung bắt với b&oacute;ng tennis): Tập theo cặp với 1 người tung b&oacute;ng cho bắt</li>
  <li>Tag dribbling ( dẫn b&oacute;ng ph&aacute; v&agrave; bảo vệ): 1on1; 2on2 theo khu vực v&ograve;ng tr&ograve;n giới hạn,</li>
</ul>', N'<p>Số lượng bóng cần 04 quả</p>', N'<ul>
  <li><em><strong>Tốt ( T)</strong></em>: Thực hiện li&ecirc;n tục, c&oacute; nhịp điệu, chắc v&agrave; hướng mắt hướng rộng xung quanh.</li>
  <li><em><strong>Kh&aacute; ( K)</strong></em>: Thực hiện li&ecirc;n tục, đ&ocirc;i l&uacute;c hỏng, hướng mắt l&uacute;c l&ecirc;n l&uacute;c nh&igrave;n xuống.</li>
  <li><em><strong>Trung b&igrave;nh ( TB)</strong></em>: Thực hiện hỏng, lập bập, chưa thể ngước l&ecirc;n quan s&aacute;t.</li>
</ul>', N'BasketballSkill', 2)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Finishing', N'Kết thúc rổ', N'<p>Kết thúc rổ ( lên rổ, móc rổ, thả rổ, lên rổ 2 tay tính thời gian)</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', N'<p>Góc sân</p>', N'<ul>
  <li>L&ecirc;n rổ/ m&oacute;c rổ/ floater ( thả rổ) tr&aacute;i- phải/ Power layup/ Giậm nhảy kết th&uacute;c rổ với 2 ch&acirc;n bật ( floater) Mỗi kĩ thuật thực hiện: 5 lượt.</li>
  <li>Kết th&uacute;c rổ ( l&ecirc;n rổ, m&oacute;c rổ, thả rổ, l&ecirc;n rổ 2 tay t&iacute;nh thời gian).</li>
</ul>', N'<p>Số lượng bóng : 02 quả</p>', N'<ul>
  <li><em><strong>Tốt ( T):</strong></em> Thực hiện ho&agrave;n chỉnh v&agrave; hiệu suất.</li>
  <li><strong><em>Kh&aacute; ( K):</em></strong> Thực hiện vững kĩ thuật, hiệu suất chưa cao.</li>
  <li><em><strong>Trung b&igrave;nh ( TB):</strong></em> Thực hiện hỏng, lập bập, chưa thể th&agrave;nh c&ocirc;ng nhiều lần.</li>
</ul>', N'BasketballSkill', 5)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'HexagonTest', N'Hexagon Test', N'<p>Bật nhảy 2 ch&acirc;n Hexagon Test Thực hiện: 01 lần.</p>', NULL, N'<p>sân 3-3</p>', N'<p>Test khả năng phối hợp của thân dưới</p>', N'<p>Băng dính xanh, thước dây</p>', NULL, N'PhysicalFitness', 13)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Leadership', N'Lãnh đạo', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', NULL, NULL, NULL, NULL, N'Scrimmage', 8)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'LeftSidePlank', N'Trái', N'<p>Plank phía trái (tính số lần)</p>', NULL, NULL, NULL, NULL, NULL, N'SidePlank', 24)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Passing', N'Chuyền bóng', N'<ul>
  <li>Chest pass</li>
  <li>Bounce pass</li>
  <li>Overhead pass</li>
  <li>1 hand left/right pass)</li>
  <li>Hook pass/ behind the back pass</li>
  <li>Tag pass</li>
</ul>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', N'<p>Góc sân</p>', N'<ul>
  <li>Chuyền b&oacute;ng: Trước ngực/ đập đất/ sau đầu/ 1 tay tr&aacute;i/phải/ chuyền nghi&ecirc;ng/ sau lưng. Mỗi kĩ thuật chuyền tổng qua lại 30 lần.</li>
  <li>Chuyền b&oacute;ng ma: Theo nh&oacute;m 4 người với 1 người bắt; theo nh&oacute;m 8 người 3 người bắt. ( hoạt động k&eacute;o d&agrave;i 2-3 ph&uacute;t rồi chuyển)</li>
</ul>', N'<p>Số lượng bóng : 04 quả</p>', N'<ul>
  <li><strong><em>Tốt ( T):</em> </strong>Thực hiện li&ecirc;n tục, chuyền c&oacute; lực, ch&iacute;nh x&aacute;c.</li>
  <li><em><strong>Kh&aacute; ( K): </strong></em>Thực hiện li&ecirc;n tục, đ&ocirc;i l&uacute;c hỏng.</li>
  <li><em><strong>Trung b&igrave;nh ( TB):</strong></em> Thực hiện hỏng, chuyền kh&ocirc;ng đủ lực, hướng b&oacute;ng đi kh&ocirc;ng ch&iacute;nh x&aacute;c.</li>
</ul>', N'BasketballSkill', 3)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'PhysicalFitness', N'Thể lực', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'PlankTest', N'Plank', N'<p>Plank (tính số lần)</p>', NULL, N'<p>Sân 3-3</p>', N'<p>Sức mạnh</p>', N'<p>Đồng hồ, thảm yoga (3-5 chiếc)</p>', NULL, N'PhysicalFitness', 21)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'PushUp', N'Chống đẩy', N'<p>Push up (tính số lần)</p>', NULL, N'<p>Sân 3-3</p>', N'<p>Sức mạnh</p>', N'<p>Đồng hồ, thảm yoga (3-5 chiếc)</p>', NULL, N'PhysicalFitness', 20)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'RightSidePlank', N'Phải', N'<p>Plank phía phải (tính số lần)</p>', NULL, NULL, NULL, NULL, NULL, N'SidePlank', 23)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'RunningVerticalJump', N'Có đà', N'<p>Bật nhảy với cao</p>
<ul>
  <li>C&oacute; đ&agrave;</li>
</ul>
<p>Thực&nbsp;hiện: 02 lần lấy th&agrave;nh t&iacute;ch cao nhất.</p>', NULL, NULL, NULL, NULL, NULL, N'VerticalJump', 17)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Scrimmage', N'Đấu tập', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', N'<p>2 rổ ( chia 16 bạn/ rổ)</p>', N'<ul>
  <li>1on1/2on2/3on3 ( Chia 2/3 h&agrave;ng thi đấu nh&oacute;m bất k&igrave;) - ( 4 ph&uacute;t)</li>
  <li>5on5 ( chia 4 đội 8 bạn) thi đấu 5 ph&uacute;t giờ tr&ocirc;i mỗi trận. Tổng số: 4 trận.</li>
</ul>', N'<ul>
  <li>Số lượng b&oacute;ng:&nbsp;01 quả</li>
  <li>&Aacute;o chiến thuật: 5 chiếc</li>
  <li>Ch&acirc;n quay&nbsp;ghi h&igrave;nh</li>
</ul>', N'<ul>
  <li>Mức 1: Yếu</li>
  <li>Mức 2: K&eacute;m</li>
  <li>Mức 3: Trung b&igrave;nh</li>
  <li>Mức 4: Kh&aacute;</li>
  <li>Mức 5: Tốt C&aacute;c ti&ecirc;u ch&iacute;: Th&aacute;i độ ( TĐ); Vai tr&ograve; l&atilde;nh đạo ( LS); Kĩ năng ( KN); Thể lực ( TL); Tư duy thi đấu ( IQ). V&iacute; dụ: Nguyễn Văn A - TĐ: 1 - LS: 5; KN: 4; TL: 3; IQ: 3.</li>
</ul>', N'BasketballSkill', 6)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'ScrimmagePhysicalFitness', N'Thể lực (TL)', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', NULL, NULL, NULL, NULL, N'Scrimmage', 10)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Shooting', N'Ném rổ', N'<ul>
  <li>Catch &amp; shoot</li>
  <li>1 dribble pull up</li>
  <li>live ball handling shot</li>
  <li>Transistion shot</li>
</ul>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', N'<p>1 rổ</p>', N'<ol>
  <li>2/2/2&nbsp;VỚI ( 2 quả bắt v&agrave; n&eacute;m; 2 quả với 1 đập qua người n&eacute;m tr&aacute;i/phải v&agrave; 2 quả n&eacute;m 3 điểm) - thực hiện 6 quả/ lượt</li>
  <li>N&eacute;m rổ kết hợp xử l&yacute; b&oacute;ng ( Cross/BTL/BTB + pull up) - thực hiện 6 quả/ lượt</li>
  <li>Transistion shot ( n&eacute;m rổ phản c&ocirc;ng) di chuyển từ cuối s&acirc;n l&ecirc;n giữa s&acirc;n rồi chạy tăng tốc về vị tr&iacute; n&eacute;m tại khu vực trung b&igrave;nh 02 quả/ khu vực 3 điểm 02 quả n&eacute;m rổ. Thực hiện 6 quả/ lượt.</li>
</ol>', N'<p>Số lượng bóng : 04 quả</p>', N'<ul>
  <li><em><strong>Tốt ( T):</strong></em> Thực hiện c&oacute; nhịp điệu, c&oacute; kĩ thuật, hiệu suất v&agrave;o tương đối.</li>
  <li><em><strong>Kh&aacute; ( K):</strong></em> Thực hiện c&oacute; nhịp điệu, đảm bảo kĩ thuật, hiệu suất c&ograve;n chưa nhiều</li>
  <li><em><strong>Trung b&igrave;nh ( TB): </strong></em>Thực hiện hỏng, kh&ocirc;ng c&oacute; nhịp điệu, kĩ thuật chưa ổn, n&eacute;m trượt nhiều.</li>
</ul>', N'BasketballSkill', 4)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'SidePlank', N'Plank một phía', NULL, NULL, NULL, NULL, NULL, NULL, N'PlankTest', 22)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Skills', N'Kỹ năng (KN)', N'<p>- 1on1; 2on2; 3on3; 5on5.</p>', N'<p>Dự kiến: 5 ph&uacute;t ( Cho mỗi trạm) x 4 lượt</p>
<p>C&aacute;c&nbsp;HLV lưu &yacute; thời gian thực hiện, trong trường hợp bị l&acirc;u qu&aacute;, ch&uacute;ng ta sẽ cho chuyển nội dung để cố gắng thực hiện đủ hết c&aacute;c nội dung.</p>
<p>Tổng thời gian kết th&uacute;c mỗi nh&oacute;m l&agrave; 40 ph&uacute;t/ nh&oacute;m.</p>
<p>Ở lượt tiếp theo, BTC sẽ gọi loa c&aacute;c nh&oacute;m chuẩn bị v&agrave;o tham gia theo đ&uacute;ng lượt của m&igrave;nh.</p>', NULL, NULL, NULL, NULL, N'Scrimmage', 9)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'Sprint', N'Chạy nước rút', N'<p>Chạy 3/4 s&acirc;n</p>
<p>Thực&nbsp;hiện: 01 lần.</p>', NULL, N'<p>Biên dọc sân 5-5</p>', N'<p>Tốc độ</p>', N'<p>4 nấm cọc, đồng hồ</p>', NULL, N'PhysicalFitness', 19)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'StandardPlank', N'Plank Thường', N'<p>Plank giữa (tính số lần)</p>', NULL, NULL, NULL, NULL, NULL, N'PlankTest', 21)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'StandingBroadJump', N'Bật xa tại chỗ', N'<p>Thực hiện: 01 lần.</p>', NULL, N'<p>sân 3-3</p>', N'<p>Sức mạnh bộc phát</p>', N'<p>Thước dây</p>', NULL, N'PhysicalFitness', 14)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'StandingVerticalJump', N'Không đà', N'<p>Bật nhảy với cao</p>
<ul>
  <li>Kh&ocirc;ng đ&agrave;</li>
</ul>
<p>Thực&nbsp;hiện: 02 lần lấy th&agrave;nh t&iacute;ch cao nhất.</p>', NULL, NULL, NULL, NULL, NULL, N'VerticalJump', 16)
INSERT [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode], [MeasurementName], [Content], [Duration], [Location], [Description], [Equipment], [MeasurementScale], [ParentMeasurementScaleCode], [SortOrder]) VALUES (N'VerticalJump', N'Bật cao', N'<p>Bật nhảy với cao</p>
<ul>
  <li>Kh&ocirc;ng đ&agrave;</li>
  <li>C&oacute; đ&agrave;</li>
</ul>
<p>Thực&nbsp;hiện: 02 lần lấy th&agrave;nh t&iacute;ch cao nhất.</p>', NULL, N'<p>Góc tường cạnh nhà thể chất cầu lông</p>', N'<p>Sức mạnh bộc phát</p>', N'<p>Thang, Băng dính xanh, Thước dây</p>', NULL, N'PhysicalFitness', 15)
GO
SET IDENTITY_INSERT [dbo].[TryOutScorecard] ON 

INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (1, 1, N'Dribble', N'T', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.740' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (2, 1, N'Passing', N'TB', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.793' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (3, 1, N'Shooting', N'K', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.803' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (4, 1, N'Finishing', N'TB', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.817' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (5, 1, N'Attitude', N'2', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.830' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (6, 1, N'Leadership', N'3', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.840' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (7, 1, N'Skills', N'4', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.847' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (8, 1, N'ScrimmagePhysicalFitness', N'3', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.857' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (9, 1, N'BasketballIQ', N'4', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.867' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (10, 1, N'HexagonTest', N'23', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.877' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (11, 1, N'StandingBroadJump', N'100', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.883' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (12, 1, N'StandingVerticalJump', N'50', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.893' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (13, 1, N'RunningVerticalJump', N'60', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.900' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (14, 1, N'AgilityTest', N'25', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.910' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (15, 1, N'Sprint', N'15', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.923' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (16, 1, N'PushUp', N'50', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.933' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (17, 1, N'StandardPlank', N'45', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.940' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (18, 1, N'LeftSidePlank', N'45', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.950' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (19, 1, N'RightSidePlank', N'76', NULL, N'd82df62c-a230-48cb-ac68-9cc8d3482325', CAST(N'2025-05-04T21:22:40.960' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (20, 2, N'Dribble', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.303' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (21, 2, N'Passing', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.313' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (22, 2, N'Shooting', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.323' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (23, 2, N'Finishing', N'K', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.330' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (24, 2, N'Attitude', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.337' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (25, 2, N'Leadership', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.343' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (26, 2, N'Skills', N'4', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.350' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (27, 2, N'ScrimmagePhysicalFitness', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.357' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (28, 2, N'BasketballIQ', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.367' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (29, 2, N'HexagonTest', N'10', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.377' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (30, 2, N'StandingBroadJump', N'60', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.387' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (31, 2, N'StandingVerticalJump', N'40', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.403' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (32, 2, N'RunningVerticalJump', N'50', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.410' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (33, 2, N'AgilityTest', N'30', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.420' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (34, 2, N'Sprint', N'10', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.430' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (35, 2, N'PushUp', N'35', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.440' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (36, 2, N'StandardPlank', N'6', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.450' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (37, 2, N'LeftSidePlank', N'7', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.460' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (38, 2, N'RightSidePlank', N'8', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-04T21:52:39.470' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (39, 5, N'Dribble', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.627' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (40, 5, N'Passing', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.663' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (41, 5, N'Shooting', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.677' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (42, 5, N'Finishing', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.687' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (43, 5, N'Attitude', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.693' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (44, 5, N'Leadership', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.700' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (45, 5, N'Skills', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.707' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (46, 5, N'ScrimmagePhysicalFitness', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.713' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (47, 5, N'BasketballIQ', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.720' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (48, 5, N'HexagonTest', N'10', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.730' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (49, 5, N'StandingBroadJump', N'300', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.737' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (50, 5, N'StandingVerticalJump', N'82', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.743' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (51, 5, N'RunningVerticalJump', N'82', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.753' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (52, 5, N'AgilityTest', N'8', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.760' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (53, 5, N'Sprint', N'4', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.767' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (54, 5, N'PushUp', N'42', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.777' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (55, 5, N'StandardPlank', N'102', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.783' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (56, 5, N'LeftSidePlank', N'102', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.790' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (57, 5, N'RightSidePlank', N'102', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-05T17:29:42.797' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (58, 7, N'HexagonTest', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.097' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (59, 7, N'StandingBroadJump', N'10', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.143' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (60, 7, N'Passing', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.157' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (61, 7, N'Shooting', N'K', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.167' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (62, 7, N'Finishing', N'TB', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.180' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (63, 7, N'Dribble', N'TB', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.190' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (64, 7, N'StandardPlank', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.203' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (65, 7, N'LeftSidePlank', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.213' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (66, 7, N'RightSidePlank', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.223' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (67, 7, N'PushUp', N'2', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.233' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (68, 7, N'Sprint', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.243' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (69, 7, N'AgilityTest', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.253' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (70, 7, N'RunningVerticalJump', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.263' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (71, 7, N'StandingVerticalJump', N'100', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.273' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (72, 7, N'Attitude', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.283' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (73, 7, N'Leadership', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.297' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (74, 7, N'Skills', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.307' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (75, 7, N'ScrimmagePhysicalFitness', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.317' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (76, 7, N'BasketballIQ', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-06T04:13:06.323' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (77, 8, N'Dribble', N'TB', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.910' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (78, 8, N'Passing', N'T', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.923' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (79, 8, N'Shooting', N'TB', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.930' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (80, 8, N'Finishing', N'TB', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.937' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (81, 8, N'Attitude', N'4', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.943' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (82, 8, N'Leadership', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.953' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (83, 8, N'Skills', N'5', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.957' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (84, 8, N'ScrimmagePhysicalFitness', N'3', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.963' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (85, 8, N'BasketballIQ', N'2', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.970' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (86, 8, N'HexagonTest', N'34', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.977' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (87, 8, N'StandingBroadJump', N'56', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.980' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (88, 8, N'StandingVerticalJump', N'45', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.987' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (89, 8, N'RunningVerticalJump', N'56', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:40.997' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (90, 8, N'AgilityTest', N'33', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.003' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (91, 8, N'Sprint', N'33', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.010' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (92, 8, N'PushUp', N'44', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.017' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (93, 8, N'StandardPlank', N'55', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.023' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (94, 8, N'LeftSidePlank', N'34', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.027' AS DateTime), NULL)
INSERT [dbo].[TryOutScorecard] ([TryOutScorecardId], [PlayerRegistrationId], [MeasurementScaleCode], [Score], [Note], [ScoredBy], [ScoredAt], [UpdatedAt]) VALUES (95, 8, N'RightSidePlank', N'22', NULL, N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', CAST(N'2025-05-07T11:27:41.033' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[TryOutScorecard] OFF
GO
SET IDENTITY_INSERT [dbo].[TryOutScoreCriteria] ON 

INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (1, N'HexagonTest', N'Bật nhảy 2 chân (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (2, N'HexagonTest', N'Bật nhảy 2 chân (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (3, N'StandingBroadJump', N'Bật xa tại chỗ (Nam)', N'cm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (4, N'StandingBroadJump', N'Bật xa tại chỗ (Nữ)', N'cm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (5, N'StandingVerticalJump', N'Bật nhảy với cao không đà (Nam)', N'cm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (6, N'StandingVerticalJump', N'Bật nhảy với cao không đà (Nữ)', N'cm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (7, N'RunningVerticalJump', N'Bật nhảy với cao có đà (Nam)', N'cm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (8, N'RunningVerticalJump', N'Bật nhảy với cao có đà (Nữ)', N'cm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (9, N'AgilityTest', N'Nhanh nhẹn (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (10, N'AgilityTest', N'Nhanh nhẹn (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (11, N'Sprint', N'Chạy nước rút 20m (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (12, N'Sprint', N'Chạy nước rút 20m (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (13, N'PushUp', N'Chống đẩy (Nam)', N'cái', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (14, N'PushUp', N'Chống đẩy (Nữ)', N'cái', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (15, N'StandardPlank', N'Plank ngửa cơ bản (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (16, N'StandardPlank', N'Plank ngửa cơ bản (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (17, N'RightSidePlank', N'Plank bên phải (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (18, N'RightSidePlank', N'Plank bên phải (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (19, N'LeftSidePlank', N'Plank bên trái (Nam)', N'giây', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (20, N'LeftSidePlank', N'Plank bên trái (Nữ)', N'giây', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (21, N'Dribble', N'Dẫn bóng (Nam)', N'mức', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (22, N'Dribble', N'Dẫn bóng (Nữ)', N'mức', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (23, N'Passing', N'Chuyền bóng (Nam)', N'mức', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (24, N'Passing', N'Chuyền bóng (Nữ)', N'mức', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (25, N'Shooting', N'Ném bóng (Nam)', N'mức', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (26, N'Shooting', N'Chuyển bóng (Nữ)', N'mức', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (27, N'Finishing', N'Kết thúc rổ (Nam)', N'mức', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (28, N'Finishing', N'Kết thúc rổ (Nữ)', N'mức', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (29, N'Attitude', N'Thái độ (Nam)', N'điểm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (30, N'Attitude', N'Thái độ (Nữ)', N'điểm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (31, N'Leadership', N'Lãnh đạo (Nam)', N'điểm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (32, N'Leadership', N'Lãnh đạo (Nữ)', N'điểm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (33, N'Skills', N'Kỹ năng (Nam)', N'điểm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (34, N'Skills', N'Kỹ năng (Nữ)', N'điểm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (35, N'ScrimmagePhysicalFitness', N'Thể lực thi đấu (Nam)', N'điểm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (36, N'ScrimmagePhysicalFitness', N'Thể lực thi đấu (Nữ)', N'điểm', 0)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (37, N'BasketballIQ', N'Tư duy (Nam)', N'điểm', 1)
INSERT [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId], [MeasurementScaleCode], [CriteriaName], [Unit], [Gender]) VALUES (38, N'BasketballIQ', N'Tư duy (Nữ)', N'điểm', 0)
SET IDENTITY_INSERT [dbo].[TryOutScoreCriteria] OFF
GO
SET IDENTITY_INSERT [dbo].[TryOutScoreLevel] ON 

INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (1, 1, NULL, CAST(11.10 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (2, 1, CAST(11.20 AS Decimal(10, 2)), CAST(13.30 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (3, 1, CAST(13.40 AS Decimal(10, 2)), CAST(15.50 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (4, 1, CAST(15.60 AS Decimal(10, 2)), CAST(17.80 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (5, 1, CAST(17.90 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (6, 2, NULL, CAST(12.10 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (7, 2, CAST(12.20 AS Decimal(10, 2)), CAST(15.30 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (8, 2, CAST(15.40 AS Decimal(10, 2)), CAST(18.50 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (9, 2, CAST(18.60 AS Decimal(10, 2)), CAST(21.80 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (10, 2, CAST(21.90 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (11, 3, CAST(251.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (12, 3, CAST(241.00 AS Decimal(10, 2)), CAST(250.00 AS Decimal(10, 2)), N'VeryGood', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (13, 3, CAST(231.00 AS Decimal(10, 2)), CAST(240.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (14, 3, CAST(221.00 AS Decimal(10, 2)), CAST(230.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (15, 3, CAST(211.00 AS Decimal(10, 2)), CAST(220.00 AS Decimal(10, 2)), N'BellowAverage', CAST(1.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (16, 3, CAST(191.00 AS Decimal(10, 2)), CAST(210.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (17, 3, NULL, CAST(190.00 AS Decimal(10, 2)), N'VeryPoor', CAST(0.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (18, 4, CAST(201.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (19, 4, CAST(191.00 AS Decimal(10, 2)), CAST(200.00 AS Decimal(10, 2)), N'VeryGood', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (20, 4, CAST(181.00 AS Decimal(10, 2)), CAST(190.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (21, 4, CAST(171.00 AS Decimal(10, 2)), CAST(180.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (22, 4, CAST(161.00 AS Decimal(10, 2)), CAST(170.00 AS Decimal(10, 2)), N'BellowAverage', CAST(1.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (23, 4, CAST(141.00 AS Decimal(10, 2)), CAST(160.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (24, 4, CAST(140.00 AS Decimal(10, 2)), NULL, N'VeryPoor', CAST(0.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (25, 5, CAST(81.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (26, 5, CAST(66.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (27, 5, CAST(56.00 AS Decimal(10, 2)), CAST(65.00 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (28, 5, CAST(46.00 AS Decimal(10, 2)), CAST(55.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (29, 5, NULL, CAST(45.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (30, 6, CAST(71.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (31, 6, CAST(56.00 AS Decimal(10, 2)), CAST(70.00 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (32, 6, CAST(46.00 AS Decimal(10, 2)), CAST(55.00 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (33, 6, CAST(36.00 AS Decimal(10, 2)), CAST(45.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (34, 6, NULL, CAST(35.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (35, 7, CAST(81.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (36, 7, CAST(66.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (37, 7, CAST(56.00 AS Decimal(10, 2)), CAST(65.00 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (38, 7, CAST(46.00 AS Decimal(10, 2)), CAST(55.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (39, 7, NULL, CAST(45.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (40, 8, CAST(71.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (41, 8, CAST(56.00 AS Decimal(10, 2)), CAST(70.00 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (42, 8, CAST(46.00 AS Decimal(10, 2)), CAST(55.00 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (43, 8, CAST(36.00 AS Decimal(10, 2)), CAST(45.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (44, 8, NULL, CAST(35.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (45, 9, NULL, CAST(9.50 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (46, 9, CAST(9.51 AS Decimal(10, 2)), CAST(10.50 AS Decimal(10, 2)), N'Good', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (47, 9, CAST(10.51 AS Decimal(10, 2)), CAST(11.50 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (48, 9, CAST(11.49 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (49, 10, NULL, CAST(10.50 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (50, 10, CAST(10.51 AS Decimal(10, 2)), CAST(11.50 AS Decimal(10, 2)), N'Good', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (51, 10, CAST(11.51 AS Decimal(10, 2)), CAST(12.50 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (52, 10, CAST(12.50 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (53, 11, NULL, CAST(4.10 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (54, 11, CAST(4.11 AS Decimal(10, 2)), CAST(4.20 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (55, 11, CAST(4.30 AS Decimal(10, 2)), CAST(4.40 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (56, 11, CAST(4.50 AS Decimal(10, 2)), CAST(4.60 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (57, 11, CAST(4.70 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (58, 12, NULL, CAST(4.40 AS Decimal(10, 2)), N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (59, 12, CAST(4.50 AS Decimal(10, 2)), CAST(4.60 AS Decimal(10, 2)), N'AboveAverage', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (60, 12, CAST(4.70 AS Decimal(10, 2)), CAST(4.80 AS Decimal(10, 2)), N'Average', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (61, 12, CAST(4.90 AS Decimal(10, 2)), CAST(5.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (62, 12, CAST(5.10 AS Decimal(10, 2)), NULL, N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (63, 15, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (64, 15, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (65, 15, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (66, 15, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (67, 15, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (68, 15, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (69, 16, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (70, 16, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (71, 16, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (72, 16, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (73, 16, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (74, 16, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (75, 17, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (76, 17, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (77, 17, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (78, 17, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (79, 17, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (80, 17, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (81, 18, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (82, 18, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (83, 18, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (84, 18, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (85, 18, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (86, 18, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (87, 19, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (88, 19, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (89, 19, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (90, 19, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (91, 19, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (92, 19, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (93, 20, CAST(101.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (94, 20, CAST(80.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (95, 20, CAST(60.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (96, 20, CAST(40.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (97, 20, CAST(20.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (98, 20, NULL, CAST(19.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (99, 21, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
GO
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (100, 21, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (101, 21, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (102, 22, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (103, 22, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (104, 22, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (105, 23, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (106, 23, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (107, 23, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (108, 24, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (109, 24, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (110, 24, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (111, 25, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (112, 25, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (113, 25, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (114, 26, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (115, 26, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (116, 26, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (117, 27, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (118, 27, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (119, 27, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (120, 28, NULL, NULL, N'T', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (121, 28, NULL, NULL, N'K', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (122, 28, NULL, NULL, N'TB', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (123, 29, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (124, 29, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (125, 29, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (126, 29, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (127, 29, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (128, 30, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (129, 30, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (130, 30, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (131, 30, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (132, 30, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (133, 31, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (134, 31, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (135, 31, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (136, 31, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (137, 31, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (138, 32, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (139, 32, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (140, 32, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (141, 32, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (142, 32, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (143, 33, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (144, 33, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (145, 33, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (146, 33, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (147, 33, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (148, 34, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (149, 34, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (150, 34, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (151, 34, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (152, 34, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (153, 35, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (154, 35, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (155, 35, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (156, 35, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (157, 35, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (158, 36, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (159, 36, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (160, 36, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (161, 36, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (162, 36, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (163, 37, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (164, 37, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (165, 37, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (166, 37, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (167, 37, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (168, 38, NULL, NULL, N'1', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (169, 38, NULL, NULL, N'2', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (170, 38, NULL, NULL, N'3', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (171, 38, NULL, NULL, N'4', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (172, 38, NULL, NULL, N'5', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (173, 13, CAST(40.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (174, 13, CAST(30.00 AS Decimal(10, 2)), CAST(39.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (175, 13, CAST(25.00 AS Decimal(10, 2)), CAST(29.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (176, 13, CAST(15.00 AS Decimal(10, 2)), CAST(24.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (177, 13, CAST(10.00 AS Decimal(10, 2)), CAST(14.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (178, 13, NULL, CAST(9.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (179, 14, CAST(25.00 AS Decimal(10, 2)), NULL, N'Excellent', CAST(5.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (180, 14, CAST(20.00 AS Decimal(10, 2)), CAST(24.00 AS Decimal(10, 2)), N'Good', CAST(4.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (181, 14, CAST(15.00 AS Decimal(10, 2)), CAST(19.00 AS Decimal(10, 2)), N'AboveAverage', CAST(3.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (182, 14, CAST(10.00 AS Decimal(10, 2)), CAST(14.00 AS Decimal(10, 2)), N'Average', CAST(2.5 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (183, 14, CAST(5.00 AS Decimal(10, 2)), CAST(9.00 AS Decimal(10, 2)), N'BellowAverage', CAST(2.0 AS Decimal(10, 1)))
INSERT [dbo].[TryOutScoreLevel] ([ScoreLevelId], [ScoreCriteriaId], [MinValue], [MaxValue], [ScoreLevel], [FivePointScaleScore]) VALUES (184, 14, NULL, CAST(4.00 AS Decimal(10, 2)), N'Poor', CAST(1.0 AS Decimal(10, 1)))
SET IDENTITY_INSERT [dbo].[TryOutScoreLevel] OFF
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'0', N'President', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'YHBT', N'president@yenhoastorm.com', NULL, N'0923476969', N'THPT Yên Hoà', CAST(N'2025-01-06' AS Date), N'President', CAST(N'2025-05-04T15:03:31.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'00b4a569-ad4a-41d2-97bf-16d25a736023', N'AnhNP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Phương Anh', N'nguyenphuonganh2k76dyh@gmail.com', NULL, N'0326116603', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2007-12-21' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'00e54246-00f7-436f-abc0-c5b9f8e9df68', N'NgocLM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Minh Ngọc', N'minhngoc07122008@gmail.com', NULL, N'0947132008', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2008-12-07' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', N'HiepTV2', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Minh Long', N'znr249121221248@toaik.com', NULL, N'0963459055', N'Ngõ 100 Yên Hòa, Cầu Giấy', CAST(N'2008-07-28' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'04c82cb5-d43a-4e6a-b610-aacbb50d2297', N'HaBN', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Bùi Ngọc Hà', N'fru04159@toaik.com', NULL, N'0343522338', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'2008-03-16' AS Date), N'Manager', CAST(N'2025-04-28T23:59:00.000' AS DateTime), CAST(N'2025-05-07T02:55:42.280' AS DateTime), 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'08847e60-0afb-4d9d-ac57-7a6bf71f4b2b', N'PhucTT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Thị Phúc', N'phuctt260880@gmail.com', NULL, N'0977913557', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1980-08-26' AS Date), N'Parent', CAST(N'2025-04-28T20:46:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'093ef81d-ea11-459a-9fc7-11e77e32f3c6', N'VyPB', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Bảo Vy', N'vypb030608@gmail.com', NULL, N'0978296308', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'2008-06-03' AS Date), N'Manager', CAST(N'2025-04-28T17:10:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'0b952eb5-3ce9-4a7b-b11c-9c03ea4e5045', N'MinhNH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Hoàng Minh', N'nguyenhoangminh09062008@gmail.com', NULL, N'0984676116', N'Ngõ 165 Trung Kính, Cầu Giấy', CAST(N'2008-06-09' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'0f751cc0-81b4-4549-9155-26bf20bce4f2', N'ChiTHH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Tạ Hồng Hà Chi ', N'tahonghachi2206@gmail.com', NULL, N'0934818881', N'Ngõ 100 Yên Hòa, Cầu Giấy', CAST(N'2009-06-22' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'10aaa2ec-ed9b-4c41-99b6-1341ba482431', N'AnhHT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Triệu Nam', N'trieunamnguyen0604@gmail.com', NULL, N'0943559550', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2007-04-06' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'11bf8993-8b96-47a4-a8be-6621f12c9d18', N'anhht2', N'$2a$11$0yx6DeOqFxEBFYA3U5UbWOvRDv8pIqNEugC1v/PCJNEzJV5/GQS4S', N'Hoàng Trung Anh', N'vomow83973@javbing.com', NULL, N'0876245125', N'N/A', CAST(N'2009-07-08' AS Date), N'Player', CAST(N'2025-05-05T17:30:31.607' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'12cc1e69-c4e4-474c-9eb3-d3a740203f29', N'phth8', N'$2a$11$FgVDETvafBVoM.CSLTb44O8cFQ9box2qNX6EjVZ0YlucQXqnVKgL2', N'Trần Hiệp PH', N'hieptv24a@gmail.com', NULL, N'0837866782', N'N/A', NULL, N'Parent', CAST(N'2025-05-07T11:29:52.227' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', N'HieuNC', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Vũ Hải', N'joz98077@toaik.com', NULL, N'0824594206', N'Ngõ 259 Yên Hòa, Cầu Giấy', CAST(N'2009-07-13' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'151c8f06-3d12-4590-b5c1-fa786aa8f2b1', N'HieuNQ', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Quang Hiếu', N'nguyenquanghieu30042008@gmail.com', NULL, N'0814842309', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2008-04-30' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'18c33107-e68f-46cd-b540-c123ed3069b3', N'annv0', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Văn An', N'peterparker.spiderman0701@gmail.com', NULL, N'0963925967', N'Ha Noi', CAST(N'2025-05-10' AS Date), N'Coach', CAST(N'2025-05-04T20:07:52.300' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1bd94322-b192-4bf9-bc15-b91fe5461ee3', N'ThanhNC', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Công Thành', N'congthanh130908@gmail.com', NULL, N'0383482993', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2008-09-13' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1c528fb0-ddf3-4648-bef2-8b8f78712cc6', N'DungNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Mạnh Dũng', N'ngmanhdung3012@gmail.com', NULL, N'0967133891', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2009-12-30' AS Date), N'Player', CAST(N'2025-04-30T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1cdc0035-9d5b-4230-961d-24a88d17daf2', N'hungdv', N'$2a$11$efFG4cXYbBu42p8Q41Y2g.72t.qOGptuLEd.LBp0.3AtRk/UQ2g1O', N'dàm vĩnh hưng', N'ecd45871@jioso.com', NULL, N'0871505050', N'AA14 Thất Sơn, Phường 15, Quận 10, Thành phố Hồ Chí Minh, Việt Nam', CAST(N'2000-05-15' AS Date), N'Coach', CAST(N'2025-05-05T09:52:03.967' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1d35fd6f-4ed0-479d-8aa4-1750a0973a53', N'AnhNQ', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Quế Anh', N'queanhnguyen210209@gmail.com', NULL, N'0356517219', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2009-02-21' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1db84fa4-807c-45d4-8a6e-6bd3adf42961', N'nguyenh', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Huy Nguyên', N'phuhuynh@gmail.com', NULL, N'0912365498', N'N/A', NULL, N'Parent', CAST(N'2025-05-04T21:53:33.173' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1e318d37-02e2-468d-b1bb-e94d6cb7bd3c', N'KietNA', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Anh Kiệt', N'anhkiet290609@gmail.com', NULL, N'0333825945', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2009-06-29' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1f0e5627-988a-4313-8714-58081655488a', N'hoangnh', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Huy Hoàng', N'tainghe171475@fpt.edu.vn', NULL, N'091234567', N'N/A', CAST(N'2004-06-01' AS Date), N'Player', CAST(N'2025-05-04T21:53:32.943' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'1f7425c5-8f4c-49d1-b00e-e5b3ca2a3721', N'AnhPN', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Nam Anh', N'namanhpham26062009@gmail.com', NULL, N'0964249308', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2009-06-26' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'24ddf986-343a-405e-a7a7-e6d8034407e7', N'ThanhLM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Minh Thành', N'bentenle100409@gmail.con', NULL, N'0978874403', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2009-04-10' AS Date), N'Player', CAST(N'2025-04-30T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'25d9eb39-dc99-46cf-9555-c9551e19341a', N'TrongHM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Minh Trọng', N'trong.hm131108@gmail.com', NULL, N'0986931828', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2008-11-13' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'2609b9b0-6a81-4dba-854e-ac2caf266375', N'KhoaTM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Minh Khoa', N'khoatm050776@gmail.com', NULL, N'0932207735', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1976-07-05' AS Date), N'Parent', CAST(N'2025-04-28T23:05:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'276f2611-0c1e-4961-81e3-d38a85236e33', N'LinhNH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nhữ Hà Linh', N'nhuhalinh080209@gmail.com', NULL, N'0859257288', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2009-02-08' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'2a066892-c0e4-4544-aed7-ced3111f9dd4', N'TrangDQ', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Quỳnh Trang', N'trangdq180707@gmail.com', NULL, N'0969363235', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'2007-07-18' AS Date), N'Manager', CAST(N'2025-04-28T13:20:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'35cca793-2166-45fe-a865-9b2c6ea2f8b6', N'HanCNB', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Chu Ngọc Bảo Hân', N'temporaryema.i.l.ad.d.r.essn@gmail.com', NULL, N'0924943075', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'2007-04-29' AS Date), N'Manager', CAST(N'2025-04-28T15:00:00.000' AS DateTime), CAST(N'2025-05-07T04:28:48.323' AS DateTime), 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'37467edf-444c-4c2f-825b-e34e7c503d3a', N'NamDQ', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Quang Nam', N'namdq020188@gmail.com', NULL, N'0954559234', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1988-01-02' AS Date), N'Parent', CAST(N'2025-04-28T23:47:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'TaiNG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Bùi Đức', N'buiduc23052003@gmail.com', NULL, N'0889950597', N'Cầu Giấy', CAST(N'2003-05-22' AS Date), N'Coach', CAST(N'2025-05-04T21:06:18.310' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'39f54802-86ea-4f90-b3fb-a7431447363f', N'LongNK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn khánh Long', N'khanhlong220109@gmail.com', NULL, N'0915344565', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2009-01-22' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'3a78d638-a0ce-445f-b392-24c175807f5d', N'ChauNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nghiêm Minh Châu', N'chaunm080307@gmail.com', NULL, N'0363376023', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'2007-03-08' AS Date), N'Manager', CAST(N'2025-04-28T18:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'3bfdf73f-ad7c-42f6-9290-b134a93fbc3b', N'MinhDN', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đinh Nhật Minh', N'nhatminh04042008@gmail.com', NULL, N'0372831661', N'Ngõ 259 Yên Hòa, Cầu Giấy', CAST(N'2008-04-04' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'3cbd6e5b-a81d-415c-b331-2e3be3c0a204', N'AnhNTT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Trần Tuệ Anh ', N'bontueanha8yh@gmail.com', NULL, N'0869271206', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2008-05-10' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'3da537d5-9d3e-4b8c-ba8b-adce4158f6fc', N'ChiDL', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đặng Linh Chi', N'linkcheese261007@gmail.com', NULL, N'036191626', N'Ngõ 165 Trung Kính, Cầu Giấy', CAST(N'2007-10-26' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'3f79e919-4bf5-4bb7-b8da-ae7d428a60b4', N'hungdd', N'$2a$11$FXZH9K8CjroUnCs4cx2ZFuUusMzCgHMxm36rTntR3RrPfPVeReOlS', N'Đỗ Đức Hùng', N'anw10907@toaik.com', NULL, N'0987983018', N'NOT UPDATE YET', NULL, N'Manager', CAST(N'2025-05-05T00:55:29.133' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'418c57ca-4ed8-4060-8f6a-eab568caee64', N'LinhNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Ngô Thanh Linh', N'linhnt120888@gmail.com', NULL, N'0943450046', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1988-08-12' AS Date), N'Parent', CAST(N'2025-04-28T12:19:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'4dc7efdd-8d0e-4d97-952e-c3338adbe7eb', N'HuyPCB', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Cao Bảo Huy', N'phamcaobaohuy1803@gmail.com', NULL, N'0899282877', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2007-03-18' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'4debbdf1-a48c-4be1-8bd4-b38b6f4e891b', N'AnhLTM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Trần Minh Anh', N'minhanhismee051209@gmail.com', NULL, N'0986963822', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2009-12-05' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'51826f45-94b4-46f9-91ac-ed6758fbe735', N'TungVV', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Vũ Văn Tùng', N'tungvv110284@gmail.com', NULL, N'0904485743', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1984-02-11' AS Date), N'Parent', CAST(N'2025-04-28T06:11:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'52ea08ae-ae0b-4319-992d-5371a4932320', N'HuongDM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Minh Hương', N'huongdm210783@gmail.com', NULL, N'0934814458', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1983-07-21' AS Date), N'Parent', CAST(N'2025-04-28T22:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'5447a7a3-94ee-4270-9dc4-2737e98a3848', N'LinhPP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Phương Linh ', N'pineapple.phamlinh25052009@gmail.com', NULL, N'0393598754', N'Ngõ 259 Yên Hòa, Cầu Giấy', CAST(N'2009-05-25' AS Date), N'Player', CAST(N'2025-04-30T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'54d10dff-edcd-4646-a8c8-5ff7c2796f45', N'AnhNVM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Vũ Minh Anh', N'anhnvm310807@gmail.com', NULL, N'0913055426', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'2007-08-31' AS Date), N'Manager', CAST(N'2025-04-28T02:32:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'55990e2c-6b6e-4f88-a063-7dd4aa8455be', N'AnhNNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Ngọc Trung Anh', N'troanhbeos230407@gmail.com', NULL, N'0961311540', N'Số 21 Trần Duy Hưng, Cầu Giấy', CAST(N'2007-04-23' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'55cd4ff1-793d-4f03-a0af-d5927f4c7f5b', N'LyNHB', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Hồng Bảo Ly', N'lynguyenhongbao1910@gmail.com', NULL, N'0984642266', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2009-10-19' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'59058d3d-d96b-4cba-a31c-ec6199c1c148', N'HuyenNK2', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Khánh Huyền', N'huyennk150108@gmail.com', NULL, N'0942018568', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'2008-01-15' AS Date), N'Manager', CAST(N'2025-04-28T21:45:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'5c2832ff-2962-4f83-a0eb-d6048cceeab8', N'TungNH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Hoàng Tùng', N'tungnofake4321@gmail.com', NULL, N'0355370209', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2007-07-23' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'5e8c30d2-b4b3-474e-bf71-7c5c6279fe36', N'LinhDT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đặng Thanh Linh', N'linhdt060885@gmail.com', NULL, N'0970716529', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1985-08-06' AS Date), N'Parent', CAST(N'2025-04-28T23:30:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'5f989406-657b-4920-b27e-91d465f648d3', N'hieuht', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Trung Hiếu', N'hieuhthe171691@fpt.edu.vn', NULL, N'0989123456', N'Hải Hậu Nam Định', CAST(N'2002-11-11' AS Date), N'Coach', CAST(N'2025-05-04T19:18:53.140' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'6207b009-8f03-4190-843b-bcbba1766b97', N'demoth', N'$2a$11$gkKydXJYtyvyBTLmSUiZCOVbWRssfFtQjEulo/JEjJThXndqRYt8C', N'Trần Hiệp DEmo', N'hieptran.pa@gmail.com', NULL, N'0839699073', N'N/A', CAST(N'2009-05-27' AS Date), N'Player', CAST(N'2025-05-07T11:29:51.450' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'62845780-4fb6-403c-b54a-2cc2f42c2b34', N'hainh', N'$2a$11$5xN571CgYzxBHXT.Kp0YiOa8FeTgFLW8kx3CEgq9lzqyJ7r7sWXny', N'Nguyễn Huy Hải', N'taing280703@gmail.com', NULL, N'0123456788', N'80 Chùa Láng, Cầu Giấy, Hà Nội', CAST(N'1989-02-09' AS Date), N'Parent', CAST(N'2025-05-05T00:57:52.950' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'62aa1aa0-cf83-4f01-9809-a7915623ec5e', N'HoaTP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Tạ Phương Hoa', N'phuonghoa12042007@gmail.com ', NULL, N'0888862939', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2007-04-12' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'64121129-6cbe-4cba-8802-39329c321b46', N'ToanNK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Khánh Toàn', N'toan07042007@gmail.com', NULL, N'0969079409', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2007-04-07' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'656c54b4-0fb2-446c-97d3-4b879c199438', N'HieuLT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Trung Hiếu', N'hieunongg161209@gmail.com', NULL, N'0784620664', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2009-12-16' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'65ee1f78-9f75-4453-9bf5-cf3558b5173a', N'TungLG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Gia Tùng', N'tung23052007@gmail.com', NULL, N'0936105427', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2007-05-23' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'673d0367-25b4-41b3-b3ac-043848279be1', N'PhuongNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Thục Phương ', N'thucphuongnguyen13112007@gmail.com', NULL, N'0966929297', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2007-11-13' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'681e3bdf-22d2-4223-b464-5df60516c51c', N'LinhNG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Gia Linh', N'linh.nguyengia211008@gmail.com', NULL, N'0376420096', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2008-10-21' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'6a6b3f7a-402e-4a3a-84c5-2e8bc960e707', N'LanDG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Gia Lan', N'landg170288@gmail.com', NULL, N'0963118185', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1988-02-17' AS Date), N'Parent', CAST(N'2025-04-28T20:40:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'6c20c894-17db-420c-b6eb-1faef8b7a561', N'TungPV', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Văn Tùng', N'tungpv080187@gmail.com', NULL, N'0991199745', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1987-01-08' AS Date), N'Parent', CAST(N'2025-04-28T15:59:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'6f7be8ee-b109-4198-90c7-913aa9c32687', N'LanTM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Minh Lan', N'lantm251187@gmail.com', NULL, N'0921232759', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1987-11-25' AS Date), N'Parent', CAST(N'2025-04-28T11:31:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'72793f52-a593-49a4-a339-6ad490cbc067', N'NgocTLH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Lê Hương Ngọc', N'kimdungvu270208@gmail.com', NULL, N'0977459958', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2008-02-27' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'753322cc-d6ca-4a7f-9527-550bfc859a1b', N'YenTT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Thị Yến', N'yentt130981@gmail.com', NULL, N'0914605577', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1981-09-13' AS Date), N'Parent', CAST(N'2025-04-28T13:43:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'78799558-9b36-4bce-89a2-39298efff3d3', N'LinhNK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Khánh Linh', N'slinh251208@gmail.com', NULL, N'0344621946', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2008-12-25' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'78acc318-5b69-4d23-a1d8-d7176f36da2c', N'AnhNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Minh Anh', N'ngminhanh08012007@gmail.com', NULL, N'0373231109', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2007-01-08' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'7a46cc87-750e-431a-8d3f-6a7ba099e5c0', N'AnhLTP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lưu Tạ Phương Anh', N'phuonganh.16052009@gmail.com', NULL, N'0983566209', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2009-05-16' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'7d8fb40b-a4d3-49ef-a1d8-f6bd130a192d', N'HuyenPNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Nguyễn Thu Huyền', N'pnthuyen150307@gmail.com', NULL, N'0392855859', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2007-03-15' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8064ed30-50c5-4c43-b56e-fb32c74c2261', N'ThuBA', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Bùi Anh Thư', N'Bthu200508@gmail.com', NULL, N'0877565819', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2008-05-20' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'83079edb-a13f-45c9-bf90-8749bef09d67', N'PhucVH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Vũ Hữu Phúc', N'phucvh250188@gmail.com', NULL, N'0938087063', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1988-01-25' AS Date), N'Parent', CAST(N'2025-04-28T14:08:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8589820a-b1ef-4377-8f61-544f398bf237', N'DucNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Minh Đức', N'minhduc02092008@gmail.com', NULL, N'0982158533', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2008-09-02' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'85f6e82a-4eca-4fe1-934a-08c1e5c0b33b', N'ChiNK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Khánh Chi', N'chikhanhnguyen10072009@gmail.com', NULL, N'0862312707', N'Ngõ 165 Trung Kính, Cầu Giấy', CAST(N'2009-07-10' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8ad8b8ca-8808-4d5e-9500-0189aa3ac6ad', N'QuanNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Minh Quân', N'minhquwn110308@gmail.com', NULL, N'0985137873', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2008-03-11' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8b3ebe4a-ee51-4f4e-ba19-8d2c25ad3719', N'LongPH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Hải Long', N'hailong17012009@gmail.com', NULL, N'0978839406', N'Ngõ 100 Yên Hòa, Cầu Giấy', CAST(N'2009-01-17' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8bfd5166-79e5-4f8d-ae40-cc681cb4bb64', N'HaPT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phan Thanh Hà', N'phanthanhha110309@gmail.com', NULL, N'0354069209', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2009-03-11' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'8d542ecd-4068-466f-bf3c-ff0f27a2d48b', N'AnhDT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Tuệ Anh', N'tueanhdo131109@gmail.com', NULL, N'0382304556', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2009-11-13' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'9400440d-4e1e-4666-ada7-31544a4eacf9', N'ThanhDM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đào Minh Thành', N'minhthanh2009k65yh@gmail.com', NULL, N'0386251332', N'Ngõ 100 Yên Hòa, Cầu Giấy', CAST(N'2009-02-16' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'95019a52-79ee-42bd-92b9-c9dfae4c9a0d', N'MaiHT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Thanh Mai', N'maiht100107@gmail.com', NULL, N'0942680505', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'2007-01-10' AS Date), N'Manager', CAST(N'2025-04-28T10:19:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'9d7378c6-032c-4bd3-808a-9773cbab1b24', N'ChiNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Thảo Chi ', N'tcmoi060708@gmail.com', NULL, N'0357340996', N'Số 21 Trần Duy Hưng, Cầu Giấy', CAST(N'2008-07-06' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'9fc6f8d3-615c-4b1f-acb9-333aa3916a9e', N'MyDH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đoàn Hà My', N'haamyy1605@gmail.com', NULL, N'0333730609', N'Ngõ 259 Yên Hòa, Cầu Giấy', CAST(N'2009-05-16' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'a44f4dac-c64f-48a7-94ae-243b7cd60255', N'TungDV', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đặng Văn Tùng', N'tungdv071176@gmail.com', NULL, N'0929242731', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1976-11-07' AS Date), N'Parent', CAST(N'2025-04-28T10:08:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'a5238a01-fde9-4a73-88c3-7d6e007bdd9c', N'PhuongPTH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phan Thị Hà Phương', N'haphuongphan1711@gmail.com', NULL, N'0984909176', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2008-11-17' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'a7a44757-63c0-4d8d-847a-30c0b191de34', N'HaiVV', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Vũ Văn Hải', N'haivv201283@gmail.com', NULL, N'0933230677', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1983-12-20' AS Date), N'Parent', CAST(N'2025-04-28T22:35:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'a94b4871-5711-4ed2-a20a-365b70735763', N'HungTP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Phúc Hưng', N'hugtran090808@gmail.com', NULL, N'0328222-66', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2008-08-09' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ab396bb3-d8e0-4012-9c08-404577f5e7d4', N'bachh', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Hữu Bắc', N'bangHH@gmail.com', NULL, N'0768925763', N'N/A', NULL, N'Parent', CAST(N'2025-05-05T17:30:31.830' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ab605003-b80c-49ae-988b-185151ad6113', N'HuyHK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Khánh Huy', N'hoangkhanhhuy290309@gmail.com', NULL, N'0395406946', N'Ngõ 165 Trung Kính, Cầu Giấy', CAST(N'2008-03-29' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'abba1a67-d0ed-4d5b-8be3-013b3af6b75c', N'YenLHH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Hà Hải Yến', N'yenlhh210208@gmail.com', NULL, N'0396914508', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'2008-02-21' AS Date), N'Manager', CAST(N'2025-04-28T11:05:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ae5be2af-83b0-47e8-baac-0e5b69cd24f7', N'AnhNT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Tiến Anh', N'tienanhisrealll@gmail.com', NULL, N'0368417446', N'Ngõ 45 Trung Kính, Cầu Giấy', CAST(N'2008-09-16' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'af66b8e6-e0ae-4fe8-b8bb-4123566dd1e8', N'TungBH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Bùi Hữu Tùng', N'tungbh020976@gmail.com', NULL, N'0956225720', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'1976-09-02' AS Date), N'Parent', CAST(N'2025-04-28T10:04:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'b02fbf06-8564-4c52-b024-b581b500f2f4', N'AnDC', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Chúc An', N'dochucan2205208@gmail.com', NULL, N'0866502405', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2008-05-22' AS Date), N'Player', CAST(N'2025-04-28T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'c0959f14-9298-447c-80c7-289d864ddf33', N'annv22', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S.XPKXfU/ue.a345/qoU9ZRWzaA9G', N'Nguyễn Văn An', N'grh46223@jioso.com', NULL, N'0987654653', N'Ha Noi', CAST(N'2025-05-10' AS Date), N'Coach', CAST(N'2025-05-04T20:42:07.520' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'c3a3b1a8-ef46-4e26-b664-8805a66a3f8c', N'SonDV', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Văn Sơn', N'sondv081286@gmail.com', NULL, N'0914495032', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1986-12-08' AS Date), N'Parent', CAST(N'2025-04-28T19:53:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'c74b4513-0ec3-4101-a813-cd9aab10f49f', N'VinhND', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Đức Vinh', N'nguyenducvinhtc061107@gmail.com', NULL, N'0799881666', N'Ngõ 259 Yên Hòa, Cầu Giấy', CAST(N'2007-11-06' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'cb36552e-1446-45b4-a76a-2a20b0603728', N'BinhPT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phạm Trúc Bình', N'ptb.bung300109@gmail.com', NULL, N'0866511165', N'Ngõ 100 Yên Hòa, Cầu Giấy', CAST(N'2009-01-30' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'cb7c3cb1-16fe-4579-9793-b51864303f39', N'HuyenNK', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Khánh Huyền', N'kn05062009@gmail.com', NULL, N'0862553918', N'Số 1 Nguyễn Chánh, Cầu Giấy', CAST(N'2009-06-05' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'd82df62c-a230-48cb-ac68-9cc8d3482325', N'MyNN', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Nhật My', N'mynn090407@gmail.com', NULL, N'0906215569', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'2007-04-09' AS Date), N'Manager', CAST(N'2025-04-28T23:44:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'd98b60ca-9293-4909-883b-83ed2d4f7906', N'hieun', N'$2a$11$aMrOCH.GrRYEXyMtJmUJ.OcZ/HTdysz5XVyhYggFT7WCMxbZpIzHa', N'Nguyễn Hiếu', N'conghieu8899@gmail.com', NULL, N'0912345678', N'N/A', CAST(N'2025-04-09' AS Date), N'Player', CAST(N'2025-05-06T04:14:56.230' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'dd6eec27-fe40-45cc-b3e6-99e2cc19a6a9', N'SonDH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đào Huy Sơn', N'daohuyson250708@gmail.com', NULL, N'0983029779', N'Ngõ 155 Yên Hòa, Cầu Giấy', CAST(N'2008-07-25' AS Date), N'Player', CAST(N'2025-04-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'dfc79fe2-5d67-4698-8189-c087b6400bce', N'NhiTL', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Linh Nhi', N'tranlinhnhi140608@gmail.com', NULL, N'0867645508', N'Số 2 Nguyễn Ngọc Vũ, Cầu Giấy', CAST(N'2008-06-14' AS Date), N'Player', CAST(N'2025-04-27T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'e78dd0b9-330c-44e4-822a-9c57d87fd4e1', N'LinhHM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Hoàng Minh Linh', N'linhhm010677@gmail.com', NULL, N'0978909709', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1977-06-01' AS Date), N'Parent', CAST(N'2025-04-28T21:54:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'e9c24726-f3ca-48bf-98fc-26ffe0caf3be', N'BinhLP', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Lê Phương Bình', N'binhlp050907@gmail.com', NULL, N'0888041677', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'2007-09-05' AS Date), N'Manager', CAST(N'2025-04-28T04:35:00.000' AS DateTime), NULL, 1, 0)
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ea410df6-cca7-4501-b9be-8940289b17a8', N'YenNM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Minh Yến', N'yennm300976@gmail.com', NULL, N'0974253545', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1976-09-30' AS Date), N'Parent', CAST(N'2025-04-28T06:13:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ea9dfac6-120f-4c4d-92a2-fb787d8d0d96', N'SonVT', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Vũ Thị Sơn', N'sonvt291081@gmail.com', NULL, N'0921044213', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1981-10-29' AS Date), N'Parent', CAST(N'2025-04-28T03:22:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'eab04127-7fea-410e-abb1-8763d5bfb410', N'NamNH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Ngô Hữu Nam', N'namnh190486@gmail.com', NULL, N'0964832227', N'Ngõ 120 Trần Duy Hưng, Cầu Giấy', CAST(N'1986-04-19' AS Date), N'Parent', CAST(N'2025-04-28T07:35:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'eb398293-ac6f-4586-a6a1-1174df259f9a', N'VyDY', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đỗ Yến Vy', N'vydy150607@gmail.com', NULL, N'0389276080', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'2007-06-15' AS Date), N'Manager', CAST(N'2025-04-28T18:39:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'eb8b08e9-ce2b-402d-8b26-f1d640e8e764', N'TamDTM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Đinh Thị Minh Tâm', N'dinhthiminhtam2008@gmail.com', NULL, N'0326469182', N'Ngõ 165 Trung Kính, Cầu Giấy', CAST(N'2008-06-11' AS Date), N'Player', CAST(N'2025-04-29T00:00:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ec8c2833-86b6-415d-aa67-f90576eb5c38', N'LanTG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Trần Gia Lan', N'lantg220283@gmail.com', NULL, N'0908871970', N'Số 45 Nguyễn Khang, Cầu Giấy', CAST(N'1983-02-22' AS Date), N'Parent', CAST(N'2025-04-28T01:41:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ecf50c37-3464-42e2-8a1b-bb2052895b69', N'DungPM', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Phùng Minh Dũng', N'dungpm100307@gmail.com', NULL, N'0967283661', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'2007-03-10' AS Date), N'Manager', CAST(N'2025-04-28T15:34:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'ef1ae1ac-096f-4f6b-8b67-bb6128fdffdc', N'anhhc', N'$2a$11$npVSx9ZUjTpb5ShC7z.ESeOB0lVHbkwh9ZpEwGXrtDW1mBU1PHNCK', N'Hoàng Công Anh', N'coach_AnhHC@gmail.com', NULL, N'0673827935', N'Hà Nội', CAST(N'1994-09-14' AS Date), N'Coach', CAST(N'2025-05-06T21:30:35.327' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'f7744d89-9622-415d-b0c3-0ec0739b9af1', N'KietNG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Gia Kiệt', N'kiet150607@gmail.com', NULL, N'0981308336', N'Số 18 Trần Kim Xuyến, Cầu Giấy', CAST(N'2007-06-15' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'fa363f97-d708-4b8e-be2a-401b5112adc0', N'YenNH', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Ngô Hữu Yến', N'yennh220178@gmail.com', NULL, N'0970247937', N'Ngõ 91 Trung Kính, Cầu Giấy', CAST(N'1978-01-22' AS Date), N'Parent', CAST(N'2025-04-28T21:22:00.000' AS DateTime), NULL, 1, 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'fe05c691-4649-4c4c-b0f1-e8eca761ed0c', N'phun5', N'$2a$11$s7EKje28cKS9naVTPBa5AuUKkpB9HIB9Qvx2Z24tuQV.OZLKWyojS', N'Nguyễn Phụ', N'tainghe280703@gmail.com', NULL, N'0912365498', N'N/A', NULL, N'Parent', CAST(N'2025-05-06T04:14:56.440' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'fece82e9-91af-4e07-918b-522c0eae87eb', N'NamBG', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Bùi Gia Nam', N'nambg170775@gmail.com', NULL, N'0959920113', N'Số 12 Nguyễn Thị Định, Cầu Giấy', CAST(N'1975-07-17' AS Date), N'Parent', CAST(N'2025-04-28T21:48:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [Fullname], [Email], [ProfileImage], [Phone], [Address], [DateOfBirth], [RoleCode], [CreatedAt], [UpdatedAt], [IsEnable], [Gender]) VALUES (N'fefd3ff9-cd81-459f-b417-ea3eab683887', N'AnhNLB', N'$2a$11$vK6KKxHnnbqEbFYoHPQOuOhzCwtrw8zdPKpYUlR/HGXkT04Mt9y2S', N'Nguyễn Lê Bảo Anh', N'nlbaoanh130908@gmail.com', NULL, N'0832536309', N'Số 6 Trung Kính, Cầu Giấy', CAST(N'2008-09-13' AS Date), N'Player', CAST(N'2025-04-26T00:00:00.000' AS DateTime), NULL, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[UserFace] ON 

INSERT [dbo].[UserFace] ([UserFaceId], [UserId], [RegisteredFaceId], [ImageUrl], [RegisteredAt], [UpdatedAt]) VALUES (29, N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', N'bdc9d8ca-d0a8-4534-b3d5-7d0312951349', N'uploads/face-recognition/0a8e779f-f0f5-4b36-9442-a1c336a2c438.png', CAST(N'2025-05-07T11:40:33.840' AS DateTime), NULL)
INSERT [dbo].[UserFace] ([UserFaceId], [UserId], [RegisteredFaceId], [ImageUrl], [RegisteredAt], [UpdatedAt]) VALUES (30, N'10aaa2ec-ed9b-4c41-99b6-1341ba482431', N'5166c62a-8bae-4851-a8ca-2b1f37222776', N'uploads/face-recognition/e64db7b1-0776-4693-8241-77333ab1c69c.jpg', CAST(N'2025-05-07T11:41:52.903' AS DateTime), NULL)
INSERT [dbo].[UserFace] ([UserFaceId], [UserId], [RegisteredFaceId], [ImageUrl], [RegisteredAt], [UpdatedAt]) VALUES (31, N'5f989406-657b-4920-b27e-91d465f648d3', N'46dec820-e4f7-447e-8421-3ccf96fbde2a', N'uploads/face-recognition/e894f22d-ed85-4f62-8f00-b99e5c2d4030.png', CAST(N'2025-05-07T12:12:04.387' AS DateTime), NULL)
INSERT [dbo].[UserFace] ([UserFaceId], [UserId], [RegisteredFaceId], [ImageUrl], [RegisteredAt], [UpdatedAt]) VALUES (32, N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', N'53abf9d8-5826-44ef-b613-1e557384b176', N'uploads/face-recognition/da9fca75-27f3-47da-ae5c-bd7b5a0ab596.png', CAST(N'2025-05-07T12:13:24.943' AS DateTime), NULL)
INSERT [dbo].[UserFace] ([UserFaceId], [UserId], [RegisteredFaceId], [ImageUrl], [RegisteredAt], [UpdatedAt]) VALUES (33, N'37d46080-f527-43a8-8d33-cc9902f9b5e0', N'9ec4c364-110c-40e0-8dbe-16222103a7b8', N'uploads/face-recognition/fcbde0c0-4972-460e-8e16-43a7ff66e71d.png', CAST(N'2025-05-07T12:14:48.997' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[UserFace] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTeamHistory] ON 

INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'00b4a569-ad4a-41d2-97bf-16d25a736023', 57, N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', CAST(N'2025-04-06T23:52:11.600' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'0f751cc0-81b4-4549-9155-26bf20bce4f2', 58, N'222ffcb8-c8ed-4cb9-98d5-9e6b2a0db79c', CAST(N'2025-04-06T23:52:11.657' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'02dbf8d2-982d-40d3-893e-15b8fe85a90a', 59, N'30517058-1cb6-4ede-8818-bfe559e479cc', CAST(N'2025-04-06T23:52:56.933' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'133a6f47-cef4-4da3-ae24-e44b1e477b6e', 60, N'30517058-1cb6-4ede-8818-bfe559e479cc', CAST(N'2025-04-06T23:52:56.940' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'0b952eb5-3ce9-4a7b-b11c-9c03ea4e5045', 61, N'9fedde57-97a7-4c26-a5c5-423c0edf13be', CAST(N'2025-04-07T00:11:34.203' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'151c8f06-3d12-4590-b5c1-fa786aa8f2b1', 62, N'9fedde57-97a7-4c26-a5c5-423c0edf13be', CAST(N'2025-04-07T00:11:34.223' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserTeamHistory] ([UserId], [UserTeamHistoryId], [TeamId], [JoinDate], [LeftDate], [Note], [RemovedByUserId]) VALUES (N'10aaa2ec-ed9b-4c41-99b6-1341ba482431', 63, N'30517058-1cb6-4ede-8818-bfe559e479cc', CAST(N'2025-04-07T00:19:38.537' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserTeamHistory] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Parent_ak_CitizenId]    Script Date: 5/9/2025 12:00:04 AM ******/
ALTER TABLE [dbo].[Parent] ADD  CONSTRAINT [Parent_ak_CitizenId] UNIQUE NONCLUSTERED 
(
	[CitizenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_ak_Email]    Script Date: 5/9/2025 12:00:04 AM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [User_ak_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_ak_Username]    Script Date: 5/9/2025 12:00:04 AM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [User_ak_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Team] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [Attendance_Manager] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Manager] ([UserId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [Attendance_Manager]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [Attendance_TrainingSession] FOREIGN KEY([TrainingSessionId])
REFERENCES [dbo].[TrainingSession] ([TrainingSessionId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [Attendance_TrainingSession]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [Attendance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [Attendance_User]
GO
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [Coach_President] FOREIGN KEY([CreatedByPresidentId])
REFERENCES [dbo].[President] ([UserId])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [Coach_President]
GO
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [Coach_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [Coach_Team]
GO
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [Coach_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [Coach_User]
GO
ALTER TABLE [dbo].[Exercise]  WITH CHECK ADD  CONSTRAINT [Exercise_Coach] FOREIGN KEY([CoachId])
REFERENCES [dbo].[Coach] ([UserId])
GO
ALTER TABLE [dbo].[Exercise] CHECK CONSTRAINT [Exercise_Coach]
GO
ALTER TABLE [dbo].[Exercise]  WITH CHECK ADD  CONSTRAINT [Exercise_TrainingSession] FOREIGN KEY([TrainingSessionId])
REFERENCES [dbo].[TrainingSession] ([TrainingSessionId])
GO
ALTER TABLE [dbo].[Exercise] CHECK CONSTRAINT [Exercise_TrainingSession]
GO
ALTER TABLE [dbo].[Expenditure]  WITH CHECK ADD  CONSTRAINT [Expenditure_Manager] FOREIGN KEY([ByManagerId])
REFERENCES [dbo].[Manager] ([UserId])
GO
ALTER TABLE [dbo].[Expenditure] CHECK CONSTRAINT [Expenditure_Manager]
GO
ALTER TABLE [dbo].[Expenditure]  WITH CHECK ADD  CONSTRAINT [Expenditure_TeamFund] FOREIGN KEY([TeamFundId])
REFERENCES [dbo].[TeamFund] ([TeamFundId])
GO
ALTER TABLE [dbo].[Expenditure] CHECK CONSTRAINT [Expenditure_TeamFund]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [Manager_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [Manager_Team]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [Manager_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [Manager_User]
GO
ALTER TABLE [dbo].[ManagerRegistration]  WITH CHECK ADD  CONSTRAINT [ManagerRegistration_MemberRegistrationSession] FOREIGN KEY([MemberRegistrationSessionId])
REFERENCES [dbo].[MemberRegistrationSession] ([MemberRegistrationSessionId])
GO
ALTER TABLE [dbo].[ManagerRegistration] CHECK CONSTRAINT [ManagerRegistration_MemberRegistrationSession]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [Match_AwayTeam] FOREIGN KEY([AwayTeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [Match_AwayTeam]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [Match_Coach] FOREIGN KEY([CreatedByCoachId])
REFERENCES [dbo].[Coach] ([UserId])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [Match_Coach]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [Match_Court] FOREIGN KEY([CourtId])
REFERENCES [dbo].[Court] ([CourtId])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [Match_Court]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [Match_HomeTeam] FOREIGN KEY([HomeTeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [Match_HomeTeam]
GO
ALTER TABLE [dbo].[MatchArticle]  WITH CHECK ADD  CONSTRAINT [MatchArticle_Match] FOREIGN KEY([MatchId])
REFERENCES [dbo].[Match] ([MatchId])
GO
ALTER TABLE [dbo].[MatchArticle] CHECK CONSTRAINT [MatchArticle_Match]
GO
ALTER TABLE [dbo].[MatchLineup]  WITH CHECK ADD  CONSTRAINT [MatchLineup_Match] FOREIGN KEY([MatchId])
REFERENCES [dbo].[Match] ([MatchId])
GO
ALTER TABLE [dbo].[MatchLineup] CHECK CONSTRAINT [MatchLineup_Match]
GO
ALTER TABLE [dbo].[MatchLineup]  WITH CHECK ADD  CONSTRAINT [MatchLineup_Player] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([UserId])
GO
ALTER TABLE [dbo].[MatchLineup] CHECK CONSTRAINT [MatchLineup_Player]
GO
ALTER TABLE [dbo].[Parent]  WITH CHECK ADD  CONSTRAINT [Parent_Manager] FOREIGN KEY([CreatedByManagerId])
REFERENCES [dbo].[Manager] ([UserId])
GO
ALTER TABLE [dbo].[Parent] CHECK CONSTRAINT [Parent_Manager]
GO
ALTER TABLE [dbo].[Parent]  WITH CHECK ADD  CONSTRAINT [Parent_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Parent] CHECK CONSTRAINT [Parent_User]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [Payment_Player] FOREIGN KEY([UserId])
REFERENCES [dbo].[Player] ([UserId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [Payment_Player]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [Payment_TeamFund] FOREIGN KEY([TeamFundId])
REFERENCES [dbo].[TeamFund] ([TeamFundId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [Payment_TeamFund]
GO
ALTER TABLE [dbo].[PaymentItem]  WITH CHECK ADD  CONSTRAINT [PaymentDetail_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([PaymentId])
GO
ALTER TABLE [dbo].[PaymentItem] CHECK CONSTRAINT [PaymentDetail_Payment]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [Player_Parent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parent] ([UserId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [Player_Parent]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [Player_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [Player_Team]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [Player_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [Player_User]
GO
ALTER TABLE [dbo].[PlayerRegistration]  WITH CHECK ADD  CONSTRAINT [PlayerRegistration_MemberRegistrationSession] FOREIGN KEY([MemberRegistrationSessionId])
REFERENCES [dbo].[MemberRegistrationSession] ([MemberRegistrationSessionId])
GO
ALTER TABLE [dbo].[PlayerRegistration] CHECK CONSTRAINT [PlayerRegistration_MemberRegistrationSession]
GO
ALTER TABLE [dbo].[PlayerRegistration]  WITH CHECK ADD  CONSTRAINT [PlayerRegistration_User] FOREIGN KEY([FormReviewedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PlayerRegistration] CHECK CONSTRAINT [PlayerRegistration_User]
GO
ALTER TABLE [dbo].[President]  WITH CHECK ADD  CONSTRAINT [President_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[President] CHECK CONSTRAINT [President_User]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_FundManager] FOREIGN KEY([FundManagerId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_FundManager]
GO
ALTER TABLE [dbo].[TeamFund]  WITH CHECK ADD  CONSTRAINT [TeamFund_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[TeamFund] CHECK CONSTRAINT [TeamFund_Team]
GO
ALTER TABLE [dbo].[TrainingSession]  WITH CHECK ADD  CONSTRAINT [Schedule_Court] FOREIGN KEY([CourtId])
REFERENCES [dbo].[Court] ([CourtId])
GO
ALTER TABLE [dbo].[TrainingSession] CHECK CONSTRAINT [Schedule_Court]
GO
ALTER TABLE [dbo].[TrainingSession]  WITH CHECK ADD  CONSTRAINT [TrainingSession_Coach] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Coach] ([UserId])
GO
ALTER TABLE [dbo].[TrainingSession] CHECK CONSTRAINT [TrainingSession_Coach]
GO
ALTER TABLE [dbo].[TrainingSession]  WITH CHECK ADD  CONSTRAINT [TrainingSession_Manager] FOREIGN KEY([CreatedDecisionByManagerId])
REFERENCES [dbo].[Manager] ([UserId])
GO
ALTER TABLE [dbo].[TrainingSession] CHECK CONSTRAINT [TrainingSession_Manager]
GO
ALTER TABLE [dbo].[TrainingSession]  WITH CHECK ADD  CONSTRAINT [TraningSession_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[TrainingSession] CHECK CONSTRAINT [TraningSession_Team]
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest]  WITH CHECK ADD  CONSTRAINT [TrainingSessionUpdateRequest_Coach] FOREIGN KEY([RequestedByCoachId])
REFERENCES [dbo].[Coach] ([UserId])
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest] CHECK CONSTRAINT [TrainingSessionUpdateRequest_Coach]
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest]  WITH CHECK ADD  CONSTRAINT [TrainingSessionUpdateRequest_Court] FOREIGN KEY([NewCourtId])
REFERENCES [dbo].[Court] ([CourtId])
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest] CHECK CONSTRAINT [TrainingSessionUpdateRequest_Court]
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest]  WITH CHECK ADD  CONSTRAINT [TrainingSessionUpdateRequest_Manager] FOREIGN KEY([DecisionByManagerId])
REFERENCES [dbo].[Manager] ([UserId])
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest] CHECK CONSTRAINT [TrainingSessionUpdateRequest_Manager]
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest]  WITH CHECK ADD  CONSTRAINT [TrainingSessionUpdateRequest_TrainingSession] FOREIGN KEY([TrainingSessionId])
REFERENCES [dbo].[TrainingSession] ([TrainingSessionId])
GO
ALTER TABLE [dbo].[TrainingSessionStatusChangeRequest] CHECK CONSTRAINT [TrainingSessionUpdateRequest_TrainingSession]
GO
ALTER TABLE [dbo].[TryOutMeasurementScale]  WITH CHECK ADD  CONSTRAINT [TryOutMeasurementScale_TryOutMeasurementScale] FOREIGN KEY([ParentMeasurementScaleCode])
REFERENCES [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode])
GO
ALTER TABLE [dbo].[TryOutMeasurementScale] CHECK CONSTRAINT [TryOutMeasurementScale_TryOutMeasurementScale]
GO
ALTER TABLE [dbo].[TryOutScorecard]  WITH CHECK ADD  CONSTRAINT [TryOutScorecard_PlayerRegistration] FOREIGN KEY([PlayerRegistrationId])
REFERENCES [dbo].[PlayerRegistration] ([PlayerRegistrationId])
GO
ALTER TABLE [dbo].[TryOutScorecard] CHECK CONSTRAINT [TryOutScorecard_PlayerRegistration]
GO
ALTER TABLE [dbo].[TryOutScorecard]  WITH CHECK ADD  CONSTRAINT [TryOutScorecard_TryOutMeasurementScale] FOREIGN KEY([MeasurementScaleCode])
REFERENCES [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode])
GO
ALTER TABLE [dbo].[TryOutScorecard] CHECK CONSTRAINT [TryOutScorecard_TryOutMeasurementScale]
GO
ALTER TABLE [dbo].[TryOutScorecard]  WITH CHECK ADD  CONSTRAINT [TryOutScorecard_User] FOREIGN KEY([ScoredBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[TryOutScorecard] CHECK CONSTRAINT [TryOutScorecard_User]
GO
ALTER TABLE [dbo].[TryOutScoreCriteria]  WITH CHECK ADD  CONSTRAINT [TryOutScoreCriteria_TryOutMeasurementScale] FOREIGN KEY([MeasurementScaleCode])
REFERENCES [dbo].[TryOutMeasurementScale] ([MeasurementScaleCode])
GO
ALTER TABLE [dbo].[TryOutScoreCriteria] CHECK CONSTRAINT [TryOutScoreCriteria_TryOutMeasurementScale]
GO
ALTER TABLE [dbo].[TryOutScoreLevel]  WITH CHECK ADD  CONSTRAINT [TryOutScoreLevel_TryOutScoreCriteria] FOREIGN KEY([ScoreCriteriaId])
REFERENCES [dbo].[TryOutScoreCriteria] ([ScoreCriteriaId])
GO
ALTER TABLE [dbo].[TryOutScoreLevel] CHECK CONSTRAINT [TryOutScoreLevel_TryOutScoreCriteria]
GO
ALTER TABLE [dbo].[UserFace]  WITH CHECK ADD  CONSTRAINT [UserFace_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserFace] CHECK CONSTRAINT [UserFace_User]
GO
ALTER TABLE [dbo].[UserForgotPasswordToken]  WITH CHECK ADD  CONSTRAINT [UserForgotPasswordToken_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserForgotPasswordToken] CHECK CONSTRAINT [UserForgotPasswordToken_User]
GO
ALTER TABLE [dbo].[UserRefreshToken]  WITH CHECK ADD  CONSTRAINT [UserRefreshToken_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRefreshToken] CHECK CONSTRAINT [UserRefreshToken_User]
GO
ALTER TABLE [dbo].[UserTeamHistory]  WITH CHECK ADD  CONSTRAINT [PlayerTeamHistory_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[UserTeamHistory] CHECK CONSTRAINT [PlayerTeamHistory_Team]
GO
ALTER TABLE [dbo].[UserTeamHistory]  WITH CHECK ADD  CONSTRAINT [UserTeamHistory_User_RemovedBy] FOREIGN KEY([RemovedByUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserTeamHistory] CHECK CONSTRAINT [UserTeamHistory_User_RemovedBy]
GO
ALTER TABLE [dbo].[UserTeamHistory]  WITH CHECK ADD  CONSTRAINT [UserTeamHistory_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserTeamHistory] CHECK CONSTRAINT [UserTeamHistory_User_UserId]
GO
