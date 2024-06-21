using Data.DTOs;
using Data.Helpers.Enums;
using Data.Responses;

namespace Web.IServices
{
    public interface IBillService
    {
        public Task<PopularResponse<string>> CreateOderAsync(CheckOutConfimDTO checkOutConfimDTO);
        public Task<PopularResponse<string>> CancelOrderAsync(int OderId);
        public Task<PopularResponse<string>> UpdateStateDeliveryBillAsync(string BillCode, Status.TrangThaiGiaoHang status);
        public Task<PopularResponse<string>> UpdateStatePayBillAsync(string BillCode, Status.TrangThaiThanhToan status);
    }
}
