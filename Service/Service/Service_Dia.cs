using Domain;
using Repository.EntitiesRepository;
using System.Collections.Generic;

namespace Service.Service
{
    public class Service_Dia
    {
        private Repository_Dia _repository = new Repository_Dia();

        public IEnumerable<Dia> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
