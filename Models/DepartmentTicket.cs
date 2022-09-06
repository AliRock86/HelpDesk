using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class DepartmentTicket
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }


        [Required]
        public DateTime? UpdatedDate { get; set; }

    }
}
