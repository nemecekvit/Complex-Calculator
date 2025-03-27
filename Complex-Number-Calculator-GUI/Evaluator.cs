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
        private static void removeWhiteSpaces(ref string input)
        {
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
        }

        public static cmplxNum Evaluate(string input)
        {

            // Replace variables with their values
            replaceVariables(ref input);

            // Remove all white spaces from the input string
            removeWhiteSpaces(ref input);

            // Validate the input string
            validateInput(input);

            Parser parser = new Parser(input);
            return parser.Parse();
        }

        public static void validateInput(string input) {
            removeWhiteSpaces(ref input);
            InputValidator.ValidateInput(input);
        }

        private static void  replaceVariables(ref string input)
        {

        }
    }
}
