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
        // properties
        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Lenght must be Zero or Greater!")]
        public int Lenght { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Width must be Zero or Greater!")]
        public int Width
        { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Height must be Zero or Greater!")]
        public int Height
        { get; set; }

        [Required]
        public string Colour
        { get; set; }

        [Required]
        public string Weight
        { get; set; }

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
