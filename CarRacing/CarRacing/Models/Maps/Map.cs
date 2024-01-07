using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            IRacer winner = null;
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return Utilities.Messages.OutputMessages.RaceCannotBeCompleted;
            }
            else if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                winner = racerTwo;
                return string.Format(Utilities.Messages.OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            else if (!racerTwo.IsAvailable() && racerOne.IsAvailable())
            {
                winner = racerOne;
                return string.Format(Utilities.Messages.OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            racerOne.Race();
            racerTwo.Race();

            double racerOneMultiplier = racerOne.RacingBehavior == "strict" ? 1.2 : 1.1;
            double racerTwoMultiplier = racerTwo.RacingBehavior == "strict" ? 1.2 : 1.1;
            double racerOneChanceOfWinning = racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOneMultiplier;
            double racerTwoChanceOfWinning = racerTwo.Car.HorsePower * racerOne.DrivingExperience * racerTwoMultiplier;

            if (racerOneChanceOfWinning > racerTwoChanceOfWinning)
            {
                winner = racerOne;
            }
            else
            {
                winner = racerTwo;
            }

            return string.Format(Utilities.Messages.OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);
        }
    }
}
