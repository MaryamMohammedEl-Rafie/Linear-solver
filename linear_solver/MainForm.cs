using LinearSolver;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace linear_solver
{
    public partial class MainForm : Form
    {
        private string[] _currentVariableNames = null;

        public MainForm()
        {
            InitializeComponent();

            panelInput.Visible = false;

            radioSystem.CheckedChanged += radioSystem_CheckedChanged;
            radioMatrix.CheckedChanged += radioMatrix_CheckedChanged;

            btnSolve.Click += btnSolve_Click;
            btnConvertSystemToMatrix.Click += btnConvertSystemToMatrix_Click;
            buttonTranspose.Click += buttonTranspose_Click;
            buttonInverse.Click += buttonInverse_Click;

            btnResetAll.Click += btnResetAll_Click;
            btnResetOutput.Click += btnResetOutput_Click;
        }

        private void radioMatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioMatrix.Checked) return;

            using (var f = new InputSizeForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    CreateMatrixGrid(f.Rows, f.Cols);
                    panelInput.Visible = true;
                }
                else
                {
                    radioMatrix.Checked = false;
                    panelInput.Visible = false;
                    return;
                }
            }
        }

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioSystem.Checked)
            {
                if (!radioMatrix.Checked) panelInput.Visible = false;
                return;
            }
            panelInput.Visible = true;
        }

        private void CreateMatrixGrid(int rows, int cols)
        {
            dgvMatrix.AllowUserToAddRows = false;
            dgvMatrix.Columns.Clear();
            for (int i = 0; i < cols; i++)
                dgvMatrix.Columns.Add($"x{i + 1}", $"x{i + 1}");
            dgvMatrix.RowCount = rows;
        }

        private double[,] GetMatrixFromGrid(bool excludeRHS = true)
        {
            var rows = dgvMatrix.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToArray();
            int m = rows.Length;
            int n = dgvMatrix.ColumnCount;

            if (excludeRHS && dgvMatrix.Columns[n - 1].HeaderText == "RHS")
                n--;

            var A = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    if (!double.TryParse(rows[i].Cells[j].Value?.ToString(), out double val))
                        throw new Exception($"Cell [{i + 1},{j + 1}] must contain a number.");
                    A[i, j] = val;
                }
            return A;
        }

        private void FillGridFromMatrix(double[,] m)
        {
            dgvMatrix.SuspendLayout();
            dgvMatrix.AllowUserToAddRows = false;
            dgvMatrix.Columns.Clear();

            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            for (int j = 0; j < cols; j++)
                dgvMatrix.Columns.Add($"x{j + 1}", $"x{j + 1}");

            dgvMatrix.RowCount = rows;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    dgvMatrix[j, i].Value = Math.Round(m[i, j], 6);

            dgvMatrix.ResumeLayout();
        }

        private void buttonTranspose_Click(object sender, EventArgs e)
        {
            try
            {
                var m = GetMatrixFromGrid();
                var t = MatrixOperations.Transpose(m);
                FillGridFromMatrix(t);
                MessageBox.Show("Transposed.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            try
            {
                var A = GetMatrixFromGrid(excludeRHS: true);
                var inv = MatrixOperations.Inverse(A);
                FillGridFromMatrix(inv);
                MessageBox.Show("Inverse calculated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inverse failed: " + ex.Message);
            }
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();
            txtEquations.Clear();
            txtFinalOutput.Clear();
            rtbOutput.Clear();
            _currentVariableNames = null;
            panelInput.Visible = false;
            radioMatrix.Checked = false;
            radioSystem.Checked = false;
        }

        private void btnResetOutput_Click(object sender, EventArgs e)
        {
            txtFinalOutput.Clear();
            rtbOutput.Clear();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Solver functionality placeholder. Use Gaussian/Gauss-Jordan solver here.");
        }

        private void btnConvertSystemToMatrix_Click(object sender, EventArgs e)
        {
            MessageBox.Show("System-to-matrix conversion placeholder.");
        }
    }
}
