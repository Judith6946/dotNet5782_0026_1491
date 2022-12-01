

using System.Runtime.CompilerServices;
using AutoMapper;

namespace BlImplementation;

 public class MappinProfile : Profile
{
    public MappinProfile()
    {
        CreateMap<DO.Product, BO.Product>();



    }
   
    
}
