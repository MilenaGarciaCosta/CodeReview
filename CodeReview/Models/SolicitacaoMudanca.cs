namespace CodeReview.Models;

public class SolicitacaoMudanca
{
    public  string Titulo { get; set; }
    public HashSet<string> Etiquetas { get; set; }
}