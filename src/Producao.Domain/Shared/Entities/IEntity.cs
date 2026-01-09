namespace Producao.Domain.Shared.Entities;

/// <summary>
/// Interface base para todas as entidades
/// </summary>
public interface IEntity
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    void SetUpdatedAt();
}

