namespace CodeReview.Models;

public class MelhoriaCodigo : SolicitacaoMudanca
{

    public MelhoriaCodigo(string titulo, HashSet<string>aaa, string teste)
    {
        teste = Teste;
    }

    public string Teste { get; set; }
}
