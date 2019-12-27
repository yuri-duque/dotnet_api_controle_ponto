using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserRoles
    {
        [Required, Key]
        public int Id { get; set; }
        public string Role { get; set; }

        [Required]
        public long IdUser { get; set; }
        public User User { get; set; }

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<UserRoles>();
            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();

            map.HasOne(x => x.User).WithMany(x => x.Roles).HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
