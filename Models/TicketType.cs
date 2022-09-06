using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class TicketType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TicketTypeName { get; set; }

        [Required]
        public string TicketTypeArb { get; set; }

        [Required]
        public int BranchId { get; set; }

        [NotMapped]
        public string BranchName { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
