using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    //Para aplicar esto BindProperties, se debe enviar el id como hidden...
    // Esto actualmente no se aplica, porque se recibe el id en el OnPost como parametro y no como parte del modelo Category..
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet( int id)
        {
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {

            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(Category);
            TempData["success"] = "Category deleted successfully";
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
