using Churn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Churn.Controllers
{

    public class ShopController : Controller
    {

        //property for database connection 
        private ApplicationDbContext _context;
            
        //constructor 
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Categories
                .OrderBy(category => category.Name)
                .ToListAsync();

            return View(products);
        }
            
        
        public async Task<IActionResult> Details (int? id)
        {
            var categoryWithProducts = await _context.Categories
                .Include(category => category.Products)
                .FirstOrDefaultAsync(category => category.Id == id);

            return View(categoryWithProducts);
        }


        public async Task<IActionResult> ProductDetails(int? id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == id);
                
               
            return View(product);
        }
        
      
    }

}
