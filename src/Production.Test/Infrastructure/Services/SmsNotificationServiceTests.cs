using Producao.Domain.ProductionOrders.Services;
using Producao.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace Production.Tests.Infrastructure.Services;

public class SmsNotificationServiceTests
{
    private readonly SmsNotificationService _service;

    public SmsNotificationServiceTests()
    {
        _service = new SmsNotificationService(
            Microsoft.Extensions.Logging.Abstractions.NullLogger<SmsNotificationService>.Instance);
    }

    [Fact]
    public async Task NotifyOrderStatusChangeAsync_WithCustomerId_ShouldComplete()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var previousStatus = "Received";
        var newStatus = "InPreparation";

        // Act
        var act = async () => await _service.NotifyOrderStatusChangeAsync(
            orderId, previousStatus, newStatus, customerId);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task NotifyOrderStatusChangeAsync_WithNullCustomerId_ShouldComplete()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        Guid? customerId = null;
        var previousStatus = "Received";
        var newStatus = "InPreparation";

        // Act
        var act = async () => await _service.NotifyOrderStatusChangeAsync(
            orderId, previousStatus, newStatus, customerId);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task NotifyOrderReadyAsync_WithCustomerId_ShouldComplete()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        // Act
        var act = async () => await _service.NotifyOrderReadyAsync(orderId, customerId);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task NotifyOrderReadyAsync_WithNullCustomerId_ShouldComplete()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        Guid? customerId = null;

        // Act
        var act = async () => await _service.NotifyOrderReadyAsync(orderId, customerId);

        // Assert
        await act.Should().NotThrowAsync();
    }
}

