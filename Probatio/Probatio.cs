using Pulvis;

namespace Probatio
{
    [TestClass]
    public sealed class Probatio
    {
        private Pulvis.Pulvis _pulvis = new Pulvis.Pulvis();

        // Тест на отображение нуля при запуске калькулятора
        [TestMethod]
        public void ProbatioTest()
        {
            Assert.AreEqual(0, _pulvis.textusSumma);
        }

        // Тест деления на ноль
        [TestMethod]
        public void ProbatioDivisionisPerNihil()
        {
            double summa = _pulvis.calculatura(8, 0, "÷");
            Assert.IsTrue(double.IsNaN(summa));
        }

        // Тест деления
        [TestMethod]
        public void ProbatioDivisionis()
        {
            double summa = _pulvis.calculatura(42, 7, "÷");
            Assert.AreEqual(6, summa);
        }

        // Тест умножения
        [TestMethod]
        public void ProbatioMultiplicationis()
        {
            double summa = _pulvis.calculatura(4, 3, "×");
            Assert.AreEqual(12, summa);
        }

        // Тест вычитания
        [TestMethod]
        public void ProbatioSubtractionis()
        {
            double summa = _pulvis.calculatura(10, 4, "-");
            Assert.AreEqual(6, summa);
        }

        // Тест сложения
        [TestMethod]
        public void ProbatioAdditionis()
        {
            double summa = _pulvis.calculatura(5, 3, "+");
            Assert.AreEqual(8, summa);
        }

        // Тест для проверки неизвестной или пустой операции
        [TestMethod]
        public void ProbatioOperationisIncongnitae()
        {
            double summa = _pulvis.calculatura(10, 5, "?");
            Assert.AreEqual(0, summa);
        }


        // ---


        // Тест последовательного ввода цифр (формирование многозначного числа)
        [TestMethod]
        public void ProbatioRegistrationisDigitorum()
        {
            string pantala = "0";
            pantala = _pulvis.RegisteDigitum(pantala, "5");
            pantala = _pulvis.RegisteDigitum(pantala, "2");

            Assert.AreEqual("52", pantala);
        }

        // Тест последовательных вычислений в цепочке (ТЗ Пункт 3)
        // Пример: 2 + 3 + 4 должен дать промежуточный итог 5 перед вторым плюсом
        [TestMethod]
        public void ProbatioChainCalculationis()
        {
            _pulvis.ClearAll();
            string pantala = "2";
            pantala = _pulvis.RegisteOperationem(pantala, "+", ",");
            pantala = _pulvis.RegisteDigitum(pantala, "3");

            // Нажимаем следующий "+" — калькулятор должен автоматически выдать промежуточные 5
            pantala = _pulvis.RegisteOperationem(pantala, "+", ",");

            Assert.AreEqual("5", pantala);
            Assert.AreEqual(5, _pulvis.NumerusPrimus);
        }

        // Тест повторного вычисления последней операции (ТЗ Пункт 4)
        // Пример: 5 + 3 = 8, затем снова = -> 11 (8 + 3)
        [TestMethod]
        public void ProbatioRepetitionisOperationis()
        {
            _pulvis.ClearAll();
            string pantala = "5";
            pantala = _pulvis.RegisteOperationem(pantala, "+", ",");
            pantala = _pulvis.RegisteDigitum(pantala, "3");

            // Первое нажатие "="
            pantala = _pulvis.ComputeFacit(pantala, ",");
            Assert.AreEqual("8", pantala);

            // Повторное нажатие "=" (должно прибавиться еще раз 3)
            pantala = _pulvis.ComputeFacit(pantala, ",");
            Assert.AreEqual("11", pantala);
        }

        // Тест блокировки некорректного ввода двух разделителей подряд (ТЗ Пункт 5)
        [TestMethod]
        public void ProbatioSeparatorisDuplicati()
        {
            _pulvis.ClearAll();
            string pantala = "5";
            pantala = _pulvis.RegisteSeparatorem(pantala, ",");
            pantala = _pulvis.RegisteSeparatorem(pantala, ",");

            Assert.AreEqual("5,", pantala);
        }

        // Тест функции полного сброса состояния калькулятора (ТЗ Пункт 6)
        [TestMethod]
        public void ProbatioClearAll()
        {
            string pantala = "7";
            pantala = _pulvis.RegisteOperationem(pantala, "+", ",");
            _pulvis.ClearAll();

            Assert.AreEqual(0, _pulvis.NumerusPrimus);
            Assert.AreEqual(string.Empty, _pulvis.OperatioCurrens);
            Assert.IsFalse(_pulvis.IsOperatioPulsata);
        }
    }
}
