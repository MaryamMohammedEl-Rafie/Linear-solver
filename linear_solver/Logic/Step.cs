namespace LinearSolver
{
    public class Step
    {
        public string Description { get; set; }
        public double[,] MatrixSnapshot { get; set; }

        public Step(string desc, double[,] matrix)
        {
            Description = desc;
            MatrixSnapshot = (double[,])matrix.Clone();
        }
    }
}
