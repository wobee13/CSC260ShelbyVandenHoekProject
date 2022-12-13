using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class LivesBoard : ContentView
{
    private uint _LivesValue;
    public uint LivesValue
    {
        get { return _LivesValue; }
        set
        {
            if (value == 0)
            {
                _LivesValue = value; Game.GameOver();
            }
            else
            {
                _LivesValue = value;
            }
            string livesHearts = "";
            for (int i = 0; i < LivesValue; i++) { livesHearts += "❤"; }
            LivesButton.Text = $"Lives: {livesHearts}";
        }
    }

    public LivesBoard()
    {
        InitializeComponent();
        if (Preferences.Default.Get("difficulty", "Easy") == "Easy")
        {
            LivesValue = 7;
        }
        else if (Preferences.Default.Get("difficulty", "Normal") == "Normal")
        {
            LivesValue = 5;
        }
        else
        {
            LivesValue = 3;
        }

        Game.RegistererLivesBoard(this);
    }

    public void LoseLife()
    {
        LivesValue -= 1;
    }

    public void GainLife()
    {
        LivesValue += 1;
    }
}