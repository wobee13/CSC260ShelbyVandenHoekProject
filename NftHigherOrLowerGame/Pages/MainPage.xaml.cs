using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        if (Preferences.Default.ContainsKey("currency") == false)
        {
            Preferences.Default.Set("currency", "USD");
        }
        if (Preferences.Default.ContainsKey("difficulty") == false)
        {
            Preferences.Default.Set("difficulty", "Easy");
        }
    }

    private async void GameButton_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if (Preferences.Default.ContainsKey("username"))
        {
            if (accessType == NetworkAccess.Internet)
            {
                await Shell.Current.GoToAsync("Game");
                Game.Start();
            }
            else
            {
                await DisplayAlert("Connection Failed", "An internet connection is required to play the game", "OK");
            }
        }
        else
        {
            await DisplayAlert("Username not set", "Please go to settings and create a username.\nThis is used for the High Scores Leaderboard.", "OK");
        }
    }

    private async void AboutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("About");
    }

    private async void SettingsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Settings");
    }

    private async void HighScoreButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("HighScores");
    }
}


