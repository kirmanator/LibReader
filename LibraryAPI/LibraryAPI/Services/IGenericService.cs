namespace LibraryAPI.Services {
    public interface IGenericService<T> {
        Task<T?> GetById(int id);
        Task<bool> ExistsById(int id);
        Task<List<T>> GetAll();
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
