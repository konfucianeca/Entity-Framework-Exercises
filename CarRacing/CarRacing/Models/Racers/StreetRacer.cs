using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const int DrivingExperience = 10;
        private const string RacingBehavior = "aggressive";
        public StreetRacer(string username, ICar car)
            : base(username, RacingBehavior, DrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            base.DrivingExperience += 5;
        }
    }
}
