using System.ComponentModel.DataAnnotations;

public class EnderecoRequest
{
    [Required]
    [MaxLength(200)]
    public string Logradouro { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Numero { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Complemento { get; set; }

    [Required]
    [MaxLength(100)]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    [StringLength(2)]
    public string Uf { get; set; } = string.Empty;

    [Required]
    [StringLength(8)]
    public string Cep { get; set; } = string.Empty;

    public bool Principal { get; set; }
}