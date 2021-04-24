using Blue_Fin_Inc.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueFinUnitTesting
{
    [TestClass]
    public class OrderModelUnitTest
    {
        //Creating an order object and a livestock item
        Order order1 = new Order();
        Livestock l1 = new Livestock(CareLevel.Medium, Temperment.SemiAggressive, WaterType.Salt, "Orange", "PH:8.1-8.4, Sg:1.020-1.02", "8cm", "Common Clown Fish", "The Ocellaris clown fish \"Nemo\" is a favourite for the beginner and experienced aquarist. The black and white ocellaris clown fishes occurs naturally in anemones and is best known from the movie Finding Nemo.The black and white ocellaris clown fish has three white stripes with vibrant black pallets on its body . Ocellaris is often confused with the percula clownfish, because of their coloration and stripe patter. The discerning difference is that A Ocellaris has 11 dorsal spines, where the Percula clownfish has 9 or 10. Nemo clownfish is often bred in captivity making it a very hardy fish, and good for beginners.It is readily available in the aquarium trade", 65.50) { ImageName = "clown_fish.jpg", Stock = 3 };

        //Testing if we can add livestock to the Order Livestock List
        [TestMethod]
        public void ShouldAddLivestock()
        {
            order1.AddLivestock(l1);
            Assert.AreEqual(order1.livestockList.Count, 1);
        }

     
        //Testing data annotation of the eircode
        [TestMethod]
        public void ShouldEirCodeBeValid()
        {
            Order order2 = new Order("Eirini","D14F653", "+353830492724");

            var checkError = ValidateModel(order2);
            Assert.IsTrue(checkError.Count() == 0);
        }

        //UnitTestDataAnnotations
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }


    }
}
