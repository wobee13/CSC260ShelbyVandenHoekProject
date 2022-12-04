using Blurhash.SkiaSharp;
using NftHigherOrLowerGame.Model.DataBaseModels;
using SkiaSharp;

namespace NftHigherOrLowerGame.Components;

public partial class NFTImage : ContentView
{
    public NFTImage()
    {
        InitializeComponent();
    }

    public async Task<bool> ChangeImage(NFT nft)
    {
        var im = Blurhasher.Decode(nft.BlurHash, 4, 4);
        var data = SKImage.FromBitmap(im).Encode();
        ImageFrame.Opacity = 0;
        PriceLabel.Opacity = 0;
        Image.Source = ImageSource.FromStream(data.AsStream); //Blurhash Loaded
        await ImageFrame.FadeTo(1, 500);
        await Task.Delay(2000);
        await ImageFrame.FadeTo(0, 500);
        Image.Source = ImageSource.FromUri(new Uri(nft.ImageUrl));
        await ImageFrame.FadeTo(1, 500);

        NameLabel.Text = nft.Name;
        PriceLabel.Text = $"${nft.priceUSD}";
        return true;
    }

    public void ShowPrice() { PriceLabel.FadeTo(1); }
    public void HidePrice() { PriceLabel.FadeTo(0); }
    public void ShowName() { NameLabel.FadeTo(1); }
    public void HideName() { NameLabel.FadeTo(0); }
}