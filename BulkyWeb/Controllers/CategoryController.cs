using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
          _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Retrieve all categories from the database
            List<Category> categories = _dbContext.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            // Server-side validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }

            //Validate if exists in database display order
            var existingCategory = _dbContext.Categories
                .FirstOrDefault(c => c.DisplayOrder == category.DisplayOrder);
            if (existingCategory != null)
            {
                ModelState.AddModelError("DisplayOrder", "A category with this Display Order already exists.");
            }

            //Validate without model attribute
            // ModelState.AddModelError("", "Custom error message without specific field.");

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? categoryFromDb = _dbContext.Categories.Find(id);
            //Category? categoryFromDb = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
            //Difference between FirstOrDefault and SingleOrDefault: FirstOrDefault returns the first matching element or a default value if no match is found, while SingleOrDefault expects exactly one matching element and throws an exception if there are multiple matches.
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            // Server-side validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }

            //Validate if exists in database display order != id

            //Validate without model attribute
            // ModelState.AddModelError("", "Custom error message without specific field.");

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? categoryFromDb = _dbContext.Categories.Find(id);
            //Category? categoryFromDb = _dbContext.Categories.SingleOrDefault(c => c.Id == id);
            //Difference between FirstOrDefault and SingleOrDefault: FirstOrDefault returns the first matching element or a default value if no match is found, while SingleOrDefault expects exactly one matching element and throws an exception if there are multiple matches.
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(categoryFromDb);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
