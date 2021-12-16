using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalManagement.Helper;
using PersonalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private ILogger<AdminController> _logger;

        public AdminController(SignInManager<ApplicationUser> signInManager,
            ILogger<AdminController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users(string Email)
        {
            var model = new Admin_Users_IndexViewModel();
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(Email))
            {
                users = users.Where(x => x.Email.Contains(Email));
            }

            model.Users.DataSource = users
                .AsNoTracking()
                .Skip(PagingHelper.Skip(model.Users.PageIndex, model.Users.RecordPerPage))
                .Take(model.Users.RecordPerPage)
                .Select(x => new DTO.Admin_UserIndexDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Username = x.UserName,
                    IsLockOut = x.LockoutEnabled,
                    IsConfirmedByEmail = x.EmailConfirmed,
                })
                .ToList();
            foreach (var record in model.Users.DataSource)
            {
                var user = await _userManager.FindByEmailAsync(record.Email);
                record.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            }
            // model.Users.DataSource.ForEach(async (x) =>  {
            //    var user = await _userManager.FindByEmailAsync(x.Email);
            //    x.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            //});
            model.Users.TotalRecords = users.Count();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Users(Admin_Users_IndexViewModel model)
        {
            return View();
        }

        public async Task<IActionResult> Roles(Admin_Roles_IndexViewModel model)
        {
            model = model ?? new Admin_Roles_IndexViewModel();
            var roles = _roleManager.Roles;


            model.Roles.DataSource = roles
                .Skip(PagingHelper.Skip(model.Roles.PageIndex, model.Roles.RecordPerPage))
                .Take(model.Roles.RecordPerPage)
                .Select(x => new DTO.Admin_RoleIndexDto
                {
                    Name = x.Name
                })
                .ToList();
            model.Roles.TotalRecords = roles.Count();
            return View(model);
        }
    }
}
