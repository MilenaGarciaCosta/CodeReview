namespace CodeReview.Models;

public class MelhoriaCodigo : SolicitacaoMudanca
{
    public MelhoriaCodigo(string titulo, HashSet<string> etiquetas) : base(titulo, etiquetas)
    {
    }
}
