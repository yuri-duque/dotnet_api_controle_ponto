using Domain;
using Repository.EntitiesRepository;
using System.Collections.Generic;

namespace Service.Service
{
    public class ServiceDia
    {
        readonly RepositoryDia _repository = new RepositoryDia();

        public IEnumerable<Dia> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
