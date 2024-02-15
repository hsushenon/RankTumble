namespace RankTumbleApp;

public partial class CategoryListPage : ContentPage
{
    CategoryViewModel categoryViewModel;
    public CategoryListPage()
	{
		InitializeComponent();
		BindingContext = categoryViewModel = new CategoryViewModel(Navigation);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        RankCategory r = (RankCategory)((Button)sender).BindingContext; 
        if (r != null) {
            _ = categoryViewModel.EditCategory(r.ID);
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        RankCategory r = (RankCategory)((Button)sender).BindingContext;
        if (r != null)
        {
            _ = categoryViewModel.DeleteCategory(r.ID);
        }
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedItem = ((ListView)sender).SelectedItem as RankCategory;
        if (selectedItem != null)
        {
            Category category = new Category();
            category.ID = selectedItem.ID;
            category.Name = selectedItem.Name;
            _ = categoryViewModel.GotoRankList(category);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        RankCategory r = (RankCategory)((Label)sender).BindingContext;
        if (r != null)
        {
            _ = categoryViewModel.EditCategory(r.ID);
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        RankCategory r = (RankCategory)((Label)sender).BindingContext;
        if (r != null)
        {
            _ = categoryViewModel.DeleteCategory(r.ID);
        }
    }
}