using X4Tac.Src.Services.Interfaces.UI;
using X4Tac.Src.ViewModels.MainPage.Content;

namespace X4Tac.Src.Views.MainPage.Content;

public partial class GameBoardView : ContentView
{
	public GameBoardView(GameBoardViewModel gameBoardView, IUIInteractionService uIInteractionService)
	{
		InitializeComponent();
        BindingContext = gameBoardView;
        uIInteractionService.RegisterView(this);
	}


    protected override void OnParentSet()
    {
        base.OnParentSet();

        Opacity = 0;
        this.FadeTo(1, 300, Easing.CubicInOut);
    }

    // Event-Handler für das Klicken auf ein Spielfeld
    private void OnCellClicked(object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        if (clickedButton != null)
        {
            // Hier logik hinzufügen, um den Zug des Spielers auszuführen
            // Beispiel: clickedButton.Text = "X"; für einen Zug von Spieler X
        }
    }
}