
# Domain Models


## Carts
A surprisingly necessary domain model, considering the validation on Magento Orders is less than perfect. Also a bit of a strange one in how it doesn't actually implement `IDomainModel`. Other than the other domain models, this does everything "live", since that seems to be necessary to calculate things like product pricing. 


```csharp
var cart = new CartModel(Context);

// assigning a customer.
// only needed for AdminContexts
var customer = Context.Customers.GetByEmailAddress("customer@example.org");
await cart.AssignCustomer(customer.Id);

// Adding a simple product
await cart.AddSimpleProduct("PRODUCT-SIMPLE", 3);

// Adding a configurable product.
// Validates if the simple product is an actual child of the configurable product.
// It's also possible to straight-up add the simple product as well.
await cart.AddConfigurableProduct("PRODUCT-CONFIGURABLE", "PRODUCT-CONFIGURABLE-XL", 3);

// Adding addresses

cart.BillingAddress = new Address();
cart.ShippingAddress = new Address();

// returns a list of valid shipping methods for the selected address

var shippingMethods = await cart.EstimateShippingMethods();
await cart.SetShippingMethod(shippingMethods.First());

// returns a list of valid payment methods
var paymentMethods = await cart.GetPaymentMethods();
await cart.SetPaymentMethod(paymentMethods.First());

// cart.Commit(); creates an order for the cart and returns the order ID for this order
var orderId = await cart.Commit();
``` 

## Categories
The Categories domain model is where the IDomainModel shines. You can save an entire tree of objects with SaveAsync. Everything from sub categories to category assignments. This is reflected in the available methods, like GetOrCreateByPath, which can create an entire category tree at once. 
### Example
```csharp
// This constructor gets the root category for the selected scope. 
var root = new CategoryModel(Context);

// This constructor gets a specific category by its ID 
var category = new CategoryModel(Context, 12);

// Let's try setting up a computer hardware store.

// Creates the tree necessary for Desktop Processors to exist.
var desktopProcessors = root.GetOrCreateByPath(
	"/Components/Core Components/Processors/Desktop Processors"
	);

// Searches the Products List endpoint.
var processors = Context.Products
			.AsQueryable()
			.Where(product => product.Name.StartsWith("Intel I")
			                  || product.Name.StartsWith("Ryzen R"))
			.ToList();

foreach (var product in processors)
{
	// Adds the product to Desktop Processors
	desktopProcessors.AddProduct(product.Sku);
}

// Creates the tree necessary for Liquid Cooling to exist.
var liquidCooling = root.GetOrCreateByPath(
	"/Components/Core Components/Processors/Liquid Cooling"
	);

// Let's add a single subcategory.
liquidCooling.GetOrCreateChild("AIO");

// We can also get a child through an indexer 
var allInOne = liquidCooling["AIO"];

// Saves the entire tree stemming from the root node.
await root.SaveAsync();

// This will save the following objects
// root
//		Components
//			Core Components
//				Processors
//					Desktop Processors
//						[All products in desktopProcessors ]
// 					Liquid Cooling
//						AIO
```

## Products

```csharp


```



## Customers
## Attribute Sets
## Attributes
## Orders
