using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private IList<IEquipment> equipment;
        private IList<IAthlete> athletes;

        private Gym()
        {
            this.athletes = new List<IAthlete>();
            this.equipment = new List<IEquipment>();
        }
        protected Gym(string name, int capacity)
            : this()
        {
            this.Name = name;
            this.Capacity = capacity;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidGymName);
                }
                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight
        {
            get
            {
                double weightSum = 0;
                foreach (var item in this.equipment)
                {
                    weightSum += item.Weight;
                }
                return weightSum;
            }
        }

        public ICollection<IEquipment> Equipment
            => this.equipment;

        public ICollection<IAthlete> Athletes
            => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (this.athletes.Count == this.Capacity)
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.NotEnoughSize);
            }

            this.athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.athletes.Remove(athlete);
        }
        public void AddEquipment(IEquipment equipment)
        {
            this.equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in this.athletes)
            {
                athlete.Exercise();
            }
        }
        
        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            List<string> athlNames = new List<string>();
            foreach (var athlete in this.athletes)
            {
                athlNames.Add(athlete.FullName);
            }
            string allAthletes = this.athletes.Count == 0 ? "No athletes" : string.Join(", ", athlNames);
            sb.AppendLine($"Athletes: {allAthletes}");
            sb.AppendLine($"Equipment total count: {equipment.Count}");
            sb.AppendLine($"Equipment total weight: {equipment.Sum(e => e.Weight):f2} grams");

            return sb.ToString().TrimEnd();
        }

    }
}
