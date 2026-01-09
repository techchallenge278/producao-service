namespace Producao.Api.Models;

/// <summary>
/// Modelo de resposta para erros na API
/// </summary>
public class ErrorResponse
{
    public ErrorResponse()
    {
        Errors = new List<string>();
    }

    public ErrorResponse(string error) : this()
    {
        Errors.Add(error);
    }

    public ErrorResponse(IEnumerable<string> errors) : this()
    {
        Errors.AddRange(errors);
    }

    public List<string> Errors { get; set; }
    public string? TraceId { get; set; }
}

