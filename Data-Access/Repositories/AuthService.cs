using Data_Access.Other_Objects;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<ResponseModel<string>> SeedRoles()
        {
            try
            {
                var response = new ResponseModel<string>();
                var adminRoleExist = await roleManager.RoleExistsAsync(UserRoles.ADMIN);
                var magnetRoleExists = await roleManager.RoleExistsAsync(UserRoles.MAGNET);
                var newMemberRoleExists = await roleManager.RoleExistsAsync(UserRoles.NEWMEMBER);
                


                if (adminRoleExist && magnetRoleExists && newMemberRoleExists)
                {
                    response.IsSuccessful = false;
                    response.ResultCode = 400;
                    response.Message = "Database already contains these roles";
                    response.Result = "Failed";

                }
        
                else
                {
                    var admin = await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
                    var magnet = await roleManager.CreateAsync(new IdentityRole(UserRoles.MAGNET));
                    var newMember = await roleManager.CreateAsync(new IdentityRole(UserRoles.NEWMEMBER));

                    response.IsSuccessful = true;
                    response.ResultCode = 200;
                    response.Message = "Roles were successfully seeded into the database";

                }
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}/n {ex.Source} /n {ex.InnerException}");
            }

           
        }

      
    }
}
