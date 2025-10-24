using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace LinearSolver
{
    public static class InputMatrixHelpers
    {
        public static void SetupEditableMatrix(DataGridView grid, int rows = 3, int cols = 4)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grid.MultiSelect = false;

            grid.Columns.Clear();
            for (int j = 0; j < cols; j++)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = (j == cols - 1) ? "RHS" : $"x{j + 1}";
                grid.Columns.Add(col);
            }

            grid.Rows.Clear();
            for (int i = 0; i < rows; i++)
                grid.Rows.Add();
        }

        public static double[,] BuildMatrixFromGrid(DataGridView grid)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));
            var rows = grid.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToArray();
            int m = rows.Length;
            int n = grid.ColumnCount;

            var A = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string s = Convert.ToString(rows[i].Cells[j].Value)?.Trim() ?? "0";

                    if (string.IsNullOrWhiteSpace(s))
                    {
                        A[i, j] = 0;
                        continue;
                    }

                    // handle fractions
                    if (s.Contains("/"))
                    {
                        var parts = s.Split('/');
                        if (parts.Length == 2 &&
                            double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double a) &&
                            double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double b) &&
                            Math.Abs(b) > 1e-15)
                        {
                            A[i, j] = a / b;
                            continue;
                        }
                        throw new ArgumentException($"Invalid fraction at row {i + 1}, col {j + 1}");
                    }

                    // parse signed/decimal number
                    if (!double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out double val))
                        throw new ArgumentException($"Invalid numeric input at row {i + 1}, col {j + 1}: '{s}'");

                    A[i, j] = val;
                }
            }

            return A;
        }

        public static void FillGridFromMatrix(DataGridView grid, double[,] matrix)
        {
            grid.Columns.Clear();
            grid.Rows.Clear();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int c = 0; c < cols; c++)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    HeaderText = (c == cols - 1) ? "RHS" : $"x{c + 1}",
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                grid.Columns.Add(col);
            }

            for (int r = 0; r < rows; r++)
            {
                var rowVals = new object[cols];
                for (int c = 0; c < cols; c++)
                    rowVals[c] = matrix[r, c].ToString("0.###", CultureInfo.InvariantCulture);
                grid.Rows.Add(rowVals);
            }
        }
    }
}
