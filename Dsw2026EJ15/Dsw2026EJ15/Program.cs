
using Dsw2026EJ15.Domain.Interfaces;
using Dsw2026EJ15.Data;
using Dsw2026EJ15.Api;
using Dsw2026Ej15.Api.Middlewares;
namespace Dsw2026EJ15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
           
            builder.Services.AddHealthChecks();
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

            app.MapHealthChecks("/health");

            app.MapControllers();

            app.Run();
        }
    }
}
