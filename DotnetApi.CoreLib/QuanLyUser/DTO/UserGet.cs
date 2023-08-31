using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.QuanLyUser.DTO
{
    public class UserGet
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string RoleId { get; set; }
    }
}