﻿using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Blue_Fin_Inc.Models
{
    public class Order
    {
        //Collection of products
        public List<CartLivestock> livestockList;
        public List<CartEquipment> equipementList;

        //Property (ies)
        [Key]
        [DisplayName("Order Number")]
        public int OrderNo { get; set; }


        [Required]
        [DisplayName("Full Name")]
        [MaxLength(50, ErrorMessage ="Max Length is 50 characters!")]
        public string CustomerName { get; set; }


        [Required]
        [RegularExpression(@"^[ACDEFHKNPRTVWXY]{1}[0-9]{1}[0-9W]{1}[\ \-]?[0-9ACDEFHKNPRTVWXY]{4}$",  ErrorMessage ="Please enter a valid Eircode!")] 
        public string Eircode { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Number must be 12 digits and include the + sign!")]
        [RegularExpression(@"^\+\d{12}$", ErrorMessage = "Number must be 12 digits including country code!")] //accepting numbers in the following pattern +353830492724
        [DisplayName("Contact Number")]
        public string ContactNo { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Order payable amount must be greater than zero!")]
        [DisplayName("Payable Amount €")]
        public double OrderPrice { get; set; }

        [Required]
        //[Range(typeof(bool), "No", "Yes", ErrorMessage = "You must indicate if the order contains livestock or not!")] //incase no and yes does not work we can replace it with 0, 1
        [DisplayName("Does the order contain livestock?")]
        public bool ContainsLivestock { get; set; }

        

        //Constructor
        public Order(string eircode_in, string contactNo_in)
        {

            Eircode = eircode_in;
            ContactNo = contactNo_in;
            OrderPrice = 0;
            ContainsLivestock = false;

            livestockList = new List<CartLivestock>();
            equipementList = new List<CartEquipment>();
        }

        public Order()
        {
            livestockList = new List<CartLivestock>();
            equipementList = new List<CartEquipment>();
        }

        //Methods
        public void AddLivestock(Livestock product_in)
        {
            CartLivestock find = livestockList.FirstOrDefault(l => l.ProductCode == product_in.ProductCode);
            if (find == null)
            {
                CartLivestock newOrderProd = new CartLivestock(product_in.CareLevel, product_in.Temperment, product_in.WaterType, product_in.Colours, product_in.WaterConditions, product_in.MaxSize, product_in.Name, product_in.Description, product_in.Price) { ProductCode = product_in.ProductCode};
                newOrderProd.Stock = 1;
                livestockList.Add(newOrderProd);
            }
            else
            {
                find.Stock++;
            }
            
            OrderPrice += product_in.Price;
           //Reminder:should create some code to check if the product is livestock so I can set the propert containsLivestock !!!
                
        }

        public void AddEquipment(Equipment product_in)
        {
            CartEquipment find = equipementList.FirstOrDefault(l => l.ProductCode == product_in.ProductCode);
            if (find == null)
            {
                CartEquipment newOrderProd = new CartEquipment(product_in.Manufacturer, product_in.Lenght, product_in.Width, product_in.Height, product_in.Colour, product_in.Weight, product_in.Name, product_in.Description, product_in.Price) { ProductCode = product_in.ProductCode }; 
                newOrderProd.Stock = 1;
                equipementList.Add(newOrderProd);
            }
            else
            {
                find.Stock++;
            }
            
            OrderPrice += product_in.Price;
            //Reminder:should create some code to check if the product is livestock so I can set the propert containsLivestock !!!

        }






        //public string RemoveProduct(Product product_out)
        //{
        //    //first lets find the product we need to remove from the order
        //    Product found = prodList.FirstOrDefault(p=> p.ProductCode == product_out.ProductCode);
        //    if(found != null)
        //    {
        //        prodList.Remove(found);
        //        OrderPrice -= product_out.Price;
        //        if (OrderPrice == 0) // Not entirely sure about this but need to check it further
        //        {
        //            return"There are no products in the basket!";
        //        }
        //        return "Product " + product_out.ProductCode + "was removed from the basket!";

        //        //Reminder:should create some code to check if remaining products are livestock so I can re-set the property containsLivestock !!!
        //    }
        //    return "Product " + product_out.Name + " was not found";

        //}
          
    }
}
