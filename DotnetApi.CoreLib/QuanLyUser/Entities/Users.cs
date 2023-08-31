using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetApi.CoreLib.Entities;

namespace DotnetApi.CoreLib.Users.Entities
{
    [Table("users")]
    public partial class User : ConcurrentEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public required string Username { get; set; }
        [StringLength(200)]
        public required string Password { get; set; }
        [StringLength(200)]
        public required string Email { get; set; }
        [StringLength(450)]
        public string? RefreshToken { get; set; } = null;
        public required string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public required Role Role { get; set; }

    }
}