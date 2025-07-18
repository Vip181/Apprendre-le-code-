using System;
using System.Drawing;
using System.Windows.Forms;

namespace apprendreLECode
{
    public partial class FormConsole : Form
    {
        public RichTextBox richConsole;
        private bool fermetureAutorisée = true;
        public FormConsole(string resultText)
        {
            richConsole = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.Black,
                ForeColor = Color.Lime,
                Font = new Font("Consolas", 12),
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            Controls.Add(richConsole);
            Width = 900;
            Height = 500;
            Text = "Console de compilation";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            WriteToConsole(resultText);

            KeyPreview = true;
            KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (fermetureAutorisée)
                        Close();
                }
            };
        }

        public void WriteToConsole(string text)
        {
            if (richConsole.InvokeRequired)
            {
                richConsole.Invoke(new Action(() => AppendText(text)));
            }
            else
            {
                AppendText(text);
            }
        }

        private void AppendText(string text)
        {
            richConsole.AppendText(text + Environment.NewLine);
            richConsole.SelectionStart = richConsole.Text.Length;
            richConsole.ScrollToCaret();
        }
        public string DemanderValeurUtilisateur(string message)
{
    string valeur = "";

    if (InvokeRequired)
    {
        Invoke(new Action(() => valeur = AfficherEtAttendreSaisie(message)));
    }
    else
    {
        valeur = AfficherEtAttendreSaisie(message);
    }

    return valeur;
}

private string AfficherEtAttendreSaisie(string message)
{
    // Affiche le message dans la console
    WriteToConsole(message);

    // Crée une boîte de saisie temporaire
    Form prompt = new Form()
    {
        Width = 500,
        Height = 150,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        Text = "Entrée requise",
        StartPosition = FormStartPosition.CenterParent
    };

    Label textLabel = new Label() { Left = 50, Top = 20, Text = message, Width = 400 };
    TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
    Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };

    confirmation.Click += (sender, e) => { prompt.Close(); };

    prompt.Controls.Add(textLabel);
    prompt.Controls.Add(inputBox);
    prompt.Controls.Add(confirmation);
    prompt.AcceptButton = confirmation;

  return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text ?? "" : "";
}

       

            }
        }
 
