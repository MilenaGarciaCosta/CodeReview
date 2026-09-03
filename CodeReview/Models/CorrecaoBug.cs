namespace CodeReview.Models;

public class CorrecaoBug : SolicitacaoMudanca
{
    public CorrecaoBug(string titulo, HashSet<string> etiquetas) : base(titulo, etiquetas)
    {
        
    }
}