namespace RankTumbleApp
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public AppShell()
        {
            InitializeComponent();
            //Register Routes
            Routes.Add(nameof(AddUpdateCategoryPage), typeof(AddUpdateCategoryPage));
            Routes.Add(nameof(RankItemListPage), typeof(RankItemListPage));
            Routes.Add(nameof(AddUpdateRankItemPage), typeof(AddUpdateRankItemPage));
            foreach (var route in Routes)
            {
                Routing.RegisterRoute(route.Key, route.Value);
            }
        }
    }
}