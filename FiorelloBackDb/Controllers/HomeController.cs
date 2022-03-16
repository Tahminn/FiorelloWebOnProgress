using FiorelloBackDb.Data;
using FiorelloBackDb.Models;
using FiorelloBackDb.Models.Admin;
using FiorelloBackDb.Models.Home;
using FiorelloBackDb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            #region Contexts
            List<Slider> sliders = await _context.Sliders
                .ToListAsync();
            SliderDetail detail = await _context.SliderDetails
                .FirstOrDefaultAsync();
            //List<Category> categories = await _context.Categories
            //    .Where(c => c.IsDeleted == false)
            //    .Include(c=>c.Products)
            //    .Where(p=>p.IsDeleted == false)
            //    .Include(c=>c.Products)
            //    .ThenInclude(p=>p.ProductImages)
            //    .Where(i => i.IsDeleted == false)                
            //    .ToListAsync();
            List<Setting> settings = _context.Settings
                .ToList();

            List<Product> products = await _context.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .OrderByDescending(m => m.Id)
                .Take(settings.FirstOrDefault().ProductTake)
                .ToListAsync();
            About about = await _context.About
                .Where(p => p.IsDeleted == false)
                .Include(p => p.AboutLists)
                .FirstOrDefaultAsync();
            List<Expert> experts = await _context.Experts
                .Where(p => p.IsDeleted == false)
                .Include(p => p.FlwExpertSection)
                .ToListAsync();
            List<Blog> blogs = await _context.Blogs
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
            BlogSection blogSection = await _context.BlogSection
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync();
            List<Testimonial> testimonials = await _context.Testimonials
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
            List<InstaCarouselItem> instaCarouselItems = await _context.InstaCarouselItems
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
            #endregion

            ViewBag.ProductCount = _context.Products
                .Where(p => p.IsDeleted == false)
                .Count();
            if (Request.Cookies["basket"] != null) 
            {
                HomeVM homeVM = new HomeVM
                {
                    Sliders = sliders,
                    Detail = detail,
                    //Categories = categories,
                    Products = products,
                    About = about,
                    Experts = experts,
                    Blogs = blogs,
                    BlogSection = blogSection,
                    Testimonials = testimonials,
                    InstaCarouselItems = instaCarouselItems,
                    BasketVM = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"])
                };
                return View(homeVM);
            }
            else
            {
                HomeVM homeVM = new HomeVM
                {
                    Sliders = sliders,
                    Detail = detail,
                    //Categories = categories,
                    Products = products,
                    About = about,
                    Experts = experts,
                    Blogs = blogs,
                    BlogSection = blogSection,
                    Testimonials = testimonials,
                    InstaCarouselItems = instaCarouselItems
                };
                return View(homeVM);
            }
           
            
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

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return BadRequest();
            List<BasketVM> basket;            

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            var existProduct = basket.Find(m => m.Id == dbProduct.Id);
            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
