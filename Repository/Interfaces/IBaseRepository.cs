namespace Codefast.Repository.Interfaces;

public interface IBaseRepository 
{
    public Task AddAsync<T>(T entity) where T : class;
    public Task UpdateAsync<T>(T entity) where T : class;
    public Task DeleteAsync<T>(T entity) where T : class;
}


