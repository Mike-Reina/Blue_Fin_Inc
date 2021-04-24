# Blue Fin Inc

## 1. Project aim and scope 
 
Our group’s project is an online aquarium shop with an inventory management system. It will allow customers to browse the site for aquarium products, add products to their order and then submit their order to the system. Employes can view and manage (create/update/delete) all products and orders. The system will automatically track stock changes when an order is submitted.
 
A database will hold information on livestock and aquarium equipment. Each are subclasses of a product class. With livestock and equipment having different properties, each have their own model and views associated with them. Along with products the database will also hold information on the orders customers submit.  
 
There are two other models associated with the system that should be noted. To allow products to be added to an order, both livestock and equipment have a “Cart” based subclass. These “Cart” subclasses are carbon copies of their respective product class (livestock/equipment). These will be used to track the products within each order with Lists.
 
There are 6 options available at the top of the website. These are “Equipment”, “Livestock”, “My Order”, “Modify Equipment”, “Modify Livestock” and “Modify Order” (Note. clicking “Blue Fin Inc” will also return you to the home page).
 
The “Equipment”, “Livestock” and “My Order” options are those which will be available to a customer. These will each bring up views for their titles namesake. “Equipment” and “Livestock” will list each of their products allowing customers to add them to their order. They can also click on the product images to be brought to a page showing all details for that product. The “My Order” view will allow customers to create an order and start adding products to it. They can return here at any time to get a summary of the order. Once happy with the order it can be submitted.
 
The “Modify Equipment”, “Modify Livestock” and “Modify Order” are views available to employees. These control the CRUD operations for each, as well as allowing an employee to quickly add stock to the system. The “Modify Order” view will also allow employees to check online orders for what is required in each when preparing for shipment. 
 
 
## 2. Database Design  
 
