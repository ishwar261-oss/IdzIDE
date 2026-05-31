using System.IO;
using System.Drawing;

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
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar = '(')
            {

            }
        }
    }
}
