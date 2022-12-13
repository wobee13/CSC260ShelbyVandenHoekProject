using NftHigherOrLowerGame.Model.DataBaseModels;
using System.Collections.ObjectModel;

namespace NftHigherOrLowerGame.Model.Binding;

public partial class HighScoresPageBind : BindableObject
{
    public ObservableCollection<HighScoreRowData> HighScoreItems { get; } = new();
    public HighScoresPageBind()
    {
        FetchScores();
    }

    private async void FetchScores()
    {
        var HighScoreList = await SupabaseClient.FetchTop100Scores();
        foreach (var HighScore in HighScoreList)
        {
            HighScoreRowData rowData = new(HighScore);
            HighScoreItems.Add(rowData);
        }
    }
}

