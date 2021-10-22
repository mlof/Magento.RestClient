# Magento.RestClient



## Getting Started

To start using Magento.Restclient, add the dependency through Nuget. 

```sh
$ dotnet add package Magento.RestClient
```






## Authentication

### Authenticate as Admin

```csharp

var context = new MagentoAdminContext("https://magento.localhost", "username", "password");

```


### Authenticate as Integration

To authenticate as Integration, go to System -> Extensions -> Integrations and add a new Integration. Just give it a name, other values are not required. Custom resource access is not explicitly supported, but might work. Your mileage may vary.

After that, all that remains is to activate the integration.

![magento_integration_activate](img/integration_activate.png)

Take note of the tokens in the next step, as you'll be using them to connect.

![magento_integration_activate](img/integration_tokens.png)

After this, to connect you'll only need the following code:
```csharp

var consumerKey = "";
var consumerSecret = "";
var accessToken = "";
var accessTokenSecret = "";

var context = new MagentoAdminContext("https://magento.localhost", consumerKey, consumerSecret, accessToken, accessTokenSecret);

```




