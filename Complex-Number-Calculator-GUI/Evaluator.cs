using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator_GUI
{
    internal class Evaluator
    {
        public static cmplxNum Evaluate(string input)
        {
            // Remove all white spaces from the input string
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);

            // Validate the input string
            try 
            { 
                InputValidator.ValidateInput(input);

                Parser parser = new Parser(input);
                return parser.Parse();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
