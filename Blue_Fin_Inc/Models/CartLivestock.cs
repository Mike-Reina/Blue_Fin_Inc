using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public class CartLivestock : Livestock
    {
        // Constructor
        public CartLivestock(CareLevel _careLevel, Temperment _temperment, WaterType _waterType, string _colours, string _waterConditions, string _maxSize, string _name, string _discription, double _price) : 
            base(_careLevel, _temperment, _waterType, _colours, _waterConditions, _maxSize, _name, _discription, _price)
        {
            
        }

        public CartLivestock(): base()
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
