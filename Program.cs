using Microsoft.EntityFrameworkCore;
using OmniSedeBackend.Config;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Implementations;
using OmniSedeBackend.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
string conn = LoadDatabase(builder.Configuration);

LoadConfig(builder.Services, builder.Configuration, conn);
LoadRepository(builder.Services);
LoadService(builder.Services);

StartApp(builder.Build());

static string LoadDatabase(ConfigurationManager config)
{
    string? connectionDbFind = config.GetConnectionString("DefaultConnection");
    return Environment.ExpandEnvironmentVariables(connectionDbFind ?? "");
}

static void LoadConfig(IServiceCollection services, IConfiguration configuration, string conn)
{
    services.AddDbContext<OmnisedeContext>(options => options.UseSqlServer(conn));
    services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
    services.PostConfigure<JwtSettings>(jwt =>
    {
        jwt.Issuer = Environment.ExpandEnvironmentVariables(jwt.Issuer ?? string.Empty);
        jwt.Audience = Environment.ExpandEnvironmentVariables(jwt.Audience ?? string.Empty);
        jwt.SecretKey = Environment.ExpandEnvironmentVariables(jwt.SecretKey ?? string.Empty);
    });
}

static void LoadRepository(IServiceCollection services)
{
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<IAziendeRepository, AziendeRepository>();
    services.AddScoped<IUtentiRepository, UtentiRepository>();
    services.AddScoped<IDocumentiRepository, DocumentiRepository>();
    services.AddScoped<IRuoliRepository, RuoliRepository>();
    services.AddScoped<ISedeRepository, SedeRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

static void LoadService(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

static void StartApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}