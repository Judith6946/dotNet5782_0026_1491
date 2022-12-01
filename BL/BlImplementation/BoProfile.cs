
using AutoMapper;

namespace BlImplementation;

/// <summary>
/// Mapper profile
/// </summary>
internal class BoProfile:Profile
{
    public BoProfile()
    {

        #region Product Mappers
        
        CreateMap<DO.Product, BO.ProductForList>().ForMember(dest => dest.Category, opts => opts.MapFrom(src => enumParser<BO.Enums.Category, DO.Enums.Category>(src.Category)));
        
        CreateMap<BO.Product, DO.Product>().ForMember(dest => dest.Category, opts => opts.MapFrom(src => enumParser<DO.Enums.Category,BO.Enums.Category>(src.Category)));
        
        CreateMap<DO.Product, BO.Product>().ForMember(dest => dest.Category, opts => opts.MapFrom(src => enumParser<BO.Enums.Category, DO.Enums.Category>(src.Category)));
        
        CreateMap<DO.Product, BO.ProductItem>().ForMember(dest => dest.Category, opts => opts.MapFrom(src => enumParser<BO.Enums.Category, DO.Enums.Category>(src.Category)))
            .ForMember(dest=>dest.Available,opts=>opts.MapFrom(src=>src.InStock>0));

        #endregion


        #region Cart Mappers

        CreateMap<DO.Product, BO.OrderItem>().ForMember(dest => dest.Amount, opts => opts.MapFrom(src => 1))
            .ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.ID))
            .ForMember(dest => dest.ID, opts => opts.MapFrom(src => 0));

        CreateMap<BO.Cart, DO.Order>().ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.DeliveryDate, opts => opts.MapFrom(src => DateTime.MinValue))
            .ForMember(dest => dest.ShipDate, opts => opts.MapFrom(src => DateTime.MinValue));
       
        CreateMap<BO.OrderItem, DO.OrderItem>();

        #endregion


        #region Order Mappers

        CreateMap<DO.Order, BO.OrderTracking>().ForMember(dest => dest.OrderId, opts => opts.MapFrom(src => src.ID))
            .ForMember(dest => dest.StatusOrder, opts => opts.MapFrom(src => getStatus(src)))
            .ForMember(dest => dest.Tracking, opts => opts.MapFrom(src => new List<Tuple<DateTime, string>>()));

        CreateMap<DO.Order, BO.Order>().ForMember(dest => dest.Status, opts => opts.MapFrom(src => getStatus(src)))
            .ForMember(dest => dest.ItemsList, opts => opts.MapFrom(src => new List<BO.OrderItem>()));

        CreateMap<DO.OrderItem, BO.OrderItem>().ForMember(dest => dest.TotalPrice, opts => opts.MapFrom(src => src.Amount*src.Price));
        CreateMap<DO.Order, BO.OrderForList>().ForMember(dest => dest.StatusOrder, opts => opts.MapFrom(src => getStatus(src)));



        #endregion

    }


    /// <summary>
    /// Convert one type of enum to another (by its text)
    /// </summary>
    /// <typeparam name="T">Enum type of destination.</typeparam>
    /// <typeparam name="K">Enum type of source.</typeparam>
    /// <param name="c">Source enum</param>
    /// <returns>Enum c, coverted to type T.</returns>
    private static T enumParser<T,K>(K c)=> (T)Enum.Parse(typeof(K), c.ToString());

    /// <summary>
    /// Get a status enum of an order object.
    /// </summary>
    /// <param name="order">Order object</param>
    /// <returns>Status of order.</returns>
    private static BO.Enums.OrderStatus getStatus(DO.Order order) => 
        order.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.delivered : 
        order.ShipDate != DateTime.MinValue ? BO.Enums.OrderStatus.sent : 
        BO.Enums.OrderStatus.approved;

}
