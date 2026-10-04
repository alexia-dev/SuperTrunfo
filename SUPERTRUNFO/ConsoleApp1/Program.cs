using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperTrunfoEstadosBrasil;

public class Carta
{
    public string Estado { get; }
    public decimal Populacao { get; }
    public decimal PIB { get; }
    public decimal Area { get; }
    public int PontosTuristicos { get; }
    public decimal DensidadeDemografica { get; }
    public bool SuperTrunfo { get; }

    public Carta(string estado, decimal populacao, decimal pib, decimal area, int pontosTuristicos, decimal densidade, bool superTrunfo = false)
    {
        Estado = estado;
        Populacao = populacao;
        PIB = pib;
        Area = area;
        PontosTuristicos = pontosTuristicos;
        DensidadeDemografica = densidade;
        SuperTrunfo = superTrunfo;
    }
}

public static class Program
{
    static readonly Random Random = new();
    static Queue<Carta> Jogador = new();
    static Queue<Carta> Computador = new();
    static int Vitorias;
    static int Derrotas;
    static int Empates;

    public static void Main()
    {
        Console.Title = "Super Trunfo • Estados do Brasil";
        while (true)
        {
            MostrarMenu();
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.D1: NovoJogo(); Jogar(); break;
                case ConsoleKey.D2: MostrarRegras(); break;
                case ConsoleKey.D3: return;
                default: Aviso("Escolha uma opção válida."); break;
            }
        }
    }

    static void MostrarMenu()
    {
        Limpar();
        Titulo("🇧🇷 SUPER TRUNFO", "ESTADOS DO BRASIL");
        Console.WriteLine("  ┌──────────────────────────────────────┐");
        Console.WriteLine("  │  [1] ▶  Novo jogo                    │");
        Console.WriteLine("  │  [2] ?   Como jogar                  │");
        Console.WriteLine("  │  [3] ×   Sair                        │");
        Console.WriteLine("  └──────────────────────────────────────┘");
        Console.WriteLine();
        Console.WriteLine($"  Histórico: {Vitorias} vitórias • {Derrotas} derrotas • {Empates} empates");
        Console.Write("\n  > ");
    }

    static void NovoJogo()
    {
        var cartas = new List<Carta>
        {
            new("São Paulo",46.65m,2.38m,248.2m,150,166.23m,true),
            new("Rio de Janeiro",17.46m,0.86m,43.8m,120,381.87m),
            new("Minas Gerais",21.17m,0.65m,586.5m,90,35.76m),
            new("Bahia",14.93m,0.31m,564.7m,80,26.03m),
            new("Paraná",11.52m,0.46m,199.3m,70,57.85m),
            new("Amazonas",4.21m,0.11m,1559.1m,40,2.67m),
            new("Ceará",9.13m,0.17m,148.9m,60,60.67m),
            new("Rio Grande do Sul",11.42m,0.48m,281.7m,75,40.37m),
            new("Pernambuco",9.54m,0.25m,98.1m,85,97.26m),
            new("Pará",8.12m,0.23m,1247.7m,55,6.52m),
            new("Santa Catarina",7.61m,0.35m,95.7m,95,79.49m),
            new("Goiás",7.06m,0.27m,340.1m,65,20.75m)
        };
        cartas = cartas.OrderBy(_ => Random.Next()).ToList();
        Jogador = new Queue<Carta>(cartas.Take(cartas.Count / 2));
        Computador = new Queue<Carta>(cartas.Skip(cartas.Count / 2));
    }

    static void Jogar()
    {
        bool jogadorEscolhe = true;
        while (Jogador.Count > 0 && Computador.Count > 0)
        {
            Limpar();
            Cabecalho();
            var sua = Jogador.Peek();
            var rival = Computador.Peek();

            MostrarCarta(sua);
            Console.WriteLine();
            Console.WriteLine("  🤖 CARTA DO COMPUTADOR");
            Console.WriteLine("  ╭──────────────────────────────────╮");
            Console.WriteLine("  │             ??????               │");
            Console.WriteLine("  │      escolha seu atributo!       │");
            Console.WriteLine("  ╰──────────────────────────────────╯\n");

            int atributo = EscolherAtributo(jogadorEscolhe);
            int resultado = Comparar(sua, rival, atributo);

            Limpar();
            Cabecalho();
            MostrarDuelo(sua, rival, atributo, resultado);

            var suaCarta = Jogador.Dequeue();
            var rivalCarta = Computador.Dequeue();

            if (resultado > 0)
            {
                Jogador.Enqueue(suaCarta);
                Jogador.Enqueue(rivalCarta);
                Vitorias++;
                jogadorEscolhe = true;
                Mensagem("★ VOCÊ LEVOU A RODADA!", ConsoleColor.Green);
            }
            else if (resultado < 0)
            {
                Computador.Enqueue(rivalCarta);
                Computador.Enqueue(suaCarta);
                Derrotas++;
                jogadorEscolhe = false;
                Mensagem("◆ O COMPUTADOR LEVOU A RODADA!", ConsoleColor.Red);
            }
            else
            {
                Jogador.Enqueue(suaCarta);
                Computador.Enqueue(rivalCarta);
                Empates++;
                Mensagem("◇ EMPATE — as cartas voltaram!", ConsoleColor.Yellow);
            }

            Console.WriteLine("\n  Pressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }

        Limpar();
        Titulo(Jogador.Count > 0 ? "🏆 VITÓRIA!" : "💥 FIM DE JOGO", "Super Trunfo");
        Console.WriteLine(Jogador.Count > 0 ? "  Você conquistou todas as cartas!" : "  O computador ficou com o baralho inteiro.");
        Console.WriteLine("\n  Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey(true);
    }

    static int EscolherAtributo(bool jogadorEscolhe)
    {
        if (!jogadorEscolhe)
        {
            int escolha = Random.Next(1, 6);
            Console.WriteLine($"  🤖 Computador escolheu: {NomeAtributo(escolha)}");
            System.Threading.Thread.Sleep(650);
            return escolha;
        }

        Console.WriteLine("  ESCOLHA O ATRIBUTO");
        Console.WriteLine("  ─────────────────────────");
        Console.WriteLine("  [1] População          ↑ maior");
        Console.WriteLine("  [2] PIB                ↑ maior");
        Console.WriteLine("  [3] Área               ↑ maior");
        Console.WriteLine("  [4] Pontos turísticos  ↑ maior");
        Console.WriteLine("  [5] Densidade          ↓ menor");
        Console.Write("\n  > ");

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int valor) && valor is >= 1 and <= 5)
                return valor;
            Console.Write("  Escolha 1–5: ");
        }
    }

    static int Comparar(Carta a, Carta b, int atributo)
    {
        if (a.SuperTrunfo && !b.SuperTrunfo) return 1;
        if (!a.SuperTrunfo && b.SuperTrunfo) return -1;
        if (a.SuperTrunfo && b.SuperTrunfo) return 0;

        return atributo switch
        {
            1 => a.Populacao.CompareTo(b.Populacao),
            2 => a.PIB.CompareTo(b.PIB),
            3 => a.Area.CompareTo(b.Area),
            4 => a.PontosTuristicos.CompareTo(b.PontosTuristicos),
            5 => b.DensidadeDemografica.CompareTo(a.DensidadeDemografica),
            _ => 0
        };
    }

    static void MostrarDuelo(Carta a, Carta b, int atributo, int resultado)
    {
        Console.WriteLine("  ╔══════════════════════════════════════════════════╗");
        Console.WriteLine($"  ║ {a.Estado,-22} VS {b.Estado,-22} ║");
        Console.WriteLine("  ╚══════════════════════════════════════════════════╝\n");
        Console.WriteLine($"  ⚔ {NomeAtributo(atributo)}");
        Console.WriteLine($"  VOCÊ       {Valor(a, atributo)}");
        Console.WriteLine($"  COMPUTADOR {Valor(b, atributo)}\n");

        if (resultado > 0) Mensagem("  ★ VOCÊ VENCEU!", ConsoleColor.Green);
        else if (resultado < 0) Mensagem("  ◆ COMPUTADOR VENCEU!", ConsoleColor.Red);
        else Mensagem("  ◇ EMPATE!", ConsoleColor.Yellow);
    }

    static void MostrarCarta(Carta c)
    {
        Console.WriteLine("  ╭──────────────────────────────────╮");
        Console.WriteLine($"  │ {(c.SuperTrunfo ? "★ SUPER TRUNFO ★" : "        SUA CARTA"),-32} │");
        Console.WriteLine("  ├──────────────────────────────────┤");
        Console.WriteLine($"  │ {c.Estado,-32} │");
        Console.WriteLine("  ├──────────────────────────────────┤");
        Console.WriteLine($"  │ População          {c.Populacao,8} mi │");
        Console.WriteLine($"  │ PIB                {c.PIB,8} bi │");
        Console.WriteLine($"  │ Área               {c.Area,8} mil │");
        Console.WriteLine($"  │ Turismo            {c.PontosTuristicos,8} pts │");
        Console.WriteLine($"  │ Densidade          {c.DensidadeDemografica,8} hab │");
        Console.WriteLine("  ╰──────────────────────────────────╯");
    }

    static string Valor(Carta c, int a) => a switch
    {
        1 => $"{c.Populacao} mi",
        2 => $"R$ {c.PIB} bi",
        3 => $"{c.Area} mil km²",
        4 => $"{c.PontosTuristicos} pts",
        5 => $"{c.DensidadeDemografica} hab/km²",
        _ => "-"
    };

    static string NomeAtributo(int a) => a switch
    {
        1 => "População",
        2 => "PIB",
        3 => "Área",
        4 => "Pontos turísticos",
        5 => "Densidade demográfica",
        _ => "-"
    };

    static void MostrarRegras()
    {
        Limpar();
        Titulo("📖 COMO JOGAR", "Super Trunfo");
        Console.WriteLine("  • Você recebe metade do baralho.");
        Console.WriteLine("  • Escolha um atributo da sua carta.");
        Console.WriteLine("  • População, PIB, área e turismo: MAIOR vence.");
        Console.WriteLine("  • Densidade: MENOR vence.");
        Console.WriteLine("  • ★ Super Trunfo ★ vence cartas comuns.");
        Console.WriteLine("  • Quem conquistar todas as cartas ganha.");
        Console.WriteLine("\n  Pressione qualquer tecla...");
        Console.ReadKey(true);
    }

    static void Cabecalho()
    {
        Console.WriteLine($"  🇧🇷 SUPER TRUNFO       Você {Jogador.Count}  ×  {Computador.Count} CPU");
        Console.WriteLine("  ─────────────────────────────────────────────────");
    }

    static void Titulo(string a, string b)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  {a}");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"  {b}");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void Mensagem(string texto, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine($"\n  {texto}");
        Console.ResetColor();
    }

    static void Aviso(string texto)
    {
        Mensagem($"⚠ {texto}", ConsoleColor.Yellow);
        System.Threading.Thread.Sleep(500);
    }

    static void Limpar() => Console.Clear();
}
