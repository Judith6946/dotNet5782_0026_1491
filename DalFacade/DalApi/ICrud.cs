

namespace DalApi;

public interface ICrud<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    int Add(T entity);
    void Update(T entity);
    void Delete(int id);

}
