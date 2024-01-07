using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private IList<ICar> Models;
        public CarRepository()
        {
            this.Models = new List<ICar>();
        }
        public void Add(ICar model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
            => (IReadOnlyCollection<ICar>)this.Models;

        public ICar GetByName(string name)
            => Models.FirstOrDefault(m => m.Model == name);

        public bool Remove(ICar model)
        {
            return this.Models.Remove(model);
        }
    }
}
