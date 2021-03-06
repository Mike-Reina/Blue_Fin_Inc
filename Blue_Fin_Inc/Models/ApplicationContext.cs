using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public class ApplicationContext : DbContext
    {
        private string connectionString;
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(connectionString);
        }

        //We need the Dbset property to maniputlate the entries in the models
        public DbSet<Livestock> Livestocks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Order> Orders { get; set; }

        //constructor
        public ApplicationContext(IConfiguration configuration, IWebHostEnvironment env)
        {

            connectionString = configuration.GetConnectionString("AzureDB");
            var builder = new SqlConnectionStringBuilder(connectionString);
        
            if (env.IsDevelopment())
            {
                builder.Password = configuration["DbPassword"];
            }


            connectionString = builder.ToString();
        }


        //Seeding to the DB method
        public void SeedDB(IConfiguration configuration, IWebHostEnvironment env)
        {
            ApplicationContext db = new ApplicationContext(configuration, env);
            db.Database.Migrate();
            if (db.Livestocks.Count() == 0)
            {
                List<Livestock> LivestockList = new List<Livestock>()
                {
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99){ ImageName = "harlequin-rasbora1.jpg", Stock = 15},
                    new Livestock(CareLevel.Easy, Temperment.Aggressive, WaterType.Fresh, "Black, Blue, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "12cm", "Crown Tail Betta", "The Crown Tail Betta has a striking, elaborate tail that differentiates it from other Bettas. The Crown Tail has a teardrop shape to its tail while the Twin Tail is split, almost giving the suggestion of having two tails.", 19.99){ ImageName = "Crowntail_Betta_Red.jpg", Stock = 5},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "All", "PH:7.0-8.6, KH 12-30, 22°C-28°", "10cm", "Sword Tail", "The Swordtail platy boasts exuberant coloration reminiscent of tropical flowers in bloom. Vibrant shades of colours are energized by an iridescent sheen, making the stunning Swordtail a very desirable colour variety of the Xiphophorus helleri Swordtail. The Swordtail is perhaps the quintessential community aquarium fish. The time-tested popularity of the Swordtail can be attributed to its ease of care, peaceful temperament, and wonderfully diverse fin and color varieties. The most common Swordtail varieties include: Red Wag, Red Velvet, Marigold, Black Nubian, and Pineapple Swordtail.", 3.99){ ImageName = "Swordtail.jpg", Stock = 15},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Blue, Yellow, White", "PH:6.0-7.5, KH 3-8, 22°C-24°C", "6cm", "Golden Dream Killifish", "The Killifish family are best in pairs. Seahorse Aquarium keeps all killifish in Pairs and recommend to house both sexes in the aquarium. The Golden Dream Killifish is captive bred, but is normally found in freshwater and brackish ponds, streams and marshes of Africa. The term Killy is derived from the Dutch word meaning ditch or channel, not because this fish is a killer in the aquarium. These fish are ideal fish for the community aquarium, and will add some vibrant colour and activity to these aquariums.", 10.99){ ImageName = "Golden-Killifish.jpg", Stock = 18},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Silver with black horizontal lines", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "3cm", "Pygmy Cory", "The Pygmy Cory is mainly found in the Madeira River basin in Brazil, but populations are found across the South American continent. Originally there was only thought to be one miniature Corydoras species (Corydoras hastatus). In the early 1900s it was realized that many species had been misidentified, and so the Pygmy Cory species was described. They thrive in tropical aquariums and are a great choice for beginners, though they are popular with experienced aquarists too.", 6.99){ ImageName = "Corydoras_pygmaeus.jpg", Stock = 22},
                    new Livestock(CareLevel.Medium, Temperment.SemiAggressive, WaterType.Salt, "White, Tan, Yellow", "PH:8.1-8.4, Sg:1.020-1.02", "50cm", "Dogface Puffer", "The Stars and Stripes Toadfish is greenish brown or yellowish brown above with white underside. The back and upper sides of the body and tail are covered in small white spots. The lower sides and belly are marked with white to pale blue bars or lines. The base of the pectoral fins and gills are surrounded by light rings. The body is a typical pufferfish rounded box shape. The mouth is like a beak with teeth fused together into plates. The body has small spines except around the nose and tail.", 54.99){ ImageName = "Dog_Puffer123.jpg", Stock = 3},
                    new Livestock(CareLevel.Medium, Temperment.Peaceful, WaterType.Salt, "Black", "PH:8.1-8.4, Sg:1.020-1.02", "28cm", "Seahorse Giant", "The Kuda Seahorse is one of the larger seahorses and is also known as the Common Seahorse, Spotted Seahorse, or Oceanic Seahorse. It has a short crown which is directed backward. Hippocampus Kuda actually comes in many colors including yellow, orange, brown, and even black. In order to maintain the coloration of these wonderful animals it is ideal to keep décor of a similar coloration in the aquarium. Some individuals may have spots. It is difficult to keep in an aquarium. Tank raised specimens are currently available.", 70.00){ ImageName = "Black_SeaHorse.jpg", Stock = 5},
                    new Livestock(CareLevel.Hard, Temperment.Peaceful, WaterType.Salt, "Black, White, Yellow", "PH:8.1-8.4, Sg:1.020-1.02", "18cm", "Moorish Idol", "The Moorish Idol is also commonly known in Hawaii as \"Kihikihi\" which means \"curves,\" \"corners,\" or \"zigzags,\" and refers to its shape and color pattern. It is the only member of the family Zanclidae, and a very close relative of the Tangs or Surgeonfish. One of the most widespread fish, it can be found throughout the Indian Ocean, Red Sea, and all of the tropical Pacific. Wild specimens can attain a length of 18 cm, but 9 cm is more likely in the aquarium.", 115.00){ ImageName = "moorish-idol-ts-1.jpg", Stock = 10},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Salt, "Red with Two White Strips", "PH:8.1-8.4, Sg:1.023-1.025, 23", "6cm", "Skunk Cleaner Shrimp", "The Scarlet Skunk Cleaner Shrimp acts like the medic of any saltwater aquarium. In fact, this active cleaner will set up shop on live rock or coral outcroppings and wait for fish to come and be cleaned of ectoparasites or dead tissue.Many fish value its services so highly that they even allow the Scarlet Skunk Cleaner Shrimp to clean inside of their mouths without harming the shrimp. No matter how your fish use the Scarlet Skunk Cleaner Shrimps services, it is easy to see why this peaceful creature is so popular amongst home aquarists.", 30.00){ ImageName = "Skunk_Cleaner_Shrimp_large.jpg", Stock = 7},
                    new Livestock(CareLevel.Hard, Temperment.SemiAggressive, WaterType.Salt, "Red, Green, Black, Yellow", "PH:8.1-8.4, Sg:1.020-1.02", "60cm", "Featherstar", "Feather Stars, also known as sea lilies or feather-stars, are marine animals that make up the class Crinoidea of the echinoderms. They live both in shallow water and in depths as great as 6,000 meters. Feather Stars are characterized by a mouth on the top surface that is surrounded by feeding arms. They have a U-shaped gut, and their anus is located next to the mouth.", 30.00){ ImageName = "feather-star-1-1.jpg", Stock = 3},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Salt, "Red and White stripes", "PH:8.1-8.4, Sg:1.023-1.025", "4cm", "Dancing Shrimp", "The Camel Shrimp is also known as the Hinge-beak Shrimp, Dancing Shrimp, or Candy Shrimp. It is distinguished by a moveable rostrum (beak) that is usually angled upwards. The Camel Shrimp has a variable pattern of red and white stripes on its body. The males of its species tend to have larger chelipeds (claws) than the females. It prefers to congregate with other shrimp of its kind in rock crevasses, under overhangs, or in the coral rubble. It especially needs hiding places when it is molting. It usually tolerates other shrimp, but may nip at colonial anemones, disc anemones, and soft leather corals. It generally leaves bubble coral and stinging anemones alone.", 20.00){ ImageName = "dancing_shrimp_1.jpg", Stock = 6},
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Salt, "Red", "PH:8.1-8.4, Sg:1.023-1.015", "5cm", "Red Leg Hermit Crab", "This tiny crab lives in abandoned snail shells which will vary in size and shape.The Dwarf Red Tip Hermit Crab is a valuable addition to either a saltwater reef or fish-only aquarium due to their voracious appetite for green hair algae and cyanobacteria. In addition, they provide a valuable service of aerating the substrate by sifting through the sand. It is one of the Hermit Crabs that is reef-safe.The ideal environment is an established saltwater aquarium with plenty of algae and/or live rock to graze upon. Be sure to provide empty shells of various sizes to accommodate their growth.If insufficient algae is present, the Dwarf Red Tip Hermit Crab will need to be fed dried seaweed.", 7.50){ ImageName = "red_hermit_crab.jpg", Stock = 5},
                    new Livestock(CareLevel.Medium, Temperment.SemiAggressive, WaterType.Salt, "Red", "PH:8.1-8.4, Sg:1.020-1.02", "35cm", "Rose Bulb Anemone", "The Rose Bubble Tip Anemone is a less common form of the Bubble Tip Anemone which is often referred to as the Four-colored, Bulb Tentacle, Bulb Tip, or Bulb Anemone. At rest, the enlarged tip at the end of the tentacles is a rose to red color. The Bubble Tip Anemone is usually found in coral rubble, or in solid reefs. Its pedal disc is usually attached deep within dead coral. It stretches its tentacles to become sweeper tentacles when hungry. That is, the tentacles become elongated to capture a meal, then the tentacles shorten and the bubble tips return. Handle this invertebrate, and all Anemones, with care. They can sting other Anemones, as well as Corals.", 65.50){ ImageName = "rose_tip_anemone.jpg", Stock = 3},
                    new Livestock(CareLevel.Medium, Temperment.SemiAggressive, WaterType.Salt, "Orange", "PH:8.1-8.4, Sg:1.020-1.02", "8cm", "Common Clown Fish", "The Ocellaris clown fish \"Nemo\" is a favourite for the beginner and experienced aquarist. The black and white ocellaris clown fishes occurs naturally in anemones and is best known from the movie Finding Nemo.The black and white ocellaris clown fish has three white stripes with vibrant black pallets on its body . Ocellaris is often confused with the percula clownfish, because of their coloration and stripe patter. The discerning difference is that A Ocellaris has 11 dorsal spines, where the Percula clownfish has 9 or 10. Nemo clownfish is often bred in captivity making it a very hardy fish, and good for beginners.It is readily available in the aquarium trade", 65.50){ ImageName = "clown_fish.jpg", Stock = 3}
                };

                foreach (Livestock l in LivestockList)
                {
                    db.Livestocks.Add(l);
                }

                List<Equipment> EquipmentList = new List<Equipment>()
                {
                    new Equipment("Juwel", 61, 41, 58, "Black, Oak, White", "50 kg", "Juwel Lido 120", "Cubism - with a feel for clear lines - is the stylistic note set by the Lido 120. At 61 cm wide and standing an extraordinary 58 cm tall, the LIDO 120 is also perfect for use as a saltwater aquarium. The safety base frame ensures especially safe positioning and allows you to set up your aquarium easily, with no need for special supports. Painstaking workmanship from Germany, top-quality materials and perfectly tuned technology guarantee the very best quality and safety, meaning a long service life for your Lido 120. Brilliant luminosity and excellent plant growth are what you get with the state - of - the - art Multilux LED lighting technology from JUWEL.", 424.99){ ImageName = "large-Rio_125_black_SBX_Combi.jpg", Stock = 12},
                    new Equipment("Fluval", 121, 46, 70, "Black with Oak stand", "65 kg", "Fluval Vicenza 260", "With its graceful curves, the Fluval Vicenza bow-fronted aquarium adds a new dimension to flat walls. With a choice of two sizes, the Vicenza brings designer style to the craft of home aquatics. Matching cabinets for the Vicenza and Venezia aquariums are available in a modern oak finish.Each is designed to co - ordinate completely with the shape of the aquarium and provides a complete streamlined solution for the modern home. All cabinets feature stylish high gloss handles, a drilled top for hidden filter connections and an internal shelf - the ideal place to keep all your fishkeeping sundries.", 925.00){ ImageName = "large-Fluval-Vicenza-260.jpg", Stock = 10},
                    new Equipment("Aqua One", 45, 45, 45, "Black with Oak stand", "55 kg", "MiniReef 90 Marine", "The Aqua One MiniReef is a stylish, contemporary and elegantly designed tank combining all the elements of the bestselling AquaReef range. The MiniReef features a glass sump filtration system. The Moray circulation pump is also included in the sump. A heater and a G213 protein skimmer are also included in the sump. The latter is extremely effective thanks to its pin wheel design impeller. Also, all pipework is provided to connect the sump and the aquarium via the compact side weir system in the aquarium. The MiniReef comes with the powerful Mariglo LED lighting system. The Mariglo features a combination of white and blue LEDs that can operate independently of each other.", 610.00){ ImageName = "minireef45.jpg", Stock = 8},
                    new Equipment("Fluval", 25, 20, 58, "Black", "5 kg", "Fluval 307 External Filter", "Fluval 406 Canister Filter delivers many practical benefits–including better filtration, less maintenance frequency, faster setup, and quieter operation–all designed to make fishkeeping more enjoyable. Building on the success of Fluval 05 Series filters, this filter provides enhanced functionality and filtration performance to create the cleanest, healthiest aquariums ever.", 159.00){ ImageName = "Fluval_308_Ex.jpg", Stock = 12},
                    new Equipment("JBL", 35, 20, 15, "Black", "1.5 kg", "I60 Internal Filter", "Internal filter for aquariums up to 80 l (or 60 cm in length). Ready-to-connect with spray bar, wide jet pipe and diffuser. With new magnetic holder (no suction pads needed). Modular construction allows as many extensions to be added as required. Height: 15.5 cm. When cleaning, water content flows off completely through valve system. Variable capacity from 300 - 800 l/h • Takes all filter media. 7 filter media available as ready-to-use modules.", 34.99){ ImageName = "JBL-i60.jpg", Stock = 7},
                    new Equipment("Hugo Kamishi", 0, 0, 0, "Pakrike Red, Royal Blue, Blue Ocean, Natural Shale", "5 kg / 10 kg", "Substrate Gravel", "All of these gravels are pH balanced and rounded off by nature. The gravel is somewhat porous, that even lets the hair roots of plants grow along the gravels. All coloured gravels must be rinsed in cold water before use. Coloured gravels can not be placed in water over 35 degrees in temperature. All coloured gravels are non toxic to fish, plants and invertebrates.", 34.99){ ImageName = "blue_ocean_sub.jpg", Stock = 7},
                    new Equipment("Fluval", 16, 4, 4, "Black", "0.6kg", "Fluval E Series 100w Heater", "FLUVAL “E” SERIES HEATERS. Simply the most technologically advanced heaters available today, the Fluval E series advanced electronic aquarium heater with VueTech technology delivers peace of mind by continuously displaying real time water temperature. Equipped with an advanced digital microprocessor monitoring system with dual temperature sensors, the E series continuously monitors and displays aquarium water temperature, ensuring the well-being of your aquarium inhabitants. Precise temperature setting with the easy to use adjustment lever the temperature can be precisely set in 0.5° increment.", 44.99){ ImageName = "fluval_e100w.jpg", Stock = 3},
                    new Equipment("Red Sea", 45, 25, 25, "Black", "22kg", "Red Sea Coral Pro Salt", "Red Sea Coral Pro Salt 22kg - 660 litres - All corals build their skeletons by absorbing the major, minor and trace elements that they need from the surrounding water. This balance of the elevated foundation elements, combined with the living reef grains makes the performance of Coral Pro second to none. Depending upon required salinity, Red Sea Coral Pro Salt will produce approximately the following:-7kg tub will make approx 210 litres / 46 gallons22kg tub will make approx 660 litres / 145 gallons. Benefits of Red Sea Salts: Living reef in every harvested grain - From the waters of the exotic Red Sea - All-natural eco-friendly harvesting - Biologically balanced levels of foundation elements - Full complement of trace elements - Guaranteed parameters for 10liter / 2.5gal mix. - No Nitrates or Phosphates(Algae Nutrients)No toxic levels of Heavy Metals - Low moisture content - High yield(seawater per gram of salt)", 84.99){ ImageName = "Red-Sea-coral-pro-salt.jpg", Stock = 17},
                    new Equipment("Aqua One", 25, 15, 15, "Black", "2.5kg", "Aqua One MiniSkim 80", "Hang on protein skimmer - 1400 L/H needle wheel venturi pump. Exit pipe sponge filtration. Detachable collection cup for easy cleaning. For tanks up to 600 Litres", 46.99){ ImageName = "aquaone_skimmer.jpg", Stock = 17},
                    new Equipment("Comline", 11, 6, 21, "Black", "2kg", "Comline DOC Skimmer 9001 DC", "Recommended from 20 to 160 liters (5.3 to 42.3 USgal.) of sea water.Immersion depth: approx. 135 to 155 mm (5.3 to 6.1 in.), Skimmer cup volume: 0.2 liters (.05 USgal.) Air capacity: approx. 150 l/h (39.6 USgal./h), Energy consumption: approx. 5 W (max.8W), Power supply unit: 100-240V / 50-60Hz, Cable length: 3 m (118.1 in.) up to the Turbelle® controller. Includes Magnet Holder for glass thickness of up to 10 mm(3 / 8\").", 159.99){ ImageName = "Comline_9001.jpg", Stock = 13},
                    new Equipment("JBL", 0, 0, 0, "Black", "12kg", "JBL ProFlora m1003 CO2", "CO2 fertilizer system with 2 kg refillable cylinder and pH control instrument. Complete system with: 2 kg CO2 cylinder with stand, pressure regulator, pH control instrument, CO2 diffusion reactor JBL Taifun 430 mm, 2 meter CO2 special hose, CO2 non-return valve, KH Test. With pH control instrument(JBL pH-Control), which automatically regulates CO2 supply and adjusts to the pH level selected(incl.calibration solution but without pH electrode!). 110 - 240V; 50 - 60Hz", 649.99){ ImageName = "proflora_m1003.jpg", Stock = 4}
                };

                foreach (Equipment e in EquipmentList)
                {
                    db.Equipments.Add(e);
                }
                db.SaveChanges();
            }
           
        }

    }

}
