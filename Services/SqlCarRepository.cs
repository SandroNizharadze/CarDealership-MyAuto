using System;
using Microsoft.EntityFrameworkCore;
using MyAuto.Models;
using MyAuto.Services;

public class SqlCarRepository : ICARRepository
{
    private readonly AppDbContext Context;

    public SqlCarRepository(AppDbContext context)
    {
        Context = context;
    }
    public Car Add(Car car)
    {
        Context.Cars.Add(car);
        Context.SaveChanges();
        return car;
    }

    public Car GetCarWithPhotos(int carId)
    {
        return Context.Cars
                    .Include(c => c.Photos)
                    .FirstOrDefault(c => c.Id == carId)!;
    }

    public Car Delete(int id)
    {
        Car car = Context.Cars.Find(id)!;
        if (car != null)
        {
            Context.Cars.Remove(car);
            Context.SaveChanges();
        }
        return car ?? throw new InvalidOperationException($"Car with id {id} not found.");
    }

    public Car GetCarOnId(int id)
    {
        var car = Context.Cars.Find(id);
        if (car == null)
        {
            throw new InvalidOperationException($"Car with id {id} not found.");
        }
        return car;
    }

    public IEnumerable<Car> GetListCars()
    {
        return Context.Cars.Include(c => c.Photos).ToList();
    }

    public Car Update(Car car)
    {
        if (car == null)
        {
            throw new ArgumentNullException(nameof(car));
        }
        var carEntity = Context.Cars.Attach(car);
        carEntity.State = EntityState.Modified;
        Context.SaveChanges();
        return car;
    }

    public void AddPhoto(CarPhoto carPhoto)
    {
        Context.CarPhotos.Add(carPhoto);
        Context.SaveChanges();
    }


    public IEnumerable<Car> SearchCars(Manufacturer? manufacturer, string model, string yearRange, string priceRange, FuelType? fuelType, string engine, Transmission? transmission)
    {
        if (Context.Cars == null)
        {
            throw new InvalidOperationException("Cars DbSet is null.");
        }
        var query = Context.Cars.AsQueryable();

        if (manufacturer.HasValue)
        {
            query = query.Where(c => c.Manufacturer == manufacturer);
        }
        if (!string.IsNullOrEmpty(model))
        {
            query = query.Where(c => c.Model != null && c.Model.ToString() == model);
        }
        if (!string.IsNullOrEmpty(yearRange))
        {
            var years = yearRange.Split('-').Select(int.Parse).ToArray();
            query = query.Where(c => c.Year >= years[0] && c.Year <= years[1]);
        }
        if (!string.IsNullOrEmpty(priceRange))
        {
            var prices = priceRange.Split('-').Select(decimal.Parse).ToArray();
            query = query.Where(c => c.Price >= prices[0] && c.Price <= prices[1]);
        }
        if (fuelType.HasValue)
        {
            query = query.Where(c => c.FuelType == fuelType);
        }
        if (!string.IsNullOrEmpty(engine))
        {
            query = query.Where(c => c.Engine != null && c.Engine.Contains(engine));
        }
        if (transmission.HasValue)
        {
            query = query.Where(c => c.Transmission == transmission);
        }

        return query.Include(c => c.Photos).ToList();
    }

}
