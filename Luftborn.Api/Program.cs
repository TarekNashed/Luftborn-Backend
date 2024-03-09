using Luftborn.Api.Extensions;
using Luftborn.Application;
using Luftborn.Infrastructure;
using Luftborn.Infrastructure.Persistence;

namespace Luftborn.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationService();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(Program));

            //builder.Services.AddAutoMapper(typeof(Program));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();



            var app = builder.Build();
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<LuftbornContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<LuftbornContextSeed>>();
                LuftbornContextSeed.SeedAsync(context, logger).Wait();
            });
            app.Run();
        }
    }
}