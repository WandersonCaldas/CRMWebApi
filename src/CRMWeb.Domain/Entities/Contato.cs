namespace CRMWeb.Domain.Entities;

public class Contato
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public bool Principal { get; set; }

    public bool Ativo { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public ICollection<Tarefa> Tarefas { get; set; } = [];
}