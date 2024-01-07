using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core.Contracts
{
    public class Controller : IController
    {
        private IRepository<IBunny> bunnies;
        private IRepository<IEgg> eggs;

        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(bunny);
            return string.Format(Utilities.Messages.OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            if (bunnies.FindByName(bunnyName) == null)
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InexistentBunny);
            }

            IBunny bunny = bunnies.FindByName(bunnyName);
            Dye newDye = new Dye(power);

            bunnies.FindByName(bunnyName).AddDye(newDye);

            return string.Format(Utilities.Messages.OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg newEgg = new Egg(eggName, energyRequired);

            eggs.Add(newEgg);

            return string.Format(Utilities.Messages.OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            bool areSuitable = bunnies.Models.Any(b => b.Energy >= 50);
            if (!areSuitable)
            {
                throw new InvalidOperationException("There is no bunny ready to start coloring!");
            }
            while (egg.EnergyRequired > 0)
            {
                int maxEnergy = bunnies.Models.Max(b => b.Energy);
                IBunny bunny = bunnies.Models.First(b => b.Energy == maxEnergy);

                while (bunny.Energy > 0)
                {
                    bunny.Work();
                    if (bunny.Energy <= 0)
                    {
                        bunnies.Remove(bunny);
                    }
                    egg.GetColored();
                    if (egg.EnergyRequired == 0)
                    {
                        break;
                    }
                }
            }
            string result = "";
            if (egg.IsDone())
            {
                result = "done";
            }
            else result = "not done";
            return $"Egg {eggName} is {result}.";
        }

        public string Report()
        {
            int coloredEggs = 0;
            foreach (var egg in eggs.Models)
            {
                if (egg.IsDone())
                {
                    coloredEggs++;
                }
            }

            StringBuilder sb= new StringBuilder();
            sb.AppendLine($"{coloredEggs} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}")
                .AppendLine($"Energy: {bunny.Energy}")
                .AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }

            return sb.ToString().Trim();
        }
    }
}
