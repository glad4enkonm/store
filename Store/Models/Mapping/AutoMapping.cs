using AutoMapper;
using System.Collections.Generic;

namespace Store.Models
{
    public static class Mapping
    {
        static Mapping()
        {
            IMapper innerMapper = (
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<KeyValuePair<long, int>, Models.QuantityListInner>()
                        .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Key))
                        .ForMember(dst => dst.Quantity, opt => opt.MapFrom(src => src.Value)))
            ).CreateMapper();


            var configuration = new MapperConfiguration(conf =>
            {
                conf.CreateMap<Domain.Item, Models.Item>()
                    .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Id));

                conf.CreateMap<Domain.Item, Models.Item>()
                   .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Id));

                conf.CreateMap<Domain.Order, Models.Order>()
                    .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Items, opt =>
                         opt.MapFrom(src => innerMapper.Map<Models.QuantityList>(src.QuantityByItemId)));

            });
            
            Instance = configuration.CreateMapper();
        }

        public static IMapper Instance = null;
    }
}
