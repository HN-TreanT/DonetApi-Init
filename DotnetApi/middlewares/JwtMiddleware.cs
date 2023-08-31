
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DotnetApi.CoreLib.options;
using DotnetApi.CoreLib.QuanLyUser.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DotnetApi.middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _options;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtOptions> options)
        {
            _next = next;
            _options = options.Value;
        }
        public async Task Invoke(HttpContext context, IUserServices usersService)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await AttachUserToContext(context, usersService, token);
            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserServices usersService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_options.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var id = jwtToken.Claims.First().Value;

                // attach user to context on successful jwt validation
                int intValue = int.Parse(id);

                var user = await usersService.GetById(intValue);

                if (user != null)
                    context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}