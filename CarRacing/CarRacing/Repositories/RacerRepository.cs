using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private readonly IList<IRacer> models;

        public RacerRepository()
        {
            this.models = new List<IRacer>();
        }
        public IReadOnlyCollection<IRacer> Models
            => (IReadOnlyCollection<IRacer>)this.models;

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidAddRacerRepository);
            }

            this.models.Add(model);
        }

        public IRacer FindBy(string property)
            => this.models.FirstOrDefault(m => m.Username == property);

        public bool Remove(IRacer model)
            =>this.models.Remove(model);
    }
}
