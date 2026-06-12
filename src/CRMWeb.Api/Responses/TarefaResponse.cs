namespace CRMWeb.Domain.Responses;

public class TarefaResponse
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int? ContatoId { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime DataVencimento { get; set; }

    public bool Concluida { get; set; }

    public DateTime? DataConclusao { get; set; }

    public bool Ativo { get; set; }
}