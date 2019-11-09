using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAppWeb.Models;
using CoreAppWeb.Services;
namespace CoreAppWeb.Controllers
{
    /// <summary>
    /// The ASP.NET Core Controller
    /// using IActionResult as a common contract between mvc Core and API Controllers
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly IService<Category, int> service;
        public CategoryController(IService<Category, int> service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            var res = service.GetAsync().Result;
            return View(res);
        }

        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category cat)
        {
             
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (cat.BasePrice < 0) throw new Exception("Base Price cannot be -Ve");
                    cat = service.CreateAsync(cat).Result;
                    return RedirectToAction("Index");
                }
                return View(cat);
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new ErrorViewModel() 
            //    { 
            //       ControllerName = this.RouteData.Values["controller"].ToString(),
            //       ActionName = this.RouteData.Values["action"].ToString(),
            //       ErrorMessage = ex.Message
            //    });
            //}
        }

        public IActionResult Edit(int id)
        {
            return View(service.GetAsync(id).Result);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category cat)
        {
            if (ModelState.IsValid)
            {
                cat = service.UpdateAsync(id, cat).Result;
                return RedirectToAction("Index");
            }
            return View(cat);
        }
    }
}