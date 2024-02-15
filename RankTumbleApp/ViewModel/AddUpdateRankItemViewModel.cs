using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace RankTumbleApp
{
    ////For uri based navigation
    [QueryProperty(nameof(RankItemId), "RankItemId")]
    [QueryProperty(nameof(CategoryName), "CategoryName")]
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class AddUpdateRankItemViewModel : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string category;

        [ObservableProperty]
        int rankPoints;

        private int rankItemId;
        public int RankItemId
        {
            get => rankItemId;
            set
            {
                SetProperty(ref rankItemId, value);
                if (rankItemId != 0)
                {
                    try
                    {
                        var c = App.Database.GetItemAsync(rankItemId);
                        if (c != null)
                        {
                            Id = c.Result.ID;
                            Name = c.Result.Name;
                            Description = c.Result.Description;
                            CategoryId = c.Result.Category;
                            RankPoints = c.Result.RankPoints;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                SetProperty(ref categoryName, value);
                if (!string.IsNullOrEmpty(categoryName))
                {
                    Category = CategoryName;
                }
            }
        }

        private string categoryId;
        public string CategoryId
        {
            get => categoryId;
            set
            {
                SetProperty(ref categoryId, value);
            }
        }
        public AddUpdateRankItemViewModel()
        {
           
        }

        [RelayCommand]
        async Task Submit()
        {
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    //Check if the point exist
                    List<RankItem> rankItems = null;

                    if (RankPoints != 0)
                    {
                        var item = await App.Database.GetItemByRankPointAsync(RankPoints);
                        if (item != null && Id != item.ID) 
                        {
                            rankItems = await App.Database.GetAllRankItemsByRankPoints(RankPoints);
                        }
                    }

                    RankItem cat = new RankItem();
                    cat.Name = Name;
                    cat.Description = Description;
                    cat.ID = Id;
                    cat.Category = CategoryId;
                    cat.RankPoints = RankPoints;
                    _ = await App.Database.AddUpdateRankItemAsync(cat);

                    //Need to move items when new item is added in middle
                    //Add rank point to all item above 
                    if (rankItems != null)
                    {
                        foreach (var item in rankItems)
                        {
                            item.RankPoints += 1;
                        }
                        _ = await App.Database.UpdateAllRankItemAsync(rankItems);
                    }

                    await Shell.Current.Navigation.PopAsync(true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert", "Enter name", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
