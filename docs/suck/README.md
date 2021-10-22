
# Things that suck



## Attribute Sets

You can get attributes for an attribute set. You can get attribute groups for an attribute set. But you can't get which attributes are assigned to a certain attribute group! Well, that is unless you use the Search functionality, but that would require reindexing every time you edit an attribute group.

## Products

### Product Attributes & Options

Alright, this one's a doozy. There's no real way to get an option ID for an attribute option through the API, except by getting a product which already has it set! So we have to match based on content, and can't delete options! 



## Creating an order

When creating an order directly in Magento, it won't calculate things like order line prices, shipping costs, etc. Probably useful, if you need to create a backlog of orders in bulk, but not so much if you actually want these things done for you. For this, you'll have to create a cart and jump through hoops. 