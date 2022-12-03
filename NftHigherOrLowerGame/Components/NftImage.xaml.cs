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

    public async void ChangeImage(NFT nft)
    {
        // decode blurhash instring into a SKBitmap
        var im = Blurhasher.Decode(nft.BlurHash, 4, 4);

        // load it into a SKImage
        var data = SKImage.FromBitmap(im).Encode();

        // use as a source for Image control
        ImageFrame.Opacity = 0;
        Image.Source = ImageSource.FromStream(data.AsStream);
        await ImageFrame.FadeTo(1, 500);
        await Task.Delay(2000);
        await ImageFrame.FadeTo(0, 500);
        Image.Source = ImageSource.FromUri(new Uri(nft.ImageUrl));
        await ImageFrame.FadeTo(1, 500);


        NameLabel.Text = nft.Name;
        PriceLabel.Text = $"${nft.priceUSD}";
    }
}