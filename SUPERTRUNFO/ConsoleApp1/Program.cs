using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuperTrunfoEstadosBrasil
{
    public class Carta
    {
        public string Estado { get; set; }
        public decimal Populacao { get; set; } // Em milhões
        public decimal PIB { get; set; }       // Em bilhões R$
        public decimal Area { get; set; }       // Em mil km²
        public int PontosTuristicos { get; set; }
        public decimal DensidadeDemografica { get; set; } // Hab/km²
        public bool SuperTrunfo { get; set; }

        public Carta(string estado, decimal populacao, decimal pib, decimal area, 
                    int pontosTuristicos, decimal densidade, bool superTrunfo = false)
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

    class Program
    {
        static List<Carta> baralho;
        static Queue<Carta> jogador;
        static Queue<Carta> computador;
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.Title = "Super Trunfo Estados do Brasil";
            MostrarMenuPrincipal();
        }

        static void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🇧🇷 SUPER TRUNFO ESTADOS BRASIL 🇧🇷");
                Console.WriteLine("====================================");
                Console.WriteLine("1 - Iniciar Novo Jogo");
                Console.WriteLine("2 - Ver Regras");
                Console.WriteLine("3 - Sair");
                Console.Write("\nEscolha: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InicializarJogo();
                        IniciarJogo();
                        break;
                    case "2":
                        MostrarRegras();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void InicializarJogo()
        {
            baralho = new List<Carta>
            {
                new Carta("São Paulo", 46.65m, 2.38m, 248.2m, 150, 166.23m, true),
                new Carta("Rio de Janeiro", 17.46m, 0.86m, 43.8m, 120, 381.87m),
                new Carta("Minas Gerais", 21.17m, 0.65m, 586.5m, 90, 35.76m),
                new Carta("Bahia", 14.93m, 0.31m, 564.7m, 80, 26.03m),
                new Carta("Paraná", 11.52m, 0.46m, 199.3m, 70, 57.85m),
                new Carta("Amazonas", 4.21m, 0.11m, 1559.1m, 40, 2.67m),
                new Carta("Ceará", 9.13m, 0.17m, 148.9m, 60, 60.67m),
                new Carta("Rio Grande do Sul", 11.42m, 0.48m, 281.7m, 75, 40.37m)
            };

            EmbaralharCartas();
            DistribuirCartas();
        }

        static void EmbaralharCartas()
        {
            baralho = baralho.OrderBy(c => random.Next()).ToList();
        }

        static void DistribuirCartas()
        {
            jogador = new Queue<Carta>(baralho.Take(baralho.Count / 2));
            computador = new Queue<Carta>(baralho.Skip(baralho.Count / 2));
        }

        static void MostrarRegras()
        {
            Console.Clear();
            Console.WriteLine("=== REGRAS DO JOGO ===");
            Console.WriteLine("1. Cada jogador recebe metade do baralho de estados");
            Console.WriteLine("2. Escolha um atributo para comparar");
            Console.WriteLine("3. Atributos com MAIOR valor vencem: População, PIB, Área, Pontos Turísticos");
            Console.WriteLine("4. Atributo com MENOR valor vence: Densidade Demográfica");
            Console.WriteLine("5. A carta Super Trunfo (São Paulo) vence qualquer outra");
            Console.WriteLine("6. O perdedor da rodada entrega sua carta ao vencedor");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        static void IniciarJogo()
        {
            while (jogador.Count > 0 && computador.Count > 0)
            {
                Console.Clear();
                MostrarPlacar();
                
                Carta cartaJogador = jogador.Peek();
                Carta cartaComputador = computador.Peek();

                Console.WriteLine("\nSua carta atual:");
                MostrarDetalhesCarta(cartaJogador);

                int atributo = MostrarMenuAtributos();
                int resultado = CompararCartas(cartaJogador, cartaComputador, atributo);

                Console.WriteLine("\nCarta do Computador:");
                MostrarDetalhesCarta(cartaComputador);
                MostrarComparacaoAtributos(cartaJogador, cartaComputador, atributo);
                ProcessarResultadoRodada(resultado);

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

            MostrarResultadoFinal();
        }

        static void MostrarDetalhesCarta(Carta carta)
        {
            Console.WriteLine($"\nEstado: {carta.Estado}");
            Console.WriteLine($"1. População: {carta.Populacao} milhões");
            Console.WriteLine($"2. PIB: R${carta.PIB} bilhões");
            Console.WriteLine($"3. Área: {carta.Area} mil km²");
            Console.WriteLine($"4. Pontos Turísticos: {carta.PontosTuristicos}");
            Console.WriteLine($"5. Densidade Demográfica: {carta.DensidadeDemografica} hab/km²");
            if (carta.SuperTrunfo) Console.WriteLine("🌟 SUPER TRUNFO 🌟");
        }

        static int MostrarMenuAtributos()
        {
            int opcao;
            do
            {
                Console.WriteLine("\nEscolha o atributo:");
                Console.WriteLine("1 - População");
                Console.WriteLine("2 - PIB");
                Console.WriteLine("3 - Área");
                Console.WriteLine("4 - Pontos Turísticos");
                Console.WriteLine("5 - Densidade Demográfica");
                Console.Write("Opção: ");
            } while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 5);

            return opcao;
        }

        static int CompararCartas(Carta jogador, Carta computador, int atributo)
        {
            if (jogador.SuperTrunfo && !computador.SuperTrunfo) return 1;
            if (!jogador.SuperTrunfo && computador.SuperTrunfo) return -1;

            switch (atributo)
            {
                case 1: return jogador.Populacao.CompareTo(computador.Populacao);
                case 2: return jogador.PIB.CompareTo(computador.PIB);
                case 3: return jogador.Area.CompareTo(computador.Area);
                case 4: return jogador.PontosTuristicos.CompareTo(computador.PontosTuristicos);
                case 5: return computador.DensidadeDemografica.CompareTo(jogador.DensidadeDemografica); // Menor valor ganha
                default: return 0;
            }
        }

        static void MostrarComparacaoAtributos(Carta jogador, Carta computador, int atributo)
        {
            string[] atributos = { 
                "População (maior ganha)", 
                "PIB (maior ganha)", 
                "Área (maior ganha)", 
                "Pontos Turísticos (maior ganha)", 
                "Densidade Demográfica (menor ganha)" 
            };

            Console.WriteLine($"\nComparando {atributos[atributo-1]}:");
            Console.WriteLine($"Jogador: {ObterValorAtributo(jogador, atributo)}");
            Console.WriteLine($"Computador: {ObterValorAtributo(computador, atributo)}");
        }

        static string ObterValorAtributo(Carta carta, int atributo)
        {
            return atributo switch
            {
                1 => $"{carta.Populacao} milhões",
                2 => $"R$ {carta.PIB} bilhões",
                3 => $"{carta.Area} mil km²",
                4 => $"{carta.PontosTuristicos} pontos",
                5 => $"{carta.DensidadeDemografica} hab/km²",
                _ => "Atributo inválido"
            };
        }

        static void ProcessarResultadoRodada(int resultado)
        {
            switch (resultado)
            {
                case 1:
                    Console.WriteLine("\n✅ Você venceu esta rodada!");
                    jogador.Enqueue(computador.Dequeue());
                    jogador.Enqueue(jogador.Dequeue());
                    break;
                case -1:
                    Console.WriteLine("\n❌ Computador venceu esta rodada!");
                    computador.Enqueue(jogador.Dequeue());
                    computador.Enqueue(computador.Dequeue());
                    break;
                default:
                    Console.WriteLine("\n⚖ Empate! As cartas voltam para os baralhos.");
                    jogador.Enqueue(jogador.Dequeue());
                    computador.Enqueue(computador.Dequeue());
                    break;
            }
        }

        static void MostrarPlacar()
        {
            Console.WriteLine($"📊 Placar: Jogador {jogador.Count} x {computador.Count} Computador");
            Console.WriteLine($"📚 Cartas restantes: {jogador.Count + computador.Count}");
        }

        static void MostrarResultadoFinal()
        {
            Console.Clear();
            if (jogador.Count == 0)
            {
                Console.WriteLine("😞 Derrota! O computador venceu o jogo!");
            }
            else
            {
                Console.WriteLine("🎉 Parabéns! Você venceu o jogo!");
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}