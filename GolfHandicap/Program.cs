using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Features.Golfers.Get;
using GolfHandicap.Features.Golfers.Get.GetById;
using GolfHandicap.Features.Golfers.Post;
using GolfHandicap.Features.Matches.Get;
using GolfHandicap.Features.Matches.Post.GolfMatches;
using GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler;
using GolfHandicap.Features.Matches.Post.Schedule;
using GolfHandicap.Features.Scores.Get;
using GolfHandicap.Features.Scores.Handicaps.Calculation;
using GolfHandicap.Features.Scores.Post;
using GolfHandicap.Features.Setup;
using GolfHandicap.Infrastructure;

namespace GolfHandicap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddTransient<IPostGolferHandler, PostGolferHandler>();
            builder.Services.AddTransient<IGetGolferHandler, GetGolferHandler>();
            builder.Services.AddTransient<IPostMatchScheduleHandler, PostMatchScheduleHandler>();
            builder.Services.AddTransient<IGetScoreHandler, GetScoreHandler>();
            builder.Services.AddTransient<IScheduleYearlySchedule, ScheduleYearlySchedule>();
            builder.Services.AddTransient<IPostScoreHandler, PostScoreHandler>();
            builder.Services.AddTransient<ICustomRounding, CustomRounding>();
            builder.Services.AddTransient<IGetHandicap, GetHandicap>();
            builder.Services.AddTransient<IHandicapCalculation, HandicapCalculation>();
            builder.Services.AddTransient<IGenerateMatchSchedule, GenerateMatchSchedule>();
            builder.Services.AddTransient<IPostSetupHandler, PostSetupHandler>();
            builder.Services.AddTransient<IGetGolfMatchHandler,  GetGolfMatchHandler>();
            builder.Services.Configure<SlopeSettings>(builder.Configuration.GetSection("SlopeSettings"));
            builder.Services.AddAutoMapper(typeof(Program));
            //builder.Logging.ClearProviders();


            var app = builder.Build();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}