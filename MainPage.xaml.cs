using System.Globalization;

namespace Abacus
{
    public partial class MainPage : ContentPage
    {
        // Переменные для памяти калькулятора:
        private double numerusPrimus = 0;
        private string operatioCurrens = string.Empty;
        private bool isOperatioPulsata = false;
        private readonly string separatorSystematis = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        // Конструктор страницы — он собирает визуальные элементы из XAML
        public MainPage()
        {
            InitializeComponent();
            
            pulsatorPunctum.Text = separatorSystematis;
        }

        // Обработчик для кнопок с цифрами
        private void InDigitoPulsato(object sender, EventArgs e)
        {
            Button pulsator = (Button)sender;
            string clavisPulsata = pulsator.Text;

            if (isOperatioPulsata)
            {
                textusSumma.Text = string.Empty;
                isOperatioPulsata = false;
            }

            if (textusSumma.Text == "0") { textusSumma.Text = string.Empty; }

            textusSumma.Text += clavisPulsata;
        }

        // Обработчик для кнопок с операциями "÷", "×", "-" и "+"
        private void InOperationePulsata(object sender, EventArgs e)
        {
            if (textusSumma.Text == "Divisio per nihil!") { return; }
            
            Button pulsator = (Button)sender;

            string textusAdParsendum = textusSumma.Text.Replace(separatorSystematis, ".");
            
            numerusPrimus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
            operatioCurrens = pulsator.Text;
            isOperatioPulsata = true;
        }

        // Обработчик для кнопки результата ("=")
        private void InFacitPostulato(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(operatioCurrens) || textusSumma.Text == "Divisio per nihil!") { return; }

            string textusAdParsendum = textusSumma.Text.Replace(separatorSystematis, ".");

            double numerusSecundus = double.Parse(textusAdParsendum, CultureInfo.InvariantCulture);
            var calculatio = new Pulvis.Pulvis();
            double summa = calculatio.calculatura(numerusPrimus, numerusSecundus, operatioCurrens);

            if (double.IsNaN(summa))
            {
                textusSumma.Text = "Divisio per nihil!";
                numerusPrimus = 0;
                operatioCurrens = string.Empty;
                return;
            }

            textusSumma.Text = summa.ToString(CultureInfo.InvariantCulture).Replace(".", separatorSystematis);
            operatioCurrens = string.Empty;
        }

        // Обработчик для кнопки сброса
        private void InDelePulsato(object sender, EventArgs e)
        {
            textusSumma.Text = "0";
            numerusPrimus = 0;
            operatioCurrens = string.Empty;
            isOperatioPulsata = false;
        }

        // Обработчик для кнопки смены знака
        private void InSignoPulsato(object sender, EventArgs e)
        {
            if (textusSumma.Text == "0" || textusSumma.Text == "Divisio per nihil!") { return; }

            if (textusSumma.Text.StartsWith("-"))
            {
                textusSumma.Text = textusSumma.Text.Substring(1);
            }
            else
            {
                textusSumma.Text = "-" + textusSumma.Text;
            }
        }

        // Обработчик для кнопки десятичного разделителя
        private void InPunctoPulsato(object sender, EventArgs e)
        {
            if (isOperatioPulsata || textusSumma.Text == "Divisio per nihil!")
            {
                textusSumma.Text = "0" + separatorSystematis;
                isOperatioPulsata = false;
                return;
            }

            if (!textusSumma.Text.Contains(separatorSystematis))
            {
                if (string.IsNullOrEmpty(textusSumma.Text)) { textusSumma.Text = "0";  }
                textusSumma.Text += separatorSystematis;
            }
        }
    }
}
