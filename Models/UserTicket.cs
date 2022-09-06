using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class UserTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }


        [Required]
        public DateTime? CreatedDate { get; set; }


        [Required]
        public DateTime? UpdatedDate { get; set; }
    }
}
