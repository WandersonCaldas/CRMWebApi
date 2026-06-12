using System.ComponentModel.DataAnnotations;

namespace CRMWeb.Domain.Requests;

public class TarefaRequest
{
    public int? ContatoId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Descricao { get; set; }

    public DateTime DataVencimento { get; set; }
}