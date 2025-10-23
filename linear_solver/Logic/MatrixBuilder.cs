namespace LinearSolver
{
    public static class MatrixBuilder
    {
        public static MatrixModel BuildAugmentedMatrix(ParsedEquationSystem parsed)
        {
            int m = parsed.Coefficients.GetLength(0);
            int n = parsed.Coefficients.GetLength(1);
            double[,] result = new double[m, n + 1];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    result[i, j] = parsed.Coefficients[i, j];

                result[i, n] = parsed.Constants[i];
            }

            return new MatrixModel(result);
        }
    }
}
