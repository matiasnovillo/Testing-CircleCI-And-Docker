using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.EntitiesConfiguration;
using FiyiStack.Areas.CMS.RoleBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.EntitiesConfiguration;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.EntitiesConfiguration;
using FiyiStack.Areas.CMS.UserBack.EntitiesConfiguration;
using FiyiStack.Areas.System.FailureBack.EntitiesConfiguration;
using FiyiStack.Areas.System.ParameterBack.Entities;
using FiyiStack.Areas.System.ParameterBack.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FiyiStack.DatabaseContexts
{
    public class FiyiStackDbContext(DbContextOptions<FiyiStackDbContext> options) : DbContext(options)
    {

        //System
        public DbSet<Failure> Failure { get; set; }
        public DbSet<Parameter> Parameter { get; set; }

        //CMS
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //System
            modelBuilder.ApplyConfiguration(new FailureConfiguration());
            modelBuilder.Entity<Failure>().ToTable("System.Failure");
            modelBuilder.ApplyConfiguration(new ParameterConfiguration());
            modelBuilder.Entity<Parameter>().ToTable("System.Parameter");

            //CMS
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<User>().ToTable("CMS.User");
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Role>().ToTable("CMS.Role");
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.Entity<Menu>().ToTable("CMS.Menu");
            modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());
            modelBuilder.Entity<RoleMenu>().ToTable("CMS.RoleMenu");
        }
    }
}
