using Domain;
using Repository.Contex;
using System.Linq;

namespace Repository.EntitiesRepository
{
    public class Repository_User : Repository<User>
    {
        readonly BaseContext _ctx = new BaseContext();

        public User Login(User usuario)
        {
            var passwordEncrypt = User.Encrypt(usuario.Password);
            var user = _ctx.Set<User>().FirstOrDefault(x =>
                x.UserName.Equals(usuario.UserName.Trim()) &&
                x.Password.Equals(passwordEncrypt)
            );

            return user;
        }

        public override void Save(User obj)
        {
            obj.Password = User.Encrypt(obj.Password);

            base.Save(obj);
        }
    }
}
