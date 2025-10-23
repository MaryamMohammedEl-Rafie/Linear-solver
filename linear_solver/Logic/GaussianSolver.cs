using System;
using System.Collections.Generic;

namespace LinearSolver
{
    public static class GaussianSolver
    {
        // Returns sequence of textbook-style steps. Each Step contains a description and a snapshot of the augmented matrix.
        public static Step[] SolveWithTextbookSteps(double[,] inputMatrix)
        {
            if (inputMatrix == null) throw new ArgumentNullException(nameof(inputMatrix));
            int m = inputMatrix.GetLength(0);
            int n = inputMatrix.GetLength(1); // augmented columns (vars + 1)

            var A = (double[,])inputMatrix.Clone();
            var steps = new List<Step>();

            steps.Add(new Step("Start: augmented matrix", A));

            int row = 0;
            for (int col = 0; col < n - 1 && row < m; col++)
            {
                // 1) find pivot (row with largest absolute value for numerical stability)
                int pivot = -1;
                double maxAbs = 0.0;
                for (int r = row; r < m; r++)
                {
                    double val = Math.Abs(A[r, col]);
                    if (val > maxAbs + 1e-15)
                    {
                        maxAbs = val;
                        pivot = r;
                    }
                }

                if (pivot == -1 || Math.Abs(A[pivot, col]) < 1e-12)
                    continue; // no pivot in this column

                // 2) swap to bring pivot up if needed
                if (pivot != row)
                {
                    SwapRows(A, pivot, row);
                    steps.Add(new Step($"Interchange row {pivot + 1} with row {row + 1} to place a nonzero pivot.", A));
                }

                // 3) normalize pivot row to make leading entry 1
                double pivotVal = A[row, col];
                if (Math.Abs(pivotVal - 1.0) > 1e-12)
                {
                    DivideRow(A, row, pivotVal);
                    steps.Add(new Step($"Multiply row {row + 1} by 1/{pivotVal:0.###} to make the leading entry 1.", A));
                }
                else
                {
                    // leading already 1; still add a step to show leading 1
                    steps.Add(new Step($"Leading entry at row {row + 1}, column {col + 1} is 1.", A));
                }

                // 4) eliminate entries below pivot
                bool eliminated = false;
                for (int r = row + 1; r < m; r++)
                {
                    if (Math.Abs(A[r, col]) > 1e-12)
                    {
                        double factor = A[r, col];
                        AddMultipleOfRow(A, row, r, -factor);
                        eliminated = true;
                    }
                }
                if (eliminated)
                    steps.Add(new Step($"Add suitable multiples of row {row + 1} to rows below to create zeros in column {col + 1}.", A));

                // move to next row/col
                row++;
            }

            steps.Add(new Step("Matrix is now in Row-Echelon Form.", A));
            return steps.ToArray();
        }

        private static void SwapRows(double[,] A, int r1, int r2)
        {
            int cols = A.GetLength(1);
            for (int c = 0; c < cols; c++)
            {
                double t = A[r1, c];
                A[r1, c] = A[r2, c];
                A[r2, c] = t;
            }
        }

        private static void DivideRow(double[,] A, int r, double divisor)
        {
            int cols = A.GetLength(1);
            if (Math.Abs(divisor) < 1e-15) throw new InvalidOperationException("Attempt to divide by zero while normalizing row.");
            for (int c = 0; c < cols; c++) A[r, c] /= divisor;
        }

        private static void AddMultipleOfRow(double[,] A, int srcRow, int targetRow, double factor)
        {
            int cols = A.GetLength(1);
            for (int c = 0; c < cols; c++) A[targetRow, c] += factor * A[srcRow, c];
        }
    }
}
