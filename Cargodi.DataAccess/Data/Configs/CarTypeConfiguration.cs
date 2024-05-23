using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Autopark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class CarTypeConfiguration : IEntityTypeConfiguration<CarType>
{
	public void Configure(EntityTypeBuilder<CarType> builder)
	{
		builder.HasData(
			CarTypes.PassengerCar,
			CarTypes.Truck,
			CarTypes.Van
		);
	}

}