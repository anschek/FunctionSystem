using FunctionSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionSystem_Test
{
    [TestClass]
    public class TopDownTests : Tests
    {
        // methods name: LevelNumber _ Class _ TestedFunction

        [TestMethod]
        public void L0_Complex_FunctionSystem()
        {
            double[] x = { DegreesToRadians(-45), 100 };

            Complex.TrigonometricFunc trigStab = (
            double y,
            Func<double, double>? sin = null,
            Func<double, double>? cos = null,
            Func<double, double>? sec = null,
            Func<double, double>? tan = null,
            Func<double, double>? cot = null
            ) => -0.1464;

            Complex.LogarithmicFunc logStub = (double y, Func<double, double>? ln = null, Func<double, double, double>? log = null) => 17.8152;

            for (int i = 0; i < x.Length; ++i)
            {
                double expected = Complex.FunctionSystem(x[i], trigStab, logStub);
                double real = Complex.FunctionSystem(x[i]);
                Console.WriteLine($"real:{real}");
                Assert.AreEqual(expected, real, 10e-5);
            }
        }

        [TestMethod]
        public void L1_Logarithmic_SystemFunc()
        {
            double x = 100;
            double lnFromX = Math.Log(x);
            Func<double, double> lnStub = (a) => lnFromX;

            double expected = Logarithmic.SystemFunc(x, lnStub);            
            double real = Logarithmic.SystemFunc(x);
            Console.WriteLine($"real:{real}");
            Assert.AreEqual(expected, real, 10e-5);
        }        
        [TestMethod]
        public void L2_Logarithmic_Log()
        {
            double @base = 10;
            double x = 10;
            double lnFromX = Math.Log(x);
            Func<double, double> lnStub = (a) => lnFromX;

            double expected = Logarithmic.Log(@base, x, lnStub);
            double real = Logarithmic.Log(@base, x);
            Assert.AreEqual(expected, real, epsilon);
        }        
        [TestMethod]
        public void LMIN_Logarithmic_base()
        {
            double x = Math.Pow(Math.E, 3);
            double expected = 3;
            double real = Logarithmic._base(x);
            Assert.AreEqual(expected, real, epsilon);
        }
        [TestMethod]
        public void L1_Trigonomety_SystemFunc()
        {
            double alpha = DegreesToRadians(-45);
            Func<double, double> sinStub = (_) => Math.Sin(alpha);
            Func<double, double> cosStub = (_) => Math.Cos(alpha);

            double expected = Trigonometry.SystemFunc(alpha, sinStub, cosStub);
            double real = Trigonometry.SystemFunc(alpha);
            Assert.AreEqual(expected, real, epsilon);
        }        
        [TestMethod]
        public void L2_Trigonomety_Cot()
        {            
            double alpha = DegreesToRadians(90);
            Func<double, double> tanStub = (_) => Math.Tan(alpha);

            double expected = Trigonometry.Cot(alpha, tanStub);
            double real = Trigonometry.Cot(alpha);
            Assert.AreEqual(double.NaN, expected);
            Assert.AreEqual(double.NaN, real);
        }        
        [TestMethod]
        public void L3_Trigonomety_Tan()
        {            
            double alpha = DegreesToRadians(360);
            Func<double, double> sinStub = (_) => Math.Sin(alpha);
            Func<double, double> cosStub = (_) => Math.Cos(alpha);

            double expected = Trigonometry.Tan(alpha, sinStub, cosStub);
            double real = Trigonometry.Tan(alpha);
            Assert.AreEqual(expected, real, epsilon);

        }
        [TestMethod]
        public void L3_Trigonomety_Sec()
        {
            double alpha = DegreesToRadians(-45);
            Func<double, double> cosStub = (_) => Math.Cos(alpha);

            double expected = Trigonometry.Sec(alpha, cosStub);
            double real = Trigonometry.Sec(alpha);
            Assert.AreEqual(expected, real, epsilon);
        }       
        [TestMethod]
        public void L4_Trigonomety_Cos()
        {
            double alpha = DegreesToRadians(-45);
            Func<double, double> sinStub = (_) => Math.Sin(alpha);

            double expected = Trigonometry.Cos(alpha, sinStub);
            double real = Trigonometry.Cos(alpha);
            Assert.AreEqual(expected, real, epsilon);
        }
        [TestMethod]
        public void LMIN_Trigonomety_base()
        {
            double alpha = DegreesToRadians(90);
            double expected = 1;
            double real = Trigonometry._base(alpha);
            Assert.AreEqual(expected, real, epsilon);
        }
    }
}