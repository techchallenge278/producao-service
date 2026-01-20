using Producao.Application.Services;
using Producao.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;

namespace Production.Tests.Infrastructure.Services;

public class OrderServiceClientTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly OrderServiceClient _client;

    public OrderServiceClientTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(x => x["Services:Order:BaseUrl"]).Returns("http://localhost:5000");

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost:5000")
        };

        _client = new OrderServiceClient(
            httpClient,
            _configurationMock.Object,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<OrderServiceClient>.Instance);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithSuccessResponse_ShouldReturnTrue()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = "Ready";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{}")
            });

        // Act
        var result = await _client.UpdateOrderStatusAsync(orderId, status);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithErrorResponse_ShouldReturnFalse()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = "Ready";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Error message")
            });

        // Act
        var result = await _client.UpdateOrderStatusAsync(orderId, status);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithException_ShouldReturnFalse()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = "Ready";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        // Act
        var result = await _client.UpdateOrderStatusAsync(orderId, status);

        // Assert
        result.Should().BeFalse();
    }
}

