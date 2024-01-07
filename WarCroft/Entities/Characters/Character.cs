using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
		private string name;
        private double health;
        private double armor;
        public string Name
		{
			get { return name; }
			private set 
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(Constants.ExceptionMessages.CharacterNameInvalid);

                }
				name = value; 
			}
		}

        public double BaseHealth { get; set; }
		public double Health
		{
			get { return health; }
			set 
			{
				if (value>BaseHealth)
				{
					this.health = BaseHealth;
				}
				else if (value < 0)
				{
					this.health = 0;
				}
				else
				{
                    health = value;
                }
			}
		}
        public double BaseArmor { get; set; }

		public double Armor
		{
			get { return armor; }
			set 
			{
				if (value < 0)
				{
					this.armor = 0;
				}
				else
				{
                    armor = value;
                }
			}
		}
        public double AbilityPoints { get; set; }
        public Bag Bag { get; set; }
        public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}
	}
}