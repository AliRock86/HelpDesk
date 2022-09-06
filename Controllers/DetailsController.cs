using HelpDesk.Models;
using HelpDesk.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HelpDesk.Controllers
{
    public class DetailsController : Controller
    {
        HelpDbContext _context = new HelpDbContext();
        public IActionResult Index()
        {
            ViewBag.brn =  _context.Branches.Where(x => x.DepartmentId == 1).ToList();
            return View();
        }

        public IActionResult AssignToDepartment(int ticketId, string? desc)
        {
            var dp = new DepartmentTicket
            {
                TicketId = ticketId,
                DepartmentId = 1,
            };
            _context.DepartmentTickets.Add(dp);
            _context.SaveChanges();
            this.ChangeStatus(ticketId, 1);
            if (desc != null)
                this.Feedback(ticketId, desc);
            return View();
        }
        public IActionResult AssignToBranch(int ticketId, int branchId, string? desc)
        {
            var branchTicket = new BranchTicket
                {
                TicketId = ticketId,
                BranchId = branchId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _context.BranchTickets.Add(branchTicket);
            _context.SaveChanges();
            this.ChangeStatus(ticketId,1);
            if (desc != null)
                this.Feedback(ticketId, desc);
            return View();
        }

        public IActionResult AssignToUser(int ticketId, int userId , string? desc)
        {
            var userTicket = new UserTicket
            {
                TicketId = ticketId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _context.UserTickets.Add(userTicket);
            _context.SaveChanges();
            this.ChangeStatus(ticketId, 1);
            if (desc != null)
                this.Feedback(ticketId, desc);
            return View();
        }

        public IActionResult Excuter(int ticketId , string? desc = null)
        {
            this.ChangeStatus(ticketId, 1);
            if (desc != null)
                this.Feedback(ticketId, desc);
            return View();
        }

        public IActionResult getTicket(int id)
        {
            ViewBag.brn = _context.Branches.Where(x => x.DepartmentId == 1).ToList();
            var order = (from Ticket in _context.Tickets
                         join User in
                         _context.Users on Ticket.UserId equals User.Id
                         join Status in _context.Statuses on Ticket.StatusId equals Status.StatusId
                         join Department in _context.Departments on Ticket.DepartmentId equals Department.Id
                         join SubCategory in _context.SubCategories on Ticket.SubCategoryId equals SubCategory.Id
                         where Ticket.Id == id
                         select new
                         {
                             TicketId = Ticket.Id,
                             Subject = Ticket.Subject,
                             Number = Ticket.Number,
                             MaterialNumber = Ticket.MaterialNumber,
                             Description = Ticket.Description,
                             CreatedByUserName = User.UserName,
                             Department = Department.DepartmentNameArb,
                             StatusArb = Status.StatusNameArb,
                             Status = Status.StatusName,
                             TicketType = Ticket.TicketTypeId,
                             SubCategory = SubCategory.SubCategoryNameArb
                         }).ToList();

            var feedback =(from FeedbackTicket in _context.FeedbackTickets
                           join User in _context.Users on FeedbackTicket.UserId equals User.Id
                           join Role in _context.Roles on User.RoleId equals Role.Id
                           where FeedbackTicket.TicketId == id 
                           select new
                           { 
                            Desc = FeedbackTicket.Description,
                            DescDate = FeedbackTicket.CreatedDate,
                            UserName = User.UserName,
                            RoleName = Role.RoleNameArb,
                           }).ToList();
            ViewBag.orders = order;
            ViewBag.feedback = feedback;
            return View("Index");
        }

        public void Feedback(int TicketId , string Desc)
        {
            var feedback = new FeedbackTicket
            {
                TicketId = TicketId,
                UserId = (int)HttpContext.Session.GetInt32("UserId"),
                Description = Desc,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _context.FeedbackTickets.Add(feedback);
            _context.SaveChanges();
        }

        public void ChangeStatus(int TicketId, int statusId)
        {
            var ticket = _context.Tickets.Where(i => i.Id == TicketId).FirstOrDefault();
            if(ticket == null)
            {
                ticket.StatusId = statusId;
                _context.SaveChanges();
            }
        }
    }
}
