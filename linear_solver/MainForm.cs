using System;
using System.Windows.Forms;
using LinearSolver;
using System.Linq;
using System.Drawing;

namespace linear_solver
{
    public partial class MainForm : Form
    {
        private Panel startPanel;
        private Button btnMatrix;
        private Button btnSystem;
        private Label lblChoose;
        private string[] _currentVariableNames;


        public MainForm()
        {
            InitializeComponent();
            comboAlgorithm.SelectedIndex = 0;

            // --- show selection panel instead of opening matrix mode ---
            CreateStartPanel();
        }

        private void CreateStartPanel()
        {
            startPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            lblChoose = new Label
            {
                Text = "Choose Input Mode",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(80, 50)
            };

            btnMatrix = new Button
            {
                Text = "Matrix Input",
                Size = new Size(150, 40),
                Location = new Point(80, 110)
            };
            btnMatrix.Click += (s, e) => StartMatrixMode();

            btnSystem = new Button
            {
                Text = "System Input",
                Size = new Size(150, 40),
                Location = new Point(80, 170)
            };
            btnSystem.Click += (s, e) => StartSystemMode();

            startPanel.Controls.Add(lblChoose);
            startPanel.Controls.Add(btnMatrix);
            startPanel.Controls.Add(btnSystem);
            this.Controls.Add(startPanel);
        }

        private void StartMatrixMode()
        {
            startPanel.Visible = false;
            radioMatrix.Checked = true;
            ToggleInputMode();
            InitializeMatrixGrid();
            ConfigureMatrixSizeOnSwitch(isStartup: false);
        }

        private void StartSystemMode()
        {
            startPanel.Visible = false;
            radioSystem.Checked = true;
            ToggleInputMode();
        }

        // everything else below is unchanged from your code
        // --------------------------------------------------
        private void SetupMatrixGrid(int rows, int totalColumns)
        {
            if (rows <= 0 || totalColumns <= 1)
                throw new ArgumentException("Rows must be > 0 and total columns must be > 1 (including RHS).");

            dgvMatrix.Columns.Clear();

            for (int c = 0; c < totalColumns; c++)
            {
                string header = (c == totalColumns - 1) ? "RHS" : $"x{c + 1}";
                var col = new DataGridViewTextBoxColumn
                {
                    HeaderText = header,
                    Name = header,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dgvMatrix.Columns.Add(col);
            }

            dgvMatrix.RowCount = rows;
        }


        private void InitializeMatrixGrid()
        {
            dgvMatrix.AllowUserToAddRows = true;
            dgvMatrix.AllowUserToDeleteRows = true;
            dgvMatrix.ReadOnly = false;
            dgvMatrix.MultiSelect = false;
            dgvMatrix.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMatrix.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatrix.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatrix.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatrix.BackgroundColor = Color.White;

            if (dgvMatrix.ColumnCount == 0)
                SetupMatrixGrid(3, 3);
        }

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            ToggleInputMode();
        }

        private void radioMatrix_CheckedChanged(object sender, EventArgs e)
        {
            ToggleInputMode();
            if (radioMatrix.Checked)
                ConfigureMatrixSizeOnSwitch(isStartup: false);
        }

        private void ConfigureMatrixSizeOnSwitch(bool isStartup)
        {
            if (radioMatrix.Checked)
            {
                using (var inputForm = new InputSizeForm())
                {
                    if (inputForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            int equations = inputForm.Rows;
                            int variables = inputForm.Columns;
                            SetupMatrixGrid(equations, variables);

                            MessageBox.Show(
                                $"Matrix ready.\nEquations (rows): {equations}\nVariables (columns): {variables}",
                                "Matrix Setup Complete",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(
                                "Could not set up matrix size. Using default 3×3.\nError: " + ex.Message,
                                "Input Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            SetupMatrixGrid(3, 3);
                        }
                    }
                    else if (dgvMatrix.ColumnCount == 0)
                        SetupMatrixGrid(3, 3);
                }
            }
        }

        private void ToggleInputMode()
        {
            bool system = radioSystem.Checked;

            // When in System mode → enable system input controls, disable matrix grid
            txtEquations.Enabled = system;
            btnConvertSystemToMatrix.Enabled = system;

            // When in Matrix mode → enable grid, disable system input controls
            dgvMatrix.Enabled = !system;
            dgvMatrix.ReadOnly = system; // ensures user can't edit in system mode

            // Optional: gray out visually when disabled (nice UX)
            dgvMatrix.DefaultCellStyle.BackColor = system
                ? System.Drawing.Color.LightGray
                : System.Drawing.Color.White;

            // Optional: also disable adding/removing rows in system mode
            dgvMatrix.AllowUserToAddRows = !system;
            dgvMatrix.AllowUserToDeleteRows = !system;
        }

        private void btnConvertSystemToMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                var parsed = SystemInputHandler.ParseSystem(txtEquations.Text);
                InputMatrixHelpers.FillGridFromMatrix(dgvMatrix, parsed.Coefficients);

                // Save variable names for later use (e.g., when solving)
                _currentVariableNames = parsed.VariableNames;

                dgvMatrix.ReadOnly = false;
                dgvMatrix.AllowUserToAddRows = false;

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] augmented = InputMatrixHelpers.BuildMatrixFromGrid(dgvMatrix);
                int varsCount = augmented.GetLength(1) - 1;
                if (varsCount < 1)
                    throw new ArgumentException("Matrix must include at least one variable column and one RHS column.");

                string[] variables;

                if (_currentVariableNames != null && _currentVariableNames.Length == varsCount)
                    variables = _currentVariableNames;
                else
                    variables = Enumerable.Range(1, varsCount).Select(i => "x" + i).ToArray();


                bool useGaussJordan = (comboAlgorithm.SelectedIndex == 1);
                Step[] steps = useGaussJordan
                    ? GaussJordanSolver.SolveWithTextbookSteps(augmented)
                    : GaussianSolver.SolveWithTextbookSteps(augmented);

                rtbOutput.Clear();
                foreach (var s in steps)
                    RtfBuilder.AppendStepToRichTextBox(rtbOutput, s);

                var final = steps.Last().MatrixSnapshot;
                var analysis = ResultAnalyzer.Analyze(final);

                txtFinalOutput.Clear();
                if (analysis.HasNoSolution)
                {
                    txtFinalOutput.Text = "No solution.\r\n";
                    txtFinalOutput.AppendText("Reason: inconsistent system.\r\n");
                }
                else if (analysis.HasInfiniteSolutions)
                {
                    txtFinalOutput.Text = "Infinite solutions.\r\n";
                }
                else
                {
                    txtFinalOutput.Text = "Unique solution:\r\n";
                    for (int i = 0; i < analysis.Solution.Length; i++)
                        txtFinalOutput.AppendText($"{variables[i]} = {analysis.Solution[i]:0.######}\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetOutput_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
            txtFinalOutput.Clear();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            txtEquations.Clear();
            rtbOutput.Clear();
            txtFinalOutput.Clear();
            _currentVariableNames = null; // reset any variable names from previous input

            if (radioMatrix.Checked)
            {
                // Prompt user again for new size
                ConfigureMatrixSizeOnSwitch(isStartup: false);
            }
            else
            {
                // System mode: just clear everything
                dgvMatrix.Columns.Clear();
                startPanel.Visible = true; // back to selection screen
            }
        }


    }
}
