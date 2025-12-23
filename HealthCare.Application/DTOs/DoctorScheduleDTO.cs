using BuildingBlocks.Shared.DTOs;

namespace HealthCare.Application.DTOs
{

	public class DoctorScheduleDTO : BaseObjectDTO
	{
		public DoctorDTO Doctor { get; set; } = default!;
		public string DaysOfWeek { get; set; } = default!;

		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }

		public bool IsActive { get; set; } = true;
	}
}
