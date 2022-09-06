using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk;
using HelpDesk.Models;
using HelpDesk.Services;
using HelpDesk.Request;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Globalization;
using Newtonsoft.Json;
using HelpDesk.Services.Interfaces;

namespace HelpDesk.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        HelpDbContext _context = new HelpDbContext();
        private AccountService accountService;

        public AuthController(AccountService _accountService)
        {
            accountService = _accountService;
        }

        // GET: Auth

        public IActionResult signup() 
        {
            ViewBag.Role = _context.Roles.ToList();
            ViewBag.dep = _context.Departments.ToList();
            return View("SignUp");
        }

        [HttpPost]
        public List<Branch> GetBranches(int id)
        {
            var bra = _context.Branches.Where(x => x.DepartmentId == id).ToList();
            return bra;
        }

        [Route("welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            return View("Welcome");
        }

      /* [Route("home")]
        public IActionResult Home()
        {
            return View("Index");
        }*/

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Login()
        {
            //TempData["v"] = "invisible";
            return View("Login");
        }
        [HttpPost]
        public IActionResult Loginh(UserRequest u)
        {
            var auth = accountService.Login(u.UserName, u.Password);
            if (auth != null)
            {
                var role = _context.Roles.FirstOrDefault(x => x.Id == auth.RoleId);
                HttpContext.Session.SetString("UserName", auth.UserName);
                HttpContext.Session.SetInt32("UserId", auth.Id);
                HttpContext.Session.SetString("Role", role.RoleName);
                var userBranch = _context.UsersBranch.Where(x => x.UserId == auth.Id).FirstOrDefault();
                if (userBranch != null)
                {
                    var userDepartment = _context.Branches.Where(i => i.Id == userBranch.BranchId).FirstOrDefault();
                    HttpContext.Session.SetInt32("userBranch", userBranch.BranchId);
                    HttpContext.Session.SetInt32("userDepartment", userDepartment.DepartmentId);
                }
                return new RedirectResult("/Home/Dashboard");
            }
            else
            {
                //TempData["v"] = "visible";
                TempData["msg"] = "يوجد خطاء في اليوزر او رمز الدخول";
                return new RedirectResult("Login");
            }
        }

        // GET: Auth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Create(UserRequest user)
        {
            if (ModelState.IsValid)
            {
                var salt = accountService.GetSecureSalt();
                var password = accountService.HashUsingPbkdf2(user.Password, salt);
                var us = new User
                {
                    UserName = user.UserName,
                    Password = password,
                    Salt = Convert.ToBase64String(salt),
                    Phone = user.Phone,
                    RoleId = user.RoleId,
                    Email = user.Email,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                _context.Users.Add(us);
                _context.SaveChanges();
               var ub = new UsersBranch
                {
                    BranchId = user.BranchId,
                    UserId = us.Id,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,

               };
                _context.UsersBranch.Add(ub);
                _context.SaveChanges();

                ViewBag.msg = "Done";
                return RedirectToAction("Login");
            }
            ViewBag.msg = "Error"; 
            ViewBag.Role = _context.Roles.ToList();
            ViewBag.dep = _context.Departments.ToList();
            return View("/Login");
        }

        // GET: Auth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Auth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email,Phone,Password,Active,CreatedDate,UpdatedDate,RoleId,EmployeeId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Auth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Active = false;
                _context.SaveChanges();
            }

            return View(user);
        }

        // POST: Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'HelpDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
