using Cargodi.DataAccess.Entities.Staff;

namespace Cargodi.DataAccess.Constants;

public static class DriverStatuses
{
    public static DriverStatus Works { get => new DriverStatus() { Id = 1, Name = "Works" }; }
}