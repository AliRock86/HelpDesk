using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class StatusType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StatusTypeName { get; set; }
        public string StatusTypeNameArb { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
    }
}
