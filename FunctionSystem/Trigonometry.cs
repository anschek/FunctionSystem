using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FunctionSystem_Test")]
namespace FunctionSystem
{
    internal static class Trigonometry
    {
        // sin a
        internal static double _base(double alpha) => Math.Sin(alpha);

        // cos a = sqrt(1 - sin^2(a))
        internal static double Cos(double alpha, Func<double, double>? sin = null) 
        {
            sin ??= _base;
            if (Math.Abs(sin(alpha)) < 1e-10)
            {
                if (alpha == Math.PI || alpha == 3 * Math.PI)  return -1; //180
                return 1; // 0360
            }
            return Math.Sqrt(1 - Math.Pow(sin(alpha), 2) );
        }
        // sec a = 1/ cos a
        internal static double Sec(double alpha, Func<double, double>? cos = null)
        {
            cos ??= (angle) => Cos(angle, _base);
            return 1/ cos(alpha);
        }
        // tg a = sin a/ cos a
        internal static double Tan(double alpha, Func<double, double>? sin = null, Func<double, double>? cos = null)
        {
            sin ??= _base;
            cos ??= (angle) => Cos(angle, sin);
            return sin(alpha)/cos(alpha);
        }
        // ctg a = 1/ tg a
        internal static double Cot(double alpha, Func<double, double>? tan = null)
        {
            if(tan == null)
            {
                Func<double, double> cos = (angle) => Cos(angle, _base);
                tan = (angle) => Tan(angle, cos);
            }
            return 1 / tan(alpha);
        }
        // first function from task
        public static double SystemFunc(
            double x, 
            Func<double, double>? sin = null,
            Func<double, double>? cos = null,
            Func<double, double>? sec = null,
            Func<double, double>? tan = null,
            Func<double, double>? cot = null
            )
        {
            if (x > 0) return double.NaN;

            sin ??= _base;
            cos ??= (angle) => Cos(angle, sin);
            sec ??= (angle) => Sec(angle, cos);
            tan ??= (angle) => Tan(angle, cos);
            cot ??= (angle) => Cot(angle, tan);

            return (cot(x) / sec(x) - tan(x)) * sin(x) / cos(x) 
                * Math.Pow( sin(x) + 2* cos(x), 2);
        }
    }

}
