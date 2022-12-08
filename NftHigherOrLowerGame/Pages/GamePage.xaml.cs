using CommunityToolkit.Maui.Views;
using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Pages;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        Game.RegisterNFTImage(LeftNFT, Game.Side.Left);
        Game.RegisterNFTImage(RightNFT, Game.Side.Right);
    }

    public void DisplayPauseMenu()
    {
        var popup = new PausePopup();

        this.ShowPopup(popup);
    }
}
