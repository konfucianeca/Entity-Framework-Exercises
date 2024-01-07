﻿using Bakery.Models.Drinks.Contracts;
using System;
using System.Text;

namespace Bakery.Models.Drinks
{
    public abstract class Drink : IDrink
    {
        private string name;
        private int portion;
        private decimal price;
        private string brand;
        protected Drink(string name, int portion, decimal price, string brand)
        {
            this.Name = name;
            this.Portion = portion;
            this.Price = price;
            this.Brand = brand;
        }
        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidName);
                }
                this.name = value;
            }
        }
        public int Portion
        {
            get { return this.portion; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidPortion);
                }
                this.portion = value;
            }
        }

        public decimal Price
        {
            get { return this.price; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidPrice);
                }
                this.price = value;
            }
        }

        public string Brand
        {
            get { return this.brand; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidBrand);
                }
                this.brand = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} {this.Brand} - {this.Portion}ml - {this.Price:f2}lv");
            return sb.ToString().Trim();
        }
    }
}
