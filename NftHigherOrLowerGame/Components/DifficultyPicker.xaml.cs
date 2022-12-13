namespace NftHigherOrLowerGame.Components;

public partial class DifficultyPicker : ContentView
{
    public DifficultyPicker()
    {
        InitializeComponent();
        DiffPicker.SelectedItem = Preferences.Default.Get("difficulty", "Easy");
    }

    private void DifficultyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        Preferences.Default.Set("difficulty", DiffPicker.ItemsSource[selectedIndex]);
    }
}