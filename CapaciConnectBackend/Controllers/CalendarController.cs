using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Calendars;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendar _calendarService;

        public CalendarController(ICalendar calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet("AllCalendars")]

        public async Task<IActionResult> GetAllCalendars()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2")
            {
                var calendars = await _calendarService.GetAllWorkshopCalendarsAsync();
                return Ok(calendars);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("CalendarsByWorkshopId/{workshopId}")]

        public async Task<IActionResult> GetCalendarsByWorkshop([FromRoute] int workshopId)
        {
            var calendars = await _calendarService.GetWorkshopCalendarsAsync(workshopId);

            if (calendars == null)
            {
                return NotFound(new { message = "No calendars found for this workshop." });
            }

            return Ok(calendars);
        }

        [HttpPost("CreateCalendar")]

        public async Task<IActionResult> CreateCalendar([FromBody] CalendarDTO calendarDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

            if (role == "1" || role == "2")
            {
                var createdCalendar = await _calendarService.CreateWorkshopCalendarAsync(calendarDTO);

                if (createdCalendar == null)
                {
                    return BadRequest(new { message = "Calendar for this workshop already exists." });
                }

                return Ok(createdCalendar);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpPut("UpdateCalendar/{calendarId}")]

        public async Task<IActionResult> UpdateCalendar([FromBody] UpdateCalendarDTO calendarDTO, [FromRoute] int calendarId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2")
            {
                var calendar = await _calendarService.UpdateWorkshopCalendarAsync(calendarDTO, calendarId);


                if (calendar == null)
                {
                    return BadRequest(new { message = "Calendar not found." });
                }

                return Ok(calendar);

            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpDelete("DeleteCalendar/{calendarId}")]

        public async Task<IActionResult> DeleteCalendar([FromRoute] int calendarId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2")
            {
                var deleted = await _calendarService.DeleteWorkshopCalendarAsync(calendarId);

                if (!deleted)
                {
                    return NotFound(new { message = "Calendar not found." });
                }

                return Ok(new { message = "Calendar deleted." });
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }
    }
}