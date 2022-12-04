using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class ScoreBoard : ContentView
{
	public ScoreBoard()
	{
		InitializeComponent();
		Game.RegistererScoreBoard(this);
	}
}