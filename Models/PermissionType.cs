using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string PermissionTypeName { get; set; }
        [Required]
        public string PermissionTypeNameArb { get; set; }
        public virtual ICollection<PermissionUser> PermissionUsers { get; set; }
    }
}
