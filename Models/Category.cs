using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryNameArb { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
