using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperTrunfoGui;

public class MainForm : Form
{
    private readonly Label status = new() { AutoSize = true, Location = new Point(20, 20) };
    private readonly Button play = new() { Text = "Jogar rodada", Location = new Point(20, 55), Size = new Size(130, 35) };
    private readonly ListBox attributes = new() { Location = new Point(20, 110), Size = new Size(190, 100) };

    public MainForm()
    {
        Text = "Super Trunfo - GUI básica";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(420, 250);
        Font = new Font("Segoe UI", 10);
        status.Text = "Sua carta está pronta. Escolha um atributo.";
        attributes.Items.AddRange(new object[] { "Velocidade", "Força", "Inteligência" });
        play.Click += (_, _) => status.Text = "Rodada executada! (GUI mínima)";
        Controls.AddRange(new Control[] { status, play, attributes });
    }
}

public static class Program
{
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
