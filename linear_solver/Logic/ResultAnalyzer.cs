using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearSolver
{
    public class AnalysisResult
    {
        public bool HasNoSolution { get; set; }
        public bool HasInfiniteSolutions { get; set; }
        public bool HasUniqueSolution { get; set; }
        public int Rank { get; set; }
        public int VariablesCount { get; set; }
        public double[] Solution { get; set; }
        public List<int> FreeVariablesIndices { get; set; } // 0-based index of variable column
    }

    public static class ResultAnalyzer
    {
        // Analyze a final matrix (REF or RREF) and produce solution info
        public static AnalysisResult Analyze(double[,] finalMatrix)
        {
            if (finalMatrix == null) throw new ArgumentNullException(nameof(finalMatrix));
            int m = finalMatrix.GetLength(0);
            int n = finalMatrix.GetLength(1);
            int vars = n - 1;

            var res = new AnalysisResult { VariablesCount = vars, FreeVariablesIndices = new List<int>() };

            // 1. Detect Inconsistent System (No Solution)
            for (int i = 0; i < m; i++)
            {
                bool allZero = true;
                for (int j = 0; j < vars; j++)
                {
                    if (Math.Abs(finalMatrix[i, j]) > 1e-9) { allZero = false; break; }
                }
                if (allZero && Math.Abs(finalMatrix[i, vars]) > 1e-9)
                {
                    res.HasNoSolution = true;
                    return res; // No solution found, exit immediately.
                }
            }

            // 2. Find pivot columns and rank (System is consistent if it reaches here)
            var pivotColForRow = new int[m];
            for (int i = 0; i < m; i++) pivotColForRow[i] = -1;
            var pivotInCol = new bool[vars];
            int rank = 0;

            for (int i = 0; i < m; i++)
            {
                int pivot = -1;
                for (int j = 0; j < vars; j++)
                {
                    if (Math.Abs(finalMatrix[i, j]) > 1e-9) { pivot = j; break; }
                }
                if (pivot >= 0)
                {
                    pivotColForRow[i] = pivot;
                    if (!pivotInCol[pivot])
                    {
                        pivotInCol[pivot] = true;
                        rank++;
                    }
                }
            }

            res.Rank = rank;

            // Identify free variables
            for (int j = 0; j < vars; j++)
            {
                if (!pivotInCol[j])
                {
                    res.FreeVariablesIndices.Add(j);
                }
            }

            // 3. Check for Infinite Solutions
            if (rank < vars)
            {
                res.HasInfiniteSolutions = true;
                // Since the system is consistent (not NoSolution) and rank < vars, 
                // it has infinite solutions. Return the result which includes the FreeVariablesIndices.
                return res;
            }

            // 4. Unique Solution (rank == vars)
            // rank == vars -> unique solution: perform back substitution
            var sol = new double[vars]; // 'sol' is declared here, inside the unique solution path

            for (int i = m - 1; i >= 0; i--)
            {
                int pivot = pivotColForRow[i];
                if (pivot == -1) continue; // zero row

                double rhs = finalMatrix[i, vars];

                // subtract known variables
                double sum = 0.0;
                for (int j = pivot + 1; j < vars; j++)
                    sum += finalMatrix[i, j] * sol[j];

                double coeff = finalMatrix[i, pivot];
                if (Math.Abs(coeff) < 1e-12)
                    throw new InvalidOperationException("Unexpected zero pivot when extracting solution.");

                sol[pivot] = (rhs - sum) / coeff;
            }

            res.HasUniqueSolution = true;
            res.Solution = sol;
            return res;
        }
    }
}