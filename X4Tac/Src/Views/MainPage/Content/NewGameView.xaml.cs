using X4Tac.Src.ViewModels.MainPage.Content;

namespace X4Tac.Src.Views.MainPage.Content;

public partial class NewGameView : ContentView
{
	public NewGameView(NewGameViewModel newGameViewModel)
	{
		InitializeComponent();
		BindingContext = newGameViewModel;
	}


    protected override void OnParentSet()
    {
        base.OnParentSet();

        // Falls diese Seite vor OnAppearing geladen wird,
        // gibt es noch keinen AnimationManager.
        try
        {
            Opacity = 0;
            this.FadeTo(1, 300, Easing.CubicInOut);
        }
        catch 
        {
            Opacity = 1;
        }

    }
}