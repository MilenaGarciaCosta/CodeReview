namespace CodeReview.Models;

public enum StatusSolicitacao
{
    Pendente,
    EmRevisao,
    Decidida
}

public enum DecisaoRevisao
{
    Pendente,
    Aprovada,
    RevisaoPendente,
    Reprovada
}

public class SolicitacaoMudanca
{
    private static int _proximoId = 1;
    public int Id { get; private set; }
    public string Titulo { get; set; }
    public HashSet<string> Etiquetas { get; set; }
    public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;
    public DecisaoRevisao Decisao { get; set; } = DecisaoRevisao.Pendente;
    public DateTime DataCriacao { get; set; }

    public SolicitacaoMudanca(string titulo, HashSet<string> etiquetas)
    {
        Id = _proximoId++;
        Titulo = titulo;
        Etiquetas = etiquetas;
        DataCriacao = DateTime.Now;
    }
}