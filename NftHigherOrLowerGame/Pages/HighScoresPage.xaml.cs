using NftHigherOrLowerGame.Model.Binding;

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


