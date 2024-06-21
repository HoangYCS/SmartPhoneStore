using Data.DTOs;
using Data.Helpers.Enums;
using Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRespositories
{
    public interface IBillRespository
    {
        public Task<PopularResponse<string>> CreateOderAsync(CheckOutConfimDTO checkOutConfimDTO);
        public Task<PopularResponse<string>> CancelOrderAsync(int OderId);
        public Task<PopularResponse<string>> UpdateStateDeliveryBillAsync(string BillCode, Status.TrangThaiGiaoHang status);
        public Task<PopularResponse<string>> UpdateStatePayBillAsync(string BillCode, Status.TrangThaiThanhToan status);
    }
}
