namespace NftHigherOrLowerGame.Components;

public partial class CurrencyRadioGroup : ContentView
{
    public CurrencyRadioGroup()
    {
        InitializeComponent();
        if (Preferences.Default.Get("currency", "USD") == "USD")
        {
            CurrPicker.SelectedItem = "US Dollars";
        }
        else
        {
            CurrPicker.SelectedItem = "Ethereum";
        }
    }



    private void CurrPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if ((string)CurrPicker.ItemsSource[selectedIndex] == "US Dollars")
        {
            Preferences.Default.Set("currency", "USD");
        }
        else
        {
            Preferences.Default.Set("currency", "ETH");
        }
    }
}