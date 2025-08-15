using Microsoft.EntityFrameworkCore;
using UserGroupManagement.Models;

namespace UserGroupManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Users ↔ Groups
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                    j =>
                    {
                        j.HasKey("UsersId", "GroupsId");
                        j.HasData(
                            new { UsersId = 1, GroupsId = 1 }, 
                            new { UsersId = 2, GroupsId = 2 }, 
                            new { UsersId = 3, GroupsId = 3 }  
                        );
                    }
                );

            
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId);

           
            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Admins" },
                new Group { Id = 2, Name = "Editors" },
                new Group { Id = 3, Name = "Viewers" }
            );

           
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, PermissionName = "Level 1", GroupId = 1 },
                new Permission { Id = 2, PermissionName = "Level 2", GroupId = 1 },
                new Permission { Id = 3, PermissionName = "Level 1", GroupId = 2 },
                new Permission { Id = 4, PermissionName = "Level 2", GroupId = 2 },
                new Permission { Id = 5, PermissionName = "Level 1", GroupId = 3 }
            );

          
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice" },
                new User { Id = 2, Name = "Bob" },
                new User { Id = 3, Name = "Charlie" }
            );
        }
    }
    
}
