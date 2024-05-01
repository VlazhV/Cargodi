namespace Cargodi.DataAccess.Entities.Autopark;

public interface ISchedule
{
	public DateTime? Start { get; set; }
	public DateTime? Finish { get; set; }

	public DateTime PlanStart { get; set; }
	public DateTime PlanFinish { get; set; }
}
