using crud2.Data;
using crud2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud2.Controllers.Api
{
    [Route("api/Categories")]
    // [ApiController]
    public class CategoriesController : Controller
    {
        private readonly EcommerceDbContext _context;

        public CategoriesController(EcommerceDbContext context)
        {
            _context = context;
        }
        [HttpGet] // New URL: api/good/categories

        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            return Ok(category);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Edit(Category category)
        {
            var CategoryId = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            CategoryId.Id = category.Id;
            CategoryId.Name = category.Name;
            await _context.SaveChangesAsync();
            return Ok(CategoryId);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine("delete");
            var categoryId = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(categoryId);
            await _context.SaveChangesAsync();
            return Ok("success");
        }

    }
}
