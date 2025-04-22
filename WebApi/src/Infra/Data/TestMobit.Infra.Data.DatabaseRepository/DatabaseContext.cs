using Microsoft.EntityFrameworkCore;
using TestMobit.Domain.Entities;

namespace TestMobit.Infra.Data.DatabaseRepository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
