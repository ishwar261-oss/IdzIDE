using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace IdzIDE
{
    public partial class frmMain : Form
    {
        private string currentFile = "";

        public frmMain()
        {
            InitializeComponent();
            editor.BackColor = Color.FromArgb(30, 30, 30);
            editor.ForeColor = Color.White;
            editor.Font = new Font("Consolas", 12);
            editor.BorderStyle = BorderStyle.None;

            this.BackColor = Color.FromArgb(45, 45, 48);

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Clear();
            currentFile = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                editor.Text = File.ReadAllText(ofd.FileName);
                currentFile = ofd.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentFile = sfd.FileName;
                }
            }
            File.WriteAllText(currentFile, editor.Text);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            lblLines.Text = $"Ln : {editor.Lines.Length}";
            lblChars.Text = $"Ch : {editor.TextLength}";

            HighlightSyntax();
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '(')
            {
                editor.SelectedText = "()";
                editor.SelectionStart--;
                e.Handled = true;

            }
            if (e.KeyChar == '{')
            {
                editor.SelectedText = "{}";
                editor.SelectionStart--;
                e.Handled = true;
            }
            if (e.KeyChar == '[')
            {
                editor.SelectedText = "[]";
                editor.SelectionStart--;
                e.Handled = true;
            }
            if (e.KeyChar == '"')
            {
                editor.SelectedText = "\"\"";
                editor.SelectionStart--;
                e.Handled = true;
            }
            if (e.KeyChar == '\'')
            {
                editor.SelectedText = "''";
                editor.SelectionStart--;
                e.Handled = true;
            }
        }

        private readonly string[] keywords =
        {
            "if", "else", "for", "while", "switch", "case", "break", "continue",
            "class", "struct", "interface", "enum", "namespace", "using", "public",
            "static", "void", "return", "new", "try","usingnamespace",
        };
        private readonly string[] types =
        {
            "int", "string", "bool", "float", "double", "decimal", "char", "object",
            "var", "dynamic", "void","include"
        };

        private void HighlightSyntax()
        {
            int cursorPos = editor.SelectionStart;

            editor.SuspendLayout();

            editor.SelectAll();

            HighlightComments();
            HighlightStrings();
            HighlightNumbers();
            HighlightTypes();
            HighlightKeywords();

            editor.SelectionStart = cursorPos;
            editor.SelectionLength = 0;
            editor.SelectionColor = Color.White;

            editor.ResumeLayout();
        }

        private void HighlightKeywords()
        {
            foreach (var keyword in keywords)
            {
                MatchCollection matches =
                    Regex.Matches(editor.Text, $@"\b{keyword}\b");

                foreach (Match match in matches)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.DeepSkyBlue;
                }
            }
        }

        private void HighlightTypes()
        {
            foreach(string type in types)
            {
                MatchCollection matches =
                    Regex.Matches(editor.Text, $@"\b{type}\b");

                foreach(Match match in matches)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.Cyan;
                }
            }
        }

        private void HighlightStrings()
        {
            MatchCollection matches =
                Regex.Matches(editor.Text, "\".*?\"");

            foreach(Match match in matches)
            {
                editor.Select(match.Index, match.Length);
                editor.SelectionColor = Color.Orange;
            }
        }

        private void HighlightNumbers()
        {
            MatchCollection matches =
                Regex.Matches(editor.Text, @"\b\d+\b");

            foreach (Match match in matches)
            {
                editor.Select(match.Index, match.Length);
                editor.SelectionColor = Color.White;
            }
        }
        private void HighlightComments()
        {
            MatchCollection matches =
                Regex.Matches(
                    editor.Text,
                    @"//.*?$",
                    RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                editor.Select(match.Index, match.Length);
                editor.SelectionColor = Color.Green;
            }
        }

    }
}
