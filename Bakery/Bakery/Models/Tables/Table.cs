using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private readonly IList<IBakedFood> foodOrders;
        private readonly IList<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;

        private Table()
        {
            this.foodOrders = new List<IBakedFood>();
            this.drinkOrders = new List<IDrink>();
        }
        protected Table(int tableNumber, int capacity, decimal pricePerPerson) : this()
        {

            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
        }
        protected IReadOnlyCollection<IBakedFood> FoodOrders
            => (IReadOnlyCollection<IBakedFood>)foodOrders;
        protected IReadOnlyCollection<IDrink> DrinkOrders
            => (IReadOnlyCollection<IDrink>)drinkOrders;
        public int TableNumber { get; private set; }

        public int Capacity
        {
            get { return this.capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidTableCapacity);
                }
                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get { return this.numberOfPeople; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidNumberOfPeople);
                }
                this.numberOfPeople = value;
            }
        }
        public decimal PricePerPerson { get; private set; }

        public bool IsReserved { get; private set; }

        public abstract decimal Price { get; }

        public void Clear()
        {
            this.foodOrders.Clear();
            this.drinkOrders.Clear();
            this.numberOfPeople = 0;
            this.IsReserved = false;
        }

        public decimal GetBill()
        {
            decimal foodPrice = 0;
            decimal drinksPrice = 0;
            foreach (var food in foodOrders)
            {
                foodPrice += food.Price;
            }
            foreach (var drink in drinkOrders)
            {
                drinksPrice += drink.Price;
            }

            decimal consumedFoodAndDrink = foodPrice + drinksPrice;
            decimal initialTablePrice = this.PricePerPerson * this.NumberOfPeople;
            return consumedFoodAndDrink + initialTablePrice;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Price per Person: {this.PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            this.IsReserved = true;
            this.NumberOfPeople = numberOfPeople;
        }
    }
}
