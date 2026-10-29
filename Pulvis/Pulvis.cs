using System.Globalization;

namespace Pulvis
{
    public class Pulvis
    {
        public double FirstNumber { get; private set; } = 0;
        public string CurrentOperator { get; private set; } = string.Empty;
        public bool IsOperatorPressed { get; set; } = false;

        public double LastSecondNumber { get; private set; } = 0;
        public string LastOperator { get; private set; } = string.Empty;
        public bool IsEqualsPressed { get; set; } = false;

        public double DisplayValue { get; private set; } = 0;

        public double calculatura(double firstNumber, double secondNumber, string selectedOperator)
        {
            double result;

            switch (selectedOperator)
            {
                case "÷":
                    if (secondNumber == 0)
                    {
                        return double.NaN;
                    }
                    result = firstNumber / secondNumber;
                    break;

                case "×":
                    result = firstNumber * secondNumber;
                    break;

                case "-":
                    result = firstNumber - secondNumber;
                    break;

                case "+":
                    result = firstNumber + secondNumber;
                    break;

                default:
                    result = 0;
                    break;
            }

            DisplayValue = result;

            return result;
        }

        public string AppendDigit(string screenText, string digitText)
        {
            if (IsOperatorPressed || IsEqualsPressed || screenText == "Деление на 0!")
            {
                screenText = string.Empty;
                IsOperatorPressed = false;

                if (IsEqualsPressed)
                {
                    LastOperator = string.Empty;
                    IsEqualsPressed = false;
                }
            }

            if (screenText == "0") { screenText = string.Empty; }

            return screenText + digitText;
        }

        public string AppendDecimalSeparator(string screenText, string decimalSeparator)
        {
            if (IsOperatorPressed || IsEqualsPressed || screenText == "Деление на 0!")
            {
                IsOperatorPressed = false;
                if (IsEqualsPressed)
                {
                    LastOperator = string.Empty;
                    IsEqualsPressed = false;
                }
                return "0" + decimalSeparator;
            }

            if (!screenText.Contains(decimalSeparator))
            {
                if (string.IsNullOrEmpty(screenText)) { screenText = "0"; }
                screenText += decimalSeparator;
            }

            return screenText;
        }

        public string ToggleSign(string screenText)
        {
            if (screenText == "0" || screenText == "Деление на 0!") { return screenText; }

            if (screenText.StartsWith("-"))
            {
                return screenText.Substring(1);
            }
            else
            {
                return "-" + screenText;
            }
        }

        public string ApplyOperator(string screenText, string selectedOperator, string decimalSeparator)
        {
            string textToParse = screenText.Replace(decimalSeparator, ".");

            if (!string.IsNullOrEmpty(CurrentOperator) && !IsOperatorPressed && !IsEqualsPressed)
            {
                double secondNumber = double.Parse(textToParse, CultureInfo.InvariantCulture);
                double intermediateResult = calculatura(FirstNumber, secondNumber, CurrentOperator);

                if (double.IsNaN(intermediateResult))
                {
                    ClearAll();
                    return "Деление на 0!";
                }

                FirstNumber = intermediateResult;
                screenText = intermediateResult.ToString(CultureInfo.InvariantCulture).Replace(".", decimalSeparator);
            }
            else
            {
                FirstNumber = double.Parse(textToParse, CultureInfo.InvariantCulture);
            }

            CurrentOperator = selectedOperator;
            IsOperatorPressed = true;
            IsEqualsPressed = false;

            return screenText;
        }

        public string CalculateResult(string screenText, string decimalSeparator)
        {
            string textToParse = screenText.Replace(decimalSeparator, ".");
            double secondNumber;

            if (!string.IsNullOrEmpty(CurrentOperator))
            {
                secondNumber = double.Parse(textToParse, CultureInfo.InvariantCulture);
                LastSecondNumber = secondNumber;
                LastOperator = CurrentOperator;
                CurrentOperator = string.Empty;
            }
            else if (!string.IsNullOrEmpty(LastOperator))
            {
                FirstNumber = double.Parse(textToParse, CultureInfo.InvariantCulture);
                secondNumber = LastSecondNumber;
            }
            else
            {
                return screenText;
            }

            double result = calculatura(FirstNumber, secondNumber, LastOperator);

            if (double.IsNaN(result))
            {
                ClearAll();
                return "Деление на 0!";
            }

            IsEqualsPressed = true;
            IsOperatorPressed = false;

            return result.ToString(CultureInfo.InvariantCulture).Replace(".", decimalSeparator);
        }

        public void ClearAll()
        {
            FirstNumber = 0;
            CurrentOperator = string.Empty;
            IsOperatorPressed = false;
            LastOperator = string.Empty;
            LastSecondNumber = 0;
            IsEqualsPressed = false;
            DisplayValue = 0;
        }
    }
}
