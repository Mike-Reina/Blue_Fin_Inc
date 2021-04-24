using Blue_Fin_Inc.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueFinUnitTesting
{
    [TestClass]
    public class ProductModelUnitTest
    {
        Equipment equip1 = new Equipment("Juwel", 61, 41, 58, "Black, Oak, White", "50 kg", "Juwel Lido 120", "Cubism - with a feel for clear lines - is the stylistic note set by the Lido 120. At 61 cm wide and standing an extraordinary 58 cm tall, the LIDO 120 is also perfect for use as a saltwater aquarium. The safety base frame ensures especially safe positioning and allows you to set up your aquarium easily, with no need for special supports. Painstaking workmanship from Germany, top-quality materials and perfectly tuned technology guarantee the very best quality and safety, meaning a long service life for your Lido 120. Brilliant luminosity and excellent plant growth are what you get with the state - of - the - art Multilux LED lighting technology from JUWEL.", 424.99) { ImageName = "large-Rio_125_black_SBX_Combi.jpg", Stock = 12 };
        Livestock live1 = new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99) { ImageName = "harlequin-rasbora1.jpg", Stock = 15 };

        [TestMethod]
        public void TestProductAddStock()
        {
            equip1.AddStock(3);
            Assert.AreEqual(15, equip1.Stock);
        }

        [TestMethod]
        public void TestProductRemoveStock()
        {
            live1.RemoveStock(5);
            Assert.AreEqual(10, live1.Stock);
        }
    }
}
