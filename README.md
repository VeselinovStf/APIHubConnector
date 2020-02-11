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

<code>services.Configure<AuthRepoHubConnectorOptions>(Configuration);</code>
<code>services.Configure<AuthHostingConnectorOptions>(Configuration);</code>
<code>APIHubConnectorServiceConfiguration.ConfigureAPIConnector(services);</code>
<code>services.AddHttpClient<GitLabHubClient>(c => c.BaseAddress = new Uri("https://gitlab.com/api/v4/"));</code>
<code>services.AddHttpClient<NetlifyHubClient>(c => c.BaseAddress = new Uri("https://api.netlify.com/api/v1/"));</code>

Use functions:

<code>INetlifyApiClientService<BaseResponse></code>
<code>IGitLabAPIClientService<BaseResponse></code>
<code>IFileTransferrer<FileTransfererResult></code>
<code>IOptions<AuthRepoHubConnectorOptions></code> 
<code>IOptions<AuthHostingConnectorOptions></code>

Go to HomeController of DemoApp to see required steps to do the job

### Future

- Manage exception handling
- Check when the project is deployed and Display to the end user
- Add options for CMD comand of Netlify
- Add options for Folder Execution of Netlify
- Extract GitLab specific API call strings and add function for seleting between GitLab and GitHub
