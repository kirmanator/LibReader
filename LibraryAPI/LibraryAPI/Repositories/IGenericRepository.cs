namespace LibraryAPI.Repositories {
    public interface IGenericRepository<T> where T : class {
        Task<T?> GetByIdAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
