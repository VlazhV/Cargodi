namespace Cargodi.DataAccess.Entities.Autopark;

public interface ISchedulable<T> where T: ISchedule
{
	List<T> Schedules { get; set; }
}
