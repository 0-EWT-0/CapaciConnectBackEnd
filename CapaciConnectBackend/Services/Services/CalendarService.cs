using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Calendars;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class CalendarService : ICalendar
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public CalendarService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Calendars>> GetAllWorkshopCalendarsAsync()
        {
            var calendars = await _context.Calendars.AsNoTracking().ToListAsync();

            return calendars;
        }

        public async Task<List<Calendars>> GetWorkshopCalendarsAsync(int workshopId)
        {
            var calendar = await _context.Calendars.Where(c => c.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

            return calendar;
        }

        public async Task<Calendars?> CreateWorkshopCalendarAsync(CalendarDTO calendarDTO)
        {
            //var exists = await _context.Calendars.AnyAsync(c => c.Id_workshop_id == calendarDTO.Id_workshop_id);

            //if (exists) return null;

            var newCalendar = new Calendars
            {
                Date_start = calendarDTO.Date_start,
                Date_end = calendarDTO.Date_end,
                Id_workshop_id = calendarDTO.Id_workshop_id,
            };

            _context.Calendars.Add(newCalendar);
            await _context.SaveChangesAsync();

            return newCalendar;
        }

        public async Task<Calendars?> UpdateWorkshopCalendarAsync(UpdateCalendarDTO calendarDTO, int calendarId)
        {
            var calendar = await _context.Calendars.FindAsync(calendarId);

            if (calendar == null) return null;

            calendar.Date_start = calendarDTO.Date_start;
            calendar.Date_end = calendarDTO.Date_end;

            await _context.SaveChangesAsync();

            return calendar;
        }

        public async Task<bool> DeleteWorkshopCalendarAsync(int calendarId)
        {
            var calendar = await _context.Calendars.FindAsync(calendarId);

            if (calendar == null) return false;

            _context.Calendars.Remove(calendar);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
