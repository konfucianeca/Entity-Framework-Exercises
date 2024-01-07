using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private IList<IDriver> Models;
        public DriverRepository()
        {
            this.Models = new List<IDriver>();
        }
        public void Add(IDriver model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
            => (IReadOnlyCollection<IDriver>)this.Models;

        public IDriver GetByName(string name)
            => Models.FirstOrDefault(m => m.Name == name);

        public bool Remove(IDriver model)
        {
            return this.Models.Remove(model);
        }
    }
}
