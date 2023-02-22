using DO;
namespace DalApi;

/// <summary>
/// Interface of a dal class.
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface ICrud<T> where T : struct
{
    /// <summary>
    /// Get all of orders. 
    /// </summary>
    /// <returns>Orders array.</returns>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>
    ///<exception cref="DalXmlFormatException">Thrown when xml format was invalid.</exception>
    IEnumerable<T?> GetAll(Func<T?, bool>? predicate = null);

    /// <summary>
    /// Get an item by condition.
    /// </summary>
    /// <param name="predicate">Condition function.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when condition is null</exception>
    /// <exception cref="NotFoundException">Thrown when item cant be found.</exception>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>
    ///<exception cref="DalXmlFormatException">Thrown when xml format was invalid.</exception>
    T? getByCondition(Func<T?, bool>? predicate);

    /// <summary>
    /// Get an item by its id. 
    /// </summary>
    /// <param name="id">Id of desired item.</param>
    /// <returns>The desired item.</returns>
    /// <exception cref="NotFoundException">Thrown when the item cant be found.</exception>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>235
    ///<exception cref="DalXmlFormatException">Thrown when xml format was invalid.</exception>
    T GetById(int id);

    /// <summary>
    /// Add an item to DB.
    /// </summary>
    /// <param name="entity">Entity object to be added.</param>
    /// <returns>New id.</returns>
    /// <exception cref="AlreadyExistException">Thrown when new entity was already exist.</exception>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>
    ///<exception cref="DalXmlFormatException">Thrown when xml format was invalid.</exception>
    int Add(T entity);

    /// <summary>
    /// Update an item.
    /// </summary>
    /// <param name="o">Updated item.</param>
    /// <exception cref="NotFoundException">Thrown when item cant be found.</exception>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>
    ///<exception cref="DalXmlFormatException">Thrown when xml format was invalid.</exception>
    void Update(T entity);

    /// <summary>
    /// Delete an item by its id.
    /// </summary>
    /// <param name="id">Id of item to be deleted</param>
    /// <exception cref="NotFoundException">Thrown when item cannot be found</exception>
    /// <exception cref="XMLFileLoadCreateException">Thrown when xml file cannot be loaded</exception>
    void Delete(int id);

}
