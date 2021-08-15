# Magento.RestClient

[TOC]

## Getting Started

Tools: 

* Postman
* Telerik Fiddler (Classic)
  * The new one sucks. Stick with the classic fiddler. 

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

#### Products

##### Configurable Products





## Common Tasks

### Creating an order

When creating an order directly in Magento, it won't calculate things like order line prices, shipping costs, etc. Probably useful, if you need to create a backlog of orders in bulk, but not so much if you actually want these things done for you. For this, you'll have to create a cart and jump through hoops. To make this process a bit less painful, I've added the Cart class.

```csharp
var cart = Cart.CreateNew(Client.Carts); // Creates a new cart.

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
// setting the addresses. This step is validated client side for your convenience. 
cart.ShippingAddress = address;
cart.BillingAddress = address;


//Adding an item, with quantity.
cart.AddItem("Your product SKU", 3);

// Gotcha here. The results in this step are based on your address and
// cart items. So the order of what you're doing matters.
var shippingMethods = cart.EstimateShippingMethods();

var paymentMethods = cart.GetPaymentMethods();
var paymentMethod = paymentMethods.First();

    
cart.SetPaymentMethod(paymentMethod.Code);
var shippingMethod = shippingMethods.First();
cart.SetShippingMethod(shippingMethod.CarrierCode, shippingMethod.MethodCode);

var orderId = cart.Commit();

// You now have committed the cart. You can proceed by adding an invoice to the order. 
Client.Orders.CreateInvoice(orderId);


```

If this looks awful, check out the official Magento documentation on the matter. It's *far* more painful.
