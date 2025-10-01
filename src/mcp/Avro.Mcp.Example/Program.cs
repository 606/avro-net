using Avro.Os.Identity;
using Avro.Os.Abstractions;

namespace Avro.Mcp.Example;

public class Program
{
    protected Program() { }
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        // Add OS detection services
        builder.Services.AddSingleton<IOperatingSystemDetector, OperatingSystemDetector>();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}