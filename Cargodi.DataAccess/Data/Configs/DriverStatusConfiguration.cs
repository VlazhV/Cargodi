using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class DriverStatusConfiguration : IEntityTypeConfiguration<DriverStatus>
{
	public void Configure(EntityTypeBuilder<DriverStatus> builder)
	{
		builder.HasData(
			DriverStatuses.SickLeave,
			DriverStatuses.Vacations,
			DriverStatuses.Works
		);
	}

}