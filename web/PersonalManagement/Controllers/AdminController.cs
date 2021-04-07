using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalManagement.Helper;
using PersonalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
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

        public async Task<IActionResult> Users(Admin_Users_IndexViewModel model)
        {
            model = model ?? new Admin_Users_IndexViewModel();
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(model.Email))
            {
                users = users.Where(x => x.Email.Contains(model.Email));
            }

            model.Users.DataSource = users
                .Skip(PagingHelper.Skip(model.Users.PageIndex, model.Users.RecordPerPage))
                .Take(model.Users.RecordPerPage)
                .Select(x => new DTO.Admin_UserIndexDto
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Username = x.UserName,
                    IsLockOut = x.LockoutEnabled,
                    IsConfirmedByEmail = x.EmailConfirmed
                })
                .ToList();
            model.Users.TotalRecords = users.Count();
            return View(model);
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
