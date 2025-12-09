using System.Drawing;
using System.Windows.Forms;

namespace linear_solver
{
    partial class MainForm
    {
        // control fields (exact names you provided)
        private Panel panelTop, panelInput, panelBottom;
        private Label lblTitle, lblInput, lblAlgorithm;
        private RadioButton radioSystem, radioMatrix;
        private TextBox txtEquations, txtFinalOutput;
        private DataGridView dgvMatrix;
        private ComboBox comboAlgorithm;
        private Button btnSolve, btnConvertSystemToMatrix, buttonTranspose, buttonInverse,
                       btnResetOutput, btnResetAll;
        private RichTextBox rtbOutput;

        /// <summary>
        /// Sets up the UI. No event wiring here (wired from MainForm ctor)
        /// </summary>
        private void InitializeComponent()
        {
            ClientSize = new Size(1200, 700);
            Text = "Linear Solver";
            FormBorderStyle = FormBorderStyle.FixedDialog;

            // top
            panelTop = new Panel() { Dock = DockStyle.Top, Height = 90, BackColor = Color.Peru };
            lblTitle = new Label()
            {
                Text = "Linear System Solver",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            radioSystem = new RadioButton() { Text = "System Input", Location = new Point(600, 35), ForeColor = Color.White, AutoSize = true };
            radioMatrix = new RadioButton() { Text = "Matrix Input", Location = new Point(750, 35), ForeColor = Color.White, AutoSize = true };
            panelTop.Controls.AddRange(new Control[] { lblTitle, radioSystem, radioMatrix });

            // input panel
            panelInput = new Panel() { BackColor = Color.Beige, Location = new Point(20, 100), Size = new Size(1150, 500), Visible = true };

            lblInput = new Label() { Text = "Enter System or Matrix:", Location = new Point(20, 15), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true };
            txtEquations = new TextBox() { Location = new Point(20, 45), Size = new Size(450, 150), Multiline = true, ScrollBars = ScrollBars.Vertical, BackColor = Color.Moccasin };
            dgvMatrix = new DataGridView() { Location = new Point(20, 220), Size = new Size(450, 160), BackgroundColor = Color.Moccasin, AllowUserToAddRows = false };

            btnConvertSystemToMatrix = new Button() { Text = "Convert System → Matrix", Location = new Point(20, 390), Size = new Size(220, 35), BackColor = Color.SaddleBrown, ForeColor = Color.White };
            buttonTranspose = new Button() { Text = "Transpose Matrix", Location = new Point(20, 430), Size = new Size(150, 30), BackColor = Color.SaddleBrown, ForeColor = Color.White };
            buttonInverse = new Button() { Text = "Inverse Matrix", Location = new Point(180, 430), Size = new Size(150, 30), BackColor = Color.SaddleBrown, ForeColor = Color.White };

            lblAlgorithm = new Label() { Text = "Algorithm:", Location = new Point(520, 15), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true };
            comboAlgorithm = new ComboBox() { Location = new Point(600, 13), Size = new Size(250, 27), DropDownStyle = ComboBoxStyle.DropDownList };
            comboAlgorithm.Items.AddRange(new object[] { "Gaussian", "Gauss-Jordan" });
            comboAlgorithm.SelectedIndex = 0;

            btnSolve = new Button() { Text = "Solve", Location = new Point(900, 13), Size = new Size(120, 30), BackColor = Color.SaddleBrown, ForeColor = Color.White };

            rtbOutput = new RichTextBox() { Location = new Point(500, 60), Size = new Size(630, 330), ReadOnly = true, BackColor = Color.FloralWhite };
            txtFinalOutput = new TextBox() { Location = new Point(500, 400), Size = new Size(630, 60), Multiline = true, ReadOnly = true, BackColor = Color.Linen };

            panelInput.Controls.AddRange(new Control[]{
                lblInput, txtEquations, dgvMatrix, btnConvertSystemToMatrix,
                buttonTranspose, buttonInverse, lblAlgorithm, comboAlgorithm, btnSolve, rtbOutput, txtFinalOutput
            });

            // bottom
            panelBottom = new Panel() { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.Peru };
            btnResetOutput = new Button() { Text = "Reset Output", Location = new Point(450, 18), Size = new Size(150, 35), BackColor = Color.SaddleBrown, ForeColor = Color.White };
            btnResetAll = new Button() { Text = "Reset All", Location = new Point(620, 18), Size = new Size(150, 35), BackColor = Color.SaddleBrown, ForeColor = Color.White };
            panelBottom.Controls.AddRange(new Control[] { btnResetOutput, btnResetAll });

            Controls.AddRange(new Control[] { panelTop, panelInput, panelBottom });
        }
    }
}
