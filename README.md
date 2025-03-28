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
