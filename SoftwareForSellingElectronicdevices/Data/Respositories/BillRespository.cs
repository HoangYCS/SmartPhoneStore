using Data.DTOs;
using Data.Helpers.Enums;
using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class BillRespository : IBillRespository
    {
        private readonly MyDBContext _dbContext;

        public BillRespository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PopularResponse<string>> CancelOrderAsync(int OderId)
        {
            throw new NotImplementedException();
        }

        public async Task<PopularResponse<string>> CreateOderAsync(CheckOutConfimDTO checkOutConfimDTO)
        {
            try
            {
                Bill bill = GetBill(checkOutConfimDTO);
                await _dbContext.Bills.AddAsync(bill);
                await _dbContext.SaveChangesAsync();
                return new PopularResponse<string>(true, bill.CodeBill);
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(true, ex.ToString());
            }

            static Bill GetBill(CheckOutConfimDTO checkOutConfimDTO)
            {
                var bill = new Bill()
                {
                    Description = checkOutConfimDTO.Note,
                    CodeBill = "HD" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    DateCreated = DateTime.Now,
                    DeliveryAddress = checkOutConfimDTO.SpecificAddress,
                    TrangThaiGiaoHang = (int)Status.TrangThaiGiaoHang.ChoXacNhan,
                    UserId = checkOutConfimDTO.UserId ?? 2,
                    ReasonCancellation = null,
                    TrangThaiThanhToan = (int)Status.TrangThaiThanhToan.ChuaThanhToan,
                    PaymentDetails = new List<PaymentDetail>()
                {
                    new PaymentDetail()
                    {
                        PaymentID = checkOutConfimDTO.PaymentId
                    }
                },
                    BillDetails = checkOutConfimDTO.CartLines.Select(i => new BillDetail()
                    {
                        Price = i.ProductDetailUserVM.Price,
                        ProductId = i.CartLineID,
                        Quantity = i.Quantity,
                    }).ToList()
                };
                return bill;
            }
        }

        public async Task<PopularResponse<string>> UpdateStateDeliveryBillAsync(string BillCode, Status.TrangThaiGiaoHang status)
        {
            try
            {
                var bill = (await _dbContext.Bills.FirstOrDefaultAsync(b => b.CodeBill == BillCode));
                if (bill == null)
                {
                    return new PopularResponse<string>(false, "Bill not found.");
                }
                bill!.TrangThaiGiaoHang = (int)status;
                _dbContext.Bills.Update(bill);
                await _dbContext.SaveChangesAsync();
                return new PopularResponse<string>(true, "Update success");
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString());
            }
        }

        public async Task<PopularResponse<string>> UpdateStatePayBillAsync(string BillCode, Status.TrangThaiThanhToan status)
        {
            try
            {
                var bill = (await _dbContext.Bills.FirstOrDefaultAsync(b => b.CodeBill == BillCode));

                if (bill == null)
                {
                    return new PopularResponse<string>(false, "Bill not found.");
                }

                bill!.TrangThaiThanhToan = (int)status;

                if (status == Status.TrangThaiThanhToan.DaThanhToan)
                {
                    bill.DateOfPayment = DateTime.Now;
                }
                _dbContext.Bills.Update(bill);
                await _dbContext.SaveChangesAsync();
                return new PopularResponse<string>(true, "Update success");
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString());
            }
        }
    }
}
