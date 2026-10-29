using Pulvis;

namespace Probatio
{
    [TestClass]
    public sealed class Probatio
    {
        private Pulvis.Pulvis _pulvis = new Pulvis.Pulvis();

        [TestMethod]
        public void TestInitialDisplayIsZero()
        {
            Assert.AreEqual(0, _pulvis.DisplayValue);
        }

        [TestMethod]
        public void TestDivisionByZero()
        {
            double result = _pulvis.calculatura(8, 0, "÷");
            Assert.IsTrue(double.IsNaN(result));
        }

        [TestMethod]
        public void TestDivision()
        {
            double result = _pulvis.calculatura(42, 7, "÷");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            double result = _pulvis.calculatura(4, 3, "×");
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            double result = _pulvis.calculatura(10, 4, "-");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void TestAddition()
        {
            double result = _pulvis.calculatura(5, 3, "+");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestUnknownOperator()
        {
            double result = _pulvis.calculatura(10, 5, "?");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestAppendDigitsSequence()
        {
            string screenText = "0";
            screenText = _pulvis.AppendDigit(screenText, "5");
            screenText = _pulvis.AppendDigit(screenText, "2");

            Assert.AreEqual("52", screenText);
        }

        [TestMethod]
        public void TestChainCalculation()
        {
            _pulvis.ClearAll();
            string screenText = "2";
            screenText = _pulvis.ApplyOperator(screenText, "+", ",");
            screenText = _pulvis.AppendDigit(screenText, "3");

            screenText = _pulvis.ApplyOperator(screenText, "+", ",");

            Assert.AreEqual("5", screenText);
            Assert.AreEqual(5, _pulvis.FirstNumber);
        }

        [TestMethod]
        public void TestRepeatLastOperator()
        {
            _pulvis.ClearAll();
            string screenText = "5";
            screenText = _pulvis.ApplyOperator(screenText, "+", ",");
            screenText = _pulvis.AppendDigit(screenText, "3");

            screenText = _pulvis.CalculateResult(screenText, ",");
            Assert.AreEqual("8", screenText);

            screenText = _pulvis.CalculateResult(screenText, ",");
            Assert.AreEqual("11", screenText);
        }

        [TestMethod]
        public void TestDuplicateDecimalSeparator()
        {
            _pulvis.ClearAll();
            string screenText = "5";
            screenText = _pulvis.AppendDecimalSeparator(screenText, ",");
            screenText = _pulvis.AppendDecimalSeparator(screenText, ",");

            Assert.AreEqual("5,", screenText);
        }

        [TestMethod]
        public void TestClearAllState()
        {
            string screenText = "7";
            screenText = _pulvis.ApplyOperator(screenText, "+", ",");
            _pulvis.ClearAll();

            Assert.AreEqual(0, _pulvis.FirstNumber);
            Assert.AreEqual(string.Empty, _pulvis.CurrentOperator);
            Assert.IsFalse(_pulvis.IsOperatorPressed);
        }
    }
}
