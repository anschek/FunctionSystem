using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionSystem
{
    internal class Complex
    {
        internal delegate double TrigonometricFunc(
        double x,
        Func<double, double>? sin = null,
        Func<double, double>? cos = null,
        Func<double, double>? sec = null,
        Func<double, double>? tan = null,
        Func<double, double>? cot = null
        );

        internal delegate double LogarithmicFunc(
        double x,
        Func<double, double>? ln = null,
        Func<double, double, double>? log = null
        );

        public static double FunctionSystem(double x, TrigonometricFunc? tFunc=null, LogarithmicFunc? lFunc = null)
        {
            tFunc ??= Trigonometry.SystemFunc;
            lFunc ??= Logarithmic.SystemFunc;
            return x<=0 ? tFunc(x) : lFunc(x);
        }
    }
}
