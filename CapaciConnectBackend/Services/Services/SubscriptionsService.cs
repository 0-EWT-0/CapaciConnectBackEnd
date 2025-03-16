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
        public SubscriptionsService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<Subscriptions>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _context.Subscriptions.AsNoTracking().ToListAsync();

            return subscriptions;
        }

        public async Task<List<Subscriptions>> GetSubscriptionsByUserAsync(int userId)
        {
            return await _context.Subscriptions
                .Where(s => s.Id_user_id == userId)
                .AsNoTracking()
                .ToListAsync();
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
                throw new Exception($"Error subscribing: {ex.Message}");
            }

        }

        public async Task<bool> UnsubscribeAsync(int workshopId, int userId)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id_user_id == userId && s.Id_workshop_id == workshopId);

            if (subscription == null) return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
