using CRMWeb.Domain.Enums;

namespace CRMWeb.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }

    public TipoPessoa TipoPessoa { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string CpfCnpj { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public DateTime DataCadastro { get; set; }

    public bool Ativo { get; set; }

    public ICollection<Endereco> Enderecos { get; set; } = [];
    public ICollection<Contato> Contatos { get; set; } = [];
}