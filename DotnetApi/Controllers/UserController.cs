using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetApi.CoreLib.QuanLyUser.DTO;
using DotnetApi.CoreLib.QuanLyUser.Interfaces;
using DotnetApi.InfraLib.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetApi.Attributes;
using DotnetApi.CoreLib.Common;

namespace DotnetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;


        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;

        }
        [HttpPost("login")]
        public async Task<IActionResult> RequestLogin([FromBody] LoginRequest request)
        {
            var result = await _userServices.Login(request);
            return Ok(result);
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] CreateUser create)
        {
            var result = await _userServices.Signup(create);
            return Ok(result);
        }
        [HttpGet("getbyid")]
        [CustomAuthorize("A", "U")]

        public async Task<IActionResult> GetById(int id)
        {
            AuthorireToken(id);
            var result = await _userServices.GetById(id);
            return Ok(result);
        }
        [HttpGet("getall")]
        [CustomAuthorize("A")]
        public async Task<IActionResult> GetAll([FromQuery] Paging paging)
        {
            var result = await _userServices.GetAll(paging);
            return Ok(result);
        }

    }
}