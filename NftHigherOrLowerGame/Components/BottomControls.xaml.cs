using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class BottomControls : ContentView
{
	public BottomControls()
	{
		InitializeComponent();
	}

    private void Higher_Button_Click(object sender, EventArgs e)
    {
        Game.Higher();
    }

    private void Lower_Button_Click(object sender, EventArgs e)
    {
        Game.Lower();
    }
}