using System;
using System.Drawing;
using System.Windows.Forms;

namespace linear_solver
{
    public class InputSizeForm : Form
    {
        private Label lblRows;
        private Label lblColumns;
        private NumericUpDown nudRows;
        private NumericUpDown nudCols;
        private Button btnOk;
        private Button btnCancel;

        public int Rows => (int)nudRows.Value;
        public int Columns => (int)nudCols.Value;

        public InputSizeForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Matrix Size";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(320, 150);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;

            lblRows = new Label()
            {
                Text = "Number of equations (rows):",
                Location = new Point(12, 16),
                AutoSize = true
            };

            nudRows = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 1000,
                Value = 3,
                Location = new Point(190, 12),
                Size = new Size(100, 22)
            };

            lblColumns = new Label()
            {
                Text = "Number of variables (columns):",
                Location = new Point(12, 54),
                AutoSize = true
            };

            nudCols = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 1000,
                Value = 3,
                Location = new Point(190, 50),
                Size = new Size(100, 22)
            };

            btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(70, 96),
                Size = new Size(80, 28)
            };
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button()
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(170, 96),
                Size = new Size(80, 28)
            };

            this.Controls.Add(lblRows);
            this.Controls.Add(nudRows);
            this.Controls.Add(lblColumns);
            this.Controls.Add(nudCols);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Basic validation (redundant because NumericUpDown enforces min/max)
            if (Rows < 1 || Columns < 1)
            {
                MessageBox.Show("Rows and columns must be at least 1.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // keep dialog open
            }
        }
    }
}
