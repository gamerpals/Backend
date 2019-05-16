using Microsoft.EntityFrameworkCore;

namespace GamerPalsBackend.DataObjects.Models
{
    public class PalsContext : DbContext
    {
        public PalsContext()
        {

        }
        public PalsContext(DbContextOptions<PalsContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameServer> Servers { get; set; }
        public DbSet<ActiveSearch> ActiveSearches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<SearchSettings> SearchSettings { get; set; }
        public DbSet<SearchType> SearchTypes { get; set; }
        public DbSet<UserOptions> UserOptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserOptionRoles> UserOptionRoles { get; set; }
        public DbSet<ActiveSearchUser> ActiveSearchUsers { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserOptionRoles>().HasKey(sc => new { sc.UserOptionId, sc.RoleId });
            modelBuilder.Entity<ActiveSearchUser>().HasKey(sc => new { sc.ActiveSearchID, sc.UserID });
            modelBuilder.Entity<UserLanguage>().HasKey(sc => new { sc.LanguageID, sc.UserID });
            modelBuilder.Entity<UserGame>().HasKey(sc => new { sc.GameID, sc.UserID });
            modelBuilder.Entity<SearchParameter>().HasKey(sc => new {sc.ActiveSearchID, sc.ParameterID});
        }
        public DbSet<SystemSettings> SystemSettings { get; set; }
        public DbSet<SearchParameter> SearchParameters { get; set; }
        public DbSet<UserGame> UserGame { get; set; }
        public DbSet<GlobalParameters> GlobalParameters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GamerPals;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").EnableSensitiveDataLogging();
            }
        }

    }
}
