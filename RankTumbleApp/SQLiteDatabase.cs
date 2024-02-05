using SQLite;

namespace RankTumbleApp
{
    public class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public SQLiteDatabase()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            _database.CreateTableAsync<RankItem>();
            _database.CreateTableAsync<RankCategory>();
        }
        // Read Data
        public async Task<List<RankItem>> GetRankItemsAsync()
        {
            return await _database.Table<RankItem>().ToListAsync();
        }

        // Read particular data
        public async Task<RankItem> GetRankItemAsync(RankItem item)
        {
            return await _database.Table<RankItem>().Where(i => i.ID == item.ID).FirstOrDefaultAsync();
        }

        // Add data   
        public async Task<int> AddRankItemAsync(RankItem item)
        {
            return await _database.InsertAsync(item);
        }

        // Remove data
        public Task<int> DeleteRankItemAsync(RankItem item)
        {
            return _database.DeleteAsync(item);
        }

        // Update data
        public Task<int> AddUpdateRankItemAsync(RankItem item)
        {
            if (item.ID != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        // Update data
        public Task<int> UpdateAllRankItemAsync(List<RankItem> items)
        {
            return _database.UpdateAllAsync(items);
        }

        public Task<List<RankItem>> GetAllRankItems()
        {
            return _database.Table<RankItem>().ToListAsync();
        }

        /// <summary>
        /// Get all items with rank point greater
        /// </summary>
        /// <returns></returns>
        public Task<List<RankItem>> GetAllRankItemsByRankPoints(int rankPoints)
        {
            return _database.Table<RankItem>().Where(i => i.RankPoints >= rankPoints).ToListAsync();
        }

        /// <summary>
        /// Get the item above or below the current item, to exchange it
        /// </summary>
        /// <returns></returns>
        public Task<RankItem> GetRankItemToChange(int rankPoints, bool isUp)
        {
            if (isUp)
            {
                return _database.Table<RankItem>().Where(i => i.RankPoints > rankPoints)?.OrderBy(x => x.RankPoints).FirstAsync();
            }
            else
            {
                return _database.Table<RankItem>().Where(i => i.RankPoints < rankPoints)?.OrderByDescending(x => x.RankPoints).FirstAsync();
            }
        }

        //Read Item  
        public Task<RankItem> GetItemAsync(int id)
        {
            return _database.Table<RankItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get item by rank point if any other item exist
        /// </summary>
        /// <returns></returns>
        public Task<RankItem> GetItemByRankPointAsync(int rankPoints)
        {
            return _database.Table<RankItem>().Where(i => i.RankPoints == rankPoints).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get rank item by category
        /// </summary>
        /// <returns></returns>
        public Task<List<RankItem>> GetRankItemByCategoryAsync(string category)
        {
            return _database.Table<RankItem>().Where(i => i.Category == category).ToListAsync();
        }

        #region Rank category
        // Read Data
        public async Task<List<RankCategory>> GetRankCategoryItemsAsync()
        {
            return await _database.Table<RankCategory>().ToListAsync();
        }

        // Read particular data
        public async Task<RankCategory> GetRankCategoryItemAsync(RankCategory item)
        {
            return await _database.Table<RankCategory>().Where(i => i.ID == item.ID).FirstOrDefaultAsync();
        }

        // Add data   
        public async Task<int> AddRankCategoryItemAsync(RankCategory item)
        {
            return await _database.InsertAsync(item);
        }

        // Remove data
        public Task<int> DeleteRankCategoryItemAsync(RankCategory item)
        {
            return _database.DeleteAsync(item);
        }

        // Update data
        public Task<int> AddUpdateRankCategoryItemAsync(RankCategory item)
        {
            if (item.ID != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        public Task<List<RankCategory>> GetAllRankCategoryItems()
        {
            return _database.Table<RankCategory>().ToListAsync();
        }

        //Read Item  
        public Task<RankCategory> GetRankCategoryItemAsync(int id)
        {
            return _database.Table<RankCategory>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        #endregion
    }
}
