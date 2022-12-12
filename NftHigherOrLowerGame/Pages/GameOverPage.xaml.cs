using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Pages;

public partial class GameOverPage : ContentPage
{
    public GameOverPage()
    {
        InitializeComponent();
        PointsLabel.Text = $"{Game.Score.ScoreValue} Points";
        CorrectLabel.Text = $"{Game.Results.TotalRight / Game.Results.TotalAnswered} Answered Correctly";
        WrongLabel.Text = $"{Game.Results.TotalWrong / Game.Results.TotalAnswered} Answered Wrong";
    }

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.SendBackButtonPressed();
        Shell.Current.SendBackButtonPressed();
    }
}