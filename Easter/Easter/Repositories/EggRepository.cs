using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly IList<IEgg> models;
        public EggRepository()
        {
            models = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models
            => (IReadOnlyCollection<IEgg>)this.models;

        public void Add(IEgg model)
        {
            this.models.Add(model);
        }

        public IEgg FindByName(string name)
            => this.models.FirstOrDefault(e => e.Name == name);

        public bool Remove(IEgg model)
            => this.models.Remove(model);
    }
}
