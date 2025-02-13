using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuto.Models;
using MyAuto.Services;

namespace MyAuto.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICARRepository CarRepository;

        public DetailsModel(ICARRepository carRepository)
        {
            CarRepository = carRepository;
        }

        public Car? Car { get; private set; }

        public void OnGet(int id)
        {
            Car = CarRepository.GetCarWithPhotos(id);
        }

        public IActionResult OnPost(int id)
        {
            Car = CarRepository.Delete(id);
            if (Car == null)
            {
                return RedirectToPage("/NotFound");
            }
            return RedirectToPage("Index");
        }
    }
}
