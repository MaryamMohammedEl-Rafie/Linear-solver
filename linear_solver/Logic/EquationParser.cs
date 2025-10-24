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
                throw new ArgumentException("Input is empty. Please enter valid equations.");

            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var allVars = new HashSet<string>();
            var coeffsList = new List<Dictionary<string, double>>();
            var constants = new List<double>();

            foreach (string raw in lines)
            {
                string line = raw.Replace(" ", "");
                if (!line.Contains("="))
                    throw new ArgumentException($"Equation missing '=': {line}");

                var parts = line.Split('=');
                string left = parts[0];
                if (!double.TryParse(parts[1], out double constant))
                    throw new ArgumentException($"Invalid constant in equation: {line}");

                var eqDict = new Dictionary<string, double>();

                var terms = Regex.Matches(left, @"([+\-]?\d*\.?\d*)([a-zA-Z]+)");
                foreach (Match t in terms)
                {
                    string coeffText = t.Groups[1].Value;
                    string var = t.Groups[2].Value;

                    double coeff = 1;
                    if (coeffText == "-") coeff = -1;
                    else if (coeffText == "+") coeff = 1;
                    else if (!string.IsNullOrEmpty(coeffText))
                        coeff = double.Parse(coeffText);

                    if (eqDict.ContainsKey(var)) eqDict[var] += coeff;
                    else eqDict[var] = coeff;

                    allVars.Add(var);
                }

                coeffsList.Add(eqDict);
                constants.Add(constant);
            }

            var variables = allVars.OrderBy(v => v).ToArray();
            int m = coeffsList.Count, n = variables.Length;
            double[,] matrix = new double[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = coeffsList[i].ContainsKey(variables[j]) ? coeffsList[i][variables[j]] : 0.0;

            return new ParsedEquationSystem
            {
                Variables = variables,
                Coefficients = matrix,
                Constants = constants.ToArray()
            };
        }
    }
}
