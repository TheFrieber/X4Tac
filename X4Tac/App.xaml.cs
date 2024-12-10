using X4Tac.Src.Services.Implementations.Interaction;
using X4Tac.Src.Services.Implementations.Logging;
using X4Tac.Src.Services.Implementations.Storage;
using X4Tac.Src.Services.Implementations.UI;
using X4Tac.Src.Services.Interfaces.Interaction;
using X4Tac.Src.Services.Interfaces.Logging;
using X4Tac.Src.Services.Interfaces.Storage;
using X4Tac.Src.Services.Interfaces.UI;
using X4Tac.Src.ViewModels.MainPage.Content;
using X4Tac.Src.ViewModels.MainPage.Head;
using X4Tac.Src.Views.MainPage.Content;

namespace X4Tac
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Dependency Injection (DI) registrieren
            var services = new ServiceCollection();


            // Services registrieren
            services.AddTransient<IViewFactory, ViewFactory>();
            services.AddSingleton<IGameHandlerService, GameHandlerService>();
            services.AddSingleton<IGameStorageService, GameStorageService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IUIInteractionService, UIInteractionService>();

            // Views registrieren
            services.AddSingleton<GameBoardView>();
            services.AddSingleton<NewGameView>();

            // Pages registrieren
            services.AddSingleton<MainPage>();

            // ViewModels registrieren
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<GameBoardViewModel>();
            services.AddSingleton<NewGameViewModel>();



            // ServiceProvider erstellen
            var serviceProvider = services.BuildServiceProvider();

            // MainPage als Einstiegspunkt festlegen und DI übergeben
            MainPage = serviceProvider.GetRequiredService<MainPage>();
        }
    }
}
