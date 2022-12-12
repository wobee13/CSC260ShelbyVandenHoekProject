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
        await ImageFrame.FadeTo(0);
        PriceLabel.Opacity = 0;
        NameLabel.Text = "";
        PriceLabel.Text = "";
        Image.Source = ImageSource.FromStream(data.AsStream); //Blurhash Loaded
        await ImageFrame.FadeTo(1, 500);
        await Task.Delay(2000);
        await ImageFrame.FadeTo(0, 500);
        Image.Source = ImageSource.FromUri(new Uri(nft.ImageUrl));
        await ImageFrame.FadeTo(1, 500);

        NameLabel.Text = nft.Name;
        if (Preferences.Default.Get("currency", "USD") == "USD")
        {
            PriceLabel.Text = $"$ {String.Format("{0:n}", nft.priceUSD)}";
        }
        else
        {
            PriceLabel.Text = $"{String.Format("{0:n}", nft.priceETH)} ETH";
        }
        return true;
    }

    public void ShowPrice(uint fadeTime = 250) { PriceLabel.FadeTo(1, fadeTime); }
    public void HidePrice(uint fadeTime = 250) { PriceLabel.FadeTo(0, fadeTime); }
    public void ShowName(uint fadeTime = 250) { NameLabel.FadeTo(1, fadeTime); }
    public void HideName(uint fadeTime = 250) { NameLabel.FadeTo(0, fadeTime); }
}