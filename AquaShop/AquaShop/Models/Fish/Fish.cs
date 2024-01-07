using AquaShop.Models.Fish.Contracts;
using System;

namespace AquaShop.Models.Fish
{
    public abstract class Fish : IFish
    {
        private string name;
        private string species;
        private decimal price;
        protected Fish(string name, string species, decimal price)
        {
            this.Name = name;
            this.Species = species;
            this.Price = price;
        }
        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidFishName);
                }
                this.name = value;
            }
        }

        public string Species
        {
            get => this.species;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidFishSpecies);
                }
                this.species = value;
            }
        }

        public int Size { get; set; }

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidFishPrice);
                }
                this.price = value;
            }
        }

        public abstract void Eat();

    }
}
