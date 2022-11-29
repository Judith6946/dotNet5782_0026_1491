

using System.Runtime.CompilerServices;
using AutoMapper;

namespace BlImplementation;

static internal class Mappers
{

    public static MapperConfiguration config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<DO.Product, BO.Product>().ForMember(dest => dest.Category, opts => opts.MapFrom(src => enumParser(src.Category)));
    });
    public static IMapper iMapper=config.CreateMapper();


    private static BO.Enums.Category enumParser(DO.Enums.Category c)
    {
        return (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), c.ToString());
    }
    
}
