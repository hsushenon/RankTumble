using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;


namespace RankTumbleApp;

public partial class CategoryViewModel : ObservableObject
{
    public INavigation Navigation { get; set; }

    [ObservableProperty]
    ObservableCollection<RankCategory> rankCategoryList;

    [RelayCommand]
    async Task AddCategory()
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(AddUpdateCategoryPage));
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]
    void LoadCategory()
    {
        try
        {
            Load();
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]//TODO Issue is with MVVM call 
    public async Task EditCategory(int categoryId)
    {
        try
        {
             await Shell.Current.GoToAsync($"{nameof(AddUpdateCategoryPage)}?CategoryId={categoryId}");
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]//TODO Issue is with MVVM call 
    public Task DeleteCategory(int categoryId)
    {
        try
        {
            var r = App.Database.GetRankCategoryItemAsync(categoryId);
            if (r.Result != null)
            {
                _ = App.Database.DeleteRankCategoryItemAsync(r.Result);
            }
         
            Load();
        }
        catch (Exception)
        {
        }

        return Task.CompletedTask;
    }

    [RelayCommand]//TODO Issue is with MVVM call 
    public async Task GotoRankList(string categoryName)
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(RankItemListPage)}?CategoryName={categoryName}");
        }
        catch (Exception)
        {
        }
    }

    public CategoryViewModel(INavigation navigation)
    {
        try
        {
            Navigation = navigation;
            Load();
        }
        catch (Exception)
        {

        }
    }

    public void Load()
    {
        try
        {
            var data = App.Database.GetAllRankCategoryItems();
            if (data.Result != null)
            {
                RankCategoryList = new ObservableCollection<RankCategory>(data.Result);
            }
        }
        catch (Exception)
        {
        }
    }
}
