namespace Complex_Number_Calculator_GUI
{
    public static class ComplexNumberHelper
    {
        public static string FormatRiI(string real, string imag)
        {
            return $"[{real}i{imag}]";
        }

        public static string FormatRjI(string real, string imag)
        {
            return $"[{real}j{imag}]";
        }

        public static string FormatPolar(string modulus, string angle)
        {
            return $"[{modulus}<{angle}]";
        }
    }
}
