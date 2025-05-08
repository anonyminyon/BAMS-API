using System.Reflection;
using System.Text;
using BasketballAcademyManagementSystemAPI.API.Extensions;
using BasketballAcademyManagementSystemAPI.API.Middlewares;
using BasketballAcademyManagementSystemAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;
using Amazon.Rekognition;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using log4net.Config;
using log4net;
using Quartz;
using BasketballAcademyManagementSystemAPI.Application.DTOs.VietQr;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using BasketballAcademyManagementSystemAPI.Common.Jobs;



namespace BasketballAcademyManagementSystemAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var builder = WebApplication.CreateBuilder(args);

            // Load env in development environment
            if (builder.Environment.IsDevelopment())
            {
                DotNetEnv.Env.Load();
            }

            // Always load from environment variables (both dev and prod)
            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddDbContext<BamsDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                      ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

                options.UseSqlServer(
                    connectionString,
                    sqlServerOptions => sqlServerOptions
                        .EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: new List<int> { 4060, 40197, 40501, 49918, 11001 }
                        )
                        .CommandTimeout(60)
                );
            });

            builder.Services.AddControllers();

            var corsPolicy = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicy,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000", "https://bams-three.vercel.app", "https://yenhoastorm.com")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            // Register JWT
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"]
                    };
                    options.MapInboundClaims = false;
                });

            // Register Services & Repositories

            // Register AWS Rekognition
            builder.Services.AddAWSService<IAmazonRekognition>();

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && !typeof(BackgroundService).IsAssignableFrom(type))
                {
                    var interfaceType = type.GetInterfaces().FirstOrDefault();
                    if (interfaceType != null)
                    {
                        builder.Services.AddScoped(interfaceType, type);
                    }
                }
            }

            // Additional Service
            builder.Services.AddAdditionalApplicationServices();

            // Register AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Background Service
            builder.Services.AddHostedService<ExpiredTokenCleanupService>();

            builder.Services.AddHostedService<MatchStatusUpdateService>();

            // Register HttpContextAccessor
            builder.Services.AddHttpContextAccessor();

			// Register GetLoginUserHelper as a Scoped Service
            builder.Services.AddScoped<GetLoginUserHelper>();
			builder.Services.AddScoped<AccountGenerateHelper>(); // Register the helper

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1_073_741_824; // 1GB
            });

            builder.Services.AddSingleton<ClaudeService>();

            builder.Services.AddSingleton<EmbeddingService>();

            builder.Services.AddHttpClient<QdrantService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(60);
            });


			builder.Services.AddQuartz(q =>
			{

				var jobKey = new JobKey("TeamFundJob");

				q.AddJob<TeamFundJob>(opt => opt.WithIdentity(jobKey));
				q.AddTrigger(opt => opt
					.ForJob(jobKey)
					.WithIdentity("TeamFundJob-trigger")
                    .WithCronSchedule("0 0 0 1 * ?", x => x.InTimeZone(TimeZoneInfo.Local)) // chạy vào 00h00p hàng tháng
				);
			});

			builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

            //job for check court fee base on attendant of a team in each training session
            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("AutoAddCourtFeeJob");

                q.AddJob<AutoAddCourtFeeJob>(opt => opt.WithIdentity(jobKey));
                q.AddTrigger(opt => opt
                    .ForJob(jobKey)
                    .WithIdentity("AutoAddCourtFeeJob-trigger")
                    .WithCronSchedule("0 0 0 1 * ?", x => x.InTimeZone(TimeZoneInfo.Local)) // chạy vào 00h00p hàng ngày
                );                                                                                                      
            });
            builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

            //job for remind payment
            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("PaymentReminderJob");
                q.AddJob<PaymentReminderJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("DailyPaymentReminderTrigger")
                   .WithCronSchedule("0 0 8 * * ?", x => x.InTimeZone(TimeZoneInfo.Local)) 
                );
            });
            builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

            builder.Services.AddScoped<CancelCreateTrainingSessionRequestSchedulerService>();
            // job to cancel create training session request
            builder.Services.AddQuartz(q =>
            {
                var cancelRequestJobKey = new JobKey("CancelCreateTrainingSessionRequestJob");
                q.AddJob<CancelCreateTrainingSessionRequestJob>(opts => opts.StoreDurably());
            });
            builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

            builder.Services.AddHostedService<CancelCreateTrainingSessionRequestSyncService>();

            builder.Services.AddScoped<CancelTrainingSessionStatusChangeRequestSchedulerService>();

            // job to cancel training session status change request
            builder.Services.AddQuartz(q =>
            {
                var cancelRequestJobKey = new JobKey("CancelTrainingSessionStatusChangeRequestSchedulerService");
                q.AddJob<CancelTrainingSessionStatusChangeRequestJob>(opts => opts.StoreDurably());
            });
            builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

            builder.Services.AddHostedService<CancelTrainingSessionStatusChangeRequestSyncService>();

            //Get Section VietQr In Appsettings
            builder.Services.Configure<VietQrOptions>(builder.Configuration.GetSection("VietQr"));

			//Add scoped Gen PaymentId
			//builder.Services.AddTransient<GeneratePaymentIdHelper>();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BAMS API V1");
                options.RoutePrefix = "swagger";
            });

            app.UseCors(corsPolicy);

            app.UseMiddleware<JwtAccessTokenInjectMiddleware>();

            app.UseMiddleware<JwtAutoTokenRefreshMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var result = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            description = entry.Value.Description
                        }),
                        totalDuration = report.TotalDuration.TotalMilliseconds + "ms"
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                }
            });

            app.Run();
        }
    }
}
