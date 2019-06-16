using Microsoft.VisualStudio.TestTools.UnitTesting;
using XRElements;

namespace Test1
{
    [TestClass]
    public class outputTest
    {
        [TestMethod]
        public void outputTest1()
        {
            packCalculator packCalculator = new packCalculator("10 SH3");
            string output = packCalculator.calculatePacks().Trim().Replace("\n","");
            string expectedOutput = "10 SH3 $8.98 2 x 5 $4.49".Trim().Replace("\n", "");
            Assert.AreEqual(expectedOutput,output);
            System.Console.WriteLine();

        }

        [TestMethod]
        public void outputTest2()
        {
            packCalculator packCalculator = new packCalculator("28 YT2");
            string output = packCalculator.calculatePacks().Trim().Replace("\n", ""); ;
            string expectedOutput = "28 YT2 $29.80 2 x 10 $9.95 2 x 4 $4.95".Trim().Replace("\n", ""); ;
            Assert.AreEqual(output, expectedOutput);
        }
        [TestMethod]
        public void outputTest3()
        {
            packCalculator packCalculator = new packCalculator("12 TR");
            string output = packCalculator.calculatePacks().Trim().Replace("\n", ""); ;
            string expectedOutput = "12 TR $10.94 1 x 9 $7.99 1 x 3 $2.95".Trim().Replace("\n", ""); ;
            Assert.AreEqual(output, expectedOutput);
        }

    }
}
