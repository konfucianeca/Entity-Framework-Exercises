using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly IList<IEquipment> models;

        public EquipmentRepository()
        {
            models = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models
            => (IReadOnlyCollection<IEquipment>)this.models;
        public void Add(IEquipment model)
        {
            this.models.Add(model);
        }

        public IEquipment FindByType(string type)
            => this.models.FirstOrDefault(m=>m.GetType().Name==type);

        public bool Remove(IEquipment model)
            => this.models.Remove(model);
    }
}
