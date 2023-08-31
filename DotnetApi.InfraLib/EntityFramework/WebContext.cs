using Microsoft.EntityFrameworkCore;
using DotnetApi.CoreLib.Users.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetApi.InfraLib.EntityFramework
{
    public partial class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public string ConnectString = @"Data Source=HN-TreanT;Initial Catalog=ApiDotnet;TrustServerCertificate=True;;Persist Security Info=True;User ID=sa;Password=hnam23012002";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectString, b => b.MigrationsAssembly("DotnetApi"));
            optionsBuilder.UseLoggerFactory(GetLoggerFactory());

        }
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                    builder.AddConsole()
                           .AddFilter(DbLoggerCategory.Database.Command.Name,
                                    LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<ArticleTag>(entity =>
            // {
            //     entity.HasIndex(p => new { p.ArticleId, p.TagId }).IsUnique();
            // });

        }

    }
}