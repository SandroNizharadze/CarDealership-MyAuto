using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuto.Models;
using MyAuto.Services;

namespace MyAuto.Pages
{
    public class SearchResultsModel : PageModel
    {
        private readonly ICARRepository carRepository;

        public SearchResultsModel(ICARRepository carRepository)
        {
            this.carRepository = carRepository;
            Cars = new List<Car>();
        }

        public IEnumerable<Car> Cars { get; set; }

        public void OnGet(Manufacturer? Manufacturer, string Model, int? MinYear, int? MaxYear, decimal? MinPrice, decimal? MaxPrice, FuelType? FuelType, Transmission? Transmission)
        {
            string yearRange = MinYear.HasValue && MaxYear.HasValue ? $"{MinYear}-{MaxYear}" : string.Empty;
            string priceRange = MinPrice.HasValue && MaxPrice.HasValue ? $"{MinPrice}-{MaxPrice}" : string.Empty;

            Cars = carRepository.SearchCars(Manufacturer, Model, yearRange, priceRange, FuelType, string.Empty, Transmission);
        }
    }
}