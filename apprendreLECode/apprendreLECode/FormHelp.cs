using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace apprendreLECode
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
          
            this.Text = "Aide - Langage personnalisé";
            this.Width = 600;
            this.Height = 600;

            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Dock = DockStyle.Fill;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = true;
            textBox.Font = new System.Drawing.Font("Consolas", 10);
            textBox.Text = GetHelpText();

            this.Controls.Add(textBox);
        }

        private string GetHelpText()
        {
            return @"Exemples valides :

a = 5
b = 10
c = 2
a + b = calcule
a * c = calcule
interchanger a b
a + b = calcule


➤ Le compilateur accepte :
  - Les opérations entre variables : +, -, *, /
  - L’assignation entre variables : a = b
  - L'assignation inversée : a + b = c

➤ Afficher un message dans une fenêtre :

str message = ""Bonjour le monde""


➤ Divisions entières avec 'fraction' :

f = 10
x = 2
f fraction x = a


➤ Calculs matriciels (matrices 4x4) :

// Donne 16 variables pour chaque matrice (4x4)
a b c d e f g h i j k l m n o p + r s t u v w x y z = matrice


// Opérateurs disponibles : + (addition), - (soustraction), * (multiplication matricielle)
";
        }
    }
}