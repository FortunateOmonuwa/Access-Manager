using Azure;
using Data_Access.DTOs;
using Data_Access.Other_Objects;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task<string> Login(LoginDTO login)
        {
            var response = new ResponseModel<string>();
            //var confirmThroughEmail = await userManager.FindByEmailAsync(login.UsernameOrEmail);
            var user = await userManager.FindByNameAsync(login.UsernameOrEmail);
            var confirm_password = await userManager.CheckPasswordAsync(user, login.Password);
           // var confirm_password_2 = await userManager.CheckPasswordAsync(confirmThroughEmail, login.Password);

            try
            {
                if(/*confirmThroughEmail == null ||*/ user == null || confirm_password == null)
                {
                    throw new ArgumentNullException();
                }
               
                else
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    //create claims
                    var authClaims = new List<Claim>
                    {
                        new(ClaimTypes.Name, user.UserName),
                        new(ClaimTypes.NameIdentifier, user.Id),
                        new("JWTID", Guid.NewGuid().ToString()),

                    };

                    foreach(var role in userRoles)
                    {
                        authClaims.Add(new(ClaimTypes.Role, role));
                        
                    };

                    var token = GenerateNewJsonWebToken(authClaims);
                    return token;
                }
             
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}/n {ex.Source} /n {ex.InnerException}");
            }
           
        }
        private string GenerateNewJsonWebToken(List<Claim> authClaims)
        {
            //create new symmetric key
            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value));
            //var tokeknObject = new JwtPayload ? Take note
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha384Signature);
            //Create the token object
            var tokenObject = new JwtSecurityToken(
                 issuer: configuration["JWT:ValidIssuer"],
                 audience: configuration["JWT:ValidAudience"],
                 expires: DateTime.Now.AddHours(1),
                 claims: authClaims,
                 signingCredentials: credentials
                ); 
           
            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }

        public async Task<ResponseModel<string>> Register(RegisterDTO registerModel)
        {
            var response = new ResponseModel<string>();
            try
            {
                var isUserExisting = await userManager.FindByEmailAsync(registerModel.Email);
                var isUserExist = await userManager.FindByNameAsync(registerModel.UserName);
               
                if (isUserExist != null || isUserExisting != null)
                {
                    response.IsSuccessful = false;
                    response.Message = $"User already exist";
                    response.ResultCode = 400;
                }
                else
                {
                    response.IsSuccessful = true;
                    response.Message = "Registration succesfull";
                    IdentityUser newUser = new()
                    {
                        UserName = registerModel.UserName,
                        Email = registerModel.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        
                    };

                    var createNewUser = await userManager.CreateAsync(newUser, registerModel.Password);
                    if (!createNewUser.Succeeded)
                    {
                        var errorString = "User creation failed";
                        response.IsSuccessful = false;
                       
        

                        foreach(var error in createNewUser.Errors)
                        {
                            //errorString += " /n" + error.Description;
                            response.Message = errorString + error.Description;
                            response.ResultCode = Convert.ToInt32(error.Code);
                        }
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser, UserRoles.NEWMEMBER);
                        response.IsSuccessful = true;
                        response.Message = "User Creation Successfull";
                        response.ResultCode = 200;
                        response.Result = $"Welcome {newUser.UserName}";
                       // response.Result = newUser;
                    }
                }
            
            }
            catch (InvalidOperationException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message + "/n" + ex.Source + "/n" + ex.InnerException;
                response.Result = ex.StackTrace;
                response.ResultCode = 400;

            }
            catch (AggregateException ex)
            {

                response.IsSuccessful = false;
                response.Message = ex.Message + "/n" + ex.Source + "/n" + ex.InnerException;
                response.Result = ex.StackTrace;
                response.ResultCode = 400;


            }
            catch (Exception ex)
            {

                response.IsSuccessful = false;
                response.Message = ex.Message + "/n" + ex.Source + "/n" + ex.InnerException;
                response.Result = ex.StackTrace;
                response.ResultCode = 400;

            }
            return response;

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
