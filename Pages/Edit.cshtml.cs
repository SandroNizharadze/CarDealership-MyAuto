using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuto.Models;
using MyAuto.Services;
using System.Text.Json;

namespace MyAuto.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICARRepository _carRepository;

        public EditModel(ICARRepository carRepository)
        {
            _carRepository = carRepository;
            Car = new Car { Photos = new List<CarPhoto>() };
            UploadedPhotos = new List<IFormFile>();
            CategoryModelMapping = CarData.ManufacturerModels;
        }

        [BindProperty]
        public Car Car { get; set; }

        [BindProperty]
        public List<IFormFile> UploadedPhotos { get; set; }

        public Dictionary<Manufacturer, List<string>>? CategoryModelMapping { get; private set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Car = _carRepository.GetCarWithPhotos(id.Value);
            }
            else
            {
                Car = new Car();
            }

            CategoryModelMapping = CarData.ManufacturerModels;
            ViewData["CategoryModelMapping"] = JsonSerializer.Serialize(CategoryModelMapping);

            return Page();
        }

        public class IndexModel : PageModel
        {

            public JsonResult OnGetModels(string manufacturer)
            {
                Console.WriteLine($"Received manufacturer: {manufacturer}");
                if (Enum.TryParse(manufacturer, out Manufacturer selectedManufacturer))
                {
                    var models = CarData.ManufacturerModels[selectedManufacturer];
                    return new JsonResult(models);
                }

                return new JsonResult(new List<string> { "Model A", "Model B" });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Car.Id > 0)
            {
                Car = _carRepository.Update(Car);
            }
            else
            {
                Car = _carRepository.Add(Car);
            }

            if (UploadedPhotos != null && UploadedPhotos.Any())
            {
                foreach (var photo in UploadedPhotos)
                {
                    var fileName = $"{Guid.NewGuid()}_{photo.FileName}";
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    var carPhoto = new CarPhoto
                    {
                        PhotoPath = $"/images/{fileName}",
                        CarId = Car.Id
                    };
                    _carRepository.AddPhoto(carPhoto);
                }
            }

            return RedirectToPage("Index");
        }
    }
}