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
        public static cmplxNum Evaluate(string input)
        {
            // Remove all white spaces from the input string
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);

            // Replace variables with their values
            replaceVariables(ref input);

            // Validate the input string
            try 
            { 
                InputValidator.ValidateInput(input);

                Parser parser = new Parser(input);
                return parser.Parse();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        private static void  replaceVariables(ref string input)
        {

        }
    }
}
