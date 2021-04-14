using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Care Level")]
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
        [DisplayName("Water Type")]
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

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        // constructor
        public Livestock(CareLevel _careLevel, Temperment _temperment, WaterType _waterType, string _colours, string _waterConditions, string _maxSize, string _name, string _description, double _price) : base(_name, _description, _price)
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
