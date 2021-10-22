
# Using Magento.RestClient


## Searching entities

The Magento API for searching entities can be incredibly awkward, and I do enjoy using LINQ whenever possible. So I decided it would be a good idea to write a LINQ expression tree parser for Magento. 

Man, was I wrong.

It's not the prettiest bit of code I've written, but it's surprisingly useable, and it looks a little bit like this.

```csharp
Context.Products.AsQueryable()
	.Where(product => product.Name.Contains("XPS") && product.Price > 900)
	.ToList();

```

The following entities are queryable:

* Attribute Sets
* Categories
* Customers
* Inventory Source Items
* Inventory Sources
* Inventory Stock
* Invoices
* Orders
* Product Attribute Groups
* Products
* Products 
* Shipments

## Repositories
> Space is big. Really big. You just won't believe how vastly hugely mind-bogglingly big it is. 
> I mean, you may think it's a long way down the road to the chemist, but that's just peanuts to space.

Magento 2 has been architected to be as modular as possible, to a fault, and the API documentation is mostly machine generated, leading to a 1,34 MB swagger.json, which, when parsed, leads to classes with names like Body141. 

This is basically why this library exists. 

I have opted to painstakingly write all of the API functionality by hand, resulting in the code you can see in `Magento.RestClient.Data.Repositories`. 
I have also chosen to merge certain endpoints into singular classes based on functionality and route. 

I won't be so bold as to claim that there is a method to this madness, I mostly did "Whatever felt right". 

*Except for the bulk functionality. That bit needs fixing.*

## Domain Models

While `Magento.RestClient.Data` is completely usable in its own right, it still is little more than a wrapper on top of the Magento API, leaving more than enough to be desired. For this, I decided to take a Domain-Driven Design approach, packing the API calls, validation, and a load of convenient methods into the following Domain Models.

### Carts
### Categories
### Products
### Customers
### Attribute Sets
### Attributes
### Orders


## Attribute Sets

### Creating an Attribute Set



### Products

##### Product Attributes & Options

Alright, this one's a doozy. There's no real way to get an option ID for an attribute option through the API, except by getting a product which already has it set! So we have to match based on content, and can't delete options! 

##### Configurable Products



## Common Tasks

### Creating a attribute set

```csharp
var hasDvi = new AttributeModel(Context, "monitor_has_dvi") {
		DefaultFrontendLabel = "Has DVI",
		FrontendInput = AttributeFrontendInput.Select
};
hasDvi.AddOptions("Yes", "No");
await hasDvi.SaveAsync();

var resolution = new AttributeModel(Context, "monitor_resolution")
{
	DefaultFrontendLabel = "Resolution",
	FrontendInput = AttributeFrontendInput.Select
};
resolution.AddOptions("1366x768", "1920x1080", "2560x1080", "2560x1440");
await resolution.SaveAsync();			
var attributeSet =  new AttributeSetModel(Context, "Monitors", EntityType.CatalogProduct);
attributeSet.AddGroup("Panel");
attributeSet.AssignAttribute("Panel", resolution.AttributeCode);
attributeSet.AddGroup("Connections");
attributeSet.AssignAttribute("Connections", hasDvi.AttributeCode);

await attributeSet.SaveAsync();
```



### Creating a category

### Creating a simple product

### Creating a configurable product

```csharp
var sizeAttribute = Context.GetAttributeModel("monitor_sizes");
sizeAttribute.DefaultFrontendLabel = "Monitor Size";
sizeAttribute.FrontendInput = "select";
sizeAttribute.AddOption("13 inch");
sizeAttribute.AddOption("14 inch");
sizeAttribute.AddOption("15 inch");
sizeAttribute.AddOption("17 inch");

sizeAttribute.Save();
    
var attributeSet = Context.GetAttributeSetModel("Laptops");
attributeSet.AddGroup("Monitor");
attributeSet.AssignAttribute("Monitor", "monitor_sizes");
attributeSet.Save();

var product = new ProductModel(this.Context, "HP-ZBOOK-FURY") {
	Name = "HP ZBook Fury",
	AttributeSetId = LaptopAttributeSet,
	Visibility = ProductVisibility.Both,
	Price = 50,
	Type = ProductType.Configurable
};
product.Save();

var smallProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-13") {
	Price = 2339,
	AttributeSetId = attributeSet.Id,
	Visibility = ProductVisibility.NotVisibleIndividually,
	Type = ProductType.Simple,
	["monitor_sizes"] = "13 inch"
};
smallProduct.Save();

var largeProduct = new ProductModel(this.Context, "HP-ZBOOK-FURY-17") {
	Price = 2279,
	AttributeSetId = attributeSet.Id,
	Visibility = ProductVisibility.NotVisibleIndividually,
	Type = ProductType.Simple,
	["monitor_sizes"] = "17 inch"
};

largeProduct.Save();

var configurableProduct = product.GetConfigurableProductModel();
configurableProduct.AddConfigurableOption("monitor_sizes");
configurableProduct.Save();
configurableProduct.AddChild(smallProduct);
configurableProduct.AddChild(largeProduct);
configurableProduct.Save();
```



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

var cart = new CartModel(Context); // Creates a new cart.

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
Context.Orders.CreateInvoice(orderId);


```

If this looks awful, check out the official Magento documentation on the matter. It's *far* more painful.
