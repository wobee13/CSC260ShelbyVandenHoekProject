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
            ScoreButton.Text = $"Score: {ScoreValue}    Lives: {Lives}";
        }
    }

    private uint _Lives = 3;
    public uint Lives
    {
        get { return _Lives; }
        set
        {
            if (value == 0)
            {
                _Lives = value; Game.GameOver();
            }
            else
            {
                _Lives = value;
            }
            ScoreButton.Text = $"Score: {ScoreValue}    Lives: {Lives}";
        }
    }

    public ScoreBoard()
    {
        InitializeComponent();
        ScoreButton.Text = $"Score: {ScoreValue}    Lives: {Lives}";
        Game.RegistererScoreBoard(this);
    }
}