using NetflixRandomizer.Models;
using SQLite;

namespace NetflixRandomizer.Services.Base
{
    public interface ILiteDBService
    {
        Task CreateTableAsync<TEntity>() where TEntity : new();
        Task<int> AddUserAsync(User user);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<List<User>> GetAllUsersSqlAsync();
    }
    public class LiteDBService : ILiteDBService
    {
        private readonly SQLiteAsyncConnection _db;

        public LiteDBService()
        {
            _db = new SQLiteAsyncConnection(GlobalSettings.DatabasePath);
            //Metodo de creacion inicial de tablas
            _db.CreateTableAsync<User>().Wait();
        }

        public Task CreateTableAsync<TEntity>() where TEntity : new()
        {
            return _db.CreateTableAsync<TEntity>();
        }

        //Estos deberian ir separados en un servicio propio por VM, por tiempo lo hacemos aqui
        public Task<int> AddUserAsync(User user)
        {
            return _db.InsertAsync(user);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _db.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return _db.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<User>> GetAllUsersSqlAsync()
        {
            return _db.QueryAsync<User>("SELECT * FROM User");
        }
    }
}
