# Contributing to Magento.Restclient

## Tools 

* Postman
* Docker
* Telerik Fiddler (Classic)
  * *The new one sucks. Stick with the classic fiddler.* 




## Setting up Docker

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


