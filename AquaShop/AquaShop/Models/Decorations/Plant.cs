namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int PlantConfort = 5;
        private const decimal PlantPrice = 10;
        public Plant() 
            : base(PlantConfort, PlantPrice)
        {
        }
    }
}
