using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Complex_Number_Calculator_GUI
{
    // Class for advanced operations with complex numbers, made for calculator. Uses complex class. Determines in what format input is and parses input, then passes it to complex.
    internal class cmplxNum
    {
        string num1;

        public cmplxNum(string input) {
            var match = Regex.Match(input, @"\[(.*?)\]");
            if (!match.Success)
                throw new ArgumentException("Invalid input format. Expected format: [number]");

            num1 = match.Groups[1].Value;

        }

        // Method for recognizing format of inputed complex numbers and conversion of gonimetric c. num to algebraic
        private complex ParseComplex(string input)
        {
            if (input.Contains("i")) // Algebraický tvar s "i"
            {
                var match = Regex.Match(input, @"([-+]?\d*\.?\d+)i([-+]?\d*\.?\d+)");
                if (!match.Success) throw new ArgumentException("Invalid algebraic format");
                return new complex(double.Parse(match.Groups[1].Value), double.Parse(match.Groups[2].Value));
            }
            else if (input.Contains("j")) // Algebraický tvar s "j"
            {
                var match = Regex.Match(input, @"([-+]?\d*\.?\d+)j([-+]?\d*\.?\d+)");
                if (!match.Success) throw new ArgumentException("Invalid algebraic format");
                return new complex(double.Parse(match.Groups[1].Value), double.Parse(match.Groups[2].Value));
            }
            else if (input.Contains("<")) // Goniometrický tvar
            {
                var match = Regex.Match(input, @"([-+]?\d*\.?\d+)<([-+]?\d*\.?\d+)");
                if (!match.Success) throw new ArgumentException("Invalid polar format");
                double magnitude = double.Parse(match.Groups[1].Value);
                double angle = double.Parse(match.Groups[2].Value) * Math.PI / 180; // Převod na radiány
                return new complex(magnitude * Math.Cos(angle), magnitude * Math.Sin(angle));
            }
            else
            {
                throw new ArgumentException("Unknown number format");
            }
        }

        // BASIC MATHEMATIC OPERATIONS
        public static cmplxNum operator +(cmplxNum numA, cmplxNum numB) {
            complex cnumA = numA.ParseComplex(numA.num1);
            complex cnumB = numB.ParseComplex(numB.num1);

            complex result = cnumA + cnumB;
            string str_result = result.ToString();
            cmplxNum Result = new cmplxNum(str_result);
            return Result;
        }

        public static cmplxNum operator -(cmplxNum numA, cmplxNum numB)
        {
            complex cnumA = numA.ParseComplex(numA.num1);
            complex cnumB = numB.ParseComplex(numB.num1);

            complex result = cnumA - cnumB;
            string str_result = result.ToString();
            cmplxNum Result = new cmplxNum(str_result);
            return Result;
        }

        public static cmplxNum operator *(cmplxNum numA, cmplxNum numB)
        {
            complex cnumA = numA.ParseComplex(numA.num1);
            complex cnumB = numB.ParseComplex(numB.num1);

            complex result = cnumA * cnumB;
            string str_result = result.ToString();
            cmplxNum Result = new cmplxNum(str_result);
            return Result;
        }

        public static cmplxNum operator /(cmplxNum numA, cmplxNum numB)
        {
            complex cnumA = numA.ParseComplex(numA.num1);
            complex cnumB = numB.ParseComplex(numB.num1);

            complex result = cnumA / cnumB;
            string str_result = result.ToString();
            cmplxNum Result = new cmplxNum(str_result);
            return Result;
        }

        public static cmplxNum operator -(cmplxNum numA)
        {
            complex cnumA = numA.ParseComplex(numA.num1);

            complex result = -cnumA;
            string str_result = result.ToString();
            cmplxNum Result = new cmplxNum(str_result);
            return Result;
        }

        public override string ToString()
        {
            return $"[{num1}]";
        }
    }
}
