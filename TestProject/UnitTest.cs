using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        CommandExecutionManager manager = new CommandExecutionManager();
        CommandEvaluator evaluator = new CommandEvaluator();

        [TestMethod]
        public void TestInvalidGet()
        {
            manager.Flush();
            object x = evaluator.EvaluateCommand("get x");
            Assert.AreEqual(x, "x is not available");
        }

        [TestMethod]
        public void TestValidGet()
        {
            manager.Flush();
            evaluator.EvaluateCommand("set x 5");
            object x = evaluator.EvaluateCommand("get x");
            Assert.AreEqual(x, "5");
        }

        [TestMethod]
        public void TestValidRPush()
        {
            manager.Flush();

            evaluator.EvaluateCommand("rpush x 5 6 7");
            object x = evaluator.EvaluateCommand("rpush x a b c");
            Assert.AreEqual(x, $"x contains: 5 6 7 a b c");
        }
    }
}