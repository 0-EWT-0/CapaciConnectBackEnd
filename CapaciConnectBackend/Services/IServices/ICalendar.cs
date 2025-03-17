using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Calendars;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface ICalendar
    {
        Task<List<Calendars>> GetAllWorkshopCalendarsAsync();
        Task<List<Calendars>> GetWorkshopCalendarsAsync(int workshopId);
        Task<Calendars?> CreateWorkshopCalendarAsync(CalendarDTO calendarDTO);
        Task<Calendars?> UpdateWorkshopCalendarAsync(UpdateCalendarDTO calendarDTO, int calendarId);
        Task<bool> DeleteWorkshopCalendarAsync(int calendarId);
    }
}
