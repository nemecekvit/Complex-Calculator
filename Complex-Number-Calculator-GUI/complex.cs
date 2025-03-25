using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator_GUI
{
    // Class for basic operations with complex numbers
    internal class complex
    {
        public double real;
        public double imag;

        public complex(double Real, double Imag)
        {
            real = Real; imag = Imag;
        }

        public static complex operator +(complex left, complex right)
        {

            left.real += right.real;
            left.imag += right.imag;

            return left;
        }
        public static complex operator -(complex left, complex right)
        {

            left.real -= right.real;
            left.imag -= right.imag;

            return left;
        }
        public static complex operator *(complex left, complex right)
        {
            //(ac - bd) + (ad + bc)i
            var real = (left.real * right.real - left.imag * right.imag);
            var imag = (left.real * right.imag + left.imag * right.real);

            return new complex(real, imag);
        }
        public static complex operator /(complex left, complex right)
        {
            //((ac + bd) / (c2 + d2)) + ((bc - ad) / (c2 + d2)i
            var real = ((left.real * right.real + left.imag * right.imag) / (right.real * right.real + right.imag * right.imag));
            var imag = (left.imag * right.real - left.real * right.imag) / (right.real * right.real + right.imag * right.imag);

            return new complex(real, imag);
        }
        public static complex operator -(complex num)
        {
            return new complex(-num.real, -num.imag);
        }

        public static bool operator ==(complex left, complex right)
        {
            if (left.real == right.real && left.imag == right.imag) return true;
            return false;
        }
        public static bool operator !=(complex left, complex right)
        {

            if (left.real != right.real || left.imag != right.imag) return true;
            return false;
        }

        public override String ToString()
        {
            return $"[{real}i{imag}]";
        }
    }
}
