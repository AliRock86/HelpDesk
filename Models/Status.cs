using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusNameArb { get; set; }

        public int StatusTypeId { get; set; }

        [NotMapped]
        public string StatusTypeName { get; set; }

        public virtual StatusType StatusType { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
