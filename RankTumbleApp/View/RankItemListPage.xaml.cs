namespace RankTumbleApp;

public partial class RankItemListPage : ContentPage
{
    RankItemViewModel rankItemViewModel;
    public RankItemListPage()
	{
		InitializeComponent();
		BindingContext = rankItemViewModel = new RankItemViewModel(Navigation);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        RankItemData r = (RankItemData)((Button)sender).BindingContext;
        if (r != null)
        {
            _ = rankItemViewModel.EditRankItem(r.ID);
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        RankItemData r = (RankItemData)((Button)sender).BindingContext;
        if (r != null)
        {
            _ = rankItemViewModel.DeleteRankItem(r.ID);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        RankItemData r = (RankItemData)((Label)sender).BindingContext;
        if (r != null)
        {    
            _ = rankItemViewModel.UpdateRankItems(r.ID, false);
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        RankItemData r = (RankItemData)((Label)sender).BindingContext;
        if (r != null)
        {
            _ = rankItemViewModel.UpdateRankItems(r.ID, true);
        }
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        RankItemData r = (RankItemData)((Label)sender).BindingContext;
        if (r != null)
        {
            _ = rankItemViewModel.EditRankItem(r.ID);
        }
    }

    private void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
    {
        RankItemData r = (RankItemData)((Label)sender).BindingContext;
        if (r != null)
        {
            _ = rankItemViewModel.DeleteRankItem(r.ID);
        }
    }

}