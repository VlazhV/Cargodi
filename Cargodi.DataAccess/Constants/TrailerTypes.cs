using Cargodi.DataAccess.Entities.Autopark;

namespace Cargodi.DataAccess.Constants;

public static class TrailerTypes
{
	public static TrailerType Cistern { get => new() { Id = 1, Name = "Cistern" }; }
	public static TrailerType Bulker { get => new() { Id = 2, Name = "Bulker" }; }
	public static TrailerType VanTrailer { get => new() { Id = 3, Name = "VanTrailer" }; }
}