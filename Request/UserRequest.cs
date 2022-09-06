using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Request
{
    public class UserRequest
    {

            [Required]
            public string UserName { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            [Column(TypeName = "bigint")]
            public int Phone { get; set; }

            [Required]
            public string Password { get; set; }

            public int BranchId { get; set; }

            //[Required]
            public DateTime? CreatedDate { get; set; }

            //[Required]
            public DateTime? UpdatedDate { get; set; }

            [Required]
            public int RoleId { get; set; }
            public int? EmployeeId { get; set; }


        }
    }
