namespace CodeReview.Models;

public class SolicitacaoMudanca
{
    public SolicitacaoMudanca(string titulo)
    {
        titulo = Titulo;
    }

    public  string Titulo { get; set; }
    public HashSet<string> Etiquetas { get; set; }
}