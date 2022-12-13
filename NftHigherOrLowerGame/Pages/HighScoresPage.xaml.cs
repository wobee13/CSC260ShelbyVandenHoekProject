using NftHigherOrLowerGame.Model;
using NftHigherOrLowerGame.Model.Binding;
using NftHigherOrLowerGame.Model.DataBaseModels;
using System.Collections.ObjectModel;
using UraniumUI.Material.Controls;

namespace NftHigherOrLowerGame.Pages;

public partial class HighScoresPage : ContentPage
{
    HighScoresPageBind BindData = new HighScoresPageBind();

    public HighScoresPage()
    {
        BindingContext = BindData;
        InitializeComponent();
    }

}


