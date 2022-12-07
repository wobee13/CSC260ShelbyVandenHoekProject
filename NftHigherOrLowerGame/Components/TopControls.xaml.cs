using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class TopControls : ContentView
{
    public TopControls()
    {
        InitializeComponent();
    }

    private void PauseButton_Clicked(object sender, EventArgs e)
    {
        Game.PauseMenu();
    }
}