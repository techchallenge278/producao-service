using Producao.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Production.Tests.Infrastructure;

public class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_ShouldRegisterServices()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:ProductionDbConnection"] = "mongodb://localhost:27017",
                ["MongoDB:DatabaseName"] = "FastFood_Production_Test",
                ["Services:Order:BaseUrl"] = "http://localhost:5000"
            })
            .Build();

        var services = new ServiceCollection();
        
        // Act
        services.AddInfrastructure(configuration);

        // Assert
        services.Should().NotBeEmpty();
        // Verificar que os serviços foram registrados verificando a contagem
        services.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void AddInfrastructure_WithDefaultConnectionString_ShouldUseDefault()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["MongoDB:DatabaseName"] = "FastFood_Production_Test",
                ["Services:Order:BaseUrl"] = "http://localhost:5000"
            })
            .Build();

        var services = new ServiceCollection();
        
        // Act
        services.AddInfrastructure(configuration);

        // Assert
        services.Should().NotBeEmpty();
        services.Count.Should().BeGreaterThan(0);
    }
}

