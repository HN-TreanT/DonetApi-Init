using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetApi.CoreLib.Common;
using DotnetApi.CoreLib.Models;
using DotnetApi.CoreLib.QuanLyUser.DTO;
using DotnetApi.CoreLib.Users.Entities;

namespace DotnetApi.CoreLib.QuanLyUser.Interfaces
{
    public interface IUserServices
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<User> Signup(CreateUser create);
        Task<UserGet> GetById(int id);
        Task<PagedData<UserGet>> GetAll(Paging paging);


    }
}