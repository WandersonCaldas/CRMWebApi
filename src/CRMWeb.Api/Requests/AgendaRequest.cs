using System.ComponentModel.DataAnnotations;

public class AgendaRequest
{
    [Required]
    public int ClienteId { get; set; }

    public int? ContatoId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Descricao { get; set; }

    [Required]
    public DateTime DataInicio { get; set; }

    [Required]
    public DateTime DataFim { get; set; }

    public bool DiaTodo { get; set; }
}