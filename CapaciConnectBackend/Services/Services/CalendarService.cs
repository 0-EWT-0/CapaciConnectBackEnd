using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Calendars;
using CapaciConnectBackend.DTOS.Responses;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class CalendarService : ICalendar
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public CalendarService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Calendars>> GetAllWorkshopCalendarsAsync()
        {
            try
            {
                var calendars = await _context.Calendars.AsNoTracking().ToListAsync();

                return calendars;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Calendar: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Calendars>();
            }
        }

        public async Task<List<Calendars>> GetWorkshopCalendarsAsync(int workshopId)
        {
            try
            {
                var calendar = await _context.Calendars.Where(c => c.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

                return calendar;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Calendar: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<Calendars?> CreateWorkshopCalendarAsync(CalendarDTO calendarDTO)
        {
            //var exists = await _context.Calendars.AnyAsync(c => c.Id_workshop_id == calendarDTO.Id_workshop_id);

            //if (exists) return null;
            try
            {
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Calendar: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<Calendars?> UpdateWorkshopCalendarAsync(UpdateCalendarDTO calendarDTO, int calendarId)
        {
            try
            {
                var calendar = await _context.Calendars.FindAsync(calendarId);

                if (calendar == null) return null;

                calendar.Date_start = calendarDTO.Date_start;
                calendar.Date_end = calendarDTO.Date_end;

                await _context.SaveChangesAsync();

                return calendar;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Calendar: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }

        public async Task<bool> DeleteWorkshopCalendarAsync(int calendarId)
        {
            try
            {
                var calendar = await _context.Calendars.FindAsync(calendarId);

                if (calendar == null) return false;

                _context.Calendars.Remove(calendar);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Calendar: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }

        }

    }
}
