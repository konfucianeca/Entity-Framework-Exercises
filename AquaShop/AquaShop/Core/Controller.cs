using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private IRepository<IDecoration> decorations;
        private IList<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = null;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidAquariumType);
            }

            aquariums.Add(aquarium);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidDecorationType);
            }

            decorations.Add(decoration);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            if (decorations.FindByType(decorationType) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.InexistentDecoration, decorationType));
            }

            IDecoration usedDecoration = decorations.FindByType(decorationType);
            aquariums.FirstOrDefault(a => a.Name == aquariumName).AddDecoration(usedDecoration);
            decorations.Remove(usedDecoration);

            return string.Format(Utilities.Messages.OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidFishType);
            }

            if ((fishType == "FreshwaterFish" && aquarium.GetType().Name == "SaltwaterAquarium") || (fishType == "SaltwaterFish" && aquarium.GetType().Name == "FreshwaterAquarium"))
            {
                return Utilities.Messages.OutputMessages.UnsuitableWater;
            }
            aquariums.FirstOrDefault(a => a.Name == aquariumName).AddFish(fish);
            return string.Format(Utilities.Messages.OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            aquariums.FirstOrDefault(a => a.Name == aquariumName).Feed();

            return $"Fish fed: {aquarium.Fish.Count}";
        }

        public string CalculateValue(string aquariumName)
        {
            decimal fishPrice = 0;
            decimal decorationPrice = 0;
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            foreach (var fish in aquarium.Fish)
            {
                fishPrice += fish.Price;
            }

            foreach (var decoration in aquarium.Decorations)
            {
                decorationPrice += decoration.Price;
            }

            decimal ttlPrice = fishPrice + decorationPrice;

            return $"The value of Aquarium {aquariumName} is {ttlPrice:f2}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().Trim();
        }
    }
}
