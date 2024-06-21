using Data.DTOs;
using Data.ModelDbContext;
using Data.Models;
using Data.Respositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.IServices;
using Web.Models;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(it => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7212/")
});

builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));

builder.Services.AddScoped<IBaseService<BrandDTO, BrandDTO>, BrandService>();
builder.Services.AddScoped<IBaseService<ColorDTO, ColorDTO>, ColorService>();
builder.Services.AddScoped<IBaseService<RamMemoryDTO, RamMemoryDTO>, RamMemoryService>();
builder.Services.AddScoped<IBaseService<ROMMemoryDTO, ROMMemoryDTO>, ROMMemoryService>();
builder.Services.AddScoped<IBaseService<ChipDTO, ChipDTO>, ChipService>();
builder.Services.AddScoped<IBaseService<ScreenDTO, ScreenDTO>, ScreenService>();
builder.Services.AddScoped<IBaseService<BatteryDTO, BatteryDTO>, BatteryService>();
builder.Services.AddScoped<IBaseService<OperatingSystemDTO, OperatingSystemDTO>, OperatingSystemService>();
builder.Services.AddScoped<IBaseService<ChargingPortDTO, ChargingPortDTO>, ChargingPortService>();
builder.Services.AddScoped<IBaseService<MemoryStickDTO, MemoryStickDTO>, MemoryStickService>();
builder.Services.AddScoped<IBaseService<SIMTypeDTO, SIMTypeDTO>, SIMTypeService>(); 
builder.Services.AddScoped<IBaseService<CategoryDTO, CategoryDTO>, CategoryService>();
builder.Services.AddScoped<IBaseService<PaymentDTO, PaymentDTO>, PaymentService>();
builder.Services.AddScoped<IProductsAdminService,ProductsAdminService>();
builder.Services.AddScoped<IProductUserService,ProductUserService>();
builder.Services.AddScoped<ICartServiceFactory, CartServiceFactory>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddAuthentication();

builder.Services.AddIdentity<User, IdentityRole<int>>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Stores.ProtectPersonalData = false;
    })
    .AddEntityFrameworkStores<MyDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
