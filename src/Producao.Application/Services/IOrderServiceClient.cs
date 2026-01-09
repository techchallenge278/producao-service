namespace Producao.Application.Services;

/// <summary>
/// Cliente HTTP para comunicação com o Order Service
/// </summary>
public interface IOrderServiceClient
{
    /// <summary>
    /// Atualiza o status de um pedido no Order Service
    /// </summary>
    Task<bool> UpdateOrderStatusAsync(Guid orderId, string status, CancellationToken cancellationToken = default);
}

