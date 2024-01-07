using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private IRepository<IEquipment> equipment;
        private IList<IGym> gyms;
        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym = null;
            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }

            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }

            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gym);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipmentEntity = null;
            if (equipmentType == "BoxingGloves")
            {
                equipmentEntity = new BoxingGloves();
            }

            else if (equipmentType == "Kettlebell")
            {
                equipmentEntity = new Kettlebell();
            }

            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidEquipmentType);
            }

            equipment.Add(equipmentEntity);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipmentToInsert = equipment.FindByType(equipmentType);
            if (equipmentToInsert == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.InexistentEquipment, equipmentType));
            }

            equipment.Remove(equipmentToInsert);
            gyms.FirstOrDefault(g => g.Name == gymName).AddEquipment(equipmentToInsert);
            return string.Format(Utilities.Messages.OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete = null;
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }

            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }

            else
            {
                throw new InvalidOperationException(Utilities.Messages.ExceptionMessages.InvalidAthleteType);
            }

            IGym gymToAddAthlete = gyms.FirstOrDefault(g => g.Name == gymName);
            if (athleteType == "Boxer")
            {
                if (gymToAddAthlete.GetType().Name != "BoxingGym")
                {
                    return Utilities.Messages.OutputMessages.InappropriateGym;
                }
                
            }

            else
            {
                if (gymToAddAthlete.GetType().Name != "WeightliftingGym")
                {
                    return Utilities.Messages.OutputMessages.InappropriateGym;
                }
            }

            gyms.FirstOrDefault(g => g.Name == gymName).AddAthlete(athlete);
            return string.Format(Utilities.Messages.OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            IGym activeGym = gyms.FirstOrDefault(g => g.Name == gymName);

            gyms.FirstOrDefault(g => g.Name == gymName).Exercise();

            return $"Exercise athletes: {activeGym.Athletes.Count}.";
        }
        public string EquipmentWeight(string gymName)
        {
            double equWeight = gyms.FirstOrDefault(g => g.Name == gymName).EquipmentWeight;

            return string.Format($"The total weight of the equipment in the gym {gymName} is {equWeight:f2} grams.");
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IGym gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().Trim();
        }


    }
}
