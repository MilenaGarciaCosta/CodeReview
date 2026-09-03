namespace CodeReview.Models;

public class NovaFuncionalidade : SolicitacaoMudanca
{
    public NovaFuncionalidade(string titulo, HashSet<string> etiquetas) : base(titulo, etiquetas)
    {
    }
}
