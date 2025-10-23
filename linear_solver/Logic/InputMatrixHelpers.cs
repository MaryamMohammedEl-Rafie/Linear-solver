using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace LinearSolver
{
    public static class InputMatrixHelpers
    {
        /// <summary>
        /// Fill a DataGridView from a double[,] matrix.
        /// The grid will be configured with text columns and AllowUserToAddRows = false.
        /// </summary>
        public static void FillGridFromMatrix(DataGridView grid, double[,] matrix)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            grid.Columns.Clear();
            grid.Rows.Clear();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Create text columns so we always receive string input when editing
            for (int c = 0; c < cols; c++)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "C" + c,
                    HeaderText = (c == cols - 1) ? "RHS" : $"x{c + 1}",
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                grid.Columns.Add(col);
            }

            grid.AllowUserToAddRows = false; // avoid the special new-row when we programmatically fill

            // Add rows
            for (int r = 0; r < rows; r++)
            {
                var values = new object[cols];
                for (int c = 0; c < cols; c++)
                {
                    // Use invariant culture to ensure consistent decimal separator
                    values[c] = matrix[r, c].ToString("0.######", CultureInfo.InvariantCulture);
                }
                grid.Rows.Add(values);
            }
        }

        /// <summary>
        /// Read double[,] from a DataGridView. Ignores the "new row".
        /// Accepts simple fractions "a/b", decimals in invariant culture, or empty cells (treated as 0).
        /// Throws an ArgumentException with a clear message on invalid entry.
        /// </summary>
        public static double[,] BuildMatrixFromGrid(DataGridView grid)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));

            // Collect rows that are not the new-row placeholder
            var validRows = grid.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToArray();

            int rows = validRows.Length;
            int cols = grid.ColumnCount;
            if (rows == 0 || cols == 0)
                throw new ArgumentException("Matrix grid is empty. Enter at least one row and one column.");

            var res = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var row = validRows[i];
                for (int j = 0; j < cols; j++)
                {
                    object cellVal = row.Cells[j].Value;
                    string s = (cellVal ?? string.Empty).ToString().Trim();

                    if (string.IsNullOrEmpty(s))
                    {
                        // empty cell => treat as zero (user-friendly). If you prefer strictness, throw here.
                        res[i, j] = 0.0;
                        continue;
                    }

                    // support fraction a/b
                    if (s.Contains("/"))
                    {
                        var parts = s.Split('/');
                        if (parts.Length == 2 &&
                            double.TryParse(parts[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double a) &&
                            double.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double b) &&
                            Math.Abs(b) > 1e-15)
                        {
                            res[i, j] = a / b;
                            continue;
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid fractional entry at row {i + 1}, column {j + 1}: '{s}'");
                        }
                    }

                    // normal numeric
                    if (!double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out double d))
                        throw new ArgumentException($"Invalid numeric entry at row {i + 1}, column {j + 1}: '{s}'");

                    res[i, j] = d;
                }
            }

            return res;
        }
    }
}
