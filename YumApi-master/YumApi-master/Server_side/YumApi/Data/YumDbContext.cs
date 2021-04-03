using Microsoft.EntityFrameworkCore;
using YumApi.Models;

namespace YumApi.Data
{
    public class YumDbContext : DbContext
    {
        public YumDbContext(DbContextOptions<YumDbContext> options)
            : base(options)
        {
        }

        public DbSet<YumApi.Models.User> User { get; set; }

        public DbSet<YumApi.Models.Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Admin"
                });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 2,
                    RoleName = "User"
                });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 3,
                    RoleName = "Blogger"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "AdminUser",
                    Email = "admin@admin.com",
                    Password = "admin",
                    RoleId = 1,
                    UserProfile = null
                });

            modelBuilder.Entity<Nutrition>().HasData(
              new Nutrition
              {
                  Id = 1,
                  NutritionName= "CALORIES"
              });

            modelBuilder.Entity<Nutrition>().HasData(
             new Nutrition
             {
                 Id = 2,
                 NutritionName = "SODIUM"
             });

            modelBuilder.Entity<Nutrition>().HasData(
             new Nutrition
             {
                 Id = 3,
                 NutritionName = "FAT"
             });

            modelBuilder.Entity<Nutrition>().HasData(
             new Nutrition
             {
                 Id = 4,
                 NutritionName = "PROTEIN"
             });

            modelBuilder.Entity<Nutrition>().HasData(
             new Nutrition
             {
                 Id = 5,
                 NutritionName = "CARBS"
             });

            modelBuilder.Entity<Nutrition>().HasData(
             new Nutrition
             {
                 Id = 6,
                 NutritionName = "FIBER"
             });

            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                CategoryName = "North Indian"
            });

            modelBuilder.Entity<Category>().HasData(
         new Category
         {
             Id = 2,
             CategoryName = "Western"
         });

            modelBuilder.Entity<Category>().HasData(
       new Category
       {
           Id = 3,
           CategoryName = "SouthIndian"
       });
        }

        public DbSet<YumApi.Models.UserProfile> UserProfile { get; set; }

        public DbSet<YumApi.Models.Recipe> Recipe { get; set; }

        public DbSet<YumApi.Models.Recipe_Nutrition> Recipe_Nutrition { get; set; }
        public DbSet<YumApi.Models.Allergies> Allergies { get; set; }

        public DbSet<YumApi.Models.Nutrition> Nutrition { get; set; }
        public DbSet<YumApi.Models.Review> Review { get; set; }

        public DbSet<YumApi.Models.Recipe_Direction> Recipe_Direction { get; set; }

        public DbSet<YumApi.Models.CartIngredient> CartIngredient { get; set; }

        public DbSet<YumApi.Models.Ingredient> Ingredient { get; set; }

        public DbSet<YumApi.Models.Order> Order { get; set; }

        public DbSet<YumApi.Models.Category> Category { get; set; }
    }
}
