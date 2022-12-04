using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class PauseMenu : ContentView
{
	public PauseMenu()
	{
		InitializeComponent();
	}

    private void Pause_Clicked(object sender, EventArgs e)
    {
		Game.PauseMenu();
    }
}