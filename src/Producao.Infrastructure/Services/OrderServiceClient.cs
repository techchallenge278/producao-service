using Producao.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Producao.Infrastructure.Services;

/// <summary>
/// Implementação do cliente HTTP para comunicação com o Order Service
/// </summary>
public class OrderServiceClient : IOrderServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OrderServiceClient> _logger;

    public OrderServiceClient(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<OrderServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        
        var orderServiceBaseUrl = configuration["Services:Order:BaseUrl"] 
            ?? throw new InvalidOperationException("Services:Order:BaseUrl não configurado");
        
        _httpClient.BaseAddress = new Uri(orderServiceBaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string status, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Chamando Order Service para atualizar status. OrderId: {OrderId}, Status: {Status}", orderId, status);

            var requestBody = new { Status = status };
            var response = await _httpClient.PutAsJsonAsync(
                $"/api/v1/orders/{orderId}/status",
                requestBody,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Erro ao atualizar status no Order Service. Status: {Status}, Error: {Error}", 
                    response.StatusCode, errorContent);
                
                return false;
            }

            _logger.LogInformation("Status atualizado com sucesso no Order Service. OrderId: {OrderId}, Status: {Status}", 
                orderId, status);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao atualizar status no Order Service");
            return false;
        }
    }
}

