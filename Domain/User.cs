using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<User>();
            map.HasKey(x => x.Id);

            map.Property(x => x.Id).ValueGeneratedOnAdd();
            map.Property(x => x.UserName).IsUnicode();
            map.Property(x => x.Email).IsUnicode();
        }

        public static string Encrypt(string password)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string Decrypt(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
