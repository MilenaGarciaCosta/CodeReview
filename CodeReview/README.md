# Code Review - Gerenciador de revisões de código
Nome: Milena Garcia Sousa Costa | RM: 555111

Code Review é uma aplicação de console em `C# (.NET 10)` que simula o fluxo completo de revisão de código de uma equipe de desenvolvimento.

## Sobre

O sistema organiza solicitações de revisão, distribui as análises entre desenvolvedores e registra as decisões técnicas tomadas. Todos os dados permanecem apenas em **memória durante a execução**, sem necessidade de banco de dados, arquivos ou APIs externas.

## Funcionalidades

- Cadastro de desenvolvedores: Adiciona novos desenvolvedores da equipe ao sistema
- Criação de solicitações: Criar pedidos de revisão
- Sistema de etiquetas: Categorizar solicitações com múltiplas tags
- Fila de revisão: Organizar solicitações em ordem FIFO
- Atribuição de desenvolvedores: Distribuir revisões entre os desenvolvedores
- Comentários: Adicionar comentários em uma revisão
- Decisões: Aprovar, solicitar ajustes ou reprovar solicitações
- Histórico: Rastreie todas as decisões tomadas
- Estatísticas: Visualize performance de cada revisor

## Arquitetura

### Herança e Polimorfismo
```csharp
SolicitacaoMudanca (classe base)
├── CorrecaoBug
├── NovaFuncionalidade
└── MelhoriaCodigo
```

### Interfaces
- **IAvaliavel**: Define o contrato para avaliação de solicitações, implementado por `Desenvolvedor`

### Genéricos
- **PainelRevisoes<T>**: Classe genérica para gerenciar revisões de qualquer tipo derivado de `SolicitacaoMudanca`

### Estruturas dos dados

| Estrutura | Uso |
|-----------|-----|
| `Queue<T>` | Solicitações aguardando revisão (FIFO) |
| `Stack<T>` | Histórico reverso de decisões |
| `Dictionary<TKey, TValue>` | Rastrear atribuições e revisões |
| `List<T>` | Comentários e desenvolvedores |
| `HashSet<T>` | Etiquetas sem duplicidade |

## Estrutura do projeto

```
CodeReview/
├── Interfaces/
│   └── IAvaliavel.cs              # Interface para avaliação
├── Models/
│   ├── Desenvolvedor.cs           # Desenvolvedor (implementa IAvaliavel)
│   ├── SolicitacaoMudanca.cs      # Classe base + enums
│   ├── CorrecaoBug.cs             # Tipo: Correção de Bug
│   ├── NovaFuncionalidade.cs      # Tipo: Nova Funcionalidade
│   ├── MelhoriaCodigo.cs          # Tipo: Melhoria de Código
│   ├── Revisao.cs                 # Representa uma revisão
│   ├── Comentario.cs              # Comentário técnico
│   └── PainelRevisoes.cs          # Gerenciador genérico
└── Program.cs                      # Aplicação principal com menu
```

## Como executar o projeto

### Pré-requisitos
- .NET 10 instalado
- Visual Studio 2026 (ou qualquer IDE com suporte a .NET de sua preferencia)

### Instalação e execução

1. **Clone o repositório**
   ```bash
   git clone https://github.com/MilenaGarciaCosta/CodeReview.git
   cd CodeReview
   ```

2. **Abra a solução**
   ```bash
   # Com Visual Studio
   start CodeReview.slnx

   # Ou compile via terminal
   dotnet build
   ```

3. **Execute a aplicação**
   ```bash
   dotnet run
   ```

Ao executar a aplicação, você deve ver o menu principal:

```
======== CODE REVIEW - GERENCIADOR DE REVISÕES ========

1 - Cadastrar desenvolvedor
2 - Exibir desenvolvedores
3 - Criar solicitação de revisão
4 - Enviar solicitação para fila de revisão
5 - Atribuir próxima solicitação a um revisor
6 - Adicionar comentário técnico à revisão
7 - Tomar decisão sobre a revisão
8 - Exibir histórico de decisões
9 - Exibir estatísticas de revisões
0 - Sair
```

### Fluxo de uso (sugestão)

1. **Cadastre Desenvolvedores** (Opção 1)
   - Adicione nome e email de cada membro da equipe

2. **Crie Solicitações** (Opção 3)
   - Escolha o tipo (Bug, Feature, Refactor)
   - Adicione título e etiquetas

3. **Envie para Fila** (Opção 4)
   - Selecione uma solicitação para revisar

4. **Atribua Revisor** (Opção 5)
   - Escolha um desenvolvedor para revisar

5. **Adicione Comentários** (Opção 6)
   - O revisor adiciona considerações técnicas

6. **Tome uma Decisão** (Opção 7)
   - Aprove, solicite ajustes ou reprove

7. **Visualize Resultados** (Opções 8 e 9)
   - Histórico de todas as decisões
   - Estatísticas de performance

## Enums do sistema

### StatusSolicitacao
- `Pendente`: Aguardando revisão
- `EmRevisao`: Atualmente sendo revisada
- `Decidida`: Decisão já foi tomada

### DecisaoRevisao
- `Pendente`: Sem decisão ainda
- `Aprovada`: Solicitação aprovada
- `RevisaoPendente`: Precisar fazer ajustes
- `Reprovada`: Solicitação 
