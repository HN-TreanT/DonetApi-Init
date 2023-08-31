using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApi.CoreLib.Users.Entities
{
    [Table("role")]
    public class Role
    {
        [Key]
        public required string RoleId { get; set; }
        [StringLength(20)]
        public required string RoleName { get; set; }

    }
}