using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuto.Models;
using MyAuto.Services;

namespace MyAuto.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICARRepository CarRepository;

        public DeleteModel(ICARRepository carRepository)
        {
            CarRepository = carRepository;
        }
        [BindProperty]
        public Car? car { get; set; }
        public IActionResult OnGet(int id)
        {
            CarRepository.GetCarOnId(id);
            if (car == null)
            {
                RedirectToPage("/NotFound");
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            CarRepository.Delete(id);
            return RedirectToPage("Index");
        }
    }
}
