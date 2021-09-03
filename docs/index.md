# Magento.RestClient

[TOC]

## Getting Started

Tools: 

* Postman
* Docker
* Telerik Fiddler (Classic)
  * *The new one sucks. Stick with the classic fiddler.* 

### Setting up Docker

For development, you can use the Docker Compose file in /docker/magento. It uses the Bitnami Magento instance. Default credentials are as follows:

```
Username: user
Password: bitnami1
```

If you're developing on Windows with WSL docker backend you may want to run the following commands, or Elasticsearch will *not* boot.

```sh
wsl -d docker-desktop
sysctl -w vm.max_map_count=262144
```







## Authentication

### Integration

To authenticate as Integration, go to System -> Extensions -> Integrations and add a new Integration. Just give it a name, other values are not required. Custom resource access is not explicitly supported, but might work. Your mileage may vary.

After that, all that remains is to activate the integration.

![magento_integration_activate](img/magento/integration_activate.png)

Take note of the tokens in the next step, as you'll be using them to connect.

![magento_integration_activate](img/magento/integration_tokens.png)

After this, to connect you'll only need the following code:
```csharp
var consumerKey = "";
var consumerSecret = "";
var accessToken = "";
var accessTokenSecret = "";

var client = new MagentoClient("http://localhost/rest/V1/");
this.IntegrationClient = client.AuthenticateAsIntegration(consumerKey, consumerSecret, accessToken, accessTokenSecret);
```

## Search





## Entities

#### Attribute Sets

You can get attributes for an attribute set. You can get attribute groups for an attribute set. But you can't get which attributes are assigned to a certain attribute group! Well, that is unless you use the Search functionality, but that would require reindexing every time you edit an attribute group.

#### Products

##### Product Attributes & Options

Alright, this one's a doozy. There's no real way to get an option ID for an attribute option through the API, except by getting a product which already has it set! So we have to match based on content, and can't delete options! 

##### Configurable Products





## Common Tasks

### Creating a attribute set



### Creating a category

### Creating a simple product

### Creating a configurable product

### Creating an order

When creating an order directly in Magento, it won't calculate things like order line prices, shipping costs, etc. Probably useful, if you need to create a backlog of orders in bulk, but not so much if you actually want these things done for you. For this, you'll have to create a cart and jump through hoops. To make this process a bit less painful, I've added the CartModel class.

```csharp
var address = new Address(){
	Firstname = "Scunthorpe",
    Lastname = "Post Office",
    Telephone = "+44 1724 843348",
    Company = "Scunthorpe Post Office",
    City = "Scunthorpe",
    Street = new List<string>() {"148 High St"},
    Postcode = "DN15 6EN",
    CountryId = "GB"
};

var cart = new CartModel(Client); // Creates a new cart.

// setting the addresses. This step is validated client side for your convenience. 
cart.ShippingAddress = address;
cart.BillingAddress = address;



//Adding a simple or virtual product, with quantity.
cart.AddSimpleProduct("Your product SKU", 3);

// Adding a simple or virtual product, without quantity.
// Defaults to a quantity of 1
cart.AddSimpleProduct("Your product SKU");

// Adding a configurable product, with quantity.
cart.AddConfigurableProduct("Parent SKU", "Child SKU");

// Adding a configurable product, without quantity.
// Defaults to a quantity of 1
cart.AddConfigurableProduct("Parent SKU", "Child SKU");


// Gotcha here. Available shipping methods
// in this step are based on your address and
// cart items. So the order of what you're doing matters.
var shippingMethods = cart.EstimateShippingMethods();
cart.SetShippingMethod(shippingMethods.First());


var paymentMethods = cart.GetPaymentMethods();   
cart.SetPaymentMethod(paymentMethods.First(););

// Commit the cart
// This turns it into a proper sales order.
var orderId = cart.Commit();

// Congratulations.
// You now have committed the cart. 
// You can proceed by adding an invoice to the order. 
// This marks the order as fully paid.
Client.Orders.CreateInvoice(orderId);


```

If this looks awful, check out the official Magento documentation on the matter. It's *far* more painful.
