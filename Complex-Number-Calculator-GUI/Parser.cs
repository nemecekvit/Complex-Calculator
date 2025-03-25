using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Parser using recursive descent
//https://en.wikipedia.org/wiki/Recursive_descent_parser
//https://www.youtube.com/watch?v=5g8Q6ZU2wqA
//  Priority(1 is highest)      Method      Operations
//          1                   Factor            Parentheses[], Absolute value{}, Unary minus -5
//          2                   Term            Multiplication*, Division/
//          3                   Expression            Addition+, Subtraction-

namespace Complex_Number_Calculator_GUI
{
    internal class Parser
    {
        private string exInput;
        private int position;
        public Parser(string input)
        {
            // Remove all white spaces from the input string
            input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
            exInput = input;
            position = 0;
            // Validate the input string
            try { InputValidator.ValidateInput(input); }
            catch (Exception)
            {
                throw;
            }
        }

        //Return the current character, unless we've reached the end of the input
        private char GetCurrentChar()
        {
            if (position < exInput.Length)
                return exInput[position];
            else
                return '\0';
        }

        //Move to the next character
        private void Consume()
        {
            //Console.WriteLine(input[position]);
            position++;
        }

        //Check if the current character matches the expected character, if so, comsume it
        private void Match(char expected)
        {
            if (GetCurrentChar() == expected)
                Consume();
            else
                throw new Exception($"Expected '{expected}' but found '{GetCurrentChar()}' at position {position}");
        }

        //Handle highest priority operations
        private int Factor()
        {
            // Handle unary minus
            if (GetCurrentChar() == '-')
            {
                Consume();
                return -Factor(); // Negate the result of the next factor
            }
            // Handle parentheses
            else if (GetCurrentChar() == '(')
            {
                Consume(); // Consume '('
                int result = Expr(); // Parse expression inside parentheses
                Match(')'); // Ensure closing ')'
                return result;
            }
            // Handle absolute value notation
            else if (GetCurrentChar() == '{')
            {
                Consume(); // Consume '{'
                int result = Math.Abs(Expr()); // Compute absolute value
                Match('}'); // Ensure closing '}'
                return result;
            }
            // Handle numbers
            else if (char.IsDigit(GetCurrentChar()))
            {
                return Number();
            }
            else
            {
                throw new Exception($"Unexpected character '{GetCurrentChar()}' at position {position}");
            }
        }

        private int Number()
        {
            // Parse a multi-digit number
            StringBuilder sb = new StringBuilder();
            while (char.IsDigit(GetCurrentChar()))
            {
                sb.Append(GetCurrentChar());
                Consume();
            }
            return int.Parse(sb.ToString()); // Convert parsed digits to an integer
        }

        //Hadnle second priority operations
        private int Term()
        {
            // Handle multiplication and division
            int result = Factor();
            while (GetCurrentChar() == '*' || GetCurrentChar() == '/')
            {
                char op = GetCurrentChar();
                Consume();
                int rightSide = Factor();

                // Perform operation
                if (op == '*')
                    result *= rightSide;
                else
                    result /= rightSide;
            }
            return result;
        }

        //Handle third priority operations
        private int Expr()
        {
            // Handle addition and subtraction
            int result = Term();
            while (GetCurrentChar() == '+' || GetCurrentChar() == '-')
            {
                char op = GetCurrentChar();
                Consume();
                int right = Term();
                // Perform operation
                if (op == '+')
                    result += right;
                else
                    result -= right;
            }
            return result;
        }

        public int Parse()
        {
            // Start parsing from the expression level
            int result = Expr();

            // Ensure that we've reached the end of the input
            if (position < exInput.Length)
                throw new Exception($"Unexpected character '{GetCurrentChar()}' at position {position}");
            return result;
        }

    }   
}
