using CodeReview.Interfaces;

namespace CodeReview.Models;

public record Desenvolvedor(
    string Nome,
    string Email
) : IAvaliavel
{
    public int RevisoesRealizadas { get; set; } = 0;
    public List<int> RevisoesAtribuidas { get; set; } = new();
    public List<int> RevisoesCompletadas { get; set; } = new();

    public void AvaliarSolicitacao(SolicitacaoMudanca solicitacao)
    {
        if (solicitacao != null)
        {
            RevisoesRealizadas++;
            RevisoesCompletadas.Add(solicitacao.Id);
        }
    }
}
