using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Extensions
{
    public static class IHostDatabaseExtentions
    {
        public static IHost SeedDatabase(this IHost self)
        {
            using var scope = self.Services.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<VotingSystemDbContext>();


            var existingAdminRole = db.UserRoles.FirstOrDefault(r => r.Name == UserRoleNames.Admin);
            if (existingAdminRole == null)
            {
                var newAdminRole = UserRole.WithName(UserRoleNames.Admin);
                db.UserRoles.Add(newAdminRole);
            }

            var existingClerkRole = db.UserRoles.FirstOrDefault(r => r.Name == UserRoleNames.Clerk);
            if (existingClerkRole == null)
            {
                var newClerkRole = UserRole.WithName(UserRoleNames.Clerk);
                db.UserRoles.Add(newClerkRole);
            }

            db.SaveChanges();


            if (!db.Users.Any())
            {
                var adminRole = db.UserRoles.FirstOrDefault(r => r.Name == UserRoleNames.Admin);
                var adminUser = User.FromCredentials("admin", "Password123&", adminRole.Id);
                db.Users.Add(adminUser);
            }

            db.SaveChanges();

            return self;
        }
    }
}
