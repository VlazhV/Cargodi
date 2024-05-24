using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Constants;

public static class DriverStatuses
{
	public static DriverStatus Works { get => new DriverStatus() { Id = 1, Name = "Works" }; }
	public static DriverStatus Vacations { get => new DriverStatus() { Id = 2, Name = "Vacations" }; }
	public static DriverStatus SickLeave { get => new DriverStatus() { Id = 3, Name = "Sick Leave" }; }
}