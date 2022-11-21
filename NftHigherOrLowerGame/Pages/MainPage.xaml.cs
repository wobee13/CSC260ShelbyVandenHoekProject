using Blurhash.SkiaSharp;
using SkiaSharp;

namespace NftHigherOrLowerGame.Pages;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count -= 20;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";
        // decode blurhash instring into a SKBitmap
        var im = Blurhasher.Decode("LEHV6nWB2yk8pyo0adR*.7kCMdnj", 4, 3);

        // load it into a SKImage
        var data = SKImage.FromBitmap(im).Encode();

        // use as a source for Image control
		
        image1.Source = ImageSource.FromStream(data.AsStream);

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


