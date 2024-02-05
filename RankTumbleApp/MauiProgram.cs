using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace RankTumbleApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("IcoMoon-Ultimate", "IcoMoon-Ultimate");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.UseMauiApp<App>().UseMauiCommunityToolkit();
            builder.Services.AddTransient<CategoryListPage, CategoryViewModel>();
            builder.Services.AddTransient<AddUpdateCategoryPage, AddUpdateCategoryViewModel>();
            builder.Services.AddTransient<RankItemListPage, RankItemViewModel>();
            builder.Services.AddTransient<AddUpdateRankItemPage, AddUpdateRankItemViewModel>();
            return builder.Build();
        }
    }
}