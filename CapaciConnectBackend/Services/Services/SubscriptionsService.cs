using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class SubscriptionsService : ISubscriptions
    {

        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public SubscriptionsService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }
        public async Task<List<Subscriptions>> GetAllSubscriptionsAsync()
        {
            try
            {
                var subscriptions = await _context.Subscriptions.AsNoTracking().ToListAsync();

                return subscriptions;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Subscriptions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Subscriptions>();
            }

        }

        public async Task<List<Subscriptions>> GetSubscriptionsByUserAsync(int userId)
        {
            try
            {
                return await _context.Subscriptions.Where(s => s.Id_user_id == userId).AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Subscriptions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Subscriptions>();
            }
        }

        public async Task<Subscriptions?> SubscribeAsync(SubscriptionDTO subscriptionDTO, int userId)
        {
            try
            {
                var alreadySubscribed = await _context.Subscriptions
                    .AnyAsync(s => s.Id_user_id == userId && s.Id_workshop_id == subscriptionDTO.Id_workshop_id);

                if (alreadySubscribed) return null;

                var newSubscription = new Subscriptions
                {
                    Id_user_id = userId,
                    Id_workshop_id = subscriptionDTO.Id_workshop_id,
                };

                _context.Subscriptions.Add(newSubscription);
                await _context.SaveChangesAsync();

                return newSubscription;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Subscriptions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }

        public async Task<bool> UnsubscribeAsync(int workshopId, int userId)
        {
            try
            {
                var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id_user_id == userId && s.Id_workshop_id == workshopId);

                if (subscription == null) return false;

                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Subscriptions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }
    }
}
