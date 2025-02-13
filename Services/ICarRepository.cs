using Microsoft.EntityFrameworkCore.Update.Internal;
using MyAuto.Models;

namespace MyAuto.Services;

public interface ICARRepository
{
    IEnumerable<Car> GetListCars();
    Car GetCarOnId(int id);
    Car Update(Car car);
    Car Add(Car car);
    Car Delete(int id);
    IEnumerable<Car> SearchCars(Manufacturer? manufacturer, string model, string yearRange, string priceRange, FuelType? fuelType, string engine, Transmission? transmission);
    Car GetCarWithPhotos(int value);
    void AddPhoto(CarPhoto carPhoto); 
}
