using BlogsDEMO.Application.Enums;
using BlogsDEMO.Application.Helpers;
using BlogsDEMO.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeds
{
    public class SeedInitialData
    {
        private readonly ApplicationDbContext _context;

        public SeedInitialData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            var roleAdminId = Guid.NewGuid().ToString();
            var userAdminId = Guid.NewGuid().ToString();

            // Add Role
            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new Role()
                {
                    Id = roleAdminId,
                    RoleName = RoleEnum.SuperAdmin.ToString(),
                    Description = RoleEnum.SuperAdmin.ToString(),
                    IsDeleted = false
                });
                _context.Roles.Add(new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleName = RoleEnum.Admin.ToString(),
                    Description = RoleEnum.Admin.ToString(),
                    IsDeleted = false
                });
                _context.Roles.Add(new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleName = RoleEnum.Staff.ToString(),
                    Description = RoleEnum.Staff.ToString(),
                    IsDeleted = false
                });
                _context.Roles.Add(new Role()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleName = RoleEnum.User.ToString(),
                    Description = RoleEnum.User.ToString(),
                    IsDeleted = false
                });
                await _context.SaveChangesAsync();
            }

            // Add User
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User()
                {
                    Id = userAdminId,
                    DateOfBirth = new DateTime(2000, 11, 08),
                    FirstName = "Vo",
                    LastName = "Tan Hung",
                    Gender = (int)GenderEnum.Male,
                    Password = HashKeyHelper.EnCodeSha1("Abc123!@#"),
                    PhoneNumber = "0988944608",
                    RoleId = "Admin",
                    UserName = "superadmin",
                    IsDeleted = false,
                });
                await _context.SaveChangesAsync();
            }

            // Add Role Permission
            //if(!_context.Users.)
        }
    }
}