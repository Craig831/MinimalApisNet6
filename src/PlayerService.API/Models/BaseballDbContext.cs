namespace PlayerService.API.Models
{
    public class BaseballDbContext : DbContext
    {
        public BaseballDbContext(DbContextOptions<BaseballDbContext> options) : base(options) { }

        public DbSet<MlbPlayer> MlbPlayers => Set<MlbPlayer>();
        public DbSet<MlbTeam> MlbTeams => Set<MlbTeam>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MlbPlayer>(entity =>
            {
                entity.HasKey(e => new { e.MlbPlayerId });

                entity.ToTable("MlbPlayer");
            });

            modelBuilder.Entity<MlbTeam>(entity =>
            {
                entity.HasKey(e => new { e.MlbTeamId });

                entity.ToTable("MlbTeam");
            });
        }
    }
}
