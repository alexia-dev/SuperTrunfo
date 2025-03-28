```markdown
# Super Trunfo - Jogo de Cartas em C#

Este projeto é uma implementação em C# do clássico jogo de cartas **Super Trunfo**, desenvolvido como parte de um desafio escolar. O jogo segue as regras tradicionais, onde os jogadores comparam atributos de cartas para vencer as rodadas e coletar as cartas do oponente.

---

## 🚀 Funcionalidades

- **Cartas personalizadas**: Carrega cartas de um arquivo JSON, permitindo fácil personalização.
- **Interface simples (CLI)**: Jogabilidade via linha de comando com feedback claro das ações.
- **Lógica de Super Trunfo**: Cartas Super Trunfo vencem automaticamente, exceto em empates entre elas.
- **Sistema de turnos dinâmico**: O vencedor de cada rodada escolhe o próximo atributo.
- **Tratamento de empates**: Acumula cartas no "pote" e joga novamente até haver um vencedor.

---

## ⚙️ Pré-requisitos

- [.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download)

---

## 📥 Instalação e Execução

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/super-trunfo-csharp.git
   cd super-trunfo-csharp
   ```

2. **Crie o arquivo `cards.json`** na pasta `Data` (exemplo abaixo):
   ```json
   [
       {
           "Name": "Carro Veloz",
           "Velocidade": 200,
           "Potencia": 150,
           "Aceleracao": 8.5,
           "Peso": 1000,
           "SuperTrunfo": false
       },
       {
           "Name": "Super Carro",
           "Velocidade": 250,
           "Potencia": 300,
           "Aceleracao": 7.0,
           "Peso": 900,
           "SuperTrunfo": true
       }
   ]
   ```

3. **Execute o projeto**:
   ```bash
   dotnet run
   ```

---

## 📜 Regras do Jogo

1. **Início**:
   - Cada jogador recebe metade do baralho.
   - O jogador 1 inicia escolhendo o primeiro atributo.

2. **Rodada**:
   - As cartas do topo dos baralhos são comparadas.
   - O atributo escolhido pelo vencedor da rodada anterior determina o vencedor.
   - **Super Trunfo**: Vence automaticamente, exceto se ambas as cartas forem Super Trunfo.

3. **Empate**:
   - Cartas vão para o "pote".
   - Nova rodada é jogada para desempate, com o vencedor levando todas as cartas acumuladas.

4. **Vitória**:
   - O jogador que coletar todas as cartas vence.

---

## 📂 Estrutura do Projeto

```
super-trunfo-csharp/
├── SuperTrunfo/
│   ├── Models/           # Classes do jogo
│   │   ├── Card.cs
│   │   ├── Deck.cs
│   │   └── Player.cs
│   ├── Services/        # Lógica do jogo
│   │   └── GameService.cs
│   ├── Data/            # Arquivo de cartas
│   │   └── cards.json
│   └── Program.cs       # Ponto de entrada
├── README.md
└── SuperTrunfo.csproj   # Configuração do projeto
```

---

## 🛠️ Personalização

Para criar **novas cartas** ou **novos atributos**, edite o arquivo `Data/cards.json` seguindo o formato:
```json
{
    "Name": "Nome da Carta",
    "Atributo1": valor_numérico,
    "Atributo2": valor_numérico,
    "SuperTrunfo": false
}
```

---

## Exemplo de Código em C#

```csharp
// Modelo de uma Carta
public class Card
{
    public string Name { get; set; }
    public Dictionary<string, double> Attributes { get; set; }
    public bool SuperTrunfo { get; set; }
}

// Classe do Jogo
public class GameService
{
    public void StartGame(Player player1, Player player2)
    {
        while (player1.HasCards() && player2.HasCards())
        {
            // Lógica das rodadas
        }
    }
}
```

---

## 🤝 Contribuição

Siga os passos:
1. Faça um fork do projeto.
2. Crie uma branch: `git checkout -b minha-feature`.
3. Faça commit: `git commit -m 'Adiciona recurso'`.
4. Faça push: `git push origin minha-feature`.
5. Abra um Pull Request.

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja [LICENSE](LICENSE) para detalhes.

---

## ✉️ Contato

**Aléxia Mendes**  
- Email: alexiamendesassis@yahoo.com  
- GitHub: [@alexia-dev](https://github.com/alexia-dev)
```

### Principais Adaptações para C#:
1. **Estrutura de Projeto**: Organização típica de pastas do .NET com separação em `Models`, `Services` e `Data`.
2. **Sintaxe C#**: Uso de propriedades (`public string Name { get; set; }`) e métodos específicos da linguagem.
3. **Gerenciamento de Dependências**: Arquivo `.csproj` para configuração do projeto.
4. **Execução**: Comandos `dotnet run` em vez de Python.

Customize os caminhos, nomes de arquivos e detalhes de contato conforme necessário! 😊
