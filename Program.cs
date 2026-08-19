using Microsoft.EntityFrameworkCore;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Implementations;
using OmniSedeBackend.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
string conn = LoadDatabase(builder.Configuration);

LoadService(builder.Services, conn);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

static string LoadDatabase(ConfigurationManager config)
{
    string? connectionDbFind = config.GetConnectionString("DefaultConnection");
    return Environment.ExpandEnvironmentVariables(connectionDbFind ?? "");
}

static void LoadService(IServiceCollection services, string conn)
{
    services.AddDbContext<OmnisedeContext>(options => options.UseSqlServer(conn));
    LoadRepository(services);
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
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