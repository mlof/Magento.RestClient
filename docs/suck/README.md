
# Things that suck
A list of things that kind of suck, and need a workaround, but are inherent to Magento.


## EAV
### Updating Attributes by Code
As of 2.4.2, doesn't work.
### Getting Attributes for Attribute Groups
You can get attributes for an attribute set. You can get attribute groups for an attribute set. But you can't get which attributes are assigned to a certain attribute group! Well, that is unless you use the Search functionality, but that would require reindexing every time you edit an attribute group.

## Catalog
### Product Attributes & Options
There's no real way to get an option ID for an attribute option through the API, except by getting a product which already has it set! So we have to match based on content, and can't delete options! 



## Orders
### Creating an order
When creating an order directly in Magento, it won't calculate things like order line prices, shipping costs, etc. Probably useful, if you need to create a backlog of orders in bulk, but not so much if you actually want these things done for you. For this, you'll have to create a cart and jump through hoops. 
### Deleting an order
You can't delete orders. This *really* helps when unit testing. 🤦‍♂️

### Minimal Order Definition
Good luck finding an official source on this. It's possible, and even easy, to send in an order which will break the Administration UI. The validation on the Magento side is practically non-existent. 

This is the minimal order you can send to Magento without crashing it (But the status will still be fucked!):

```json
{
    "entity": {
        "customer_email": "customer@example.com",
        "billing_address": {
            "city": "string",
            "country_id": "NL",
            "firstname": "string",
            "lastname": "string",
            "postcode": "string",
            "street": [
                "string"
            ],
            "telephone": "string"
        },
        "extension_attributes": {
            "shipping_assignments": [
                {
                    "shipping": {
                        "address": {
                            "city": "string",
                            "country_id": "NL",
                            "firstname": "string",
                            "lastname": "string",
                            "postcode": "string",
                            "street": [
                                "string"
                            ],
                            "telephone": "string"
                        }
                    }
                }
            ]
        },
        "payment": {
            "method": "checkmo"
        }
    }
}
```
