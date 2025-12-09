using System;
using System.Drawing;
using System.Windows.Forms;

namespace linear_solver
{
    public class InputSizeForm : Form
    {
        NumericUpDown numR = new NumericUpDown() { Minimum = 1, Maximum = 50, Value = 3, Location = new Point(20, 30), Width = 60 };
        NumericUpDown numC = new NumericUpDown() { Minimum = 1, Maximum = 50, Value = 3, Location = new Point(120, 30), Width = 60 };
        Button ok = new Button() { Text = "OK", Location = new Point(20, 70), DialogResult = DialogResult.OK };
        Button cancel = new Button() { Text = "Cancel", Location = new Point(110, 70), DialogResult = DialogResult.Cancel };

        public int Rows => (int)numR.Value;
        public int Cols => (int)numC.Value;

        public InputSizeForm()
        {
            Text = "Matrix Size";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(220, 120);
            StartPosition = FormStartPosition.CenterParent;

            Controls.AddRange(new Control[]
            {
                new Label() { Text = "Rows:", Location = new Point(20, 10), AutoSize = true },
                new Label() { Text = "Cols:", Location = new Point(120, 10), AutoSize = true },
                numR, numC, ok, cancel
            });

            AcceptButton = ok;
            CancelButton = cancel;
        }
    }
}
