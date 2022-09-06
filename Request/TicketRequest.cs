using HelpDesk.Models;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Request
{
    public class TicketRequest
    {


        [Required]
        public string Subject { get; set; }

        public int? MaterialNumber { get; set; } = 0;

        public int? Number { get; set; } = 0;

        public bool? CaseType { get; set; } = false;

        [Required]
        public string Description { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TicketTypeId { get; set; }

        public int? StatusId { get; set; }


        public DateTime? CreatedDate { get; set; }


        public DateTime? UpdatedDate { get; set; }

        [Required]
        public int BranchId { get; set; }


    }
}
