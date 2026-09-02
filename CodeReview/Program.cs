using CodeReview.Models;

class Program
{
    public static void Main(string[] args)
    {
        List<Desenvolvedor> desenvolvedores = new();
        string opcaoSelecionada;

        do
        {
            Console.Clear();
            opcaoSelecionada = ExibirMenu();
        }
        while (!InputTextoValido(opcaoSelecionada));

        switch (opcaoSelecionada)
        {
            case "1":
                Desenvolvedor desenvolvedor = CriarDesenvolvedor();
                desenvolvedores.Add(desenvolvedor);
                break;
        }

        Console.Clear();
        ExibirMenu();
    }

    private static string ExibirMenu()
    {
        Console.WriteLine("======== CODE REVIEW ========");

        ExibirOpcoesMenu();

        Console.WriteLine("Insira o número correspondente a ação que deseja executar: ");

       //MelhoriaCodigo aaa = new MelhoriaCodigo("aaa");

        return Console.ReadLine().Trim();
    }

    private static void ExibirOpcoesMenu()
    {
        Console.WriteLine("1 - Cadastrar desenvolvedor");
        Console.WriteLine("2 - Tarefa 2");
        Console.WriteLine("3 - Tarefa 3");
    }

    private static bool InputTextoValido(string opcaoSelecionada)
    {
        bool inputValido = opcaoSelecionada is "" ? false : true;

        if (!inputValido)
        {
            Console.WriteLine("Entrada inválida, tente novamente...");
            Thread.Sleep(1000);
        }

        Console.Clear();
        return inputValido;
    }

    private static Desenvolvedor CriarDesenvolvedor()
    {
        Console.WriteLine("===== CRIAR DESENVOLVEDOR =====");

        string nome;
        string email;

        do
        {
            Console.WriteLine("Digite o nome do desenvolvedor: ");
            nome = Console.ReadLine();
        }
        while (!InputTextoValido(nome));

        do
        {
            Console.WriteLine("Digite o e-mail do desenvolvedor: ");
            email = Console.ReadLine();
        }
        while (!InputTextoValido(email));

        Console.WriteLine("Desenvolvedor cadastrado com sucesso!");
        return new Desenvolvedor(nome, email);
    }
}

//Criar um menu interativo para acessar as funcionalidades do sistema.


//Criar solicitações de mudança de tipos diferentes.
//Adicionar uma ou mais etiquetas a cada solicitação.

//Cadastrar desenvolvedores.
//Enviar uma solicitação para a fila de revisão.
//Atribuir a próxima solicitação da fila a um revisor.
//Adicionar comentários técnicos à revisão.
//Aprovar, solicitar ajustes ou reprovar uma solicitação.
//Registrar cada decisão no histórico.
//Exibir a quantidade de revisões realizadas por cada desenvolvedor.