using Microsoft.EntityFrameworkCore;

namespace ExploreWords.WebApi
{
	public class ExploreWordsDbContext : DbContext
	{
		public DbSet<Module> Modules { get; set; }
		public DbSet<Word> Words { get; set; }

		public ExploreWordsDbContext(DbContextOptions<ExploreWordsDbContext> dbContextOptions) : base(dbContextOptions)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Module>().HasKey(x => x.Id);
			modelBuilder.Entity<Module>().Property(x => x.Name).IsRequired();
			modelBuilder.Entity<Module>().HasData(new Module
			{
				Id = Guid.NewGuid(),
				Name = "New Module",
			});

			modelBuilder.Entity<Word>().HasKey(x => x.Id);
			modelBuilder.Entity<Word>().Property(x => x.Text).IsRequired().IsUnicode();
			modelBuilder.Entity<Word>().Property(x => x.Translate).IsRequired();


			modelBuilder.Entity<Module>().HasMany(x => x.Words).WithOne(x => x.Module).HasForeignKey(x => x.ModuleId);
		}
	}
}
