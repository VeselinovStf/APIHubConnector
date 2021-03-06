# API Hub Connector ( APIHC )

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

![](https://github.com/VeselinovStf/APIHubConnector/blob/master/repoImg/demo.jpeg)

APIHC allows users to upload local project to GitLab and deploy it try Netlify.APIHC is tested with deploying of local Jekyll project

# Features!

  - Gets deplay key from Netlify
  - Creates GitLab repository
  - Adds hosting key to repository
  - Transferes your local project files
  - Commit and Pushes them to repository
  - Deploys all from repository to Netlify ( hosting service )

You can also:
  - Extend existing futures
  - Use builded futures - CreateHubAsync / PushDataToHub / TransferAsync /

> This project is part of Static Store Builder 
> core functionality

### Tech

Used technologies

* .NET Core 2.2
* ASP.NET CORE MVC

### Installation

APIHC requires [.NET CORE 2.2] to run.

Clone or Download project.

Use:
```sh
APIHubConnector.Core
APIHubConnector.Services
APIHubConnector.Utility.Services
```

### Setting Up

Create two classes for GitLab And Netlify access tokens ( see DemoApp.Web.APIKeyModels namespace ).

```sh
 public class AuthHostingConnectorOptions
    {
        public string HostAccesToken { get; set; }
    }
```

```sh
  public class AuthRepoHubConnectorOptions
    {
        public string RepoAccesTokken { get; set; }
    }
```

### Register Functions

```sh
services.Configure<AuthRepoHubConnectorOptions>(Configuration);
services.Configure<AuthHostingConnectorOptions>(Configuration);

APIHubConnectorServiceConfiguration.ConfigureAPIConnector(services);

services.AddHttpClient<GitLabHubClient>(c =>
    c.BaseAddress = new Uri("https://gitlab.com/api/v4/")
    
services.AddHttpClient<NetlifyHubClient>(c =>
   c.BaseAddress = new Uri("https://api.netlify.com/api/v1/")
   );

APIHubConnectorUtilityServiceConfiguration.ConfigureAPIConnectorUtility(services);
```
### Open APIs

```sh
INetlifyApiClientService<BaseResponse>
IGitLabAPIClientService<BaseResponse>
IFileTransferrer<FileTransfererResult>
ILocalStorageFileTransfer<LocalStorageFileTransferResultDTO>
IOptions<AuthRepoHubConnectorOptions>
IOptions<AuthHostingConnectorOptions>
ISiteStorageCreatorService<SiteStorageCreatorResultDTO> 
```

### Example

DemoApp.Web - HomeController

### Current Build Functions

- Execute ( Core function  )
```sh
- ISiteStorageCreatorService<SiteStorageCreatorResultDTO> 
```

### Todos

- Manage exception handling
- Check when the project is deployed and Display to the end user
- Add options for CMD comand of Netlify
- Add options for Folder Execution of Netlify
- Extract GitLab specific API call strings and add function for seleting between GitLab and GitHub

License
----

MIT

