using System.ComponentModel.DataAnnotations;

namespace Producao.Api.DTOs;

public class CreateProductionOrderDto
{
    [Required(ErrorMessage = "OrderId é obrigatório")]
    public required Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "TotalPrice é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice deve ser maior que zero")]
    public required decimal TotalPrice { get; set; }

    [Required(ErrorMessage = "Items são obrigatórios")]
    [MinLength(1, ErrorMessage = "O pedido deve ter pelo menos um item")]
    public required List<CreateProductionOrderItemDto> Items { get; set; } = new();
}

public class CreateProductionOrderItemDto
{
    [Required(ErrorMessage = "ProductId é obrigatório")]
    public required Guid ProductId { get; set; }

    [Required(ErrorMessage = "ProductName é obrigatório")]
    public required string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity é obrigatória")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity deve ser maior que zero")]
    public required int Quantity { get; set; }

    [Required(ErrorMessage = "UnitPrice é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice deve ser maior que zero")]
    public required decimal UnitPrice { get; set; }
}

