

namespace DalApi;

/// <summary>
/// Interface of a dal class.
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface ICrud<T> where T : struct
{
    IEnumerable<T?> GetAll(Func<T?, bool>? predicate = null);
    T? getByCondition(Func<T?, bool>? predicate);
    T GetById(int id);
    int Add(T entity);
    void Update(T entity);
    void Delete(int id);

}
