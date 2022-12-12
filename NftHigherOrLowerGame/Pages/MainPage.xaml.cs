using CommunityToolkit.Maui.Views;
using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public void DisplayPopup()
    {
        var popup = new PausePopup();

        this.ShowPopup(popup);
    }

    private async void GameButton_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
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
}


