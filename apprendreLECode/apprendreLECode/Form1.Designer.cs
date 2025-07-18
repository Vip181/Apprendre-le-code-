namespace apprendreLECode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fichierToolStripMenuItem = new ToolStripMenuItem();
            optionToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            compilerEnCConoleSeulementToolStripMenuItem = new ToolStripMenuItem();
            compilerToolStripMenuItem = new ToolStripMenuItem();
            richTextBox1 = new RichTextBox();
            lineNumberPanel = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fichierToolStripMenuItem, optionToolStripMenuItem, compilerToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1129, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            fichierToolStripMenuItem.Size = new Size(78, 29);
            fichierToolStripMenuItem.Text = "Fichier";
            // 
            // optionToolStripMenuItem
            // 
            optionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearToolStripMenuItem, helpToolStripMenuItem, compilerEnCConoleSeulementToolStripMenuItem });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            optionToolStripMenuItem.Size = new Size(81, 29);
            optionToolStripMenuItem.Text = "option";
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(380, 34);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(380, 34);
            helpToolStripMenuItem.Text = "help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // compilerEnCConoleSeulementToolStripMenuItem
            // 
            compilerEnCConoleSeulementToolStripMenuItem.Name = "compilerEnCConoleSeulementToolStripMenuItem";
            compilerEnCConoleSeulementToolStripMenuItem.Size = new Size(380, 34);
            compilerEnCConoleSeulementToolStripMenuItem.Text = "compiler en c# conole seulement ";
            compilerEnCConoleSeulementToolStripMenuItem.Click += compilerEnCConoleSeulementToolStripMenuItem_Click;
            // 
            // compilerToolStripMenuItem
            // 
            compilerToolStripMenuItem.Name = "compilerToolStripMenuItem";
            compilerToolStripMenuItem.Size = new Size(102, 29);
            compilerToolStripMenuItem.Text = "compiler ";
            compilerToolStripMenuItem.Click += compilerToolStripMenuItem_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Cursor = Cursors.IBeam;
            richTextBox1.Location = new Point(93, 33);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1036, 742);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // lineNumberPanel
            // 
            lineNumberPanel.Dock = DockStyle.Left;
            lineNumberPanel.Location = new Point(0, 33);
            lineNumberPanel.Name = "lineNumberPanel";
            lineNumberPanel.Size = new Size(96, 742);
            lineNumberPanel.TabIndex = 2;
            lineNumberPanel.Paint += panel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 775);
            Controls.Add(lineNumberPanel);
            Controls.Add(richTextBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fichierToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private RichTextBox richTextBox1;
        private Panel lineNumberPanel;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem compilerEnCConoleSeulementToolStripMenuItem;
        private ToolStripMenuItem compilerToolStripMenuItem;
    }
}
