using DataAnnotationsExtensions;
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
        [Required]
        public string Manufacturer
        {
            get => manufacturer;

            set
            {
                manufacturer = value;
            }
        }

        [Required]
        [Min(0, ErrorMessage = "Lenght must be Zero or Greater!")]
        public int Lenght
        {
            get => lenght;

            set
            { 
                if (value >= 0)
                {
                    lenght = value;
                }
                else
                {
                    throw new ArgumentException("Lenght must be grater than 0.");
                }
            }
        }

        [Required]
        [Min(0, ErrorMessage = "Width must be Zero or Greater!")]
        public int Width
        {
            get => width;

            set
            {
                if (value >= 0)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentException("Width must be grater than 0.");
                }
            }
        }

        [Required]
        [Min(0, ErrorMessage = "Height must be Zero or Greater!")]
        public int Height
        {
            get => height;

            set
            {
                if (value >= 0)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentException("Height must be grater than 0.");
                }
            }
        }

        [Required]
        public string Colour
        {
            get => colour;

            set
            {
                colour = value;
            }
        }

        [Required]
        public string Weight
        {
            get => weight;

            set
            {
                weight = value;
            }
        }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        // constructor
        public Equipment(string _manufacturer, int _lenght, int _width, int _height, string _colour, string _weight, string _name, string _description, double _price) : base( _name, _description, _price)
        {
            Manufacturer = _manufacturer;
            Lenght = _lenght;
            Width = _width;
            Height = _height;
            Colour = _colour;
            Weight = _weight;
        }

        public Equipment(): base()
        {

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
