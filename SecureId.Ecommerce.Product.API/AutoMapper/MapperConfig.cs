using AutoMapper;
using SecureId.Ecommerce.Product.Application.DTOs;

namespace SecureId.Ecommerce.Product.API.AutoMapper
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<SecureId.Ecommerce.Product.Domain.Product, ProductDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
