using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface ISubscriptions
    {
        Task<List<Subscriptions>> GetAllSubscriptionsAsync();
        Task<List<Subscriptions>> GetSubscriptionsByUserAsync(int userId);
        Task<Subscriptions?> SubscribeAsync(SubscriptionDTO subscriptionDTO, int userId);
        Task<bool> UnsubscribeAsync(int workshopId, int userId);
    }
}
