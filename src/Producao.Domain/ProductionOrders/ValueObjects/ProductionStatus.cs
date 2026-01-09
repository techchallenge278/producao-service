namespace Producao.Domain.ProductionOrders.ValueObjects;

/// <summary>
/// Status possíveis de um pedido em produção
/// </summary>
public enum ProductionStatus
{
    /// <summary>
    /// Pedido recebido (aguardando início da preparação)
    /// </summary>
    Received = 0,

    /// <summary>
    /// Pedido em preparação
    /// </summary>
    InPreparation = 1,

    /// <summary>
    /// Pedido pronto para retirada
    /// </summary>
    Ready = 2,

    /// <summary>
    /// Pedido finalizado (entregue ao cliente)
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Pedido cancelado
    /// </summary>
    Cancelled = 4
}

