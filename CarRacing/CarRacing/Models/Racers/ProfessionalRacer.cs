using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const int DrivingExperience = 30;
        private const string RacingBehavior = "strict";

        public ProfessionalRacer(string username, ICar car) 
            : base(username, RacingBehavior, DrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            base.DrivingExperience += 10;
        }
    }
}
