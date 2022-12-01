

namespace DalApi;

/// <summary>
/// Interface of a dal class.
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface ICrud<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    int Add(T entity);
    void Update(T entity);
    void Delete(int id);

}
