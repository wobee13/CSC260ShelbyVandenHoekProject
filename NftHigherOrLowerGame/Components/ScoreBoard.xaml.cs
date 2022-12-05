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
                _ScoreValue = value * 100;
            }
            ScoreButton.Text = $"Score: {ScoreValue}";
        }
    }


    public ScoreBoard()
    {
        InitializeComponent();
        ScoreValue = 0;
        Game.RegistererScoreBoard(this);
    }
}