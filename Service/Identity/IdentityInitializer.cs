using Domain;
using Repository.EntitiesRepository;
using System.Collections.Generic;

namespace Service.Identity
{
    public class IdentityInitializer
    {
        private readonly RepositoryUser _repository = new RepositoryUser();

        public void Initialize()
        {
            CreateUser(
                new User()
                {
                    UserName = "admin",
                    Email = "admin@teste.com.br",
                    EmailConfirmed = true,
                    PasswordHash = "admin"
                }, Roles.ROLE_ADMIN);

            CreateUser(
                new User()
                {
                    UserName = "usuario",
                    Email = "usuario@teste.com.br",
                    EmailConfirmed = true,
                    PasswordHash = "usuario"
                });
        }

        private void CreateUser(
            User user,
            string initialRole = null)
        {
            if (_repository.FindByName(user.UserName) == null)
            {
                if (!string.IsNullOrEmpty(initialRole))
                {
                    user.Roles = new List<UserRoles>();
                    user.Roles.Add(new UserRoles() { Role = initialRole });
                }

                _repository.Save(user);
            }
        }
    }
}