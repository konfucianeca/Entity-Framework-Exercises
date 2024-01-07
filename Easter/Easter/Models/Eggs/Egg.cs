using Easter.Models.Eggs.Contracts;
using System;

namespace Easter.Models.Eggs
{
    public class Egg : IEgg
    {
        private string name;
        private int energyRequired;
        public Egg(string name, int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidEggName);
                }
                this.name = value;
            }
        }

        public int EnergyRequired
        {
            get=>this.energyRequired;
            set
            {
                if (value<0)
                {
                    this.energyRequired = 0;
                }
                else
                {
                    this.energyRequired = value;
                }
            }
        }

        public void GetColored()
        {
            this.EnergyRequired -= 10;
        }

        public bool IsDone()
        {
            if (this.EnergyRequired==0)
            {
                return true;
            }
            return false;
        }
    }
}
