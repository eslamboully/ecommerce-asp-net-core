using System;
using Microsoft.EntityFrameworkCore;

namespace ecommerceAspCore.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RoleAccount> RoleAccounts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleAccount>( entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.AccountId });

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role_Account");

                    entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role_Role");
            });
        }
    }
}