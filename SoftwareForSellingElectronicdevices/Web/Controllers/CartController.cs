using Data.DTOs;
using Data.Helpers.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Web.IServices;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBaseService<PaymentDTO, PaymentDTO> _paymentSer;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBillService _billService;
        private readonly IVNPayService _vpnService;

        public CartController(ICartServiceFactory cartServiceFactory, IBaseService<PaymentDTO, PaymentDTO> baseService, IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor contextAccessor, IBillService billService, IVNPayService vpnService)
        {
            _cartService = cartServiceFactory.CreateCartService();
            _paymentSer = baseService;
            _configuration = configuration;
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _billService = billService;
            _vpnService = vpnService;
        }

        public IActionResult Pay()
        {
            return View();
        }

        public async Task<IActionResult> CheckOut()
        {
            var cartLines = await _cartService.GetCartItems();
            if (!cartLines.Any())
            {
                return RedirectToAction("MyCart");
            }
            ViewBag.Cart = cartLines;
            ViewBag.Total = await _cartService.ComputeTotalValue();
            ViewBag.Payments = (await _paymentSer.GetEntitysAsync()).ObjectReturn;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(CheckOutConfimDTO checkOutConfimDTO)
        {
            checkOutConfimDTO.UserId = GetUserId();
            await SetCartLines(checkOutConfimDTO);
            var billCode = (await _billService.CreateOderAsync(checkOutConfimDTO)).Notification;

            if (checkOutConfimDTO.PaymentId == 2)
            {
                var priceTotal = (await GetPriceViewInCartVM(checkOutConfimDTO.WardCode, checkOutConfimDTO.DistrictId)).PriceTotal;
                var paymentUrl = _vpnService.GenerateVnPayPaymentUrl(priceTotal, billCode, $"{checkOutConfimDTO.Name} thanh toán", Request, _configuration, _contextAccessor);
                return Redirect(paymentUrl);
            }
            else
            {
                return Redirect($"/OrderReturn/{billCode}");
            }
        }

        private async Task SetCartLines(CheckOutConfimDTO checkOutConfimDTO)
        {
            checkOutConfimDTO.CartLines = await _cartService.GetCartItems();
        }

        [HttpGet]
        [Route("OrderReturn/{billCode?}")]
        public async Task<IActionResult> OrderReturn(string? billCode)
        {
            if (billCode == null)
            {
                var vnPay = new VnPayLibrary();
                var queryParams = HttpContext.Request.Query;

                foreach (var param in queryParams)
                {
                    vnPay.AddResponseData(param.Key, param.Value);
                }

                var vnp_HashSecret = _configuration["VnPay:HashSecret"];
                var vnp_SecureHash = vnPay.GetResponseData("vnp_SecureHash");

                if (vnPay.ValidateSignature(vnp_SecureHash, vnp_HashSecret))
                {
                    var billCodeget = vnPay.GetResponseData("vnp_TxnRef");
                    var responseCode = vnPay.GetResponseData("vnp_ResponseCode");

                    if (responseCode == "00")
                    {
                        await _billService.UpdateStatePayBillAsync(billCodeget, Status.TrangThaiThanhToan.DaThanhToan);
                        return View("PaymentSuccess", billCodeget);
                    }
                    else
                    {
                        return View("PaymentFailure", billCodeget);
                    }
                }
                else
                {
                    return View("PaymentError");
                }
            }
            else
            {
                return View("PaymentSuccess", billCode);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int quantity)
        {
            await _cartService.AddItem(productId, quantity);
            return RedirectToAction("MyCart");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _cartService.Clear();
            return RedirectToAction("MyCart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            await _cartService.RemoveLine(productId);
            return RedirectToAction("MyCart");
        }

        public async Task<IActionResult> MyCart()
        {
            ViewBag.SumTotal = await _cartService.ComputeTotalValue();
            var model = await _cartService.GetCartItems();
            return View(model);
        }


        private async Task<PriceViewInCartVM> GetPriceViewInCartVM(string selectedWardId, int selectedDistrictId)
        {
            var price = (decimal)(await _cartService.ComputeTotalValue());

            if (selectedDistrictId == 0 || selectedWardId == null)
            {
                return new PriceViewInCartVM()
                {
                    Price = price,
                    PriceTotal = price,
                    ShippingFee = 0,
                };
            }

            var shopID = _configuration["GHNSettings:ShopId"];

            var token = _configuration["GHNSettings:Token"];

            object requestData = await GetRequestData(selectedWardId, selectedDistrictId, price);

            HttpResponseMessage response = await SendRequest(shopID, token, requestData);

            response.EnsureSuccessStatusCode();

            dynamic result = await GetResult(response);
            var shippingFee = Convert.ToDecimal(result.data.total);
            var sum = price + shippingFee;
            return new PriceViewInCartVM()
            {
                Price = price,
                PriceTotal = sum,
                ShippingFee = shippingFee,
            };

            async Task<object> GetRequestData(string selectedWardId, int selectedDistrictId, decimal price)
            {
                return new
                {
                    to_ward_code = selectedWardId,
                    to_district_id = selectedDistrictId,
                    insurance_value = (int)price,
                    weight = (int)(await _cartService.GetCartItems()).Sum(x => x.Quantity * x.ProductDetailUserVM.Weight),
                    service_type_id = 2,
                    from_district_id = 3440
                };
            }

            async Task<HttpResponseMessage> SendRequest(string? shopID, string? token, object requestData)
            {
                return await _httpClient.SendAsync(new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee"),
                    Content = new StringContent(JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json"),
                    Headers = {
                    {"token", token },
                    {"shop_id", shopID }
                },
                });
            }

            static async Task<dynamic> GetResult(HttpResponseMessage response)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseData)!;
                return result;
            }
        }

        public async Task<IActionResult> GetPriceViewCart(string selectedWardId, int selectedDistrictId)
        {
            return PartialView("_PricePartialView", await GetPriceViewInCartVM(selectedWardId, selectedDistrictId));
        }

        private int? GetUserId()
        {
            return _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value != null
              ? Convert.ToInt32(_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
              : (int?)null;
        }

    }
}
