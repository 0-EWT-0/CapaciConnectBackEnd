using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Context
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) 
        {
        
        }

    }
}
