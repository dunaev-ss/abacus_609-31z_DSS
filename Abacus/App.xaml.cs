using Microsoft.Extensions.DependencyInjection;

namespace Abacus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Создаем окно и сразу запускаем в нем MainPage, а не AppShell
            var window = new Window(new MainPage());

            // Задаем размеры окна специально для Windows
#if WINDOWS
    window.Width = 420;         // Ширина окна в пикселях
    window.Height = 680;        // Высота окна в пикселях
    window.MinimumWidth = 420;  // Ограничение на минимальное сжатие по ширине
    window.MinimumHeight = 680; // Ограничение на минимальное сжатие по высоте
#endif

            // Возвращаем полностью настроенное окно
            return window;
        }
    }
}
