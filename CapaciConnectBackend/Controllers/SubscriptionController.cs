using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptions _subscriptionService;

        public SubscriptionController(ISubscriptions subscription)
        {
            _subscriptionService = subscription;
        }

        [HttpGet("AllSubscriptions")]

        public async Task<IActionResult> GetAllSubscriptions()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
                return Ok(subscriptions);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("UserSubscriptions")]

        public async Task<IActionResult> GetUserSubscriptions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized." });
            }

            var subscriptions = await _subscriptionService.GetSubscriptionsByUserAsync(int.Parse(userId));

            if (subscriptions == null || !subscriptions.Any())
            {
                return NotFound(new { message = "No subscriptions found." });
            }

            return Ok(subscriptions);
        }

        [HttpPost("Subscribe")]

        public async Task<IActionResult> Subscribe([FromBody] SubscriptionDTO subscriptionDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized." });
            }

            var subscription = await _subscriptionService.SubscribeAsync(subscriptionDTO, int.Parse(userId));

            if (subscription == null)
            {
                return Conflict(new { message = "User is already subscribed to this workshop." });
            }

            return Ok(subscription);
        }

        [HttpDelete("Unsubscribe/{workshopId}")]

        public async Task<IActionResult> Unsubscribe([FromRoute] int workshopId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized." });
            }

            var deleted = await _subscriptionService.UnsubscribeAsync(workshopId, int.Parse(userId));

            if (!deleted)
            {
                return NotFound(new { message = "Subscription not found." });
            }

            return Ok(new { message = "Successfully unsubscribed." });
        }
    }
}
