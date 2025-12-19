namespace Helper;

public interface IGeneralServices<T>
{
    Task<GeneralResponse> Add(T item);
    Task<GeneralResponse> Edit(T item);
    Task<GeneralResponse> Delete(string id);
    Task<GeneralResponse> Delete(T entity);
    Task Save();
    Task<T> GetById(string id);
    Task<List<T>> GetAll();
    IQueryable<T> GetQuery();
}
