using System.Data.Entity;
using Locator.Entity.Entities;

namespace Locator.DAL.EF
{
    public class LocatorContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(r => r.ID);
            modelBuilder.Entity<User>().Property(r => r.Login).IsRequired();
            modelBuilder.Entity<User>().Property(r => r.Password).IsRequired();

            modelBuilder.Entity<UserPush>().HasKey(r => r.ID);
            modelBuilder.Entity<UserPush>().Property(r => r.DeviceAppId).IsRequired();
            modelBuilder.Entity<UserPush>().HasRequired(r => r.User).WithMany().HasForeignKey(r => r.UserId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserFriends>().HasKey(r => r.ID);
            //modelBuilder.Entity<UserFriends>().HasRequired(r => r.User).WithMany().HasForeignKey(r => r.UserId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<UserFriends>().HasRequired(r => r.Friend).WithMany().HasForeignKey(r => r.FriendId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>().HasKey(r => r.ID);
            modelBuilder.Entity<Location>().HasRequired(r => r.FromUser).WithMany().HasForeignKey(r => r.FromUserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Location>().HasRequired(r => r.ToUser).WithMany().HasForeignKey(r => r.ToUserId).WillCascadeOnDelete(false);

        }
    }
}
