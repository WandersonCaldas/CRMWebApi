namespace CRMWeb.Domain.Responses;

public class ContatoResponse
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public bool Principal { get; set; }

    public bool Ativo { get; set; }
}