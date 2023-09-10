using AutoMapper;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Domain;

namespace SecureId.AccessControl.API.AutoMapper
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Activity, ActivityDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
