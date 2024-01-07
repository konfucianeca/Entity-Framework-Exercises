namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Races.Contracts;
    using Contracts;
    public class RaceRepository : IRepository<IRace>
    {
        private IList<IRace> Models;
        public RaceRepository()
        {
            Models = new List<IRace>();
        }
        public void Add(IRace model)
        {
            this.Models.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
            => (IReadOnlyCollection<IRace>)this.Models;

        public IRace GetByName(string name)
            => Models.FirstOrDefault(m => m.Name == name);

        public bool Remove(IRace model)
            => this.Models.Remove(model);
    }
}
