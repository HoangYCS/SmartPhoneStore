using API.Helpers;
using Data.DTOs;
using Data.Helpers;
using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));

builder.Services.AddScoped<IBaseRespository<BrandDTO,BrandDTO>,BrandRespository<Brand,BrandDTO,BrandDTO>>();
builder.Services.AddScoped<IBaseRespository<ROMMemoryDTO,ROMMemoryDTO>,ROMMemoryRespository<ROMMemory,ROMMemoryDTO,ROMMemoryDTO>>();
builder.Services.AddScoped<IBaseRespository<RamMemoryDTO, RamMemoryDTO>, RamMemoryRespository<RAMMemory, RamMemoryDTO, RamMemoryDTO>>();
builder.Services.AddScoped<IBaseRespository<ColorDTO, ColorDTO>, ColorRespository<ColorMS, ColorDTO, ColorDTO>>();
builder.Services.AddScoped<IProductsAdminRespository, ProductAdminRespository>();
builder.Services.AddScoped<IProductUserRespository, ProductUserRespository>();
builder.Services.AddScoped<ICartRespository, CartRespository>();
builder.Services.AddScoped<IBillRespository, BillRespository>();
builder.Services.AddScoped<IBaseRespository<ChargingPortDTO, ChargingPortDTO>,BrandRespository<ChargingPort, ChargingPortDTO, ChargingPortDTO>>();
builder.Services.AddScoped<IBaseRespository<ChipDTO,ChipDTO>,BrandRespository<Chip,ChipDTO,ChipDTO>>();
builder.Services.AddScoped<IBaseRespository<ColorDTO,ColorDTO>,BrandRespository<ColorMS,ColorDTO,ColorDTO>>();
builder.Services.AddScoped<IBaseRespository<SIMTypeDTO, SIMTypeDTO>, SIMTypeRespository<SIMType, SIMTypeDTO, SIMTypeDTO>>();
builder.Services.AddScoped<IBaseRespository<ScreenDTO,ScreenDTO>,BrandRespository<Screen,ScreenDTO,ScreenDTO>>();
builder.Services.AddScoped<IBaseRespository<CategoryDTO, CategoryDTO>, CategoryRespository<Category, CategoryDTO, CategoryDTO>>();
builder.Services.AddScoped<IBaseRespository<PaymentDTO, PaymentDTO>, CategoryRespository<Payment, PaymentDTO, PaymentDTO>>();
builder.Services.AddScoped<IBaseRespository<BatteryDTO,BatteryDTO>,BrandRespository<Battery,BatteryDTO,BatteryDTO>>();
builder.Services.AddScoped<IBaseRespository<RamMemoryDTO,RamMemoryDTO>,BrandRespository<RAMMemory,RamMemoryDTO,RamMemoryDTO>>();
builder.Services.AddScoped<IBaseRespository<ROMMemoryDTO,ROMMemoryDTO>,BrandRespository<ROMMemory,ROMMemoryDTO,ROMMemoryDTO>>();
builder.Services.AddScoped<IBaseRespository<OperatingSystemDTO,OperatingSystemDTO>,BrandRespository<OperatingSystemProduct,OperatingSystemDTO,OperatingSystemDTO>>();
builder.Services.AddScoped<IBaseRespository<MemoryStickDTO, MemoryStickDTO>, BrandRespository<MemoryStick, MemoryStickDTO, MemoryStickDTO>>();

builder.Services.AddIdentity<User, IdentityRole<int>>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Stores.ProtectPersonalData = false;
    })
    .AddEntityFrameworkStores<MyDBContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAccount, AccountRespository>();

var app = builder.Build();

await SeedData.SeedDataToDB(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
