using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class PauseButton : ContentView
{
    public PauseButton()
    {
        InitializeComponent();
    }

    private void Pause_Clicked(object sender, EventArgs e)
    {
        Game.PauseMenu();
    }
}