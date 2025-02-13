using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuto.Models;
using MyAuto.Services;
using System.Text.Json;

namespace MyAuto.Pages;

public class CarSearchModel : PageModel
{
    private readonly ICARRepository carRepository;

    public IEnumerable<Car>? Cars { get; set; }

    public Dictionary<Manufacturer, List<string>> CategoryModelMapping { get; private set; }

    public CarSearchModel(ICARRepository carRepository)
    {
        this.carRepository = carRepository;
        CategoryModelMapping = CarData.ManufacturerModels; 
    }

    public void OnGet()
    {
        Cars = carRepository.GetListCars();
        ViewData["CategoryModelMapping"] = JsonSerializer.Serialize(CategoryModelMapping);
    }
}