using System.Globalization;

namespace Pulvis
{
    public class Pulvis
    {
        public double NumerusPrimus { get; private set; } = 0;
        public string OperatioCurrens { get; private set; } = string.Empty;
        public bool IsOperatioPulsata { get; set; } = false;

        public double NumerusUltimusSecundus { get; private set; } = 0;
        public string OperatioUltima { get; private set; } = string.Empty;
        public bool IsFacitPulsatum { get; set; } = false;

        public double textusSumma { get; private set; } = 0;

        public double calculatura(double primus, double secundus, string operatio)
        {
            double summa;

            switch (operatio)
            {
                case "÷":
                    if (secundus == 0)
                    {
                        return double.NaN;
                    }
                    summa = primus / secundus;
                    break;

                case "×":
                    summa = primus * secundus;
                    break;

                case "-":
                    summa = primus - secundus;
                    break;

                case "+":
                    summa = primus + secundus;
                    break;

                default:
                    summa = 0;
                    break;
            }

            textusSumma = summa;

            return summa;
        }

        // Управление вводом цифр (0-9)
        public string RegisteDigitum(string textusPantalae, string digitus)
        {
            if (IsOperatioPulsata || IsFacitPulsatum || textusPantalae == "Divisio per nihil!")
            {
                textusPantalae = string.Empty;
                IsOperatioPulsata = false;

                if (IsFacitPulsatum)
                {
                    OperatioUltima = string.Empty;
                    IsFacitPulsatum = false;
                }
            }

            if (textusPantalae == "0") { textusPantalae = string.Empty; }

            return textusPantalae + digitus;
        }

        // Управление вводом десятичного разделителя
        public string RegisteSeparatorem(string textusPantalae, string separator)
        {
            if (IsOperatioPulsata || IsFacitPulsatum || textusPantalae == "Divisio per nihil!")
            {
                IsOperatioPulsata = false;
                if (IsFacitPulsatum)
                {
                    OperatioUltima = string.Empty;
                    IsFacitPulsatum = false;
                }
                return "0" + separator;
            }

            if (!textusPantalae.Contains(separator))
            {
                if (string.IsNullOrEmpty(textusPantalae)) { textusPantalae = "0"; }
                textusPantalae += separator;
            }

            return textusPantalae;
        }

        // Управление сменой знака числа
        public string RegisteSignum(string textusPantalae)
        {
            if (textusPantalae == "0" || textusPantalae == "Divisio per nihil!") { return textusPantalae; }

            if (textusPantalae.StartsWith("-"))
            {
                return textusPantalae.Substring(1);
            }
            else
            {
                return "-" + textusPantalae;
            }
        }

        // Управление последовательными вычислениями в цепочке
        public string RegisteOperationem(string textusPantalae, string operatioNova, string separator)
        {
            string textusAdParsendum = textusPantalae.Replace(separator, ".");

            if (!string.IsNullOrEmpty(OperatioCurrens) && !IsOperatioPulsata && !IsFacitPulsatum)
            {
                double numerusSecundus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
                double summaIntermedia = calculatura(NumerusPrimus, numerusSecundus, OperatioCurrens);

                if (double.IsNaN(summaIntermedia))
                {
                    ClearAll();
                    return "Divisio per nihil!";
                }

                NumerusPrimus = summaIntermedia;
                textusPantalae = summaIntermedia.ToString(CultureInfo.InvariantCulture).Replace(".", separator);
            }
            else
            {
                NumerusPrimus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
            }

            OperatioCurrens = operatioNova;
            IsOperatioPulsata = true;
            IsFacitPulsatum = false;

            return textusPantalae;
        }

        // Управление вычислением по кнопке "=" и повторением последней операции
        public string ComputeFacit(string textusPantalae, string separator)
        {
            string textusAdParsendum = textusPantalae.Replace(separator, ".");
            double numerusSecundus;

            if (!string.IsNullOrEmpty(OperatioCurrens))
            {
                numerusSecundus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
                NumerusUltimusSecundus = numerusSecundus;
                OperatioUltima = OperatioCurrens;
                OperatioCurrens = string.Empty;
            }
            else if (!string.IsNullOrEmpty(OperatioUltima))
            {
                NumerusPrimus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
                numerusSecundus = NumerusUltimusSecundus;
            }
            else
            {
                return textusPantalae;
            }

            double summa = calculatura(NumerusPrimus, numerusSecundus, OperatioUltima);

            if (double.IsNaN(summa))
            {
                ClearAll();
                return "Divisio per nihil!";
            }

            IsFacitPulsatum = true;
            IsOperatioPulsata = false;

            return summa.ToString(CultureInfo.InvariantCulture).Replace(".", separator);
        }

        // Полный сброс всего калькулятора (кнопка Сброс)
        public void ClearAll()
        {
            NumerusPrimus = 0;
            OperatioCurrens = string.Empty;
            IsOperatioPulsata = false;
            OperatioUltima = string.Empty;
            NumerusUltimusSecundus = 0;
            IsFacitPulsatum = false;
            textusSumma = 0;
        }
    }
}
