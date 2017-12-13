using AutoMapper;
using System.Collections.Generic;

namespace Store.Model
{
    public static class Mapping
    {
        static Mapping()
        {
            IMapper innerMapper = (
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<KeyValuePair<long, int>, Transport.QuantityListInner>()
                        .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Key))
                        .ForMember(dst => dst.Quantity, opt => opt.MapFrom(src => src.Value)))
            ).CreateMapper();


            var configuration = new MapperConfiguration(conf =>
            {
                conf.CreateMap<Business.Item, Transport.Item>()
                    .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Id));

                conf.CreateMap<Transport.Item, Business.Item>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ItemId));

                conf.CreateMap<Business.Item, Transport.Item>()
                   .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Id));

                conf.CreateMap<Business.Order, Transport.Order>()
                    .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Items, opt =>
                         opt.MapFrom(src => innerMapper.Map<Transport.QuantityList>(src.QuantityByItemId)));

            });
            
            Instance = configuration.CreateMapper();
        }

        public static IMapper Instance = null;
    }
}
