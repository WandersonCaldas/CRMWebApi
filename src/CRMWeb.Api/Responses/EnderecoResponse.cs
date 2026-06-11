namespace CRMWeb.Domain.Responses;

public class EnderecoResponse
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Logradouro { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public string? Complemento { get; set; }

    public string Bairro { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string Uf { get; set; } = string.Empty;

    public string Cep { get; set; } = string.Empty;

    public bool Principal { get; set; }
}