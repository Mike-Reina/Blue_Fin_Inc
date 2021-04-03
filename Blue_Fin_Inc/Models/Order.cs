using DataAnnotationsExtensions;
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
        public List<Product> prodList;

        //Property (ies)
        [Key]
        [DisplayName("Order Number")]
        public int OrderNo { get; set; }

        [Required]
        [RegularExpression(@"?:^[AC-FHKNPRTV-Y][0-9]{2}|D6W)[ -]?[0-9AC-FHKNPRTV-Y]{4}$")] 
        public string Eircode { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Number must be 12 digits and include the + sign!")]
        [RegularExpression(@"^\+\d{12}$", ErrorMessage = "Number must be 12 digits including country code!")] //accepting numbers in the following pattern +353830492724
        [DisplayName("Contact Number")]
        public string ContactNo { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Order payable amount must be greater than zero!")]
        [DisplayName("Payable Amount")]
        public double OrderPrice { get; set; }

        [Required]
        [Range(typeof(bool), "No", "Yes", ErrorMessage = "You must indicate if the order contains livestock or not!")] //incase no and yes does not work we can replace it with 0, 1
        [DisplayName("Does the order contain livestock?")]
        public bool ContainsLivestock { get; set; }

        //Default Constructor
        public Order()
        {

        }

        //Constructor
        public Order(string eircode_in, string contactNo_in)
        {

            Eircode = eircode_in;
            ContactNo = contactNo_in;
            OrderPrice = 0;
            ContainsLivestock = false;

            prodList = new List<Product>();
        }

        //Methods
        public void AddProduct(Product product_in)
        {
            prodList.Add(product_in);
            OrderPrice += product_in.Price;
           //Reminder:should create some code to check if the product is livestock so I can set the propert containsLivestock !!!
                
        }

        public string RemoveProduct(Product product_out)
        {
            //first lets find the product we need to remove from the order
            Product found = prodList.FirstOrDefault(p=> p.ProductCode == product_out.ProductCode);
            if(found != null)
            {
                prodList.Remove(found);
                OrderPrice -= product_out.Price;
                if (OrderPrice == 0) // Not entirely sure about this but need to check it further
                {
                    return"There are no products in the basket!";
                }
                return "Product " + product_out.ProductCode + "was removed from the basket!";

                //Reminder:should create some code to check if remaining products are livestock so I can re-set the property containsLivestock !!!
            }
            return "Product " + product_out.Name + " was not found";

        }
          
    }
}
