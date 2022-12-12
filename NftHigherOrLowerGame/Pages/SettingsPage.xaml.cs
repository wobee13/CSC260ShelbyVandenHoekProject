using System.Diagnostics;

namespace NftHigherOrLowerGame.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        if (Preferences.Default.ContainsKey("username"))
        {
            UserName.Text = Preferences.Default.Get("username", "");
        }

        if (Preferences.Default.Get("currency", "USD") == "USD")
        {
            USDRadio.IsChecked = true;
        }
        else
        {
            EthRadio.IsChecked = true;
        }
    }

    private void UserName_Completed(object sender, EventArgs e)
    {
        Preferences.Default.Set("username", UserName.Text);
    }

    private void USDRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Preferences.Default.Set("currency", "USD");
        }
    }

    private void EthRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Preferences.Default.Set("currency", "ETH");
        }
    }
}