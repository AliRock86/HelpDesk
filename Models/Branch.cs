using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        [Required]
        public string BranchName { get; set; }

        [Required]
        public string BranchNameArb { get; set; }

        public int DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }

       // [Required]
        public DateTime? CreatedDate { get; set; }


       // [Required]
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<BranchTicket> BranchTickets { get; set; }

        public virtual ICollection<UsersBranch> UsersBranch { get; set; }
    }
}
