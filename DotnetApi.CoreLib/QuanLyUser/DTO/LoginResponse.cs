using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.QuanLyUser.DTO
{
    public class LoginResponse
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Token { get; set; }


    }
}