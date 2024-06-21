namespace Web.IServices
{
    public interface IVNPayService
    {
        string GenerateVnPayPaymentUrl(decimal priceTotalOrder, string OderCode, string OderInfo, HttpRequest httpRequest, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor);
    }
}
    