using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public class Equipment : Product
    {
        // fields
        private string manufacturer;
        private int lenght;
        private int width;
        private int height;
        private string colour;
        private string weight;

        // properties
        public string Manufacturer
        {
            get => manufacturer;

            private set
            {
                manufacturer = value;
            }
        }

        public int Lenght
        {
            get => lenght;

            private set
            { 
                if (value > 0)
                {
                    lenght = value;
                }
                else
                {
                    throw new ArgumentException("Lenght must be grater than 0.");
                }
            }
        }

        public int Width
        {
            get => width;

            private set
            {
                if (value > 0)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentException("Widght must be grater than 0.");
                }
            }
        }

        public int Height
        {
            get => height;

            private set
            {
                if (value > 0)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentException("Height must be grater than 0.");
                }
            }
        }
        public string Colour
        {
            get => colour;

            private set
            {
                colour = value;
            }
        }

        public string Weight
        {
            get => weight;

            private set
            {
                weight = value;
            }
        }

        // constructor
        public Equipment(string _manufacturer, int _lenght, int _width, int _height, string _colour, string _weight, int _productCode, string _name, string _discription, double _price) : base(_productCode, _name, _discription, _price)
        {
            Manufacturer = _manufacturer;
            Lenght = _lenght;
            Width = _width;
            Height = _height;
            Colour = _colour;
            Weight = _weight;
        }

        //methods
        public override void AddStock(int amount)
        {
            base.AddStock(amount);
        }

        public override void RemoveStock(int amount)
        {
            base.RemoveStock(amount);
        }
    }
}
