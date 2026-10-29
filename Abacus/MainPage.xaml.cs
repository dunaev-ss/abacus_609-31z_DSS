using System.Globalization;

namespace Abacus
{
    public partial class MainPage : ContentPage
    {
        private readonly Pulvis.Pulvis _calculator = new Pulvis.Pulvis();
        private readonly string _decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        
        public MainPage()
        {
            InitializeComponent();

            buttonDecimal.Text = _decimalSeparator;
        }

        private void buttonClear_Clicked(object sender, EventArgs e)
        {
            _calculator.ClearAll();
            displayText.Text = "0";
        }
        
        private void buttonDigits_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string digitText = button.Text;

            displayText.Text = _calculator.AppendDigit(displayText.Text, digitText);
        }
        
        private void buttonToggleSign_Clicked(object sender, EventArgs e)
        {
            displayText.Text = _calculator.ToggleSign(displayText.Text);
        }

        private void buttonDecimal_Clicked(object sender, EventArgs e)
        {
            displayText.Text = _calculator.AppendDecimalSeparator(displayText.Text, _decimalSeparator);
        }
        
        private void buttonOperators_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string selectedOperator = button.Text;

            displayText.Text = _calculator.ApplyOperator(displayText.Text, selectedOperator, _decimalSeparator);
        }
        
        private void buttonEqual_Clicked(object sender, EventArgs e)
        {
            displayText.Text = _calculator.CalculateResult(displayText.Text, _decimalSeparator);
        }
    }
}
