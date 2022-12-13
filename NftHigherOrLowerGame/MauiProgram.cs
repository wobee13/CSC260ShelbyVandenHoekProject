using CommunityToolkit.Maui;
using UraniumUI;

namespace NftHigherOrLowerGame;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit().ConfigureMauiHandlers(handlers =>
        {
            handlers.AddUraniumUIHandlers(); // 👈 This line should be added.
        });
        return builder.Build();
    }
}