using CodeReview.Models;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

class Program
{
    static List<Desenvolvedor> desenvolvedores = new List<Desenvolvedor> { new Desenvolvedor("lena", "adsasd") };
    static PainelRevisoes<SolicitacaoMudanca> painelRevisoes = new();
    static string opcaoSelecionada;

    public static void Main(string[] args)
    {
        while (true)
        {
            do
            {
                Console.Clear();
                opcaoSelecionada = ExibirMenu();
            }
            while (!InputTextoValido(opcaoSelecionada));

            ExecutarAcoes();
        }
    }

    private static void ExecutarAcoes()
    {
        switch (opcaoSelecionada)
        {
            case "1":
                CadastraiDesenvolvedor();
                break;

            case "2":
                ExibirDesenvolvedores();
                break;

            case "3":
                CriarSolicitacao();
                break;

            case "4":
                EnviarParaFila();
                break;

            case "5":
                AtribuirRevisor();
                break;

            case "6":
                AdicionarComentario();
                break;

            case "7":
                TomardecisaoRevisao();
                break;

            case "8":
                ExibirHistorico();
                break;

            case "9":
                ExibirEstatisticas();
                break;

            case "0":
                Console.WriteLine("Saindo do programa...");
                Thread.Sleep(1000);
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Opção inválida, tente novamente...");
                Thread.Sleep(1500);
                break;
        }
    }

    private static string ExibirMenu()
    {
        Console.WriteLine("======== CODE REVIEW - GERENCIADOR DE REVISÕES ========\n");

        Console.WriteLine("1 - Cadastrar desenvolvedor");
        Console.WriteLine("2 - Exibir desenvolvedores");
        Console.WriteLine("3 - Criar solicitação de revisão");
        Console.WriteLine("4 - Enviar solicitação para fila de revisão");
        Console.WriteLine("5 - Atribuir próxima solicitação a um revisor");
        Console.WriteLine("6 - Adicionar comentário técnico à revisão");
        Console.WriteLine("7 - Tomar decisão sobre a revisão");
        Console.WriteLine("8 - Exibir histórico de decisões");
        Console.WriteLine("9 - Exibir estatísticas de revisões");
        Console.WriteLine("0 - Sair\n");

        Console.WriteLine("Insira o número correspondente a ação que deseja executar: ");

        return Console.ReadLine().Trim();
    }

    private static bool InputTextoValido(string opcaoSelecionada)
    {
        bool inputValido = !string.IsNullOrWhiteSpace(opcaoSelecionada);

        if (!inputValido)
        {
            Console.WriteLine("Entrada inválida, tente novamente...");
            Thread.Sleep(1000);
        }

        Console.Clear();
        return inputValido;
    }

    private static void CadastraiDesenvolvedor()
    {
        Desenvolvedor desenvolvedor = CriarDesenvolvedor();
        desenvolvedores.Add(desenvolvedor);
        Console.WriteLine("Desenvolvedor cadastrado com sucesso!");
        Thread.Sleep(1500);
    }

