using Data_Access.DTOs;
using Domain.DTOs;
using Domain.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel<string>> SeedRoles();
        Task<ResponseModel<string>> Register(RegisterDTO registerModel);
        Task<string>Login(LoginDTO login);
    }
}
