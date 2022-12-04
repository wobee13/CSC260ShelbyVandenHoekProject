using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class AnswerFeedBack : ContentView
{
    public AnswerFeedBack()
    {
        InitializeComponent();
        Game.RegisterAnswerDisplay(this);
    }

    public void Correct(string message)
    {
        AnswerIcon.Source = "icons8_ok.png";
        AnswerLabel.Text = "Correct Answer!";
        AnswerScore.Text = message;
        ShowFeedBack(500);
    }

    public void Wrong(string message)
    {
        AnswerIcon.Source = "icons8_cancel.png";
        AnswerLabel.Text = "Wrong Answer";
        AnswerScore.Text = message;
        ShowFeedBack(500);
    }

    public void HideFeedBack(uint fadeTime = 250)
    {
        AnswerStack.FadeTo(0, fadeTime);
    }

    public void ShowFeedBack(uint fadeTime = 250)
    {
        AnswerStack.FadeTo(1, fadeTime);
    }
}