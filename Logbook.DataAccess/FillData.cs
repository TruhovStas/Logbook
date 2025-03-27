//using System.Security.Claims;
//using Logbook.DataAccess.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Logbook.Domain.Entities;

//namespace Logbook.DataAccess
//{
//    public static class FillData
//    {
//        public static async Task FillDatabaseAsync(DatabaseContext context, UserManager<User> userManager, IUnitOfWork unitOfWork)
//        {
//            if (context.Database.GetPendingMigrations().Any())
//            {
//                await context.Database.MigrateAsync();
//            }

//            if (!context.Users.Any())
//            {
//                var newUser = new User()
//                {
//                    UserName = "admin",
//                    Email = "admin@gmail.com"
//                };
//                await userManager.CreateAsync(newUser, "Admin123-");
//                var claims = new List<Claim>()
//                {
//                    new Claim(ClaimTypes.Name, newUser.UserName),
//                    new Claim(ClaimTypes.Email, newUser.Email),
//                    new Claim(ClaimTypes.Role, "Admin")
//                };
//                var addedUser = await userManager.FindByNameAsync(newUser.UserName);
//                await userManager.AddClaimsAsync(addedUser, claims);
//            }

//            if (!context.Events.Any())
//            {
//                await context.Events.AddAsync(new Event()
//                {
//                    Title = "Концерт",
//                    Description = "Крутой концерт",
//                    DateTime = new DateTime(2024, 11, 9, 17, 0, 0),
//                    Location = "ст.м. Немига",
//                    Category = "Музыка",
//                    MaxParticipants = 1000,
//                    Image = "125.jpg"
//                });
//                await context.SaveChangesAsync();
//            }

//            if (!context.Participants.Any())
//            {
//                await context.Participants.AddAsync(new Forecast()
//                {
//                    Name = "Александр",
//                    LastName = "Иванов",
//                    Email = "sasha@gmail.com",
//                    BirthDate = new DateOnly(1999, 1, 1),
//                    Event = await unitOfWork.Events.GetFirstByPredicateAsync(e => e.Title == "Концерт"
//                    && e.Description == "Крутой концерт"),
//                    RegistrationDate = DateTime.Now,
//                });
//                await context.SaveChangesAsync();
//            }
//        }
//    }
//}
