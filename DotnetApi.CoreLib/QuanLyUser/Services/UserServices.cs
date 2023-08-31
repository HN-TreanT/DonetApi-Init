
using System.Collections.Generic;
using AutoMapper;
using DotnetApi.CoreLib.Interfaces;
using DotnetApi.CoreLib.QuanLyUser.DTO;
using DotnetApi.CoreLib.QuanLyUser.Interfaces;
using Microsoft.Extensions.Logging;
using DotnetApi.CoreLib.Errors;
using DotnetApi.CoreLib.Users.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DotnetApi.CoreLib.options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using DotnetApi.CoreLib.Models;
using DotnetApi.CoreLib.Common;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.CoreLib.Users.Services
{
    public class UserServices : IUserServices
    {
        private readonly JwtOptions _options;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServices> _logger;
        public string GenerateJwtToken(UserGet user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
            ClaimsIdentity getClaimsIdentity()
            {
                return new ClaimsIdentity(
                   getClaims()
                   );

                Claim[] getClaims()
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Username.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, user.RoleId));
                    return claims.ToArray();
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = getClaimsIdentity(),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public UserServices(IRepository repository, IMapper mapper, ILogger<UserServices> logger, IOptions<JwtOptions> options)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _options = options.Value;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (request.Username == null || request.Password == null || request.Username == " " || request.Password == "")
            {
                throw new Error("User or password invalid");
            }
            var user = await _repository.SingleOrDefaultAsync<User>(t => t.Username == request.Username && t.Password == request.Password);
            if (user == null)
            {
                throw new Error("username or password incorrect");
            }
            var u = new UserGet
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
            };
            var token = GenerateJwtToken(u);


            return new LoginResponse
            {
                Id = user.Id,
                Username = user.Username,
                Token = token,
            };
        }

        public async Task<User> Signup(CreateUser create)
        {
            if (create.Username == null || create.Password == null || create.Email == null
               || create.Email == "" || create.Password == "" || create.Username == "")
            {
                throw new Error("username or password or email invalid");
            }
            var user = await _repository.SingleOrDefaultAsync<User>(t => t.Username == create.Username);
            if (user != null)
            {
                throw new Error("username exists");
            }
            var role = await _repository.SingleOrDefaultAsync<Role>(t => t.RoleId == "U");
            User newUSer = new User()
            {
                Username = create.Username,
                Password = create.Password,
                Email = create.Email,
                RoleId = "U",
                Role = role
            };
            await _repository.AddAsync(newUSer);
            await _repository.SaveChangeAsync();
            return newUSer;
        }

        public async Task<UserGet> GetById(int id)
        {

            var user = await _repository.FindAsync<User>(id);

            return new UserGet
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
            };
        }

        public async Task<PagedData<UserGet>> GetAll(Paging paging)
        {
            IQueryable<User> data = _repository.GetListAsync<User>(t => t.RoleId != "A");
            // IQueryable<User> data = _repository.GetListAsync<User>(null);
            List<User> result = await data.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();
            var totalItem = await data.CountAsync();
            PagedData<UserGet> listUser = new PagedData<UserGet>
            {
                CurrentPage = paging.PageNumber,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / paging.PageSize),
                TotalItem = totalItem,
                CanBack = paging.PageNumber > 1,
                CanNext = (int)Math.Ceiling((decimal)totalItem / paging.PageSize) > paging.PageNumber,
                Items = _mapper.Map<List<UserGet>>(result)
            };
            return listUser;
        }

    }
}