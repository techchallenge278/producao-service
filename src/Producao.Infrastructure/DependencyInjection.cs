using Producao.Application.Services;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.Services;
using Producao.Infrastructure.Data;
using Producao.Infrastructure.Repositories;
using Producao.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace Producao.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar MongoDB
        var connectionString = configuration.GetConnectionString("ProductionDbConnection") 
            ?? "mongodb://localhost:27017";
        var databaseName = configuration["MongoDB:DatabaseName"] ?? "FastFood_Production";

        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        services.AddSingleton<IMongoDatabase>(mongoDatabase);
        services.AddScoped<ProductionDbContext>();
            
        // Registrar repositórios
        services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
        
        // Registrar serviço de notificação (Email por padrão)
        // Para usar SMS em vez de email, altere o registro abaixo para SmsNotificationService
        services.AddScoped<INotificationService, EmailNotificationService>();

        // Registrar HttpClient para comunicação com Order Service
        services.AddHttpClient<IOrderServiceClient, OrderServiceClient>();

        return services;
    }
}

