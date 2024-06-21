using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Respositories
{
    public class ProductUserRespository(MyDBContext _dbContext) : IProductUserRespository
    {
        public async Task<ProductDetailUserVM> GetProductDetailUserAsync(int idProduct)
        {
            var product = await (from p in _dbContext.Products
                                 where p.Id == idProduct
                                 join b in _dbContext.Brands on p.BrandId equals b.Id
                                 join ra in _dbContext.RAMMemories on p.RAMMemoryId equals ra.Id
                                 join sc in _dbContext.Screens on p.ScreenId equals sc.Id
                                 join co in _dbContext.Colors on p.ColorMSId equals co.Id
                                 join ba in _dbContext.Batteries on p.BatteryId equals ba.Id
                                 join cha in _dbContext.ChargingPorts on p.ChargingPortId equals cha.Id
                                 join chi in _dbContext.Chips on p.ChipId equals chi.Id
                                 join memS in _dbContext.MemorySticks on p.MemoryStickId equals memS.Id
                                 join rom in _dbContext.ROMMemories on p.RAMMemoryId equals rom.Id
                                 join ope in _dbContext.OperatingSystemProducts on p.OperatingSystemProductId equals ope.Id
                                 select new ProductDetailUserVM()
                                 {
                                     Id = idProduct,
                                     Urls = (from anh in _dbContext.ProductImages
                                             orderby anh.IsMainImage descending
                                             where anh.ProductId == idProduct
                                             select anh.Url).ToList(),
                                     Name = p.ProductName,
                                     Price = p.Price,
                                     Brand = b.BrandName,
                                     Ram = ra.NameRAMMemory,
                                     Screen = sc.NameScreen,
                                     Battery = ba.BatteryType + " " + ba.BatteryCapacity,
                                     ChargingPort = cha.ChargingPortName,
                                     Chip = chi.NameChip,
                                     Description = p.Description,
                                     MemoryStick = memS.NameMemoryStick,
                                     Rom = rom.NameROMMemory,
                                     OperatingSystem = ope.NameOperatingSystem,
                                     Tags = string.Join(", ", from cap in _dbContext.CategoryProducts
                                                              where cap.ProductId == p.Id
                                                              join ca in _dbContext.Categories on cap.CategoryId equals ca.Id
                                                              select ca.Name),
                                     Sims = string.Join(", ", from ps in _dbContext.PhoneSIMTypes
                                                              where ps.ProductId == p.Id
                                                              join si in _dbContext.SIMTypes on ps.SIMTypeID equals si.Id
                                                              select si.Type),
                                     FrontCamera = p.FrontCamera ?? "Chưa có thông tin",
                                     RearCamera = p.BackCamera,
                                     Weight = p.Weight

                                 }).FirstOrDefaultAsync();
            return product!;
        }

        public async Task<PageBasic<ProductUserViewModel>> GetProductsDisplayForUserAsync(List<int>? brands, List<int>? colors, int currentPage)
        {
            var products = (from p in _dbContext.Products
                            join pi in _dbContext.ProductImages on new { Id = p.Id, IsMainImage = true } equals new { Id = pi.ProductId, IsMainImage = pi.IsMainImage }
                            join b in _dbContext.Brands on p.BrandId equals b.Id
                            join ra in _dbContext.RAMMemories on p.RAMMemoryId equals ra.Id
                            join sc in _dbContext.Screens on p.ScreenId equals sc.Id
                            join co in _dbContext.Colors on p.ColorMSId equals co.Id
                            where (brands == null || brands.Contains(p.BrandId)) && (colors == null || colors.Contains(p.ColorMSId))
                            select new ProductUserViewModel
                            {
                                Id = p.Id,
                                Url = pi.Url,
                                Brand = b.BrandName,
                                Name = p.ProductName,
                                Ram = ra.NameRAMMemory,
                                Price = p.Price,
                                Screen = sc.NameScreen,
                                Category = string.Join(" ", from cap in _dbContext.CategoryProducts
                                                            where cap.ProductId == p.Id
                                                            join ca in _dbContext.Categories on cap.CategoryId equals ca.Id
                                                            select ca.Name),
                                Color = co.NameColorMS.ToString()
                            });
            return new PageBasic<ProductUserViewModel>()
            {
                Info = new PageInfo()
                {
                    CurrentPage = currentPage,
                    NumberItemInThePage = 8,
                    TotalNumber = await products.CountAsync()
                },
                ListItem = await products.Skip((currentPage - 1) * 8).Take(8).ToListAsync()
           };
        }
    }
}
