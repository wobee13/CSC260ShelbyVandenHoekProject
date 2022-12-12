using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Pages;

public partial class GameOverPage : ContentPage
{
    public GameOverPage()
    {
        InitializeComponent();
        PointsLabel.Text = $"{Game.Score.ScoreValue} Points";
        CorrectLabel.Text = $"{Game.Results.TotalRight} Answered Correctly";
        WrongLabel.Text = $"{Game.Results.TotalWrong} Answered Wrong";
        TotalLabel.Text = $"{Game.Results.TotalAnswered} Rounds Played";
    }

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.SendBackButtonPressed();
        Shell.Current.SendBackButtonPressed();
    }
}