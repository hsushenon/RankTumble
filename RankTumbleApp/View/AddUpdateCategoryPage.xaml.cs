namespace RankTumbleApp;

public partial class AddUpdateCategoryPage : ContentPage
{
	public AddUpdateCategoryPage(AddUpdateCategoryViewModel addUpdateCategoryViewModel)
	{
		InitializeComponent();
        BindingContext = addUpdateCategoryViewModel;
		
    }
}