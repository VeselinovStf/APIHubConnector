# APIHubConnector

## Description

APIHC allows users to upload local project to GitLab and deploy it try Netlify.
APIHC is tested with deploying of local Jekyll project

## Usage

### Getting Project

Clone or Download project.
Use APIHubConnector.Core and APIHubConnector service in your project.

### Setting Up

Create two classes for GitLab And Netlify access tokens ( see DemoApp.Web.APIKeyModels namespace ).

#### Register Functions

In StartUp.cs of your project add

services.Configure<AuthRepoHubConnectorOptions>(Configuration);
services.Configure<AuthHostingConnectorOptions>(Configuration);
APIHubConnectorServiceConfiguration.ConfigureAPIConnector(services);
services.AddHttpClient<GitLabHubClient>(c => c.BaseAddress = new Uri("https://gitlab.com/api/v4/"));
services.AddHttpClient<NetlifyHubClient>(c => c.BaseAddress = new Uri("https://api.netlify.com/api/v1/"));

Use functions:

INetlifyApiClientService<BaseResponse>
IGitLabAPIClientService<BaseResponse>
IFileTransferrer<FileTransfererResult>
IOptions<AuthRepoHubConnectorOptions>
IOptions<AuthHostingConnectorOptions>

Go to HomeController of DemoApp to see required steps to do the job

### Future

- Manage exception handling
- Check when the project is deployed and Display to the end user
- Add options for CMD comand of Netlify
- Add options for Folder Execution of Netlify
- Extract GitLab specific API call strings and add function for seleting between GitLab and GitHub
