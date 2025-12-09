using System;

namespace LinearSolver
{
    public static class MatrixOperations
    {
        public static double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[j, i] = matrix[i, j];

            return result;
        }

        public static double[,] Inverse(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
                throw new Exception("Matrix must be square.");

            double[,] copy = (double[,])matrix.Clone();
            double[,] inv = new double[n, n];

            // Initialize as identity
            for (int i = 0; i < n; i++)
                inv[i, i] = 1;

            for (int i = 0; i < n; i++)
            {
                // Pivot selection (partial pivoting)
                int pivot = i;
                double maxAbs = Math.Abs(copy[i, i]);
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(copy[k, i]) > maxAbs)
                    {
                        pivot = k;
                        maxAbs = Math.Abs(copy[k, i]);
                    }
                }

                if (maxAbs < 1e-12)
                    throw new Exception("Matrix is singular or nearly singular.");

                // Swap rows if needed
                if (pivot != i)
                {
                    SwapRows(copy, i, pivot);
                    SwapRows(inv, i, pivot);
                }

                // Normalize pivot row
                double diag = copy[i, i];
                for (int j = 0; j < n; j++)
                {
                    copy[i, j] /= diag;
                    inv[i, j] /= diag;
                }

                // Eliminate other rows
                for (int k = 0; k < n; k++)
                {
                    if (k == i) continue;
                    double factor = copy[k, i];
                    for (int j = 0; j < n; j++)
                    {
                        copy[k, j] -= factor * copy[i, j];
                        inv[k, j] -= factor * inv[i, j];
                    }
                }
            }

            return inv;
        }

        private static void SwapRows(double[,] matrix, int r1, int r2)
        {
            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                double tmp = matrix[r1, j];
                matrix[r1, j] = matrix[r2, j];
                matrix[r2, j] = tmp;
            }
        }
    }
}
