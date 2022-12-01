
namespace BlApi;

/// <summary>
/// BL interface
/// </summary>
public interface IBl
{
    public IProduct Product { get; }

    public IOrder Order { get; }

    public ICart Cart { get; }

}
