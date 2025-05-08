using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
	public class AttendanceMappingProfileL:Profile
	{
		// Ánh xạ từ TakeAttendanceDTO sang Attendance
		public AttendanceMappingProfileL() {
			CreateMap<TakeAttendanceDTO, Attendance>();

		}
	}
}
