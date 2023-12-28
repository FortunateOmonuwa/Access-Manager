using Data_Access.DTOs;
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Access_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("Add-Roles")]
        public async Task<IActionResult> AddRoles()
        {
            try
            {
                var response = await authService.SeedRoles();
                if(response.IsSuccessful)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} /n {ex.Source}/n{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("Register-new-User")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var register_Response = await authService.Register(registerDTO);
                if(register_Response.IsSuccessful)
                {
                    return Ok(register_Response);
                }
                else
                {
                    return BadRequest(register_Response);
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message} /n {ex.Source}/n{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var login_Response = await authService.Login(login);
                if (login_Response != null)
                {
                    return Ok(login_Response);
                }
                else
                {
                    return BadRequest(login_Response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} /n {ex.Source}/n{ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("Give-Admin-Access")]
        public async Task<IActionResult> GiveAdminAccess([FromBody]UpdatePermission username)
        {
            try
            {
                var update_response = await authService.GiveAdminAccess(username);
                if (update_response is not null)
                {
                    return Ok(update_response);
                }
                else
                {
                    return BadRequest(update_response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} /n {ex.Source}/n{ex.InnerException}");
            }
        }


        [HttpPost]
        [Route("Give-Magnet-Access")]
        public async Task<IActionResult> GiveMagnetAccess([FromBody]UpdatePermission username)
        {
            try
            {
                var update_response = await authService.GiveMagnetAccess(username);
                if (update_response != null)
                {
                    return Ok(update_response);
                }
                else
                {
                    return BadRequest(update_response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} /n {ex.Source}/n{ex.InnerException}");
            }
        }
    }
}
