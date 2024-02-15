using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace RankTumbleApp;
////For uri based navigation
[QueryProperty(nameof(CategoryName), "CategoryName")]
[QueryProperty(nameof(CategoryId), "CategoryId")]
public partial class RankItemViewModel : ObservableObject
{
    public INavigation Navigation { get; set; }

    [ObservableProperty]
    ObservableCollection<RankItemData> rankItemList;

    private string categoryName;
    public string CategoryName
    {
        get => categoryName;
        set
        {
            SetProperty(ref categoryName, value);
        }
    }

    private string categoryId;
    public string CategoryId
    {
        get => categoryId;
        set
        {
            SetProperty(ref categoryId, value);
            if (!string.IsNullOrEmpty(categoryId))
            {
                Load();
            }
        }
    }

    private string editModeIcon = IcoFont.Pencil5;
    public string EditModeIcon
    {
        get => editModeIcon;
        set
        {
            SetProperty(ref editModeIcon, value);
        }
    }

    [RelayCommand]
    async Task AddRankItem()
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(AddUpdateRankItemPage)}?CategoryName={categoryName}&CategoryId={categoryId}");
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]//TODO Issue is with MVVM call 
    public async Task EditRankItem(int rankItemId)
    {
        try
        {
              await Shell.Current.GoToAsync($"{nameof(AddUpdateRankItemPage)}?RankItemId={rankItemId}&CategoryName={categoryName}");
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]//TODO Issue is with MVVM call 
    public Task DeleteRankItem(int rankItemId)
    {
        try
        {
            var r = App.Database.GetItemAsync(rankItemId);
            if (r.Result != null)
            {
                _ = App.Database.DeleteRankItemAsync(r.Result);
            }

            Load();
        }
        catch (Exception)
        {
        }

        return Task.CompletedTask;
    }

    private bool _isEditMode = false;

    [RelayCommand]
    public void UpdateEditMode()
    {
        try
        {
            _isEditMode = !_isEditMode;

            if (RankItemList != null)
            {
                foreach (var item in RankItemList)
                {
                    item.IsEditMode = _isEditMode;
                }
                RankItemList = new ObservableCollection<RankItemData>(RankItemList);
            }

            if (_isEditMode)
            {
                EditModeIcon = IcoFont.Eye;
            }
            else
            {
                EditModeIcon = IcoFont.Pencil5;
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// Exchange the item with the one above or below
    /// </summary>
    /// <param name="rankItemId"></param>
    /// <param name="isUp"></param>
    /// <returns></returns>
    public async Task UpdateRankItems(int rankItemId, bool isUp)
    {
        try
        {
            var r1 = await App.Database.GetItemAsync(rankItemId);
            if (r1 != null)
            {
                var r2 = await App.Database.GetRankItemToChange(r1.RankPoints, isUp);
                if (r2 != null)
                {
                    int rankPointR1 = r1.RankPoints;
                    r1.RankPoints = r2.RankPoints;
                    r2.RankPoints = rankPointR1;

                    _ = await App.Database.AddUpdateRankItemAsync(r1);
                    _ = await App.Database.AddUpdateRankItemAsync(r2);

                    Load();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public async Task UpdateRankPoints(int rankItemId, int pointsToAdd)
    {
        try
        {
            var r = await App.Database.GetItemAsync(rankItemId);
            if (r != null)
            {
                r.RankPoints += pointsToAdd;
                _ = await App.Database.AddUpdateRankItemAsync(r);
            }
            Load();
        }
        catch (Exception)
        {
        }
    }
    public RankItemViewModel(INavigation navigation)
    {
        try
        {
            Navigation = navigation;
        }
        catch (Exception)
        {

        }
    }

    public void Load()
    {
        try
        {
            var data = App.Database.GetRankItemByCategoryAsync(CategoryId); 
            if (data.Result != null)
            {
                RankItemList = GetRankList(data.Result);
            }
        }
        catch (Exception)
        {
        }
    }

    private ObservableCollection<RankItemData> GetRankList(List<RankItem> unRankItems)
    {
        ObservableCollection < RankItemData > rankList = new ObservableCollection<RankItemData>();
        try
        {
            var result = unRankItems.OrderByDescending(x=>x.RankPoints).ToList().GroupBy(x=>x.RankPoints);
            int rank = 1;
            foreach (var item in result)
            {
                foreach (var rankItem in item)
                {
                    RankItemData rankItemData = new RankItemData();
                    rankItemData.RankPoints = rankItem.RankPoints;
                    rankItemData.Rank = rank;
                    rankItemData.Category = rankItem.Category;
                    rankItemData.Description = rankItem.Description;
                    rankItemData.ID = rankItem.ID;
                    rankItemData.Name = rankItem.Name;
                    rankItemData.IsEditMode = _isEditMode;
                    rankList.Add(rankItemData);
                }
                rank++;
            }
        }
        catch (Exception)
        {
        }
        return rankList;
    }
}
