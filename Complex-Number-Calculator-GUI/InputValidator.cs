using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A class to validate the input string for the calculator.
/// </summary>
internal class InputValidator
{
    private static char[] allowedCharacters = new char[] 
            { '+','-','*','/','{','}', '\'','(',')','[',']','a','b','c','i','j','<','1','2','3','4','5','6','7','8','9','0'};

    private static char[] allowerdParenthese = new char[] { '(', ')', '[', ']', '{', '}' };

    
    ///<summary>
    /// Checks if the parentheses in the input string are balanced.
    /// </summary>
    /// <param name="input">The input string to check for balanced parentheses.</param>
    /// <returns>
    /// A tuple containing the index and character of the first unbalanced parenthesis found.
    /// If all parentheses are balanced, returns a tuple with -1 and null.
    /// </returns>
    private static Tuple<int, char?> checkParentheses(string input)
    {
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(' || input[i] == '[' || input[i] == '{')
            {
                stack.Push(input[i]);
                continue;
            }

            if (input[i] == ')' || input[i] == ']' || input[i] == '}')
            {
                //If the stack is empty, then the parentheses are not balanced
                if (stack.Count == 0)
                    return new Tuple<int, char?>(i, input[i]);

                //If the top of the stack is the corresponding opening parenthesis, then pop it, else unbalanced parentheses
                if ((stack.Peek() == '(' && input[i] == ')') || 
                    (stack.Peek() == '[' && input[i] == ']') || 
                    (stack.Peek() == '{' && input[i] == '}'))
                {
                    stack.Pop();
                    continue;
                }
                else
                {
                    return new Tuple<int, char?>(i, input[i-1]);
                }
            }
        }

        //If the stack is empty, then all parentheses are balanced
        if (stack.Count == 0)
            return new Tuple<int, char?>(-1, null);
        else
            return new Tuple<int, char?>(input.Length - 1, input[input.Length - 1]);
    }

   
    /// <summary>
    /// Validates the input string for the calculator.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <exception cref="FormatException">Thrown when the input contains invalid characters.</exception>
    /// <exception cref="Exception">Thrown when the parentheses in the input are not balanced.</exception>
    public static void ValidateInput(string input)
    {
        //Remove all white spaces
        input = input.Where(c => !Char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);

        //Check for invalid characters
        for (int i = 0; i < input.Length; i++)
        {
            if (!allowedCharacters.Contains(input[i]))
            {
                throw new FormatException(string.Format("Invalid characte: '{0}' found at index: {1}", input[i], i));
            }
        }

        //Check for invalid parentheses
        Tuple<int, char?> parenthesesErrorState = checkParentheses(input);
        if (parenthesesErrorState.Item1 != -1)
        {
            throw new FormatException(string.Format("Parentheses are not balanced near {0} at index {1}", parenthesesErrorState.Item2, parenthesesErrorState.Item1));
        }
    }
}

