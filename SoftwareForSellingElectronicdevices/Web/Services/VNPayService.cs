using Azure.Core;
using Web.IServices;
using Web.Models;

namespace Web.Services
{
    public class VNPayService : IVNPayService
    {
        public string GenerateVnPayPaymentUrl(decimal priceTotalOrder, string OderCode, string OderInfo, HttpRequest httpRequest, IConfiguration _configuration,IHttpContextAccessor httpContextAccessor)
        {
            string vnp_Returnurl = $"{httpRequest.Scheme}://{httpRequest.Host}/OrderReturn";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = _configuration["VnPay:TmnCode"]!;
            string vnp_HashSecret = _configuration["VnPay:HashSecret"]!;
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((double)(priceTotalOrder * 100)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContextAccessor));
            vnpay.AddRequestData("vnp_TxnRef", OderCode);
            if (!string.IsNullOrEmpty("vn"))
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", OderInfo);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }
    }
}
