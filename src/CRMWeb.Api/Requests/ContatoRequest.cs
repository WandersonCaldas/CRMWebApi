using System.ComponentModel.DataAnnotations;

namespace CRMWeb.Domain.Requests;

public class ContatoRequest
{
    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Cargo { get; set; }

    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Telefone { get; set; }

    public bool Principal { get; set; }
}