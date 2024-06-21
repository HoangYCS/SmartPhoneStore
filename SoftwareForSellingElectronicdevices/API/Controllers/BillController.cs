using Data.DTOs;
using Data.Helpers.Enums;
using Data.IRespositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRespository _billRespository;

        public BillController(IBillRespository billRespository)
        {
            _billRespository = billRespository;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateBill(CheckOutConfimDTO checkOutConfimDTO)
        {
            return Ok(await _billRespository.CreateOderAsync(checkOutConfimDTO));
        }

        public class UpdateStateDeliveryBillDTO
        {
            public string BillCode { get; set; }
            public Status.TrangThaiGiaoHang Status { get; set; }
        }

        [HttpPut("update-state-delivery")]
        public async Task<IActionResult> UpdateStateDeliveryBillAsync(UpdateStateDeliveryBillDTO updateStateDeliveryBillDTO)
        {
            return Ok(await _billRespository.UpdateStateDeliveryBillAsync(updateStateDeliveryBillDTO.BillCode, updateStateDeliveryBillDTO.Status));
        }

        
        public class UpdateStatePayBillDTO
        {
            public string BillCode { get; set; }
            public Status.TrangThaiThanhToan Status { get; set; }
        }

        [HttpPut("update-state-pay")]
        public async Task<IActionResult> UpdateStatePayBillAsync(UpdateStatePayBillDTO payBillDTO)
        {
            return Ok(await _billRespository.UpdateStatePayBillAsync(payBillDTO.BillCode, payBillDTO.Status));
        }
    }
}
