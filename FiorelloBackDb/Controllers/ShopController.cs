using FiorelloBackDb.Data;
using FiorelloBackDb.Models.Admin;
using FiorelloBackDb.Models.Home;
using FiorelloBackDb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            #region Contexts            
            List<Setting> settings = _context.Settings
                .ToList();

            List<Product> products = await _context.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .OrderByDescending(m => m.Id)
                .Take(settings.FirstOrDefault().ProductTake)
                .ToListAsync();            
            #endregion

            ViewBag.ProductCount = _context.Products
                .Where(p => p.IsDeleted == false)
                .Count();

            HomeVM homeVM = new HomeVM
            {
                Products = products
            };
            return View(homeVM);
        }
        public IActionResult LoadMore(int skip)
        {
            List<Setting> settings = _context.Settings
                .ToList();

            List<Product> products = _context.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .OrderByDescending(m => m.Id)
                .Skip(skip)
                .Take(settings.FirstOrDefault().LoadTake)
                .ToList();
            HomeVM homeVM = new HomeVM
            {
                Products = products
            };

            return PartialView("_ProductsPartial", homeVM);
        }
    }
}
