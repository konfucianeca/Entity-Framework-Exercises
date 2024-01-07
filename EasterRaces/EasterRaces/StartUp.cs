using EasterRaces.Core.Contracts;
using EasterRaces.IO;
using EasterRaces.IO.Contracts;
using EasterRaces.Core.Entities;

namespace EasterRaces
{
    public class StartUp
    {
        public static void Main()
        {
            IChampionshipController controller = new ChampionshipController(); //new ChampionshipController();
            IReader reader = new ConsoleReader();
            //IWriter writer = new ConsoleWriter();
            IWriter writer = new FileWriter();

            Engine enigne = new Engine(controller, reader, writer);
            enigne.Run();
        }
    }
}
