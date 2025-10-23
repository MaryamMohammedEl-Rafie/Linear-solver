using System;
using System.Text;
using System.Windows.Forms;

namespace LinearSolver
{
    public static class RtfBuilder
    {
        public static void AppendStepToRichTextBox(RichTextBox box, Step step)
        {
            box.SelectionFont = new System.Drawing.Font("Consolas", 10, System.Drawing.FontStyle.Bold);
            box.SelectionColor = System.Drawing.Color.Navy;
            box.AppendText(step.Description + "\r\n");

            box.SelectionFont = new System.Drawing.Font("Consolas", 9);
            box.SelectionColor = System.Drawing.Color.Black;
            box.AppendText(MatrixToText(step.MatrixSnapshot));
            box.AppendText("\r\n\r\n");
        }

        private static string MatrixToText(double[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            var sb = new StringBuilder();

            for (int i = 0; i < m; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < n; j++)
                    sb.Append($"{matrix[i, j],8:0.###} ");
                sb.Append("|\r\n");
            }
            return sb.ToString();
        }
    }
}
