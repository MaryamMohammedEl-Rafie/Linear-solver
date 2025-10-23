using System;

namespace LinearSolver
{
    public class MatrixModel
    {
        public double[,] Data { get; }
        public int Rows => Data.GetLength(0);
        public int Cols => Data.GetLength(1);

        public MatrixModel(double[,] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public double this[int r, int c]
        {
            get => Data[r, c];
            set => Data[r, c] = value;
        }

        public MatrixModel Clone()
        {
            return new MatrixModel((double[,])Data.Clone());
        }
    }
}
