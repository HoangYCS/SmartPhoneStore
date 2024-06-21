using System.Security.Claims;
using Web.IServices;

namespace Web.Services
{
    public class CartServiceFactory : ICartServiceFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductUserService _productUserService;
        private readonly HttpClient _httpClient;

        public CartServiceFactory(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider, HttpClient httpClient, IProductUserService productUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
            _productUserService = productUserService;
        }

        public ICartService CreateCartService()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext!.User.Identity!.IsAuthenticated)
            {
                int userId = int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                return new CartService(userId, _httpClient);
            }
            else
            {
                return new SessionCartService(_httpContextAccessor, _productUserService);
            }
        }
    }
}
