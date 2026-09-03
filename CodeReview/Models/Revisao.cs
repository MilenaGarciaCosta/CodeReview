namespace CodeReview.Models;

public class Revisao
{
    public int Id { get; set; }
    public SolicitacaoMudanca Solicitacao { get; set; }
    public Desenvolvedor Revisor { get; set; }
    public List<Comentario> Comentarios { get; set; } = new();
    public DecisaoRevisao Decisao { get; set; } = DecisaoRevisao.Pendente;
    public DateTime DataAtribuicao { get; set; }
    public DateTime? DataDecisao { get; set; }

    public Revisao(SolicitacaoMudanca solicitacao, Desenvolvedor revisor)
    {
        Solicitacao = solicitacao;
        Revisor = revisor;
        DataAtribuicao = DateTime.Now;
    }

    public void RegistrarDecisao(DecisaoRevisao decisao)
    {
        Decisao = decisao;
        Solicitacao.Decisao = decisao;
        DataDecisao = DateTime.Now;

        if (decisao == DecisaoRevisao.Aprovada)
        {
            Solicitacao.Status = StatusSolicitacao.Decidida;
            Revisor.AvaliarSolicitacao(Solicitacao);
        }
    }

    public void AdicionarComentario(Comentario comentario)
    {
        Comentarios.Add(comentario);
        Solicitacao.Status = StatusSolicitacao.EmRevisao;
    }
}
