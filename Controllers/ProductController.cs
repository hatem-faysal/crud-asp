using crud2.Data;
using crud2.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace crud2.Controllers
{
    public class ProductController: Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ProductController(EcommerceDbContext context,IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        // [HttpGet("home/index")]
        public IActionResult index()
        {
            var Response = _context.Products.Include(x=>x.Category).ToList();
            ViewBag.good = "hatem";
            return View(Response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name","Description","Price","CategoryId","ProductPicture")] Product product)
        {

            // if (ModelState.IsValid)
            // {
                // await _context.CreateAsync(product);
    if (product.ProductPicture != null)
    {
        var productname = $"{Guid.NewGuid()} - {product.ProductPicture.FileName}";
        var directoryPath = Path.Combine(_webHost.WebRootPath, "images");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        var src = "/images/" + productname;
        var path = Path.Combine(directoryPath, productname);

        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await product.ProductPicture.CopyToAsync(fileStream);
        }

        product.ImageURL = src;
    }


                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            // }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.CategorySelectList = new SelectList(categories, "Id", "Name");
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }

        [HttpPost]
public async Task<IActionResult> Edit(Product product)
{
    // Find the existing product in the database
    var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
    if (existingProduct == null)
    {
        return NotFound();
    }

    // Update product details
    existingProduct.Name = product.Name;
    existingProduct.Description = product.Description;
    existingProduct.Price = product.Price;
    existingProduct.CategoryId = product.CategoryId;
    
    // Handle image upload
    if (product.ProductPicture != null)
    {

        // Delete the old image if it exists
        if (!string.IsNullOrEmpty(existingProduct.ImageURL))
        {
            var oldImagePath = Path.Combine(_webHost.WebRootPath, existingProduct.ImageURL.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }

        // Upload the new image
        var productName = $"{Guid.NewGuid()} - {product.ProductPicture.FileName}";
        var directoryPath = Path.Combine(_webHost.WebRootPath, "images");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        var src = "/images/" + productName;
        var newPath = Path.Combine(directoryPath, productName);

        using (var fileStream = new FileStream(newPath, FileMode.Create))
        {
            await product.ProductPicture.CopyToAsync(fileStream);
        }

        // Update the ImageURL of the product
        existingProduct.ImageURL = src;
    }

    // Save changes to the database
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}

public async Task<IActionResult> Delete(int id)
{
    // Find the product in the database
    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
    if (product == null)
    {
        return NotFound();
    }

    // Delete the associated image file if it exists
    if (!string.IsNullOrEmpty(product.ImageURL))
    {
        var imagePath = Path.Combine(_webHost.WebRootPath, product.ImageURL.TrimStart('/'));
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
    }

    // Remove the product from the database
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
    }
}
