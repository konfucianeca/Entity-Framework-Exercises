using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using System;
using System.Collections.Generic;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private ICollection<IDye> dyes;

        private Bunny()
        {
            this.dyes = new List<IDye>();
        }
        protected Bunny(string name, int energy)
            : this()
        {
            this.Name = name;
            this.Energy = energy;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidBunnyName);
                }
                this.name = value;
            }
        }

        public int Energy
        {
            get => this.energy;
            protected set
            {
                if (value < 0)
                {
                    this.energy = 0;
                }
                else
                {
                    this.energy = value;
                }
            }
        }

        public ICollection<IDye> Dyes
            => this.dyes;

        public void AddDye(IDye dye)
        {
            this.dyes.Add(dye);
        }

        public virtual void Work()
        {
            this.Energy -= 10;
        }
    }
}
