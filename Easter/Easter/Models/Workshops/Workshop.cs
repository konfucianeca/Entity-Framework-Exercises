using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {
        }
        public void Color(IEgg egg, IBunny bunny)
        {
            IDye currDye = bunny.Dyes.FirstOrDefault(d => d.IsFinished() == false);
            while (bunny.Energy != 0 && bunny.Dyes.Any(d => d.IsFinished() == false) && bunny.Dyes.Any(d => d.IsFinished() == false))
            {
                bunny.Work();
                egg.GetColored();
                currDye.Use();
                if (currDye.IsFinished())
                {
                    currDye = bunny.Dyes.FirstOrDefault(d => d.IsFinished() == false);
                }
            }
        }
    }
}
