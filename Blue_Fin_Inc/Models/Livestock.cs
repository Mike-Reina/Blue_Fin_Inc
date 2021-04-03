using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    // enums
    public enum CareLevel { Easy, Medium, Hard}
    public enum Temperment { Peaceful,[Display (Name = "Semi-Aggressive")] SemiAggressive, Aggressive }
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
        [Required]
        public CareLevel CareLevel 
        {
            get => careLevel;

            set
            {
                careLevel = value;
            }
        }
        [Required]
        public Temperment Temperment
        {
            get => temperment;

            set
            {
                temperment = value;
            }
        }
        [Required]
        public WaterType WaterType
        {
            get => waterType;

            set
            {
                waterType = value;
            }
        }
        [Required]
        public string Colours
        {
            get => colours;

            set
            {
                colours = value;
            }
        }
        [Required]
        public string WaterConditions
        {
            get => waterConditions;

            set
            {
                waterConditions = value;
            }
        }
        [Required]
        public string MaxSize
        {
            get => maxSize;

            set
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
        public Livestock() : base()
        {

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
