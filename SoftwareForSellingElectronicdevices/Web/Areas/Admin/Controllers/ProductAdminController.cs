using Data.DTOs;
using Data.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using OfficeOpenXml;
using System.Net.Http;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductAdminController(IProductsAdminService productsAdminService, IWebHostEnvironment webHostEnvironment) : Controller
    {

        
        public IActionResult Management()
        {
            return View();
        }

        public async Task<IActionResult> GetNames()
        {
            return Ok((await productsAdminService.GetNamesAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetConfigsFrontCamera()
        {
            return Ok((await productsAdminService.GetConfigurationFrontCamerasAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetConfigsBackCamera()
        {
            return Ok((await productsAdminService.GetConfigurationBackCamerasAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts([FromBody] ParametersProductAdminDTO? parameters)
        {
            return Ok((await productsAdminService.GetEntitysAsync(parameters)));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO productDTO)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Ok(new PopularResponse<string>(false, string.Join(",", errors), default(string)));
            }

            var lstFile = HttpContext.Request.Form.Files;

            await CoppyFileToFoder(webHostEnvironment, productDTO, lstFile);

            return Ok(await productsAdminService.CreateAsync(productDTO));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Ok(new PopularResponse<string>(false, string.Join(",", errors), default(string)));
            }
            var lstFile = HttpContext.Request.Form.Files;
            
            if (lstFile.Any()) {
                await CoppyFileToFoder(webHostEnvironment, productDTO, lstFile);
            }
            return Ok(await productsAdminService.UpdateAsync(productDTO));
        }

        public IActionResult GetComponentProductManagement(int? id)
        {
            return ViewComponent("ProductManagement", id);
        }

        [HttpPost]
        public async Task<ActionResult> ImportProducts(IFormFile file)
        {
            var listProductExcel = new List<ProductExcelDTO>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = file.OpenReadStream())
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    bool isRowEmpty = true;
                    Console.WriteLine(row);
                    Console.WriteLine(rowCount);
                    for (int col = 1; col <= colCount; col++)
                    {
                        Console.WriteLine(row.ToString() + "-" + worksheet.Cells[row, col].Text + "-");
                        if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, col].Text))
                        {
                            isRowEmpty = false;
                            break;
                        }
                    }

                    if (isRowEmpty)
                    {
                        break;
                    }

                    listProductExcel.Add(new ProductExcelDTO()
                    {
                        ProductName = worksheet.Cells[row, 1].GetCellValue<string>(),
                        Brand = worksheet.Cells[row, 2].GetCellValue<string>(),
                        Status = worksheet.Cells[row, 3].GetCellValue<int>(),
                        ListCategory = worksheet.Cells[row, 4].Text.Split(',').ToList(),
                        Weight = worksheet.Cells[row, 5].GetCellValue<decimal>(),
                        Price = worksheet.Cells[row, 6].GetCellValue<decimal>(),
                        Quantity = worksheet.Cells[row, 7].GetCellValue<int>(),
                        BackCamera = worksheet.Cells[row, 8].GetCellValue<string>(),
                        FrontCamera = worksheet.Cells[row, 9].GetCellValue<string>(),
                        Chip = worksheet.Cells[row, 10].GetCellValue<string>(),
                        Battery = worksheet.Cells[row, 11].GetCellValue<string>(),
                        ROMMemory = worksheet.Cells[row, 12].GetCellValue<string>(),
                        RAMMemory = worksheet.Cells[row, 13].GetCellValue<string>(),
                        Screen = worksheet.Cells[row, 14].GetCellValue<string>(),
                        ListSim = worksheet.Cells[row, 15].Text.Split(',').ToList(),
                        ColorMS = worksheet.Cells[row, 16].GetCellValue<string>(),
                        ChargingPort = worksheet.Cells[row, 17].GetCellValue<string>(),
                        MemoryStick = worksheet.Cells[row, 18].GetCellValue<string>(),
                        OperatingSystemProduct = worksheet.Cells[row, 19].GetCellValue<string>(),
                        Description = worksheet.Cells[row, 20].GetCellValue<string>(),
                        ListUrl = worksheet.Cells[row, 21].Text.Split(',').ToList(),
                    });
                }
            }
            var requestExcel = new RequestExcel()
            {
                ProductExcels = listProductExcel,
            };
            return Ok(await productsAdminService.AddProductExcelAsync(requestExcel));
        }

            private static async Task CoppyFileToFoder(IWebHostEnvironment webHostEnvironment, ProductDTO productDTO, IFormFileCollection lstFile)
        {
            var uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "ImageProduct");

            if (!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);
            }

            foreach (var file in lstFile)
            {
                var fileName = file!.FileName;

                var filePath = Path.Combine(uploadfolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                productDTO.ListUrl!.Add(fileName);
            }
        }
    }
}
