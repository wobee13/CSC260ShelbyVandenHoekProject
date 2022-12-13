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
    }

    private void UserName_Completed(object sender, EventArgs e)
    {
        Preferences.Default.Set("username", UserName.Text);
    }
}