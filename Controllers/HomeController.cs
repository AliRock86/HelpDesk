using HelpDesk.Models;
using HelpDesk.Request;
using HelpDesk.Services;
using HelpDesk.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {
        HelpDbContext _context = new HelpDbContext();
        private InterfaceGetTickets _getTicktes ;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , InterfaceGetTickets getTicktes)
        {
            _logger = logger;
            _getTicktes = getTicktes;
        }

        public IActionResult Request1()
        {
            ViewBag.SubCat = _context.SubCategories.Where(i => i.CategoryId == 1).ToList();
            ViewBag.dep = _context.Departments.ToList();
           // TempData["sh"] = null;
            return View("Request1");
        }

        public IActionResult Request2()
        {
            ViewBag.SubCat = _context.SubCategories.Where(i => i.CategoryId == 2).ToList();
            ViewBag.dep = _context.Departments.ToList();
            return View("Request2");
        }

        public IActionResult AllRequestes()
        {
            return View("AllRequestes");
        }

        public IActionResult CreateTicket1(TicketRequest ticket)
        {
            try
            {
                var tk = new Ticket
                {
                    Subject = ticket.Subject,
                    TicketTypeId = 1,
                    SubCategoryId = ticket.SubCategoryId,
                    DepartmentId = ticket.DepartmentId,
                    StatusId = 1,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    Description = ticket.Description,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                _context.Tickets.Add(tk);
                _context.SaveChanges();
               /* var dp = new DepartmentTicket
                {
                    TicketId = tk.Id,
                    DepartmentId = ticket.DepartmentId,
                };
                _context.DepartmentTickets.Add(dp);
                _context.SaveChanges();*/
                TempData["sh"] = true;
                TempData["msg"] = "تم ارسال الطلب بنجاح";
            }
            catch (Exception)
            {
                TempData["sh"] = true;
                TempData["msg"] = "هنالك مشكلة في ارسال الطلب!!";
            }
            return new RedirectResult("Request1");
        }

        public IActionResult CreateTicket2(TicketRequest ticket)
        {
            try
            {
                var tk = new Ticket
                {
                    Subject = ticket.Subject,
                    TicketTypeId = 1,
                    SubCategoryId = ticket.SubCategoryId,
                    DepartmentId = ticket.DepartmentId,
                    Number = ticket.Number,
                    MaterialNumber = ticket.MaterialNumber,
                    StatusId = 1,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    Description = ticket.Description,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                _context.Tickets.Add(tk);
                _context.SaveChanges();
              /*  var dp = new DepartmentTicket
                {
                    TicketId = tk.Id,
                    DepartmentId = ticket.DepartmentId,
                };
                _context.DepartmentTickets.Add(dp);
                _context.SaveChanges()*/;
                TempData["sh"] = true;
                TempData["class"] = "success";
                TempData["msg"] = "تم ارسال الطلب بنجاح";
            }
            catch (Exception)
            {
                TempData["sh"] = true;
                TempData["class"] = "danger";
                TempData["msg"] = "هنالك مشكلة في ارسال الطلب!!";
            }
            return new RedirectResult("Request2");
        }

        public IActionResult Dashboard()
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            string Role = HttpContext.Session.GetString("Role");
            switch (Role)
            {
                case "sender":
                    ViewBag.orders = _getTicktes.SenderTickets(UserId);
                    break;
                case "mng_employee":
                    var DepartmentId = (int)HttpContext.Session.GetInt32("userDepartment");
                    ViewBag.orders = _getTicktes.SenderDepartmentTickets(DepartmentId);
                    break;
                case "mng_it":
                    ViewBag.orders = _getTicktes.DepartmentTickets(UserId);
                    break;
                case "mng_section":
                    ViewBag.orders = _getTicktes.BranchTickets(UserId);
                    break;
                default:
                    ViewBag.orders = _getTicktes.SenderTickets(UserId);
                    break;
            }
             ViewBag.rej = _context.Tickets.Where(t=>t.StatusId == 1).Count();
             ViewBag.comp = _context.Tickets.Where(t => t.StatusId == 2).Count();
             ViewBag.bin = _context.Tickets.Where(t => t.StatusId == 3).Count();
             ViewBag.all = _context.Tickets.Count();
            return View("Dashboard");
        }
    }
}