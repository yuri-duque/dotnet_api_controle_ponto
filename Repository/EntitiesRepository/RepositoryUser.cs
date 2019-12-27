using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Contex;
using System.Linq;

namespace Repository.EntitiesRepository
{
    public class RepositoryUser : Repository<User>
    {
        readonly BaseContext _ctx = new BaseContext();

        public User Login(User usuario)
        {
            if (usuario != null && !string.IsNullOrEmpty(usuario.PasswordHash))
            {
                var passwordEncrypt = User.Encrypt(usuario.PasswordHash);
                var user = _ctx.Set<User>()
                    .Include(x => x.Roles)
                    .FirstOrDefault(x =>
                        x.UserName.Equals(usuario.UserName.Trim()) &&
                        x.PasswordHash.Equals(passwordEncrypt)
                );

                return user;
            }

            return null;
        }

        public override void Save(User obj)
        {
            obj.PasswordHash = User.Encrypt(obj.PasswordHash);

            base.Save(obj);
        }

        public User FindByName(string userName)
        {
            return _ctx.Set<User>().FirstOrDefault(x => x.UserName.Equals(userName));
        }
    }
}
