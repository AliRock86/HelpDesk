using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string DepartmentNameArb { get; set; }

        public int? CompanyId { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<DepartmentTicket> DepartmentTickets { get; set; }
    }
}
