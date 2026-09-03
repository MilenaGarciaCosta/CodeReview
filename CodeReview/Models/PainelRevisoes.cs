namespace CodeReview.Models;

public class PainelRevisoes<T> where T : SolicitacaoMudanca
{
    private Queue<T> solicitacoesAguardando = new Queue<T>();

    private Dictionary<int, Desenvolvedor> atribuicoes = new Dictionary<int, Desenvolvedor>();

    private Dictionary<int, Revisao> revisoes = new Dictionary<int, Revisao>();

    private Stack<(int SolicitacaoId, DecisaoRevisao Decisao, DateTime Data)> historicoDecisoes = new Stack<(int, DecisaoRevisao, DateTime)>();

    private List<T> solicitacoes = new List<T>();

    public void AdicionarSolicitacao(T solicitacao)
    {
        solicitacoes.Add(solicitacao);
    }

    public void EnviarParaFila(T solicitacao)
    {
        solicitacao.Status = StatusSolicitacao.Pendente;
        solicitacoesAguardando.Enqueue(solicitacao);
    }

    public T ObterProximaSolicitacao()
    {
        return solicitacoesAguardando.Count > 0 ? solicitacoesAguardando.Peek() : null;
    }

    public T RemoverProximaSolicitacao()
    {
        return solicitacoesAguardando.Count > 0 ? solicitacoesAguardando.Dequeue() : null;
    }

    public void AtribuirRevisor(T solicitacao, Desenvolvedor revisor)
    {
        if (atribuicoes.ContainsKey(solicitacao.Id))
        {
            atribuicoes[solicitacao.Id] = revisor;
        }
        else
        {
            atribuicoes.Add(solicitacao.Id, revisor);
        }

        var revisao = new Revisao(solicitacao, revisor);
        revisoes[solicitacao.Id] = revisao;
        revisor.RevisoesAtribuidas.Add(solicitacao.Id);
        solicitacao.Status = StatusSolicitacao.EmRevisao;
    }

    public Revisao ObterRevisao(int solicitacaoId)
    {
        return revisoes.ContainsKey(solicitacaoId) ? revisoes[solicitacaoId] : null;
    }

    public Desenvolvedor ObterRevisor(int solicitacaoId)
    {
        return atribuicoes.ContainsKey(solicitacaoId) ? atribuicoes[solicitacaoId] : null;
    }

    public void RegistrarDecisao(int solicitacaoId, DecisaoRevisao decisao)
    {
        if (revisoes.ContainsKey(solicitacaoId))
        {
            var revisao = revisoes[solicitacaoId];
            revisao.RegistrarDecisao(decisao);
            historicoDecisoes.Push((solicitacaoId, decisao, DateTime.Now));
        }
    }

    public Stack<(int SolicitacaoId, DecisaoRevisao Decisao, DateTime Data)> ObterHistorico()
    {
        return new Stack<(int, DecisaoRevisao, DateTime)>(historicoDecisoes);
    }

    public List<T> ObterTodasSolicitacoes()
    {
        return solicitacoes;
    }

    public Queue<T> ObterFilaPendentes()
    {
        return solicitacoesAguardando;
    }

    public Dictionary<int, Desenvolvedor> ObterAtribuicoes()
    {
        return atribuicoes;
    }
}
