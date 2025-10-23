using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinearSolver
{
    public class ParsedEquationSystem
    {
        public string[] Variables { get; set; }
        public double[,] Coefficients { get; set; }
        public double[] Constants { get; set; }
    }

    public static class EquationParser
    {
        public static ParsedEquationSystem Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input is empty. Please enter equations or a matrix.");

            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var variableSet = new HashSet<string>();
            var equations = new List<Dictionary<string, double>>();
            var constants = new List<double>();

            foreach (var rawLine in lines)
            {
                string line = rawLine.Replace(" ", "");
                if (!line.Contains("="))
                    throw new ArgumentException($"Missing '=' in equation: {line}");

                var parts = line.Split('=');
                string lhs = parts[0];
                string rhs = parts[1];

                double constant = double.Parse(rhs);
                var coeffs = ParseCoefficients(lhs);
                foreach (var v in coeffs.Keys)
                    variableSet.Add(v);

                equations.Add(coeffs);
                constants.Add(constant);
            }

            var variables = variableSet.ToArray();
            Array.Sort(variables, StringComparer.Ordinal);

            int m = equations.Count;
            int n = variables.Length;
            double[,] coeffMatrix = new double[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    coeffMatrix[i, j] = equations[i].ContainsKey(variables[j]) ? equations[i][variables[j]] : 0.0;

            return new ParsedEquationSystem
            {
                Variables = variables,
                Coefficients = coeffMatrix,
                Constants = constants.ToArray()
            };
        }

        private static Dictionary<string, double> ParseCoefficients(string expr)
        {
            var result = new Dictionary<string, double>();
            var matches = Regex.Matches(expr, @"([+-]?\d*\.?\d*)([a-zA-Z])");

            foreach (Match m in matches)
            {
                string numPart = m.Groups[1].Value;
                string var = m.Groups[2].Value;

                double coeff = 1;
                if (numPart == "-") coeff = -1;
                else if (numPart != "" && numPart != "+") coeff = double.Parse(numPart);

                if (result.ContainsKey(var))
                    result[var] += coeff;
                else
                    result[var] = coeff;
            }

            return result;
        }
    }
}
