using Producao.Api.Models;
using Producao.Application.Commands;
using Producao.Application.Common.Exceptions;
using Producao.Api.DTOs;
using Producao.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Producao.Api.Controllers;

/// <summary>
/// Controlador para gerenciamento de pedidos em produção (visão da cozinha)
/// 
/// **Finalidade:** Gerencia o processo de produção dos pedidos, desde o recebimento até a finalização.
/// 
/// **Fluxo de produção:**
/// 1. **Recebido** - Pedido pago e recebido na cozinha
/// 2. **Em Preparação** - Pedido sendo preparado
/// 3. **Pronto** - Pedido pronto para retirada
/// 4. **Finalizado** - Pedido entregue ao cliente
/// 
/// **Níveis de acesso:**
/// - **Protegidos**: Todas as operações (apenas cozinha/administração)
/// </summary>
[ApiController]
[Route("api/v1/production")]
[Authorize] // Todas as operações requerem autenticação
public class ProductionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductionController> _logger;

    public ProductionController(IMediator mediator, ILogger<ProductionController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo pedido em produção
    /// </summary>
    [HttpPost("orders")]
    [AllowAnonymous]

    [ProducesResponseType(typeof(CreateProductionOrderCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductionOrder([FromBody] CreateProductionOrderDto request)
    {
        _logger.LogInformation("Criando pedido em produção para OrderId {OrderId}", request.OrderId);

        var command = new CreateProductionOrderCommand
        {
            OrderId = request.OrderId,
            CustomerId = request.CustomerId,
            CustomerName = request.CustomerName,
            TotalPrice = request.TotalPrice,
            Items = request.Items.Select(item => new CreateProductionOrderItemCommand
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetByOrderId), new { orderId = result.OrderId }, result);
    }

    /// <summary>
    /// Lista pedidos em produção (visão da cozinha)
    /// </summary>
    [HttpGet("orders")]
    [AllowAnonymous]

    [ProducesResponseType(typeof(GetOrdersInProductionQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersInProduction(
        [FromQuery] string? status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Buscando pedidos em produção. Status: {Status}, Página: {Page}", status, pageNumber);

        var query = new GetOrdersInProductionQuery
        {
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    /// <summary>
    /// Busca um pedido em produção pelo OrderId
    /// </summary>
    [HttpGet("orders/{orderId:guid}")]
    [AllowAnonymous]

    [ProducesResponseType(typeof(ProductionOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByOrderId([FromRoute] Guid orderId)
    {
        _logger.LogInformation("Buscando pedido em produção pelo OrderId {OrderId}", orderId);

        var query = new GetProductionOrderByOrderIdQuery { OrderId = orderId };
        var result = await _mediator.Send(query);

        if (!result.Success)
        {
            return NotFound(new ErrorResponse { Errors = new List<string> { result.Error ?? "Pedido não encontrado" } });
        }

        return Ok(result.Order);
    }

    /// <summary>
    /// Atualiza o status de um pedido em produção
    /// </summary>
    [HttpPut("orders/{orderId:guid}/status")]
    [AllowAnonymous]

    [ProducesResponseType(typeof(UpdateProductionOrderStatusCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid orderId, [FromBody] UpdateProductionOrderStatusDto request)
    {
        _logger.LogInformation("Atualizando status do pedido {OrderId} para {Status}", orderId, request.Status);

        try
        {
            var command = new UpdateProductionOrderStatusCommand
            {
                OrderId = orderId,
                Status = request.Status
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ErrorResponse { Errors = new List<string> { ex.Message } });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ErrorResponse { Errors = new List<string> { ex.Message } });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar status do pedido {OrderId}", orderId);
            return StatusCode(500, new ErrorResponse { Errors = new List<string> { "Erro ao processar solicitação" } });
        }
    }
}

