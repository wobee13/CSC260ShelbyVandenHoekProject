using CommunityToolkit.Maui.Views;
using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model;
using NftHigherOrLowerGame.Model.Enums;

namespace NftHigherOrLowerGame.Pages;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        Game.RegisterNFTImage(LeftNFT, Side.Left);
        Game.RegisterNFTImage(RightNFT, Side.Right);
    }

    public void DisplayPauseMenu()
    {
        var popup = new PausePopup();

        this.ShowPopup(popup);
    }
}
