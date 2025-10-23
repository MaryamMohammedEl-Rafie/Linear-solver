using System;
using System.Collections.Generic;

namespace LinearSolver
{
    public static class GaussJordanSolver
    {
        // Produces steps culminating in RREF
        public static Step[] SolveWithTextbookSteps(double[,] inputMatrix)
        {
            if (inputMatrix == null) throw new ArgumentNullException(nameof(inputMatrix));
            // First convert to REF using Gaussian solver steps
            var forwardSteps = GaussianSolver.SolveWithTextbookSteps(inputMatrix);
            double[,] A = (double[,])forwardSteps[forwardSteps.Length - 1].MatrixSnapshot.Clone();

            var steps = new List<Step>(forwardSteps);

            int m = A.GetLength(0);
            int n = A.GetLength(1);
            // find pivot columns (first nonzero in each row)
            var pivotCols = new int[m];
            for (int i = 0; i < m; i++)
            {
                int p = -1;
                for (int j = 0; j < n - 1; j++)
                    if (Math.Abs(A[i, j]) > 1e-12) { p = j; break; }
                pivotCols[i] = p;
            }

            // Work from bottom to top to eliminate above pivots
            for (int i = m - 1; i >= 0; i--)
            {
                int pc = pivotCols[i];
                if (pc < 0) continue;

                // Normalize pivot row if pivot not 1
                if (Math.Abs(A[i, pc] - 1.0) > 1e-12)
                {
                    double d = A[i, pc];
                    DivideRow(A, i, d);
                    steps.Add(new Step($"Multiply row {i + 1} by 1/{d:0.###} to make the leading entry 1.", A));
                }

                // eliminate above
                bool anyElim = false;
                for (int r = 0; r < i; r++)
                {
                    if (Math.Abs(A[r, pc]) > 1e-12)
                    {
                        double factor = A[r, pc];
                        AddMultipleOfRow(A, i, r, -factor);
                        anyElim = true;
                    }
                }
                if (anyElim)
                    steps.Add(new Step($"Use row {i + 1} to eliminate entries above the leading 1 in column {pc + 1}.", A));
            }

            steps.Add(new Step("Matrix is now in Reduced Row-Echelon Form (RREF).", A));
            return steps.ToArray();
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
