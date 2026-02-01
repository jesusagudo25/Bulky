using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Categories.Update(Category);
            TempData["success"] = "Category updated successfully";
            _db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnGet(int id)
        {

            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();

        }
    }
}
