# Magento.RestClient



## Getting Started



To start using Magento.Restclient, add the dependency through Nuget. 

<!-- tabs:start -->
### **Package Manager**

```powershell
Install-Package Magento.RestClient 
```
### **.NET CLI**

```bash
dotnet add package Magento.RestClient
```
### **PackageReference**

```xml
<PackageReference Include="Magento.RestClient" Version="0.6.2" />
```

<!-- tabs:end -->




## Authentication

### Authenticate as Admin

```csharp
var context = new MagentoAdminContext("https://magento.localhost", "username", "password");

```


### Authenticate as Integration

To authenticate as Integration, go to System -> Extensions -> Integrations and add a new Integration. Just give it a name, other values are not required. Custom resource access is not explicitly supported, but might work. Your mileage may vary.

<!-- panels:start -->

<!-- div:left-panel -->
After that, all that remains is to activate the integration.
<!-- div:right-panel -->

![magento_integration_activate](img/integration_activate.png)

<!-- div:left-panel -->
Take note of the tokens in the next step, as you'll be using them to connect.
<!-- div:right-panel -->
![magento_integration_activate](img/integration_tokens.png)

<!-- div:left-panel -->
After this, you'll be able to connect with the following code:
<!-- div:right-panel -->
```csharp

var consumerKey = "";
var consumerSecret = "";
var accessToken = "";
var accessTokenSecret = "";

var context = new MagentoAdminContext(
    "https://magento.localhost", 
    consumerKey, 
    consumerSecret, 
    accessToken, 
    accessTokenSecret);


```
<!-- panels:end -->


