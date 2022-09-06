using System.Collections;

namespace HelpDesk.Services.Interfaces
{
    public interface InterfaceGetTickets
    {
        public IList DepartmentTickets(int? StatusId =null);
        public IList BranchTickets(int BranchId, int? StatusId = 1);
        public IList UserTickets(int UserId, int? StatusId = 1);
        public IList SenderTickets(int UserId, int? StatusId = 1);
        public IList SenderDepartmentTickets(int DepartmentId, int? StatusId = 1); 
    }
}
