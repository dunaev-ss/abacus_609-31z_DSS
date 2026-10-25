namespace Abacus
{
    public partial class MainPage : ContentPage
    {
        // Переменные для памяти калькулятора:
        private double numerusPrimus = 0;
        private string operatioCurrens = string.Empty;
        private bool isOperatioPulsata = false;

        // Конструктор страницы — он собирает визуальные элементы из XAML
        public MainPage()
        {
            InitializeComponent();
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
            Button pulsator = (Button)sender;

            numerusPrimus = double.Parse(textusSumma.Text);
            operatioCurrens = pulsator.Text;
            isOperatioPulsata = true;
        }

        // Обработчик для кнопки результата ("=")
        private void InFacitPostulato(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(operatioCurrens)) { return; }

            double numerusSecundus = double.Parse(textusSumma.Text);
            var calculatio = new Pulvis.Pulvis();
            double summa = calculatio.calculatura(numerusPrimus, numerusSecundus, operatioCurrens);

            if (double.IsNaN(summa))
            {
                textusSumma.Text = "Divisio per nihil!";
                numerusPrimus = 0;
                operatioCurrens = string.Empty;
                return;
            }

            textusSumma.Text = summa.ToString();
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
    }
}
