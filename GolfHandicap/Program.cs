using GolfHandicap.Data;
using GolfHandicap.Features.Golfers.Post;
using GolfHandicap.Features.Golfers.Get;
using GolfHandicap.Features.Golfers.Get.GetById;
using GolfHandicap.Features.Matches.Post.GolfMatches.Preview;
using GolfHandicap.Features.Matches.Post.Schedule;

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
            builder.Services.AddTransient<IPostGolferHandler, PostGolferHandler>();
            builder.Services.AddTransient<IGetGolferHandler, GetGolferHandler>();
            builder.Services.AddTransient<IPreviewGolfMatchHandler, PreviewGolfMatchHandler>();
            builder.Services.AddTransient<IPreviewGolfMatchValidator, PreviewGolfMatchValidator>();
            builder.Services.AddTransient<IPostMatchScheduleHandler, PostMatchScheduleHandler>();
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}