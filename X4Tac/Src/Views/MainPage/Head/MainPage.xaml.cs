using X4Tac.Src.ViewModels.MainPage.Head;

namespace X4Tac
{
    public partial class MainPage : ContentPage
    {
        private bool isSidePanelVisible = false;
        private bool lockAnimHandler = false;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }


        // Animations-Handler für das Hamburger-Menü
        private async void OnHamburgerClicked(object sender, EventArgs e)
        {
            // Führe nicht gleichzeitig aus
            if (lockAnimHandler)
                return;
            lockAnimHandler = true;

            // Toggle die Sichtbarkeit des SidePanels und animiere die Translation
            if (isSidePanelVisible)
            {
                await ViewContainer.FadeTo(0, 200, Easing.CubicInOut);

                // Panel nach links verschieben (ausblenden)
                await SidePanel.TranslateTo(-SidePanel.Width, 0, 300, Easing.CubicInOut);
                SidePanel.IsVisible = false;

                ViewContainer.FadeTo(1, 200, Easing.CubicInOut);
            }
            else
            {
                await ViewContainer.FadeTo(0, 200, Easing.CubicInOut);

                // Panel nach rechts verschieben (einblenden)
                SidePanel.IsVisible = true;
                SidePanel.TranslationX = 300;
                SidePanel.Opacity = 0;

                await Task.WhenAll(
                    SidePanel.FadeTo(1, 250, Easing.CubicInOut),
                    SidePanel.TranslateTo(0, 0, 250, Easing.CubicInOut)
                    );

                await ViewContainer.FadeTo(1, 200, Easing.CubicInOut);
            }

            // Den Zustand des SidePanels umschalten
            isSidePanelVisible = !isSidePanelVisible;

            lockAnimHandler = false;
        }
    }

}
