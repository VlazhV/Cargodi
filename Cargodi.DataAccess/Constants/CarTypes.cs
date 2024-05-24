using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Constants;

public static class CarTypes
{
	public static CarType Truck { get => new CarType { Id = 1, Name = "Truck" }; } 
	public static CarType Van { get => new CarType { Id = 2, Name = "Van" }; }
	public static CarType PassengerCar { get => new CarType { Id = 3, Name = "Passenger Car" }; }
}