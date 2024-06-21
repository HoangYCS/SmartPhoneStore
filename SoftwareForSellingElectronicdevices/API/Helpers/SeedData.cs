using Data.ModelDbContext;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class SeedData
    {
        private const string RoleName = "Admin";
        private const string UserName = "Admin";
        private const string Password = "123456";
        private const string AdminEmail = "admin@gmail.com";

        public static async Task SeedDataToDB(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            await SeedAdminRole(serviceProvider);
            await SeedUserAdmin(serviceProvider);
            await SeedPayment(serviceProvider);
            await SeedVisitor(serviceProvider);
        }

        private static async Task SeedAdminRole(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole<int>>>();
            if (!await roleManager.RoleExistsAsync(RoleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole<int>(RoleName));
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }

        private static async Task SeedPayment(IServiceProvider service)
        {
            var dbContext = service.GetRequiredService<MyDBContext>();
            if (!dbContext.Payments.Any())
            {
                var lstPayment = new List<Payment>()
                    {
                        new Payment()
                        {
                            Name = "COD"
                        },
                        new Payment()
                        {
                            Name="VNPay"
                        }
                    };
                await dbContext.Payments.AddRangeAsync(lstPayment);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedVisitor(IServiceProvider service)
        {
            var dbContext = service.GetRequiredService<MyDBContext>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == 2);
            if (user == null)
            {
                var newUser = new User()
                {
                    UserName = "KhachVangLai",
                    Email = "khachvanglai@gmail.com",
                };
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(newUser, "Password11122321312");
                newUser.PasswordHash = hashedPassword;
                var createUserResult = await userManager.CreateAsync(newUser);
            }
        }

        private static async Task SeedUserAdmin(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var dbContext = service.GetRequiredService<MyDBContext>();
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                var newUser = new User()
                {
                    UserName = UserName,
                    Email = AdminEmail,
                };
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(newUser, Password);
                newUser.PasswordHash = hashedPassword;

                var createUserResult = await userManager.CreateAsync(newUser);

                if (createUserResult.Succeeded)
                {
                    await AddCartForUser(dbContext, newUser);
                    await userManager.AddToRoleAsync(newUser, RoleName);
                }
                else
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }

            static async Task AddCartForUser(MyDBContext dbContext, User newUser)
            {
                var cart = new Cart()
                {
                    UserId = newUser.Id,
                };

                await dbContext.Carts.AddAsync(cart);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
