using NftHigherOrLowerGame.Pages;

namespace NftHigherOrLowerGame;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("Game", typeof(GamePage));
        Routing.RegisterRoute("Settings", typeof(SettingsPage));
        Routing.RegisterRoute("About", typeof(AboutPage));
        Routing.RegisterRoute("GameOver", typeof(GameOverPage));
        Routing.RegisterRoute("HighScores", typeof(HighScoresPage));
    }
}

