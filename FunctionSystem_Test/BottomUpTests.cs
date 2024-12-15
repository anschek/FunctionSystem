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
            double[] alpha = new double[] { -360, -180, -90, 0, 90, 180, 360 }.Select(DegreesToRadians).ToArray();
            double[] expected = [1, -1, 0, 1, 0, -1, 1];
            var driver = Math.Sin;

            for (int i = 0; i < alpha.Length; ++i)
            {
                double real = Trigonometry.Cos(alpha[i], driver);

                Console.WriteLine($"cos({alpha[i]}) = {real}");
                Assert.AreEqual(expected[i], real, epsilon);
            }
        }        
        
        [TestMethod]
        public void L2_Trigonomety_Sec()
        {
            double[] alpha = new double[] { -360, -180, -90, 0, 90, 180, 360 }.Select(DegreesToRadians).ToArray();
            double[] expected = [1, -1, double.PositiveInfinity, 1, double.PositiveInfinity, -1, 1];
            var driver = Math.Cos;

            for (int i = 0; i < alpha.Length; ++i)
            {
                double real = Trigonometry.Sec(alpha[i], driver);

                Console.WriteLine($"sec({alpha[i]}) = {real}");
                Assert.AreEqual(expected[i], real, epsilon);
            }
        }        
        
        [TestMethod]
        public void L2_Trigonomety_Tan()
        {
            double[] alpha = new double[] { -360, -315, 315, 360 }.Select(DegreesToRadians).ToArray();
            double[] expected = [0, 1, -1, 0];
            var driver = Math.Cos;

            for (int i = 0; i < alpha.Length; ++i)
            {
                double real = Trigonometry.Tan(alpha[i], null, driver);

                Console.WriteLine($"tan({alpha[i]}) = {real}");
                Assert.AreEqual(expected[i], real, epsilon);
            }

            double[] alphaNan = new double[] { -90,90 }.Select(DegreesToRadians).ToArray();
            for (int i = 0; i < alphaNan.Length; ++i)
            {
                double real = Trigonometry.Tan(alphaNan[i], null, driver);

                Console.WriteLine($"tan({alphaNan[i]}) = {real}");
                Assert.AreEqual(real, double.NaN);
            }
        }        
        
        [TestMethod]
        public void L3_Trigonomety_Cot()
        {
            double[] alpha = new double[] { -45, 45 }.Select(DegreesToRadians).ToArray();
            double[] expected = [-1,1];
            var driver = Math.Tan;

            for (int i = 0; i < alpha.Length; ++i)
            {
                double real = Trigonometry.Cot(alpha[i], driver);

                Console.WriteLine($"cot({alpha[i]}) = {real}");
                Assert.AreEqual(expected[i], real, epsilon);
            }

            double[] alphaNan = new double[] { -90, 90}.Select(DegreesToRadians).ToArray();            
            for (int i = 0; i < alphaNan.Length; ++i)
            {
                double real = Trigonometry.Cot(alphaNan[i], driver);

                Console.WriteLine($"cot({alphaNan[i]}) = {real}");
                Assert.AreEqual(double.NaN, real);
            }

            double[] alphaInfinity = new double[] { -360, 360}.Select(DegreesToRadians).ToArray();            
            for (int i = 0; i < alphaInfinity.Length; ++i)
            {
                double real = Trigonometry.Cot(alphaInfinity[i], driver);

                Console.WriteLine($"cot({alphaInfinity[i]}) = {real}");
                Assert.AreEqual(double.PositiveInfinity, real);
            }
        }        
        
        [TestMethod]
        public void L4_Trigonomety_SystemFunc()
        {
            double alpha = DegreesToRadians(-45);
            double alphaNan = DegreesToRadians(0);
            var driverSin = Math.Sin;
            var driverCos = Math.Cos;

            double expected = Trigonometry.SystemFunc(alpha, driverSin, driverCos);
            double real = Trigonometry.SystemFunc(alpha);
            Console.WriteLine($"expected:{expected}\nreal:{real}");
            Assert.AreEqual(expected, real, epsilon);            
            
            expected = Trigonometry.SystemFunc(alphaNan, driverSin, driverCos);
            real = Trigonometry.SystemFunc(alphaNan);
            Assert.AreEqual(double.NaN, expected);
            Assert.AreEqual(double.NaN, real);
        }
    }
}