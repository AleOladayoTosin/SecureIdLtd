using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureId.Ecommerce.ShoppingCart.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _config;
        public ProductService(IBaseService baseService,
            ILogger<ProductService> logger, IConfiguration config)
        {
            _baseService = baseService;
            _logger = logger;
            _config = config;
        }

        public async Task<ResponseMessage> GetCoupon(string code, string accessToken)
        {
            _logger.LogInformation($"{"About to fetch coupon information" + " | " + code + " | "}{DateTime.Now}");

            var url = _config["ProductUrl"];
            var response = await _baseService.SendAsync<ResponseMessage>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = url + "api/code/" + code,
            });
            return response;
        }

        public async Task<ResponseMessage> GetProduct(Guid productId, string accessToken)
        {
            _logger.LogInformation($"{"About to fetch product information" + " | " + productId + " | "}{DateTime.Now}");

            var url = _config["ProductUrl"];
            var response = await _baseService.SendAsync<ResponseMessage>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = url + "api/Product/" + productId,
            });
            return response;
        }
    }
}
