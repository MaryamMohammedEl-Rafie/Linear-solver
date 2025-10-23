namespace LinearSolver
{
    public static class SystemInputHandler
    {
        public class ParsedSystem
        {
            public double[,] Coefficients { get; set; }
            public string[] VariableNames { get; set; }
        }
        public static ParsedSystem ParseSystem(string systemText)
        {
            // Example input: "2x + 3y = 5; x - y = 1"
            var equations = systemText.Split(new[] { ';', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var variableSet = new HashSet<string>();
            var parsedEquations = new List<Dictionary<string, double>>();

            foreach (var eq in equations)
            {
                var sides = eq.Split('=');
                if (sides.Length != 2)
                    throw new ArgumentException("Each equation must contain '='.");

                string left = sides[0];
                double rhs = double.Parse(sides[1]);

                var coefficients = new Dictionary<string, double>();
                var matches = System.Text.RegularExpressions.Regex.Matches(left, @"([+-]?\s*\d*\.?\d*)\s*([a-zA-Z]\w*)");

                foreach (System.Text.RegularExpressions.Match m in matches)
                {
                    double coeff = 1;
                    string coeffText = m.Groups[1].Value.Replace(" ", "");
                    if (!string.IsNullOrEmpty(coeffText) && coeffText != "+" && coeffText != "-")
                        coeff = double.Parse(coeffText);
                    else if (coeffText == "-")
                        coeff = -1;

                    string varName = m.Groups[2].Value;
                    variableSet.Add(varName);
                    coefficients[varName] = coeff;
                }

                coefficients["RHS"] = rhs;
                parsedEquations.Add(coefficients);
            }

            // Keep consistent order of variables
            var variables = variableSet.ToArray();
            Array.Sort(variables, StringComparer.OrdinalIgnoreCase);

            int rows = parsedEquations.Count;
            int cols = variables.Length + 1; // +1 for RHS
            double[,] matrix = new double[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < variables.Length; c++)
                    matrix[r, c] = parsedEquations[r].ContainsKey(variables[c]) ? parsedEquations[r][variables[c]] : 0;

                matrix[r, cols - 1] = parsedEquations[r]["RHS"];
            }

            return new ParsedSystem
            {
                Coefficients = matrix,
                VariableNames = variables
            };
        }

    }
}
