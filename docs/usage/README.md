
# Using Magento.RestClient


## Searching entities
<!-- panels:start -->

<!-- div:left-panel -->


The Magento API for searching entities can be incredibly awkward, and I do enjoy using LINQ whenever possible. So I decided it would be a good idea to write a LINQ expression tree parser for Magento. 

Man, was I wrong.

It's not the prettiest bit of code I've written, but it's surprisingly useable, and it looks a little bit like this.

```csharp
Context.Products.AsQueryable()
	.Where(product => product.Name.Contains("XPS") && product.Price > 900)
	.ToList();

```
There are also a couple of quality-of-life extension methods on `IQueryable<T>`.  

<!-- div:right-panel -->

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

<!-- panels:end -->

## Repositories

<!-- panels:start -->

<!-- div:left-panel -->


> Space is big. Really big. You just won't believe how vastly hugely mind-bogglingly big it is. 
> I mean, you may think it's a long way down the road to the chemist, but that's just peanuts to space.

Magento 2 has been architected to be as modular as possible, to a fault, and the API documentation is mostly machine generated, leading to a 1,34 MB swagger.json, which, when parsed, leads to classes with names like Body141. 

This is basically why this library exists. 

I have opted to painstakingly write all of the API functionality by hand, resulting in the code you can see in `Magento.RestClient.Data.Repositories`. 
I have also chosen to merge certain endpoints into singular classes based on functionality and route. 

I won't be so bold as to claim that there is a method to this madness, I mostly did "Whatever felt right". 

*Except for the bulk functionality. That bit needs fixing.*
<!-- div:right-panel -->

The repositories are as follows: 

* ProductAttributeGroups
* Stores
* Products
* ProductMedia
* ConfigurableProducts
* Orders
* Customers
* CustomerGroups
* Directory
* AttributeSets
* Invoices
* Categories
* InventoryStocks
* InventorySourceItems
* InventorySources
* Carts
* Attributes
* Shipments
* Bulk
* SpecialPrices
* Modules
<!-- panels:end -->

## Domain Models

<!-- panels:start -->

<!-- div:left-panel -->
While `Magento.RestClient.Data` is completely usable in its own right, it still is little more than a wrapper on top of the Magento API, leaving more than enough to be desired. For this, I decided to take a Domain-Driven Design approach, packing the API calls, validation, and a load of convenient methods into Domain Models.
<!-- div:right-panel -->

[Documentation](domain-models/ )

<!-- panels:end -->
