namespace RankTumbleApp;

public partial class AddUpdateRankItemPage : ContentPage
{
	public AddUpdateRankItemPage(AddUpdateRankItemViewModel addUpdateRankItemViewModel)
	{
		InitializeComponent();
        BindingContext = addUpdateRankItemViewModel;
    }
}