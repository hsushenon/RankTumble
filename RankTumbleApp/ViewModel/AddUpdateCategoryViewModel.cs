using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RankTumbleApp
{
    ////For uri based navigation
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class AddUpdateCategoryViewModel : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        private int categoryId;
        public int CategoryId
        {
            get => categoryId;
            set
            {
                SetProperty(ref categoryId, value);
                if (categoryId != 0)
                {
                    try
                    {
                        var c = App.Database.GetRankCategoryItemAsync(categoryId);
                        if (c != null)
                        {
                            Id = c.Result.ID;
                            Name = c.Result.Name;
                            Description = c.Result.Description;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        public AddUpdateCategoryViewModel()
        {
           
        }

        [RelayCommand]
        async Task Submit()
        {
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    RankCategory cat = new RankCategory();
                    cat.Name = Name;
                    cat.Description = Description;
                    cat.ID = Id;

                    _ = App.Database.AddUpdateRankCategoryItemAsync(cat);

                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert", "Enter name", "OK");
                }
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Some error occured.", "OK");
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
