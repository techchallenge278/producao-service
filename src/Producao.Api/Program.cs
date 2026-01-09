using Producao.Api.Filters;
using Producao.Application;
using Producao.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Producao.Api.Filters;
using System.Globalization;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;



var builder = WebApplication.CreateBuilder(args);

// Configurar o fuso horário padrão para o Brasil
TimeZoneInfo brazilTimeZone;
try
{
    brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
}
catch
{
    brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
}
AppDomain.CurrentDomain.SetData("TimeZone", brazilTimeZone);

// Configurar a cultura padrão para pt-BR
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));


// Add services to the container
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configurar autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        
        var authority = builder.Configuration["Auth:Authority"];
        var audience = builder.Configuration["Auth:Audience"];

        var useExternalAuth =
            !builder.Environment.IsDevelopment() &&
            !string.IsNullOrWhiteSpace(authority) &&
            !string.IsNullOrWhiteSpace(audience);

        if (!useExternalAuth)
        {
            var jwtSecret = builder.Configuration["Jwt:Secret"]
                ?? throw new InvalidOperationException("JWT Secret não configurado");

            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecret)
                ),
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        }
        else
        {
            options.Authority = authority;
            options.Audience = audience;
        }
    });

// Configurar políticas de autorização
builder.Services.AddAuthorizationBuilder() // NOSONAR S4834
    .AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
        {
            var roles = context.User.FindAll("cognito:groups")
                .Concat(context.User.FindAll("roles"))
                .Concat(context.User.FindAll("role"))
                .Select(c => c.Value);

            return roles.Any(role => role.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                                   role.Equals("administrator", StringComparison.OrdinalIgnoreCase));
        });
    });

// Add health checks
builder.Services.AddHealthChecks();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (string.IsNullOrEmpty(context.Request.Path) || context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

// Map health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

await app.RunAsync();

return 0;

public partial class Program
{
    protected Program() { } // NOSONAR S1118: Required for top-level statements with partial class
}
