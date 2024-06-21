using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helpers.Enums
{
    public class Status
    {
        public enum TrangThaiGiaoHang
        {
            [Description("Tại quầy")]
            TaiQuay = 0,
            [Description("Chờ xác nhận")]
            ChoXacNhan = 1,
            [Description("Chờ lấy hàng")]
            ChoLayHang = 2,
            [Description("Đang giao")]
            DangGiao = 3,
            [Description("Đã giao")]
            DaGiao = 4,
            [Description("Đã huỷ")]
            DaHuy = 5,
            [Description("Trả hàng")]
            TraHang = 6,
        }

        public enum TrangThaiThanhToan 
        {
            [Description("Chưa thanh toán")]
            ChuaThanhToan = 0,
            [Description("Đã thanh toán")]
            DaThanhToan = 1,
        }

    }
}
