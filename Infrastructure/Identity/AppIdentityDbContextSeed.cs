using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Raph",
                    Email = "raph@gmail.com",
                    UserName = "raph@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Raphael",
                        LastName = "Nagato",
                        Street = "1, Akatsuki",
                        City = "Tokyo",
                        State = "TK",
                        ZipCode = "10000"

                    }

                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}