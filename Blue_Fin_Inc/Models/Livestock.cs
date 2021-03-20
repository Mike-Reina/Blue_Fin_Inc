using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    // enums
    public enum CareLevel { Easy, Medium, Hard}
    public enum Temperment { Peaceful, SemiAggresive, Aggresive }
    public enum WaterType { Fresh, Salt }
    public class Livestock : Product
    {
        // fields
        private CareLevel careLevel;
        private Temperment temperment;
        private WaterType waterType;
        private string colours;
        private string waterConditions;
        private string maxSize;

        // properties
        public CareLevel CareLevel 
        {
            get => careLevel;

            private set
            {
                careLevel = value;
            }
        }

        public Temperment Temperment
        {
            get => temperment;

            private set
            {
                temperment = value;
            }
        }

        public WaterType WaterType
        {
            get => waterType;

            private set
            {
                waterType = value;
            }
        }

        public string Colours
        {
            get => colours;

            private set
            {
                colours = value;
            }
        }
        public string WaterConditions
        {
            get => waterConditions;

            private set
            {
                waterConditions = value;
            }
        }

        public string MaxSize
        {
            get => maxSize;

            private set
            {
                    maxSize = value;
            }
        }

        // constructor
        public Livestock(CareLevel _careLevel, Temperment _temperment, WaterType _waterType, string _colours, string _waterConditions, string _maxSize, int _productCode, string _name, string _discription, double _price) : base(_productCode, _name, _discription, _price)
        {
            CareLevel = _careLevel;
            Temperment = _temperment;
            WaterType = _waterType;
            Colours = _colours;
            WaterConditions = _waterConditions;
            MaxSize = _maxSize;
        }

        // methods 
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
