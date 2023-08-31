using DotnetApi.CoreLib.Errors;
using DotnetApi.CoreLib.QuanLyUser.DTO;
using DotnetApi.CoreLib.Users.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace DotnetApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected void AuthorireToken(int userId)
        {
            var user = (UserGet)HttpContext.Items["User"];
            if (user.Id != userId)
            {
                throw new Error("Invalid token");
            }
        }
        protected async Task Delay()
        {
            var delayValue = new Random().Next(500, 2000);
            await Task.Delay(delayValue);
        }
    }
}