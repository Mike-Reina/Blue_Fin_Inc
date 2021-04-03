using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public class CartEquipment : Equipment
    {
        // Constructor
        public CartEquipment(string _manufacturer, int _lenght, int _width, int _height, string _colour, string _weight, string _name, string _discription, double _price) : 
            base(_manufacturer, _lenght, _width, _height, _colour, _weight, _name, _discription, _price)
        {

        }

        public CartEquipment() : base()
        {

        }

        // Methods
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
