using System.ComponentModel.DataAnnotations;

namespace Producao.Api.DTOs;

public class UpdateProductionOrderStatusDto
{
    [Required(ErrorMessage = "Status é obrigatório")]
    public required string Status { get; set; } = string.Empty;
}

