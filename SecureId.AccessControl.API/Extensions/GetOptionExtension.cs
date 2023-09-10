using Microsoft.Extensions.Configuration;

namespace SecureId.AccessControl.API.Extensions
{
    public static class GetOptionExtension
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}
