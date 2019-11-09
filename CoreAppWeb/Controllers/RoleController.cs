using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreAppWeb.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roles;
        public RoleController(RoleManager<IdentityRole> roles)
        {
            this.roles = roles;
        }

        public IActionResult Index()
        {
            var data = roles.Roles.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            var r = roles.CreateAsync(role).Result;
            return RedirectToAction("Index");
        }
    }
}