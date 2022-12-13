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

}
