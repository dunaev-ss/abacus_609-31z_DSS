using System.Globalization;

namespace Abacus
{
    public partial class MainPage : ContentPage
    {
        private readonly Pulvis.Pulvis _calculatio = new Pulvis.Pulvis();
        private readonly string _separatorSystematis = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        
        // Конструктор страницы — он собирает визуальные элементы из XAML
        public MainPage()
        {
            InitializeComponent();
            
            pulsatorPunctum.Text = _separatorSystematis;
        }

        // Обработчик для кнопок с цифрами
        private void InDigitoPulsato(object sender, EventArgs e)
        {
            Button pulsator = (Button)sender;
            string clavisPulsata = pulsator.Text;
                        
            textusSumma.Text = _calculatio.RegisteDigitum(textusSumma.Text, clavisPulsata);
        }

        // Обработчик для кнопок с операциями "÷", "×", "-" и "+"
        private void InOperationePulsata(object sender, EventArgs e)
        {
            Button pulsator = (Button)sender;
            string operationNova = pulsator.Text;

            textusSumma.Text = _calculatio.RegisteOperationem(textusSumma.Text, operationNova, _separatorSystematis);
        }

        // Обработчик для кнопки результата ("=")
        private void InFacitPostulato(object sender, EventArgs e)
        {
            textusSumma.Text = _calculatio.ComputeFacit(textusSumma.Text, _separatorSystematis);
        }

        // Обработчик для кнопки сброса
        private void InDelePulsato(object sender, EventArgs e)
        {
           _calculatio.ClearAll();
            textusSumma.Text = "0";
        }

        // Обработчик для кнопки смены знака
        private void InSignoPulsato(object sender, EventArgs e)
        {
            textusSumma.Text = _calculatio.RegisteSignum(textusSumma.Text);
        }

        // Обработчик для кнопки десятичного разделителя
        private void InPunctoPulsato(object sender, EventArgs e)
        {
            textusSumma.Text = _calculatio.RegisteSeparatorem(textusSumma.Text, _separatorSystematis);
        }
    }
}
