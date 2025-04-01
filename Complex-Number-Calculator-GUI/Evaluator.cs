using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator_GUI
{   /// <summary>
    /// Class for evaluating expressions with complex numbers
    /// </summary>
    internal class Evaluator
    {
        public string[] currentMemory = new string[3];
        private void removeWhiteSpaces(ref string input)
        {
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
        }
        
        ///<summary>
        /// Evaluates a string expression containing complex numbers.
        /// </summary>
        /// <param name="input">The input string containing the expression to evaluate.</param>
        /// <returns>A cmplxNum object representing the result of the evaluation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input string is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when the input string is not in a valid format.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the input string contains invalid operations or variables.</exception>
        public cmplxNum Evaluate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null or empty.");
            }

            // Replace variables with their values
            replaceVariables(ref input, currentMemory);

            // Remove all white spaces from the input string
            removeWhiteSpaces(ref input);

            // Validate the input string
            validateInput(input);

            Parser parser = new Parser(input);
            return parser.Parse();
        }

        public void validateInput(string input) {
            removeWhiteSpaces(ref input);
            InputValidator.ValidateInput(input);
        }

        private void replaceVariables(ref string input, string[] expressions)
        {
            input = input.Replace("a", expressions[0]);
            input = input.Replace("b", expressions[1]);
            input = input.Replace("c", expressions[2]);
        }
    }
}