From a database perspective, we are utilizing two tables that inherit from the Product: Livestock and Equipment. When a user places an order, we store information about the order in the Order table. The above Entity Relationship diagram demonstrates the relationships between the fore said tables as well as the attributes we are interested in recording within each table.  
 
 ![image](https://drive.google.com/uc?export=view&id=15CegLeIU12G8efakt-rVHZJD48GYg5jC)
 
## 3. Screenshots of the Web Application 
 
### Home Page: 
 
 ![image](https://drive.google.com/uc?export=view&id=17Fz8Zv2_A5AA1FfOOCsPMP_MLDg9EPoZ)
 
### Equipment Page:
 
 ![image](https://drive.google.com/uc?export=view&id=1SnOe5yFXRSpjdEaXvWWKUEARdwSimPN7)
 
### Equipment Details Page:
 
 ![image](https://drive.google.com/uc?export=view&id=1q2Xv7DMcVLyRu3ta7ghi8PHsO0vgFDGl)
 
### Livestock Page:
 
 ![image](https://drive.google.com/uc?export=view&id=1w4UDeJniPGvmIkAG1D_5Jvh-8sA5ebYp)
 
### Livestock Details Page:
 
 ![image](https://drive.google.com/uc?export=view&id=1MHdYdSkE17RWT1_2Tz6g-xRLnQJ4U219)

### No Order:

  ![image](https://drive.google.com/uc?export=view&id=1AUjDx1gOVIu1HMFmD0T16-I-cCD1oGie)
  
### Full Order:
 
  ![image](https://drive.google.com/uc?export=view&id=1OSAJvrPKl3F20HGRmEQN5--LrQcKDIJq) 
  
### Order Submitted:

  ![image](https://drive.google.com/uc?export=view&id=1A38_hT5AW-exOPOUsujtbPOfk7WTIr6O) 
 
### Modify Equipment:
 
  ![image](https://drive.google.com/uc?export=view&id=1SPvSwUPKRylY5Wkote1o4qtx84ByNvFJ) 
 
### Modify Livestock:

  ![image](https://drive.google.com/uc?export=view&id=19gFCDRFg3UM32vCcuQBDIZmhREoPLiqD)
 
### Modify Order:

  ![image](https://drive.google.com/uc?export=view&id=1veWut3nOwpRrkzp3DSBiLrTddhQ7cZQn)

## 4. URI Addressing Scheme 
 
Home:
http://bluefininc.azurewebsites.net/ 
 
Equipment Listed:
http://bluefininc.azurewebsites.net/Equipment/Index
 
Equipment List w/ Search-Term:
http://bluefininc.azurewebsites.net/Equipment/Index?EquSearch={search-term}
 
Equipment Detailed:
http://bluefininc.azurewebsites.net/Equipment/Details/{id}
 
Equipment Details by API Request:
http://bluefininc.azurewebsites.net/Equipment/Details/{id}?json=yes
 
Equipment Listed for Modification(Edit):
http://bluefininc.azurewebsites.net/Equipment/EditIndex
 
Equipment Listed for Modification w/ Search-Term:
http://bluefininc.azurewebsites.net/Equipment/EditIndex?EquSearch={search-term}
 
Create Equipment:
http://bluefininc.azurewebsites.net/Equipment/Create/{equipment}
 
Edit Equipment:
http://bluefininc.azurewebsites.net/Equipment/Edit/{id}/{equipment}
 
Delete Equipment:
http://bluefininc.azurewebsites.net/Equipment/Delete/{id}
 
Equipment Add Stock:
http://bluefininc.azurewebsites.net/Equipment/AddStock/{id}/{amount}
 
Livestock Listed:
http://bluefininc.azurewebsites.net/Livestock/Index
 
Livestock List w/ Search-Term:
http://bluefininc.azurewebsites.net/Livestock/Index?LiveSearch={search-term}
 
Livestock Detailed:
http://bluefininc.azurewebsites.net/Livestock/Details/{id}
Livestock Details by API Request:
http://bluefininc.azurewebsites.net/Livestock/Details/{id}?json=yes
 
Livestock Listed for Modification(Edit):
http://bluefininc.azurewebsites.net/Livestock/EditIndex
 
Livestock Listed for Modification w/ Search-Term:
http://bluefininc.azurewebsites.net/Livestock/EditIndex?LiveSearch={search-term}
 
Create Livestock:
http://bluefininc.azurewebsites.net/Livestock/Create/{livestock}
 
Edit Livestock:
http://bluefininc.azurewebsites.net/Livestock/Edit/{id}/{livestock}
 
Delete Livestock:
http://bluefininc.azurewebsites.net/Livestock/Delete/{id}
 
Livestock Add Stock:
http://bluefininc.azurewebsites.net/Livestock/AddStock/{id}/{amount}
 
Livestock/Equipment ‘Add To Order’ e.g. product code 3 from the Livestock list of products:
http://bluefininc.azurewebsites.net/Order/Check/3?productType=Livestock
 
Create Order:
http://bluefininc.azurewebsites.net/Order/Create 
 
My Order (contains information about the order and the products added to the order): 
http://bluefininc.azurewebsites.net/Order/Details 
 
Remove Item from the Order Summary prior to the order being placed e.g. an equipment with product code 2:
http://bluefininc.azurewebsites.net/Order/Remove/2?productType=Equipment 
 
Place Order Button: 
http://bluefininc.azurewebsites.net/Order/PlaceOrder  
 
Modify Order Listing all the placed orders:
http://bluefininc.azurewebsites.net/Order 
 
Modify Order Listing all the placed orders containing a string e.g. ‘Eirini’: 
http://bluefininc.azurewebsites.net/Order?OrderSearch=Eirini 
 
 
Edit Order e.g. order #1:
http://bluefininc.azurewebsites.net/Order/Edit/1 
 
 
Show Order Details e.g. order #1
http://bluefininc.azurewebsites.net/Order/ShowOrderDetails/1 
 
Client request to Show Order Details e.g. order #1:
http://bluefininc.azurewebsites.net/Order/ShowOrderDetails/1?json=yes 
 
Delete Order e.g. order #1
http://bluefininc.azurewebsites.net/Order/Delete/1 
