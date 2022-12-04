using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class ScoreBoard : ContentView
{
    private int _ScoreValue = 0;
    public int ScoreValue
    {
        get { return _ScoreValue; }
        set
        {
            if (value < 0)
            {
                _ScoreValue = 0;
            }
            else
            {
                _ScoreValue = value;
            }
            ScoreButton.Text = $"Score: {ScoreValue}";
        }
    }

    public ScoreBoard()
    {
        InitializeComponent();
        Game.RegistererScoreBoard(this);
    }
}