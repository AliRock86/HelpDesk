using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class UsersBranch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BranchId { get; set; }

        [NotMapped]
        public string BranchName { get; set; }
        public virtual Branch Branch { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }


        [Required]
        public DateTime? UpdatedDate { get; set; }
    }
}
