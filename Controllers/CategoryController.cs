using crud2.Data;
using crud2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly EcommerceDbContext _context;

        public CategoryController(EcommerceDbContext context)
        {
            _context = context;
        }

        public IActionResult index()
        {
            var Response = _context.Categories.ToList();
            return View(Response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                return View(category);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var CategoryId = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            CategoryId.Id = category.Id;
            CategoryId.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoryId = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(categoryId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
