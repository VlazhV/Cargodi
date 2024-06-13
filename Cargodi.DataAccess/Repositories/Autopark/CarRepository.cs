using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Autopark;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Autopark;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Autopark;

public class CarRepository : RepositoryBase<Car, int>, ICarRepository
{
    public CarRepository(DatabaseContext db) : base(db)
    {
    }

    public override Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _db.Cars
             
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(int id, CancellationToken cancellationToken)
    {
       return await _db.Cars
             
            .AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> DoesItExistAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return await _db.Cars
             
            .AnyAsync(c => c.LicenseNumber.Equals(licenseNumber), cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetAllCarsFullInfoAsync(CancellationToken cancellationToken)
    {
        return await _db.Cars
             
            .Include(c => c.CarType)
            .ToListAsync(cancellationToken);
    }

    public Task<Car?> GetCarFullInfoByIdAsync(int carId, CancellationToken cancellationToken)
    {
        return _db.Cars
             
            .Include(c => c.CarType)
            .FirstOrDefaultAsync(c => c.Id == carId);
    }


    public async Task<IEnumerable<Car>> GetCarsOfTypeAsync(CarType carType, CancellationToken cancellationToken)
    {
        return await _db.Cars
             
            .Where(cars => cars.CarType.Id == carType.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetCategoriesToDriveAsync(Car car, CancellationToken cancellationToken)
    {
        return await _db.Cars
             
            .Include(car => car.CarType)
                .ThenInclude(type => type.CarTypeCategories)
                    .ThenInclude(ct => ct.Category)
            .Where(c => c.Id == car.Id)
            .Select(car => car.CarType)
                .SelectMany(type => type.CarTypeCategories)
                    .Select(ct => ct.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetFreeCarsAsync(CancellationToken cancellationToken)
    {
        return await _db.Ships
             
            .Include(ship => ship.Car)
            .Where(ship => ship.Start != null && ship.Finish == null)
            .Select(ship => ship.Car)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetSuitableCarsOrderedAsync(int weight, 
        int volume, 
        int biggestLinearSize, 
        int autoparkStartId, 
        int? shipId, 
        CancellationToken cancellationToken)
    {
        var cars = await _db.Cars
             
            .Include(car => car.Ships)
            .Where(car => 
                car.Carrying > weight 
                && car.CapacityHeight * car.CapacityLength * car.CapacityWidth > volume 
                && 
                    car.CapacityHeight > biggestLinearSize 
                    || car.CapacityLength > biggestLinearSize 
                    || car.CapacityWidth > biggestLinearSize 
                && 
                car.ActualAutoparkId == autoparkStartId)
            .OrderBy(car => car.CapacityHeight * car.CapacityLength * car.CapacityWidth).ThenBy(car => car.Carrying)
            .ToListAsync(cancellationToken);

        return cars;
        // return cars.Where(car => car.Ships == null 
        //         || car.Ships.Count == 0  
        //         || car.Ships.Last().Id == shipId 
        //         || car.Ships.Last().Finish != null);
    }

    public Task<Car?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken)
    {
        return _db.Cars
             
            .Where(c => c.LicenseNumber.Equals(licenseNumber))
            .FirstOrDefaultAsync();
    }

}