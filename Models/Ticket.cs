using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        public int? MaterialNumber { get; set; } = 0;

        public int? Number { get; set; } = 0;

        public bool? CaseType { get; set; } = false;

        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }
        public virtual Department Department { get; set; }


        [Required]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        [Required]
        public int TicketTypeId { get; set; }

        public int? StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }


        [Required]
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<FeedbackTicket> FeedbackTickets { get; set; }

        public virtual ICollection<DepartmentTicket> DepartmentTickets { get; set; }

        public virtual ICollection<BranchTicket> BranchTickets { get; set; }
        public virtual ICollection<UserTicket> UserTickets { get; set; }
    }
}
