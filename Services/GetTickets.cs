using System.Collections;
using HelpDesk.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace HelpDesk.Services
{
    public class GetTickets : InterfaceGetTickets
    {
        HelpDbContext _context = new HelpDbContext();
        public IList DepartmentTickets(int? StatusId = null)
        {
            var orders = (from DepartmentTicket in _context.DepartmentTickets
                          join Ticket in _context.Tickets on DepartmentTicket.TicketId equals Ticket.Id
                          join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                          join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                          join User in _context.Users on Ticket.UserId equals User.Id
                          where(StatusId != null && Status.StatusId == StatusId)
                          select new
                          {
                              TicketId = Ticket.Id,
                              UserName = User.UserName,
                              Department = Department.DepartmentNameArb,
                              Status = Status.StatusNameArb,
                              TicketType = Ticket.TicketTypeId
                          }).OrderByDescending(x => x.TicketId).ToList();
            return orders;
        }

        public IList BranchTickets(int BranchId, int? StatusId = 1)
        {
            var orders = (from BranchTicket in _context.BranchTickets
                          join Ticket in _context.Tickets on BranchTicket.TicketId equals Ticket.Id
                          join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                          join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                          join User in _context.Users on Ticket.UserId equals User.Id
                          where BranchTicket.BranchId == BranchId || (StatusId != null && Status.StatusId == StatusId)
                          select new
                          {
                              TicketId = Ticket.Id,
                              UserName = User.UserName,
                              Department = Department.DepartmentNameArb,
                              Status = Status.StatusNameArb,
                              TicketType = Ticket.TicketTypeId
                          }).OrderByDescending(x => x.TicketId).ToList();
            return orders;
        }

        public IList UserTickets(int UserId ,int? StatusId = 1)
        {
            var orders = (from UserTicket in _context.UserTickets
                          join Ticket in _context.Tickets on UserTicket.TicketId equals Ticket.Id
                          join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                          join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                          join User in _context.Users on Ticket.UserId equals User.Id
                          where User.Id == UserId || (StatusId != null && Status.StatusId == StatusId)
                          select new
                          {
                              TicketId = Ticket.Id,
                              UserName = User.UserName,
                              Department = Department.DepartmentNameArb,
                              Status = Status.StatusNameArb,
                              TicketType = Ticket.TicketTypeId
                          }).OrderByDescending(x => x.TicketId).ToList();
            return orders;
        }

        public IList SenderTickets(int UserId, int? StatusId = 1)
        {
            var orders = (from Ticket in _context.Tickets
                          join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                          join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                          join User in _context.Users on Ticket.UserId equals User.Id
                          where Ticket.UserId == UserId || (StatusId != null && Status.StatusId == StatusId)
                          select new
                          {
                              TicketId = Ticket.Id,
                              UserName = User.UserName,
                              Department = Department.DepartmentNameArb,
                              Status = Status.StatusNameArb,
                              TicketType = Ticket.TicketTypeId
                          }).OrderByDescending(x => x.TicketId).ToList();
            return orders;
        }


        public IList SenderDepartmentTickets(int DepartmentId, int? StatusId = 1)
        {
            var orders = (from Ticket in _context.Tickets
                          join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                          join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                          join User in _context.Users on Ticket.UserId equals User.Id
                          where Ticket.DepartmentId == DepartmentId || (StatusId != null && Status.StatusId == StatusId)
                          select new
                          {
                              TicketId = Ticket.Id,
                              UserName = User.UserName,
                              Department = Department.DepartmentNameArb,
                              Status = Status.StatusNameArb,
                              TicketType = Ticket.TicketTypeId
                          }).OrderByDescending(x => x.TicketId).ToList();
            return orders;
        }
    }
}
