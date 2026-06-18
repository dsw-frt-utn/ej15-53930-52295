
using Dsw2026EJ15.Domain.Interfaces;
using Dsw2026EJ15.Data;
using Dsw2026EJ15.Api.Middleware;

namespace Dsw2026EJ15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapGet("/health-check", () => Results.Content("Healthy", "text/plain"));

            app.MapControllers();

            app.Run();
        }
    }
}
