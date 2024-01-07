using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private IList<IDecoration> decorations;
        private IList<IFish> fish;

        private Aquarium()
        {
            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
        }
        protected Aquarium(string name, int capacity)
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort
        {
            get
            {
                return decorations.Sum(d => d.Comfort);
            }
        }

        public ICollection<IDecoration> Decorations
            => this.decorations;

        public ICollection<IFish> Fish
            => this.fish;
        public void AddFish(IFish fish)
        {
            if (this.Fish.Count == this.Capacity)
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.NotEnoughCapacity);
            }
            this.Fish.Add(fish);
        }
        public bool RemoveFish(IFish fish)
        {
            return this.Fish.Remove(fish);
        }
        public void AddDecoration(IDecoration decoration)
        {
            this.Decorations.Add(decoration);
        }
        public void Feed()
        {
            foreach (var fish in this.Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            IList<string> fishList = new List<string>();
            foreach (var fish in this.Fish)
            {
                fishList.Add(fish.Name);
            }
            string result = this.Fish.Count == 0 ? "none" : $"{string.Join(", ", fishList)}";
            sb.AppendLine($"Fish: {result}");
            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().Trim();
        }


    }
}
