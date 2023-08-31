using DotnetApi.CoreLib.Interfaces;
using DotnetApi.CoreLib.QuanLyUser.Interfaces;
using DotnetApi.CoreLib.Users.Services;
using DotnetApi.InfraLib.EntityFramework;
using Microsoft.EntityFrameworkCore;
using DotnetApi.middlewares;
using DotnetApi.CoreLib.options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddControllers();
string ConnectString = @"Data Source=HN-TreanT;Initial Catalog=ApiDotnet;TrustServerCertificate=True;;Persist Security Info=True;User ID=sa;Password=hnam23012002";
builder.Services.AddDbContext<WebContext>(
    opt =>
    {
        opt.UseSqlServer(ConnectString);
    }
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));
///
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

//
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCors");
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
