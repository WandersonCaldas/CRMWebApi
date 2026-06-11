using System.ComponentModel.DataAnnotations;
using CRMWeb.Domain.Enums;

namespace CRMWeb.Domain.Requests;

public class ClienteRequest
{
    public TipoPessoa TipoPessoa { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(14)]
    public string CpfCnpj { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Telefone { get; set; }
}