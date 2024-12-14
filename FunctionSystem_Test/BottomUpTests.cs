using FunctionSystem;

namespace FunctionSystem_Test
{
    public class Tests
    {
        //protected static double RadiansToDegrees(double radians) => radians * 180 / Math.PI;
        protected static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
        protected double epsilon = 1e-10;
    }

    [TestClass]
    public class BottomUpTests:Tests
    {
        // methods name: LevelNumber _ Class _ TestedFunction

        [TestMethod]
        public void L0_Trigonomety_base()
        {
            double alpha = DegreesToRadians(180);
            int expected = 0;

            double real = Trigonometry._base(alpha);

            Console.WriteLine(real);
            Assert.AreEqual(expected, real, epsilon);
        }        
        
        [TestMethod]
        public void L1_Trigonomety_Cos()
        {
            double alpha = DegreesToRadians(90);
            int expected = 0;
            var driver = Math.Sin;

            double real = Trigonometry.Cos(alpha, driver);

            Console.WriteLine(real);
            Assert.AreEqual(expected, real, epsilon);
        }        
        
        [TestMethod]
        public void L2_Trigonomety_Sec()
        {
            double alpha = DegreesToRadians(180);
            int expected = -1;
            var driver = Math.Cos;

            double real = Trigonometry.Sec(alpha, driver);

            Console.WriteLine(real);
            Assert.AreEqual(expected, real, epsilon);
        }        
        
        [TestMethod]
        public void L2_Trigonomety_Tan()
        {
            double alpha = 0;
            int expected = 0;
            var driver = Math.Cos;

            double real = Trigonometry.Tan(alpha, null, driver);

            Console.WriteLine(real);
            Assert.AreEqual(expected, real, epsilon);
        }        
        
        [TestMethod]
        public void L3_Trigonomety_Cot()
        {
            double alpha = DegreesToRadians(180);
            int expected = -1;
            var driver = Math.Tan;

            double real = Trigonometry.Cos(alpha, driver);

            Console.WriteLine(real);
            Assert.AreEqual(expected, real, epsilon);
        }        
        
        [TestMethod]
        public void L4_Trigonomety_SystemFunc()
        {
            double alpha = -1;
            //double expected = 0;
            var driverSin = Math.Sin;
            var driverCos = Math.Cos;
            var driverTan = Math.Tan;
            Func<double,double> driverSec = (x) =>  1 / Math.Cos(x); 
            Func<double,double> driverCot = (x) =>  1 / Math.Tan(x); 

            double expected = Trigonometry.SystemFunc(alpha, driverSin, driverCos, driverSec, driverTan, driverCot);
            double real = Trigonometry.SystemFunc(alpha);
            Console.WriteLine(expected);
            Console.WriteLine(real);
        }
    }
}