    private static void ExibirDesenvolvedores()
    {
        Console.WriteLine("\n======== DESENVOLVEDORES CADASTRADOS ========\n");
        if (desenvolvedores.Count == 0)
        {
            Console.WriteLine("Nenhum desenvolvedor cadastrado.");
        }
        else
        {
            foreach (var dev in desenvolvedores)
            {
                Console.WriteLine($"Nome: {dev.Nome} | Email: {dev.Email} | Revisões Realizadas: {dev.RevisoesRealizadas}");
            }
        }
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private static void EnviarParaFila()
    {
        var solicitacoes = painelRevisoes.ObterTodasSolicitacoes();

        if (solicitacoes.Count == 0)
        {
            Console.WriteLine("\nNenhuma solicitação criada.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("\n======== SOLICITAÇÕES CRIADAS ========\n");
        for (int i = 0; i < solicitacoes.Count; i++)
        {
            var s = solicitacoes[i];
            Console.WriteLine($"{i + 1}. ID: {s.Id} | Título: {s.Titulo} | Status: {s.Status}");
        }

        Console.WriteLine("\nDigite o número da solicitação para enviar para a fila: ");
        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= solicitacoes.Count)
        {
            var solicitacao = solicitacoes[indice - 1];
            painelRevisoes.EnviarParaFila(solicitacao);
            Console.WriteLine($"Solicitação ID {solicitacao.Id} enviada para fila de revisão!");
            Thread.Sleep(1500);
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(1500);
        }
    }

    private static void AtribuirRevisor()
    {
        var proximaSolicitacao = painelRevisoes.ObterProximaSolicitacao();

        if (proximaSolicitacao == null)
        {
            Console.WriteLine("\nNenhuma solicitação aguardando revisão.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine($"\n======== ATRIBUIR REVISOR ========");
        Console.WriteLine($"Solicitação ID {proximaSolicitacao.Id}: {proximaSolicitacao.Titulo}\n");

        if (desenvolvedores.Count == 0)
        {
            Console.WriteLine("Nenhum desenvolvedor disponível para revisar.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("Desenvolvedores disponíveis:");
        for (int i = 0; i < desenvolvedores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {desenvolvedores[i].Nome}");
        }

        Console.WriteLine("\nSelecione o revisor: ");
        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= desenvolvedores.Count)
        {
            var revisor = desenvolvedores[indice - 1];
            painelRevisoes.RemoverProximaSolicitacao();
            painelRevisoes.AtribuirRevisor(proximaSolicitacao, revisor);
            Console.WriteLine($"Solicitação atribuída a {revisor.Nome}!");
            Thread.Sleep(1500);
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(1500);
        }
    }

    private static void AdicionarComentario()
    {
        var atribuicoes = painelRevisoes.ObterAtribuicoes();

        if (atribuicoes.Count == 0)
        {
            Console.WriteLine("\nNenhuma solicitação em revisão.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("\n======== ADICIONAR COMENTÁRIO ========\n");
        Console.WriteLine("Solicitações em revisão:");
        var solicitacoes = atribuicoes.Keys.ToList();

        for (int i = 0; i < solicitacoes.Count; i++)
        {
            var revisao = painelRevisoes.ObterRevisao(solicitacoes[i]);
            if (revisao != null)
            {
                Console.WriteLine($"{i + 1}. ID {revisao.Solicitacao.Id}: {revisao.Solicitacao.Titulo} (Revisor: {revisao.Revisor.Nome})");
            }
        }

        Console.WriteLine("\nSelecione a solicitação: ");
        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= solicitacoes.Count)
        {
            var solicitacaoId = solicitacoes[indice - 1];
            var revisao = painelRevisoes.ObterRevisao(solicitacaoId);

            Console.WriteLine("Digite o comentário: ");
            string textoComentario = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(textoComentario))
            {
                var comentario = new Comentario(textoComentario, revisao.Revisor);
                revisao.AdicionarComentario(comentario);
                Console.WriteLine("Comentário adicionado com sucesso!");
                Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(1500);
        }
    }

    private static void TomardecisaoRevisao()
    {
        var atribuicoes = painelRevisoes.ObterAtribuicoes();

        if (atribuicoes.Count == 0)
        {
            Console.WriteLine("\nNenhuma solicitação em revisão.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("\n======== DECIDIR SOBRE REVISÃO ========\n");
        var solicitacoes = atribuicoes.Keys.ToList();

        for (int i = 0; i < solicitacoes.Count; i++)
        {
            var revisao = painelRevisoes.ObterRevisao(solicitacoes[i]);
            if (revisao != null)
            {
                Console.WriteLine($"{i + 1}. ID {revisao.Solicitacao.Id}: {revisao.Solicitacao.Titulo}");
            }
        }

        Console.WriteLine("\nSelecione a solicitação: ");
        if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= solicitacoes.Count)
        {
            var solicitacaoId = solicitacoes[indice - 1];

            Console.WriteLine("\nDecisão:");
            Console.WriteLine("1 - Aprovada");
            Console.WriteLine("2 - Revisão Pendente");
            Console.WriteLine("3 - Reprovada");
            Console.WriteLine("\nSelecione: ");

            if (int.TryParse(Console.ReadLine(), out int decisao) && decisao >= 1 && decisao <= 3)
            {
                var decisaoEnum = decisao switch
                {
                    1 => DecisaoRevisao.Aprovada,
                    2 => DecisaoRevisao.RevisaoPendente,
                    3 => DecisaoRevisao.Reprovada,
                    _ => DecisaoRevisao.Pendente
                };

                painelRevisoes.RegistrarDecisao(solicitacaoId, decisaoEnum);
                Console.WriteLine("Decisão registrada com sucesso!");
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("Opção inválida.");
                Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(1500);
        }
    }


    private static void ExibirHistorico()
    {
        var historico = painelRevisoes.ObterHistorico();

        Console.WriteLine("\n======== HISTÓRICO DE DECISÕES ========\n");

        if (historico.Count == 0)
        {
            Console.WriteLine("Nenhuma decisão registrada ainda.");
        }
        else
        {
            while (historico.Count > 0)
            {
                var (solicitacaoId, decisao, data) = historico.Pop();
                Console.WriteLine($"Solicitação ID {solicitacaoId}: {decisao} em {data:dd/MM/yyyy HH:mm:ss}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private static void ExibirEstatisticas()
    {
        Console.WriteLine("\n======== ESTATÍSTICAS DE REVISÕES ========\n");

        if (desenvolvedores.Count == 0)
        {
            Console.WriteLine("Nenhum desenvolvedor cadastrado.");
        }
        else
        {
            foreach (var dev in desenvolvedores)
            {
                Console.WriteLine($"Desenvolvedor: {dev.Nome}");
                Console.WriteLine($"  - Email: {dev.Email}");
                Console.WriteLine($"  - Revisões Completadas: {dev.RevisoesRealizadas}");
                Console.WriteLine($"  - Revisões Atribuídas: {dev.RevisoesAtribuidas.Count}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    private static void CriarSolicitacao()
    {
        Console.WriteLine("\n===== CRIAR SOLICITAÇÃO DE REVISÃO =====\n");

        string titulo;
        string etiquetas;
        string tipoSolicitacao;
        HashSet<string> etiquetasList;

        do
        {
            Console.WriteLine("Selecione o tipo de solicitação:");
            Console.WriteLine("1 - Correção de Bug");
            Console.WriteLine("2 - Nova Funcionalidade");
            Console.WriteLine("3 - Melhoria de Código");
            Console.WriteLine("\nDigite o número correspondente: ");
            tipoSolicitacao = Console.ReadLine().Trim();
        }
        while (!Regex.IsMatch(tipoSolicitacao, @"^[1-3]$"));

        do
        {
            Console.WriteLine("\nDigite o título da solicitação: ");
            titulo = Console.ReadLine();
        }
        while (string.IsNullOrWhiteSpace(titulo));

        do
        {
            Console.WriteLine("Adicione etiquetas à solicitação (separadas por vírgula): ");
            etiquetas = Console.ReadLine();
        }
        while (string.IsNullOrWhiteSpace(etiquetas));

        etiquetasList = new HashSet<string>(etiquetas.Split(',').Select(e => e.Trim()));

        SolicitacaoMudanca solicitacao = tipoSolicitacao switch
        {
            "1" => new CorrecaoBug(titulo, etiquetasList),
            "2" => new NovaFuncionalidade(titulo, etiquetasList),
            "3" => new MelhoriaCodigo(titulo, etiquetasList),
            _ => new SolicitacaoMudanca(titulo, etiquetasList)
        };

        painelRevisoes.AdicionarSolicitacao(solicitacao);

        Console.WriteLine($"\nSolicitação de revisão ({ObterNomeTipo(tipoSolicitacao)}) criada com sucesso!");
        Console.WriteLine($"ID da solicitação: {solicitacao.Id}");
        Thread.Sleep(1500);
    }

    private static Desenvolvedor CriarDesenvolvedor()
    {
        Console.WriteLine("\n===== CRIAR DESENVOLVEDOR =====\n");

        string nome;
        string email;

        do
        {
            Console.WriteLine("Digite o nome do desenvolvedor: ");
            nome = Console.ReadLine();
        }
        while (string.IsNullOrWhiteSpace(nome));

        do
        {
            Console.WriteLine("Digite o e-mail do desenvolvedor: ");
            email = Console.ReadLine();
        }
        while (string.IsNullOrWhiteSpace(email));

        return new Desenvolvedor(nome, email);
    }

    private static string ObterNomeTipo(string tipo)
    {
        return tipo switch
        {
            "1" => "Correção de Bug",
            "2" => "Nova Funcionalidade",
            "3" => "Melhoria de Código",
            _ => "Desconhecido"
        };
    }
}
