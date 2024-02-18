
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var superAdminRoleId = "57a78954-8804-424c-a9bf-1d9ef3606219";
        var adminRoledId = "6bd01384-8804-4698-a55c-438b9899a418";
        var userRoledId = "8ae5b640-62bc-49da-9cbd-f2dde966d558";

        // Seed Roles (User, Admin, Super Admin)
        var roles = new List<IdentityRole>
        {
            new IdentityRole()
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoledId,
                ConcurrencyStamp = adminRoledId
            },
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoledId,
                ConcurrencyStamp = userRoledId
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // Seed SuperAdmin user
        var superAdminId = "d9023e09-d8c7-428b-9387-77f36492d748";
        var superAdminUser = new IdentityUser()
        {
            Id = superAdminId,
            UserName = "superadmin@blog.com",
            NormalizedUserName = "superadmin@blog.com".ToUpper(),
            Email = "superadmin@blog.com",
            NormalizedEmail = "superadmin@blog.com".ToUpper(),
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin123");

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        // Add all roles to superAdmin user
        var superAdminRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = adminRoledId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoledId,
                UserId = superAdminId
            },
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}
