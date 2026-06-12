public class AgendaResponse
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int? ContatoId { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFim { get; set; }

    public bool DiaTodo { get; set; }

    public bool Ativo { get; set; }
}