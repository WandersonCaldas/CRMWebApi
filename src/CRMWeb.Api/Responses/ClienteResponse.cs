using CRMWeb.Domain.Enums;

namespace CRMWeb.Domain.Responses;

public class ClienteResponse
{
    public int Id { get; set; }

    public TipoPessoa TipoPessoa { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string CpfCnpj { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public DateTime DataCadastro { get; set; }

    public bool Ativo { get; set; }
}