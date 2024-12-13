using FoosLeague.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FoosLeague.Data.Contexts;

public class ReadContext(ReadContext.ReadonlyContext context)
{
    private readonly ReadonlyContext context = context ?? throw new ArgumentNullException(nameof(context));

    public IQueryable<Player> Players => GetSet<Player>();
    private IQueryable<T> GetSet<T>() where T : class => context.Set<T>().AsNoTracking();

    public class ReadonlyContext : IdentityDbContext<IdentityUser>
    {
        public ReadonlyContext(DbContextOptions<ReadonlyContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>();
        }
    }
}
