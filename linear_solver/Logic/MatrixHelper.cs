using System;

namespace linear_solver
{
    public static class MatrixHelper
    {
        public static double[,] Transpose(double[,] A)
        {
            int m = A.GetLength(0), n = A.GetLength(1);
            double[,] T = new double[n, m];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    T[j, i] = A[i, j];

            return T;
        }

        public static double[,] Inverse(double[,] A)
        {
            int n = A.GetLength(0);
            if (n != A.GetLength(1)) throw new Exception("Matrix must be square.");

            double[,] aug = new double[n, 2 * n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    aug[i, j] = A[i, j];

            for (int i = 0; i < n; i++)
                aug[i, n + i] = 1;

            for (int col = 0; col < n; col++)
            {
                int pivot = col;
                for (int r = col + 1; r < n; r++)
                    if (Math.Abs(aug[r, col]) > Math.Abs(aug[pivot, col])) pivot = r;

                if (Math.Abs(aug[pivot, col]) < 1e-12)
                    throw new Exception("Matrix is singular.");

                if (pivot != col)
                    for (int c = 0; c < 2 * n; c++)
                        (aug[col, c], aug[pivot, c]) = (aug[pivot, c], aug[col, c]);

                double diag = aug[col, col];
                for (int c = 0; c < 2 * n; c++) aug[col, c] /= diag;

                for (int r = 0; r < n; r++)
                {
                    if (r == col) continue;
                    double factor = aug[r, col];
                    for (int c = 0; c < 2 * n; c++)
                        aug[r, c] -= factor * aug[col, c];
                }
            }

            double[,] inv = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inv[i, j] = aug[i, n + j];

            return inv;
        }
    }
}
