using CapaciConnectBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Context
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
            // Checar error al "dotnet ef database update"
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Workshops> Workshops { get; set; }
        public DbSet<Calendars> Calendars { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Multimedia> Multimedia { get; set; }
        public DbSet<Progressions> Progressions { get; set; }
        public DbSet<Rols> Rols { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<WorkshopMultimedia> WorkshopMultimedia { get; set; }
        public DbSet<WorkshopTypes> WorkshopTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<Users>().HasKey(u => u.Id_user);
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Id_rol_id);

            //Workshops
            modelBuilder.Entity<Workshops>().HasKey(w => w.Id_workshop);
            modelBuilder.Entity<Workshops>()
                .HasOne(w => w.WorkshopType)
                .WithMany(t => t.Workshops)
                .HasForeignKey(w => w.Id_type_id);
            modelBuilder.Entity<Workshops>()
                .HasOne(w => w.User)
                .WithMany(u => u.Workshops)
                .HasForeignKey(w => w.Id_user_id);

            //Calendars
            modelBuilder.Entity<Calendars>().HasKey(c => c.Id_calendar);
            modelBuilder.Entity<Calendars>()
                .HasOne(c => c.Workshop)
                .WithMany(w => w.Calendars)
                .HasForeignKey(c => c.Id_workshop_id);

            //Comments
            modelBuilder.Entity<Comments>().HasKey(c => c.Id_comment);
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Workshop)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.Id_workshop_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.Id_user_id)
                .OnDelete(DeleteBehavior.NoAction);

            //Logs
            modelBuilder.Entity<Logs>().HasKey(l => l.Id_log);
            //modelBuilder.Entity<Logs>()
            //    .HasOne(l => l.User)
            //    .WithMany(u => u.Logs)
            //    .HasForeignKey(l => l.Id_user_id);

            //Reports
            modelBuilder.Entity<Reports>().HasKey(r => r.Id_Report);
            modelBuilder.Entity<Reports>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.Id_user_id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Reports>()
                .HasOne(r => r.Workshops)
                .WithMany(w => w.Reports)
                .HasForeignKey(r => r.Id_workshop_id)
                .OnDelete(DeleteBehavior.NoAction);

            //Multimedia
            modelBuilder.Entity<Multimedia>().HasKey(m => m.Id_multimedia);
            modelBuilder.Entity<Multimedia>()
                .Property(m => m.Media_type)
                .HasConversion<string>();

            //Progressions
            modelBuilder.Entity<Progressions>().HasKey(p => p.Id_progression);
            modelBuilder.Entity<Progressions>()
                .HasOne(p => p.User)
                .WithMany(u => u.Progressions)
                .HasForeignKey(p => p.Id_user_id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Progressions>()
                .HasOne(p => p.Workshop)
                .WithMany(w => w.Progressions)
                .HasForeignKey(p => p.Id_workshop_id)
                .OnDelete(DeleteBehavior.NoAction);

            //Rols
            modelBuilder.Entity<Rols>().HasKey(r => r.Id_rol);

            //Sessions
            modelBuilder.Entity<Sessions>().HasKey(s => s.Id_session);
            modelBuilder.Entity<Sessions>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.Id_user_id);

            //Subscriptions
            modelBuilder.Entity<Subscriptions>().HasKey(s => s.Id_subscription);
            modelBuilder.Entity<Subscriptions>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.Id_user_id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Subscriptions>()
                .HasOne(s => s.Workshop)
                .WithMany(w => w.Subscriptions)
                .HasForeignKey(s => s.Id_workshop_id)
                .OnDelete(DeleteBehavior.NoAction);

            //WorkshopMultimedia
            modelBuilder.Entity<WorkshopMultimedia>()
                .HasKey(wm => new { wm.Id_workshop_id, wm.Id_multimedia_id });
            modelBuilder.Entity<WorkshopMultimedia>()
                .HasOne(wm => wm.Multimedia)
                .WithMany(m => m.WorkshopMultimedia)
                .HasForeignKey(wm => wm.Id_multimedia_id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<WorkshopMultimedia>()
                .HasOne(wm => wm.Workshop)
                .WithMany(w => w.WorkshopMultimedia)
                .HasForeignKey(wm => wm.Id_workshop_id)
                .OnDelete(DeleteBehavior.NoAction);

            //WorkshopTypes
            modelBuilder.Entity<WorkshopTypes>().HasKey(w => w.Id_type);
        }
    }
}
