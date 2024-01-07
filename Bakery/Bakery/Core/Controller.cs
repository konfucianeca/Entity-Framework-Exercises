using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private IList<IBakedFood> bakedFoods;
        private IList<IDrink> drinks;
        private IList<ITable> tables;
        decimal totalIncome = 0;
        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }
        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = null;
            if (type == "Bread")
            {
                food = new Bread(name, price);
            }
            else if (type == "Cake")
            {
                food = new Cake(name, price);
            }

            bakedFoods.Add(food);
            return string.Format(Utilities.Messages.OutputMessages.FoodAdded, name, type);
        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = null;
            if (type == "Tea")
            {
                drink = new Tea(name, portion, brand);
            }
            else if (type == "Water")
            {
                drink = new Water(name, portion, brand);
            }

            drinks.Add(drink);
            return string.Format(Utilities.Messages.OutputMessages.DrinkAdded, name, brand);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;
            if (type == "InsideTable")
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else if (type == "OutsideTable")
            {
                table = new OutsideTable(tableNumber, capacity);
            }

            tables.Add(table);
            return string.Format(Utilities.Messages.OutputMessages.TableAdded, tableNumber);
        }
        public string ReserveTable(int numberOfPeople)
        {
            ITable tableToReserve = tables.FirstOrDefault(t => t.Capacity >= numberOfPeople && t.IsReserved == false);
            if (tableToReserve == null)
            {
                return string.Format(Utilities.Messages.OutputMessages.ReservationNotPossible, numberOfPeople);
            }

            else
            {
                tableToReserve.Reserve(numberOfPeople);
                return string.Format(Utilities.Messages.OutputMessages.TableReserved, tableToReserve.TableNumber, numberOfPeople);
            }
        }
        public string OrderFood(int tableNumber, string foodName)
        {
            IBakedFood food = bakedFoods.FirstOrDefault(f => f.Name == foodName);
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(Utilities.Messages.OutputMessages.WrongTableNumber, tableNumber);
            }
            if (food == null)
            {
                return string.Format(Utilities.Messages.OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return string.Format(Utilities.Messages.OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }
        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(Utilities.Messages.OutputMessages.WrongTableNumber, tableNumber);
            }

            IDrink drink = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand == drinkBrand);
            if (drink == null)
            {
                return string.Format(Utilities.Messages.OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            table.OrderDrink(drink);
            return string.Format(Utilities.Messages.OutputMessages.DrinkOrderSuccessful, tableNumber, drinkName, drinkBrand);
        }
        public string LeaveTable(int tableNumber)
        {
            ITable tableToLeave = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            decimal tableBill = tableToLeave.GetBill();
            tables.FirstOrDefault(t => t.TableNumber == tableNumber).Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {tableNumber}");
            sb.AppendLine($"Bill: {tableBill:f2}");

            totalIncome += tableBill;

            return sb.ToString().Trim();
        }
        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            var freeTables = tables.Where(t => t.IsReserved == false);
            foreach (var table in freeTables)
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }
            return sb.ToString().Trim();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }








    }
}
