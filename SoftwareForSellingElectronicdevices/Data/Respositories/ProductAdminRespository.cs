using AutoMapper;
using Data.DTOs;
using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.Responses;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Respositories
{
    public class ProductAdminRespository(MyDBContext _dbContext, IMapper _mapper) : IProductsAdminRespository
    {
        public async Task<PopularResponse<string>> CreateAsync(ProductDTO productDTO)
        {
            try
            {
                if (await IsDuplicateProduct(_dbContext.Products, productDTO))
                {
                    return new PopularResponse<string>(false, "The product already exists", default(string));
                }
                Product product = await AddAndSaveProduct(_dbContext, _mapper, productDTO);
                await AddListPhoneSimType(_dbContext, productDTO, product);
                await AddListProductCategories(_dbContext, productDTO, product);
                if (productDTO.ListUrl != null && productDTO.indexMainImage != null)
                {

                    var listUrl = productDTO.ListUrl.Select(url => new ProductImage()
                    {
                        Url = url,
                        ProductId = product.Id,
                    }).ToList();
                    listUrl[Convert.ToInt16(productDTO.indexMainImage)].IsMainImage = true;
                    await _dbContext.ProductImages.AddRangeAsync(listUrl);

                }

                await Save(_dbContext);
                return new PopularResponse<string>(true, "Add success", default(string));

            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString(), default(string));
                throw;
            }

            static async Task<Product> AddAndSaveProduct(MyDBContext _dbContext, IMapper _mapper, ProductDTO productDTO)
            {
                var product = _mapper.Map<Product>(productDTO);
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }

            static async Task AddListPhoneSimType(MyDBContext _dbContext, ProductDTO productDTO, Product product)
            {
                var listPhoneSimType = productDTO.ListIdSim.Select(id => new PhoneSIMType()
                {
                    ProductId = product.Id,
                    SIMTypeID = id
                });

                await _dbContext.PhoneSIMTypes.AddRangeAsync(listPhoneSimType);
            }

            static async Task AddListProductCategories(MyDBContext _dbContext, ProductDTO productDTO, Product product)
            {
                var listCategoryProduct = new List<CategoryProduct>();

                foreach (var item in productDTO.ListIdCategory)
                {
                    listCategoryProduct.Add(new CategoryProduct()
                    {
                        CategoryId = item,
                        ProductId = product.Id,
                    });
                }
                await _dbContext.AddRangeAsync(listCategoryProduct);
            }

            static async Task Save(MyDBContext _dbContext)
            {
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<PopularResponse<string>> UpdateAsync(ProductDTO entityDTO)
        {
            try
            {
                if (await IsDuplicateProduct(_dbContext.Products.Where(p => p.Id != entityDTO.Id), entityDTO))
                {
                    return new PopularResponse<string>(false, "The product already exists", default(string));
                }
                Product product = await Edit(_dbContext, _mapper, entityDTO);
                await HanldePhoneSIMTypes(_dbContext, entityDTO, product);
                await HanldeCategoryProducts(_dbContext, entityDTO, product);
                await HanldeImages(_dbContext, entityDTO, product);
                await Save(_dbContext);
                return new PopularResponse<string>(true, "Edit success", default(string));
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString(), default(string));
            }

            static async Task Save(MyDBContext _dbContext)
            {
                await _dbContext.SaveChangesAsync();
            }

            static async Task<Product> Edit(MyDBContext _dbContext, IMapper _mapper, ProductDTO entityDTO)
            {
                var product = _mapper.Map<Product>(entityDTO);
                _dbContext.Products.Update(product);
                await Save(_dbContext);
                return product;
            }

            static async Task HanldePhoneSIMTypes(MyDBContext _dbContext, ProductDTO entityDTO, Product product)
            {
                _dbContext.PhoneSIMTypes.RemoveRange(_dbContext.PhoneSIMTypes.Where(i => i.ProductId == product.Id));
                await _dbContext.PhoneSIMTypes.AddRangeAsync(entityDTO.ListIdSim.Select(s => new PhoneSIMType()
                {
                    ProductId = product.Id,
                    SIMTypeID = s
                }));
            }

            static async Task HanldeCategoryProducts(MyDBContext _dbContext, ProductDTO entityDTO, Product product)
            {
                _dbContext.CategoryProducts.RemoveRange(_dbContext.CategoryProducts.Where(c => c.ProductId == product.Id));
                await _dbContext.CategoryProducts.AddRangeAsync(entityDTO.ListIdCategory.Select(c => new CategoryProduct()
                {
                    ProductId = product.Id,
                    CategoryId = c
                }));
            }

            static async Task HanldeImages(MyDBContext _dbContext, ProductDTO entityDTO, Product product)
            {
                if (entityDTO.ListUrl!.Any())
                {
                    _dbContext.ProductImages.RemoveRange(_dbContext.ProductImages.Where(i => i.ProductId == product.Id));

                    var images = entityDTO.ListUrl!.Select(i => new ProductImage()
                    {
                        ProductId = product.Id,
                        Url = i
                    }).ToList();
                    images[Convert.ToInt16(entityDTO.indexMainImage)].IsMainImage = true;

                    await _dbContext.ProductImages.AddRangeAsync(images);
                }

                if (!entityDTO.ListUrl!.Any())
                {
                    if (entityDTO.indexMainImage != null)
                    {
                        var images = _dbContext.ProductImages.Where(i => i.ProductId == product.Id).Select(i => new ProductImage()
                        {
                            Id = i.Id,
                            IsMainImage = false,
                            ProductId = product.Id,
                            Url = i.Url
                        }).ToList();

                        images[Convert.ToInt16(entityDTO.indexMainImage)].IsMainImage = true;

                        _dbContext.UpdateRange(images);
                    }
                }
            }
        }

        public Task<PopularResponse<string>> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PopularResponse<List<string>>> GetConfigurationBackCamerasAsync()
        {
            return new PopularResponse<List<string>>(true, "Get list name success", await _dbContext.Products.Select(x => x.BackCamera).Distinct().ToListAsync());
        }

        public async Task<PopularResponse<List<string>>> GetConfigurationFrontCamerasAsync()
        {
            return new PopularResponse<List<string>>(true, "Get list name success", (await _dbContext.Products.Select(x => x.FrontCamera).Distinct().ToListAsync())!);
        }

        public Task<PopularResponse<string>> GetEntityAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ProductAdminViewModel>> GetEntitysAsync(ParametersProductAdminDTO? parametersProduct)
        {
            var products = (from p in _dbContext.Products
                            join pi in _dbContext.ProductImages on new { Id = p.Id, IsMainImage = true } equals new { Id = pi.ProductId, IsMainImage = pi.IsMainImage }
                            join b in _dbContext.Brands on p.BrandId equals b.Id
                            join o in _dbContext.OperatingSystemProducts on p.OperatingSystemProductId equals o.Id
                            join ro in _dbContext.ROMMemories on p.ROMMemoryId equals ro.Id
                            join ra in _dbContext.RAMMemories on p.RAMMemoryId equals ra.Id
                            join ba in _dbContext.Batteries on p.BatteryId equals ba.Id
                            join chi in _dbContext.Chips on p.ChipId equals chi.Id
                            join sc in _dbContext.Screens on p.ScreenId equals sc.Id
                            orderby p.Id
                            select new ProductAdminViewModel
                            {
                                Id = p.Id,
                                Url = pi.Url,
                                Brand = b.BrandName,
                                OperatingSystem = o.NameOperatingSystem,
                                PhoneName = p.ProductName,
                                Status = p.Status == 1 ? "Active" : "No Active",
                                Ram = ra.NameRAMMemory,
                                Rom = ro.NameROMMemory,
                                Price = p.Price,
                                Chip = chi.NameChip,
                                Battery = ba.BatteryType + " " + ba.BatteryCapacity,
                                Screen = sc.NameScreen
                            });
            var totalRecords = await products.CountAsync();
            products = products
                .Skip(parametersProduct!.start)
                .Take(parametersProduct!.length);
            var objectJson = new ApiResponse<ProductAdminViewModel>
            {
                Draw = parametersProduct.draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = await products.ToListAsync()
            };

            return objectJson;
        }

        public async Task<PopularResponse<ProductDTO>> GetInforProductDTOAsync(int id)
        {
            var product = await _dbContext.Products
                               .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return new PopularResponse<ProductDTO>(false, "Product not found", null);
            }

            var images = await _dbContext.ProductImages
                                         .Where(im => im.ProductId == id)
                                         .ToListAsync();

            var categories = await _dbContext.CategoryProducts
                                             .Where(ca => ca.ProductId == id)
                                             .Select(ca => ca.CategoryId)
                                             .ToListAsync();

            var sims = await _dbContext.PhoneSIMTypes
                                       .Where(sim => sim.ProductId == id)
                                       .Select(sim => sim.SIMTypeID)
                                       .ToListAsync();

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                BackCamera = product.BackCamera,
                BatteryId = product.BatteryId,
                BrandId = product.BrandId,
                ChargingPortId = product.ChargingPortId,
                ChipId = product.ChipId,
                ColorMSId = product.ColorMSId,
                Description = product.Description,
                FrontCamera = product.FrontCamera,
                MemoryStickId = product.MemoryStickId,
                Quantity = product.Quantity,
                RAMMemoryId = product.RAMMemoryId,
                ROMMemoryId = product.ROMMemoryId,
                Weight = product.Weight,
                OperatingSystemProductId = product.OperatingSystemProductId,
                Price = product.Price,
                Status = product.Status,
                ScreenId = product.ScreenId,
                ProductName = product.ProductName,
                ListIdCategory = categories,
                ListIdSim = sims,
                ListUrl = images.Select(i => i.Url).ToList(),
                indexMainImage = images.FindIndex(x => x.IsMainImage)

            };

            return new PopularResponse<ProductDTO>(true, "Get success", productDTO);

        }

        public async Task<PopularResponse<List<string>>> GetNamesAsync()
        {
            return new PopularResponse<List<string>>(true, "Get list name success", await _dbContext.Products.Select(x => x.ProductName).Distinct().ToListAsync());
        }

        private async Task<bool> IsDuplicateProduct(IQueryable<Product> products, ProductDTO productDTO)
        {
            return await products.AnyAsync(p =>
                p.ProductName == productDTO.ProductName &&
                p.BrandId == productDTO.BrandId &&
                p.ScreenId == productDTO.ScreenId &&
                p.BatteryId == productDTO.BatteryId &&
                p.MemoryStickId == productDTO.MemoryStickId &&
                p.RAMMemoryId == productDTO.RAMMemoryId &&
                p.ROMMemoryId == productDTO.ROMMemoryId &&
                p.ColorMSId == productDTO.ColorMSId &&
                p.Weight == productDTO.Weight &&
                p.MemoryStickId == productDTO.MemoryStickId
            );
        }

        public async Task<bool> AddProductExcelAsync(List<ProductExcelDTO> productExcelDTPOs)
        {
            var listProductTasks = productExcelDTPOs.Select(async ped =>
            {
                var batteryParts = ped.Battery.Split(' ');
                if (batteryParts.Length < 2)
                {
                    throw new ArgumentException("Battery string is not in the correct format");
                }

                var batteryType = batteryParts[0];
                if (!int.TryParse(batteryParts[1], out int batteryCapacity))
                {
                    throw new ArgumentException("Battery capacity is not a valid integer");
                }

                var batteryId = await GetOrCreateEntityIdAsync<Battery>(
                    b => b.BatteryCapacity == batteryCapacity && b.BatteryType.ToUpper() == batteryType.ToUpper(),
                    () => new Battery
                    {
                        BatteryCapacity = batteryCapacity,
                        BatteryType = batteryType
                    });

                var brandId = await GetOrCreateEntityIdAsync<Brand>(i => i.BrandName.ToUpper() == ped.Brand.ToUpper(),
                    () => new Brand()
                    {
                        BrandName = ped.Brand
                    });

                var chargingPortId = await GetOrCreateEntityIdAsync<ChargingPort>(i => i.ChargingPortName.ToUpper() == ped.ChargingPort.ToUpper(),
                    () => new ChargingPort()
                    {
                        ChargingPortName = ped.ChargingPort
                    });

                // Đối với các đối tượng còn lại, thay thế đối tượng bằng id
                var chipId = await GetOrCreateEntityIdAsync<Chip>(i => i.NameChip.ToUpper() == ped.Chip.ToUpper(),
                    () => new Chip()
                    {
                        NameChip = ped.Chip
                    });

                var memoryStickId = await GetOrCreateEntityIdAsync<MemoryStick>(i => i.NameMemoryStick.ToUpper() == ped.MemoryStick.ToUpper(),
                    () => new MemoryStick()
                    {
                        NameMemoryStick = ped.MemoryStick
                    });

                var operatingSystemProductId = await GetOrCreateEntityIdAsync<OperatingSystemProduct>(i => i.NameOperatingSystem.ToUpper() == ped.OperatingSystemProduct.ToUpper(),
                    () => new OperatingSystemProduct()
                    {
                        NameOperatingSystem = ped.OperatingSystemProduct,
                    });

                var screenId = await GetOrCreateEntityIdAsync<Screen>(i => i.NameScreen.ToUpper() == ped.Screen.ToUpper(),
                    () => new Screen()
                    {
                        NameScreen = ped.Screen
                    });

                var colorId = await GetOrCreateEntityIdAsync<ColorMS>(i => i.NameColorMS.ToUpper() == ped.ColorMS.ToUpper(),
                    () => new ColorMS()
                    {
                        NameColorMS = ped.ColorMS
                    });

                var rAMMemoryId = await GetOrCreateEntityIdAsync<RAMMemory>(i => i.NameRAMMemory.ToUpper() == ped.RAMMemory.ToUpper(),
                    () => new RAMMemory()
                    {
                        NameRAMMemory = ped.RAMMemory
                    });

                var rOMMemoryId = await GetOrCreateEntityIdAsync<ROMMemory>(i => i.NameROMMemory.ToUpper() == ped.ROMMemory.ToUpper(),
                    () => new ROMMemory()
                    {
                        NameROMMemory = ped.ROMMemory
                    });

                var sims = new List<int>(); // Lưu trữ id của SIMType thay vì đối tượng
                foreach (var s in ped.ListSim)
                {
                    var simId = await GetOrCreateEntityIdAsync<SIMType>(i => i.Type.ToUpper() == s.ToUpper(),
                        () => new SIMType()
                        {
                            Type = s
                        });
                    sims.Add(simId);
                }

                var cates = new List<int>(); // Lưu trữ id của SIMType thay vì đối tượng
                foreach (var s in ped.ListCategory)
                {
                    var catId = await GetOrCreateEntityIdAsync<Category>(i => i.Name.ToUpper() == s.ToUpper(),
                        () => new Category()
                        {
                            Name = s
                        });
                    cates.Add(catId);
                }

                var lstImage = ped.ListUrl.Select(u => new ProductImage()
                {
                    Url = u,
                    IsMainImage = false
                }).ToList();

                lstImage[0].IsMainImage = true;

                return new Product()
                {
                    BackCamera = ped.BackCamera,
                    ProductName = ped.ProductName,
                    BatteryId = batteryId,
                    BrandId = brandId,
                    ChargingPortId = chargingPortId,
                    Weight = ped.Weight,
                    Status = ped.Status,
                    Quantity = ped.Quantity,
                    Price = ped.Price,
                    FrontCamera = ped.FrontCamera,
                    Description = ped.Description,
                    ChipId = chipId,
                    MemoryStickId = memoryStickId,
                    OperatingSystemProductId = operatingSystemProductId,
                    ScreenId = screenId,
                    ColorMSId = colorId,
                    RAMMemoryId = rAMMemoryId,
                    ROMMemoryId = rOMMemoryId,
                    PhoneSIMTypes = sims.Select(s => new PhoneSIMType()
                    {
                        SIMTypeID = s,
                    }).ToList(),
                    CategoryProducts = sims.Select(s => new CategoryProduct()
                    {
                        CategoryId = s,
                    }).ToList(),
                    Images = lstImage
                };
            });
            var listProducts = new List<Product>();
            foreach (var task in listProductTasks)
            {
                    var product = await task;
                    listProducts.Add(product);
            }
            _dbContext.AddRange(listProducts);
            var x = 10;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetOrCreateEntityIdAsync<T>(
     Expression<Func<T, bool>> predicate,
     Func<T> createEntityFunc) where T : class
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                // Nếu thực thể đã tồn tại, trả về id của thực thể
                var entityIdProperty = _dbContext.Entry(entity).Metadata.FindPrimaryKey().Properties.FirstOrDefault();
                return (int)entityIdProperty.GetGetter().GetClrValue(entity);
            }
            else
            {
                // Nếu thực thể không tồn tại, tạo mới và thêm vào cơ sở dữ liệu
                entity = createEntityFunc();
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync(); // Lưu thay đổi để có thể lấy được id

                // Trả về id của thực thể mới được tạo
                var entityIdProperty = _dbContext.Entry(entity).Metadata.FindPrimaryKey().Properties.FirstOrDefault();
                return (int)entityIdProperty.GetGetter().GetClrValue(entity);
            }
        }

    }
}
