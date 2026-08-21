using OmniSedeBackend.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OmniSedeBackend.Config;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Implementations;
using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Services.Implementations;
using OmniSedeBackend.Services.Interfaces;

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
    services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);
    
    services.Configure<BlobConfig>(configuration.GetSection("Blob"));
    services.PostConfigure<BlobConfig>(blobConfig =>
    {
        blobConfig.BlobConn = Environment.ExpandEnvironmentVariables(blobConfig.BlobConn ?? string.Empty);
        blobConfig.BlobContainer = Environment.ExpandEnvironmentVariables(blobConfig.BlobContainer ?? string.Empty);
    });
    services.AddSingleton(sp => sp.GetRequiredService<IOptions<BlobConfig>>().Value);
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
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IUploaderService, UploaderService>();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    LoadSwagger(services);
}

static void LoadSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Inserisci il token nel formato: Bearer {token}"
        });
        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

static void StartApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseJwtMiddleware();
    app.MapControllers();

    app.Run();
}