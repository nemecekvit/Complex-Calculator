using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator_GUI
{
    internal class Parser
    {
        public Parser(string input)
        {
            // Remove all white spaces from the input string
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
            // Validate the input string
            try{ InputValidator.ValidateInput(input); }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
