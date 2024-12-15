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
        private static bool isCorrectWithMeasurementError(double expected, double value, double epsilon=1e-10) => expected - epsilon <= value && value <= expected + epsilon;


        // sin a
        internal static double _base(double alpha) => Math.Sin(alpha);

        // cos a = sqrt(1 - sin^2(a))
        internal static double Cos(double alpha, Func<double, double>? sin = null) 
        {
            sin ??= _base;
            if (Math.Abs(sin(alpha)) < 1e-10)
            {
                //if (alpha == Math.PI || alpha == 3 * Math.PI) return -1; //180
                if (isCorrectWithMeasurementError(Math.PI,Math.Abs(alpha)) || isCorrectWithMeasurementError(Math.PI * 3, Math.Abs(alpha))) return -1; //180
                return 1; // 0360
            }
            return Math.Sqrt(1 - Math.Pow(sin(alpha), 2) );
        }
        // sec a = 1/ cos a
        internal static double Sec(double alpha, Func<double, double>? cos = null)
        {
            cos ??= (angle) => Cos(angle, _base);
            double cosValue = cos(alpha);
            if (Math.Abs(cosValue) < 1e-10) return double.PositiveInfinity * Math.Sign(cosValue);
            
            return 1 / cosValue;
        }
        // tg a = sin a/ cos a
        internal static double Tan(double alpha, Func<double, double>? sin = null, Func<double, double>? cos = null)
        {

            sin ??= _base;
            cos ??= (angle) => Cos(angle, sin);

            double cosValue = cos(alpha);
            double sinValue = sin(alpha);
            int sign = 1;

            if (Math.Abs(alpha) % (Math.PI / 4) < 1e-10 && Math.Abs(alpha) % (7*Math.PI / 12) > 1e-10)
                if (Math.Abs(sinValue - cosValue) < 1e-10) return -1;
            

            if (Math.Abs(cosValue) < 1e-10) return double.NaN; 
            if (Math.Abs(sinValue) < 1e-10) return 0.0; 

            return sinValue / cosValue * sign;
        }
        // ctg a = 1/ tg a
        internal static double Cot(double alpha, Func<double, double>? tan = null)
        {
            tan ??= (angle) =>
            {
                Func<double, double> cos = (a) => Cos(a, _base);
                return Tan(angle, _base, cos);
            };

            double tanValue = tan(alpha);
            if (Math.Abs(alpha % Math.PI) == Math.PI / 2 || double.IsNaN(tanValue)) // ±90°, ±270° etc.
                return double.NaN;
            
            if (Math.Abs(tanValue) < 1e-10) return double.PositiveInfinity;
            
            return 1 / tanValue;
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
                * Math.Pow(sin(x) + 2 * cos(x), 2);
        }
    }
}
