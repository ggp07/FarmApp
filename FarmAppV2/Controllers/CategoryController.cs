using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmAppV2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmAppV2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }
    }
}
