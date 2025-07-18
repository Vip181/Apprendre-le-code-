using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace apprendreLECode
{
    public partial class Form1 : Form
    {
        private FormConsole consoleInstance;
        Dictionary<string, int> variables = new Dictionary<string, int>();
        private Color textColor;


        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            lineNumberPanel.Invalidate(); // Redessine les numéros de ligne à chaque scroll
        }

        public Form1()
        {
            InitializeComponent();

            lineNumberPanel.Dock = DockStyle.Left;
            lineNumberPanel.BackColor = Color.FromArgb(50, 50, 50);
            lineNumberPanel.Paint += lineNumberPanel_Paint;




            richTextBox1.ForeColor = Color.FromArgb(220, 220, 220);
            richTextBox1.Font = new Font("Consolas", 25, FontStyle.Regular);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Padding = new Padding(0);
            richTextBox1.Margin = new Padding(10);

            richTextBox1.BackColor = Color.FromArgb(80, 70, 63);

            richTextBox1.KeyDown += richTextBox1_KeyDown;
            richTextBox1.VScroll += richTextBox1_VScroll;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // Couleurs principales
            Color backgroundColor1 = Color.FromArgb(80, 70, 63); // Marron-gris (fond éditeur)
            Color codeColor = Color.FromArgb(80, 70, 60); // Couleur utilisée pour tableLayoutPanel
            richTextBox1.BackColor = backgroundColor1;
        }
        private void lineNumberPanel_Paint(object sender, PaintEventArgs e)
        {
            // Couleur du texte des numéros de ligne (tu peux la personnaliser)
            Color textColor = Color.Gray;

            // Obtenir la première ligne visible
            int firstCharIndex = richTextBox1.GetCharIndexFromPosition(new Point(0, 0));
            int firstVisibleLine = richTextBox1.GetLineFromCharIndex(firstCharIndex);

            // Nombre total de lignes dans le texte
            int totalLines = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;

            for (int i = firstVisibleLine; i < totalLines; i++)
            {
                int charIndex = richTextBox1.GetFirstCharIndexFromLine(i);
                if (charIndex == -1) break;

                Point pos = richTextBox1.GetPositionFromCharIndex(charIndex);
                string lineNum = (i + 1).ToString();

                // Centre verticalement le texte dans la ligne
                float lineHeight = richTextBox1.Font.GetHeight();
                float y = pos.Y + (lineHeight - e.Graphics.MeasureString(lineNum, richTextBox1.Font).Height) / 2;

                // Décalage horizontal
                float x = lineNumberPanel.Width - e.Graphics.MeasureString(lineNum, richTextBox1.Font).Width - 5;

                e.Graphics.DrawString(lineNum, richTextBox1.Font, new SolidBrush(textColor), x, y);
            }
        }


        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                lineNumberPanel.Invalidate();
                if (richTextBox1.CanUndo)
                {
                    richTextBox1.Undo();
                    e.SuppressKeyPress = true;
                }
                return;
            }

            if (e.Control && e.KeyCode == Keys.Y)
            {
                if (richTextBox1.CanRedo)
                {
                    richTextBox1.Redo();
                    e.SuppressKeyPress = true;
                }
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                lineNumberPanel.Invalidate();

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lineNumberPanel.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Couleur du texte des numéros de ligne (tu peux la personnaliser)
            Color textColor = Color.Gray;

            // Obtenir la première ligne visible
            int firstCharIndex = richTextBox1.GetCharIndexFromPosition(new Point(0, 0));
            int firstVisibleLine = richTextBox1.GetLineFromCharIndex(firstCharIndex);

            // Nombre total de lignes dans le texte
            int totalLines = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;

            for (int i = firstVisibleLine; i < totalLines; i++)
            {
                int charIndex = richTextBox1.GetFirstCharIndexFromLine(i);
                if (charIndex == -1) break;

                Point pos = richTextBox1.GetPositionFromCharIndex(charIndex);
                string lineNum = (i + 1).ToString();

                // Centre verticalement le texte dans la ligne
                float lineHeight = richTextBox1.Font.GetHeight();
                float y = pos.Y + (lineHeight - e.Graphics.MeasureString(lineNum, richTextBox1.Font).Height) / 2;

                // Décalage horizontal
                float x = lineNumberPanel.Width - e.Graphics.MeasureString(lineNum, richTextBox1.Font).Width - 5;

                e.Graphics.DrawString(lineNum, richTextBox1.Font, new SolidBrush(textColor), x, y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CompilerCode(string[] lignes)
        {

            variables.Clear();
            List<string> resultats = new List<string>();
            string output = "";
            Dictionary<string, string> stringVariables = new Dictionary<string, string>();
            foreach (string ligne in lignes)
            {
                string trimmed = ligne.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.ToLower() == "compiler")
                    continue;

                if (trimmed.StartsWith("interchanger"))
                {
                    string[] parts = trimmed.Split(' ');
                    if (parts.Length == 3)
                    {
                        Interchanger(parts[1], parts[2], ref output);

                        // Récupération des valeurs après échange
                        string val1 = variables.ContainsKey(parts[1]) ? variables[parts[1]].ToString() : "indéfinie";
                        string val2 = variables.ContainsKey(parts[2]) ? variables[parts[2]].ToString() : "indéfinie";

                        // Affichage du message dans le format souhaité
                        output += $"\nLes valeurs ont bien été interchangées :" + "";
                        output += $"\n{parts[1]} = {val1} " + "";
                        output += $"\n{parts[2]} = {val2}" + "";
                    }
                }
                else if (ligne.EndsWith("= Message"))
                {
                    string varName = ligne.Replace("= Message", "").Trim();

                    if (stringVariables.ContainsKey(varName))
                    {
                        output += $"{stringVariables[varName]}\n";
                    }
                    else if (variables.ContainsKey(varName))
                    {
                        output += $"{variables[varName]}\n";
                    }
                    else
                    {
                        output += $"Erreur : variable '{varName}' non définie pour Message.\n";
                    }
                }
                else if (ligne.Contains("fraction") && ligne.Contains("="))
                {
                    // Exemple : f fraction x = a
                    var parts = ligne.Split('=');
                    if (parts.Length == 2)
                    {
                        string expression = parts[0].Trim(); // f fraction x
                        string resultVar = parts[1].Trim(); // a

                        var exprParts = expression.Split(new string[] { "fraction" }, StringSplitOptions.None);
                        if (exprParts.Length == 2)
                        {
                            string left = exprParts[0].Trim(); // f
                            string right = exprParts[1].Trim(); // x

                            if (variables.ContainsKey(left) && variables.ContainsKey(right))
                            {
                                int val1 = Convert.ToInt32(variables[left]);
                                int val2 = Convert.ToInt32(variables[right]);
                                int res = val2 != 0 ? val1 / val2 : 0;

                                variables[resultVar] = res;
                                output += $"\n{left} fraction {right} = {res}" + "";
                            }
                            else
                            {
                                output += $"Erreur : variable(s) introuvable(s) dans '{ligne}'\n" + "";
                            }
                        }
                    }
                }

                else if (trimmed.Replace(" ", "").EndsWith("=calcule"))
                {
                    string expr = trimmed.Substring(0, trimmed.Length - "= calcule".Length).Trim();
                    try
                    {
                        // Remplacer les noms de variables dans l'expression par leurs vraies valeurs (en tenant compte des échanges)
                        foreach (var variable in variables.Keys)
                        {
                            int val = CalculerValeur(variable);
                            expr = Regex.Replace(expr, $@"\b{variable}\b", val.ToString());
                        }

                        double result = EvaluerExpression(expr);
                        resultats.Add($"{trimmed} = {result}");
                    }
                    catch (Exception ex)
                    {
                        resultats.Add("Erreur de calcul : " + ex.Message);
                    }
                }
                else if (ligne.StartsWith("str "))
                {
                    string[] parts = ligne.Substring(4).Split('=');
                    if (parts.Length == 2)
                    {
                        string varName = parts[0].Trim();
                        string value = parts[1].Trim();

                        // Si la valeur est entre guillemets => string directe
                        if (value.StartsWith("\"") && value.EndsWith("\""))
                        {
                            value = value.Substring(1, value.Length - 2);
                        }
                        // Si c'est une variable numérique
                        else if (variables.ContainsKey(value))
                        {
                            value = variables[value].ToString();
                        }
                        // Si c'est une autre variable string
                        else if (stringVariables.ContainsKey(value))
                        {
                            value = stringVariables[value];
                        }
                        else
                        {
                            output += $"Erreur : impossible d'affecter {value} à une string.\n";
                            continue;
                        }

                        stringVariables[varName] = value;
                        output += $"Variable string '{varName}' = \"{value}\"\n";
                    }
                    else
                    {
                        output += $"Erreur : valeur ou variable invalide pour str {ligne}\n";
                    }
                }
                else if (Regex.IsMatch(trimmed, @"^[a-zA-Z]\w*\s*=\s*.+$"))
                {
                    string[] parts = trimmed.Split('=');
                    string nom = parts[0].Trim();
                    string valeurStr = parts[1].Trim();

                    try
                    {
                        int result = EvaluerExpression(valeurStr);
                        variables[nom] = result;
                    }
                    catch
                    {
                        output += $"\nErreur : valeur ou variable invalide pour {nom}" + "";
                    }
                }
                else if (Regex.IsMatch(trimmed, @"^[a-zA-Z]\w*\s*=\s*modifier$"))
                {
                    string nomVariable = trimmed.Split('=')[0].Trim();

                    // Affiche un message si défini juste avant
                    string previousLine = lignes.ToList().FindLast(l => l != ligne && l.Trim().EndsWith("= Message"));
                    if (previousLine != null)
                    {
                        string varName = previousLine.Replace("= Message", "").Trim();
                        if (stringVariables.ContainsKey(varName))
                        {
                            output += stringVariables[varName] + "\n";
                        }
                    }

                    // Demande à la console d’attendre une entrée utilisateur

                    else if (ligne.EndsWith("= msbox"))
                    {
                        string varName = ligne.Replace("= msbox", "").Trim();

                        if (stringVariables.ContainsKey(varName))
                        {
                            // Affiche les variables string
                            MessageBox.Show(stringVariables[varName]);
                            output += $"Affichage de {varName} (string) dans une MessageBox.\n";
                        }
                        if (trimmed.ToLower().EndsWith("= matrice"))
                        {
                            CalculerMatrice(trimmed, ref output);
                            continue;
                        }
                        else if (variables.ContainsKey(varName))
                        {
                            // Affiche les variables int
                            MessageBox.Show(variables[varName].ToString());
                            output += $"Affichage de {varName} (int) dans une MessageBox.\n";
                        }
                        else
                        {
                            try
                            {
                                // Tente d'évaluer une expression directement
                                string expr = varName;
                                foreach (var variable in variables)
                                {
                                    expr = Regex.Replace(expr, $@"\b{variable.Key}\b", variable.Value.ToString());
                                }

                                double result = EvaluerExpression(expr);
                                MessageBox.Show(result.ToString());
                                output += $"Affichage du résultat du calcul dans une MessageBox : {result}\n";
                            }
                            catch
                            {
                                output += $"Erreur : {varName} n'est ni une variable, ni une expression valide.\n";
                            }
                        }
                    }
                    else if (trimmed.StartsWith("modifier "))
                    {
                        string[] parts = trimmed.Substring(9).Split('=');
                        if (parts.Length == 2)
                        {
                            string nom = parts[0].Trim();
                            string valeurStr = parts[1].Trim();

                            try
                            {
                                int result = EvaluerExpression(valeurStr);
                                variables[nom] = result;
                                output += $"La variable '{nom}' a été modifiée à {result}\n";
                            }
                            catch
                            {
                                output += $"Erreur : valeur invalide pour modification de '{nom}'\n";
                            }
                        }
                        else
                        {
                            output += "Erreur : syntaxe incorrecte pour modifier une variable. Utilisez : modifier x = 10\n";
                        }
                    }
                    else if (ligne.EndsWith("=  cloneVar"))
                    {

                    }
                    else if (ligne.EndsWith("= afficher"))
                    {
                        string contenu = ligne.Replace("= afficher", "").Trim();
                        string[] noms = contenu.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string nom in noms)
                        {
                            if (variables.ContainsKey(nom))
                            {
                                output += $"{nom} = {variables[nom]}\n";
                            }
                            else if (stringVariables.ContainsKey(nom))
                            {
                                output += $"{nom} = \"{stringVariables[nom]}\"\n";
                            }
                            else
                            {
                                output += $"Erreur : variable '{nom}' non définie.\n";
                            }
                        }
                    }
                    else if (Regex.IsMatch(trimmed, @"^.+=\s*[a-zA-Z]\w*$"))
                    {
                        // Traitement des affectations inversées ex: A + B = C
                        string[] parts = trimmed.Split('=');
                        string expression = parts[0].Trim();
                        string destination = parts[1].Trim();

                        try
                        {
                            int result = EvaluerExpression(expression);
                            variables[destination] = result;
                        }
                        catch
                        {
                            output += $"\nErreur : expression invalide pour {destination}" + "";
                        }
                    }
                }

                if (resultats.Count > 0)
                {
                    output += "\n[Compilation terminée]\n";
                    foreach (string res in resultats)
                        output += res + "\n";
                }

                // Crée et affiche la console
                if (consoleInstance == null || consoleInstance.IsDisposed)
                {
                    consoleInstance = new FormConsole(output);
                    consoleInstance.Show(); // ou ShowDialog() si tu veux bloquer la fenêtre principale
                }
                else
                {
                    // Si déjà ouverte, on met à jour le texte
                    foreach (Control c in consoleInstance.Controls)
                    {
                        if (c is RichTextBox richText)
                        {
                            richText.Text = output;
                            break;
                        }
                    }

                    consoleInstance.BringToFront();
                }
            }
        }
        private Dictionary<string, string> variablesEchangees = new Dictionary<string, string>();
        private int CalculerValeur(string nomVariable)
        {
            // Si la variable a été interchangée, on récupère la valeur de la variable échangée
            if (variablesEchangees.ContainsKey(nomVariable))
            {
                nomVariable = variablesEchangees[nomVariable];
            }

            if (variables.ContainsKey(nomVariable))
            {
                return variables[nomVariable];
            }

            throw new Exception($"Variable {nomVariable} non définie.");
        }
        private int EvaluerExpression(string expression)
        {
            expression = Regex.Replace(expression, @"\s+", "");

            List<int> valeurs = new List<int>();
            List<char> operateurs = new List<char>();

            string nombreActuel = "";
            foreach (char c in expression)
            {
                if ("+-*/".Contains(c))
                {
                    valeurs.Add(ObtenirValeur(nombreActuel));
                    operateurs.Add(c);
                    nombreActuel = "";
                }
                else
                {
                    nombreActuel += c;
                }
            }
            valeurs.Add(ObtenirValeur(nombreActuel));

            int result = valeurs[0];
            for (int i = 1; i < valeurs.Count; i++)
            {
                char op = operateurs[i - 1];
                int val = valeurs[i];

                switch (op)
                {
                    case '+': result += val; break;
                    case '-': result -= val; break;
                    case '*': result *= val; break;
                    case '/':
                        if (val == 0) throw new DivideByZeroException();
                        result /= val;
                        break;
                }
            }

            return result;
        }

        private int ObtenirValeur(string s)
        {
            if (int.TryParse(s, out int val)) return val;
            if (variables.ContainsKey(s)) return variables[s];
            throw new Exception($"Variable inconnue : {s}");
        }
       
        private void CalculerMatrice(string ligne, ref string output)
        {
            string contenu = ligne.Replace("= matrice", "").Trim();
            string operateur = "";

            if (contenu.Contains("+")) operateur = "+";
            else if (contenu.Contains("-")) operateur = "-";
            else if (contenu.Contains("*")) operateur = "*";
            else
            {
                output += "\nErreur : opérateur matriciel manquant (+, -, *)" + "";
                return;
            }

            string[] parties = contenu.Split(new string[] { operateur }, StringSplitOptions.None);
            if (parties.Length != 2)
            {
                output += "\nErreur : expression matricielle mal formée." + "";
                return;
            }

            string[] gauche = parties[0].Trim().Split(' ');
            string[] droite = parties[1].Trim().Split(' ');

            if (gauche.Length != 16 || droite.Length != 16)
            {
                output += "\nErreur : chaque matrice doit avoir exactement 16 variables (4x4)." + "";
                return;
            }

            int[] matriceG = new int[16];
            int[] matriceD = new int[16];
            int[] resultat = new int[16];

            try
            {
                for (int i = 0; i < 16; i++)
                {
                    matriceG[i] = ObtenirValeur(gauche[i]);
                    matriceD[i] = ObtenirValeur(droite[i]);
                }

                for (int i = 0; i < 16; i++)
                {
                    switch (operateur)
                    {
                        case "+":
                            resultat[i] = matriceG[i] + matriceD[i];
                            break;
                        case "-":
                            resultat[i] = matriceG[i] - matriceD[i];
                            break;
                        case "*":
                            int lignes = i / 4;
                            int col = i % 4;
                            resultat[i] = 0;
                            for (int k = 0; k < 4; k++)
                            {
                                resultat[i] += matriceG[lignes * 4 + k] * matriceD[k * 4 + col];
                            }
                            break;
                    }
                }

                output += "\nRésultat de la matrice 4x4 :\n" + "";

                for (int i = 0; i < 4; i++)
                {
                    string ligneRes = "";
                    for (int j = 0; j < 4; j++)
                    {
                        ligneRes += resultat[i * 4 + j].ToString().PadLeft(5);
                    }
                    output += ligneRes + "\n" + "";
                }
            }
            catch (Exception ex)
            {
                output += "\nErreur de calcul matriciel : " + ex.Message + "";
            }
        }


        private void Interchanger(string var1, string var2, ref string output)
        {
            if (variables.ContainsKey(var1) && variables.ContainsKey(var2))
            {
                int temp = variables[var1];
                variables[var1] = variables[var2];
                variables[var2] = temp;
            }
            else
            {
                string[] lignes = richTextBox1.Text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                output += "\nErreur : Une des variables n'existe pas." + "";
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {

            richTextBox1.Clear();
            variables.Clear();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp formHelp = new FormHelp();
            formHelp.Show();
        }

        private void compilerEnCConoleSeulementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void compilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] lignes = richTextBox1.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            CompilerCode(lignes);
        }
    }
}
