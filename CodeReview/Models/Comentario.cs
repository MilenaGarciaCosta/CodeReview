namespace CodeReview.Models;

public class Comentario
{
    public string Texto { get; set; }
    public Desenvolvedor Autor { get; set; }
    public DateTime DataComentario { get; set; }

    public Comentario(string texto, Desenvolvedor autor)
    {
        Texto = texto;
        Autor = autor;
        DataComentario = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[{DataComentario:dd/MM/yyyy HH:mm}] {Autor.Nome}: {Texto}";
    }
}
