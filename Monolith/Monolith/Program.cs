using API.Controller;
using Monolith.Configurations;
using Monolith.DependencyInjections;

namespace Monolith
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Config services to the container.
            builder.Configuration.SettingsBinding();
            builder.Services.AddPersistence();

            // Config Controller
            var controllerAssembly = typeof(TodoListController).Assembly;
            builder.Services.AddControllers()
                .AddApplicationPart(controllerAssembly);

            // Config Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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
