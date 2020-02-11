using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHUbConnector.Services.FileTransfer;
using APIHUbConnector.Services.FileTransfer.DTOs;
using DemoApp.Web.APIKeyModels;
using DemoApp.Web.Models;
using DemoApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Required services for APIHC
        /// </summary>
        private readonly INetlifyApiClientService<BaseResponse> _hostingService;
        private readonly IGitLabAPIClientService<BaseResponse> _repoService;
        private readonly IFileTransferrer<FileTransfererResult> _fileTransferrer;
        private readonly AuthRepoHubConnectorOptions _repoOptions;
        private readonly AuthHostingConnectorOptions _hostingOptions;

        public HomeController(
            INetlifyApiClientService<BaseResponse> hostingService,
            IGitLabAPIClientService<BaseResponse> repoService,
            IFileTransferrer<FileTransfererResult> fileTransferrer,
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IOptions<AuthHostingConnectorOptions> hostingOptions)
        {
            this._hostingService = hostingService;
            this._repoService = repoService;
            this._fileTransferrer = fileTransferrer;
            this._repoOptions = repoOptions.Value;
            this._hostingOptions = hostingOptions.Value;
        }

        public IActionResult Index()
        {
            var model = new DemoHubCreationViewModel();

            return View(model);
        }

        /// <summary>
        /// Demo of the required steps for application task
        /// </summary>
        /// <param name="model">Required params to create project</param>
        /// <returns>error view or view with project name - for url display</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DemoHubCreationViewModel model)
        {
            //Extract the process in custom service layer
            //1- create hosting deploy key
            var hostingDeployKey = await this._hostingService.CreateDeployKey(_hostingOptions.HostAccesToken);

            if (hostingDeployKey.Success)
            {
                //2- Create repo and get id
                var createRepoHubId = await this._repoService.CreateHubAsync(model.RepositoryName, _repoOptions.RepoAccesTokken);

                if (createRepoHubId.Success)
                {
                    //3- add deploy key to repository
                    var repoUserKey = await this._repoService.AddKey(_repoOptions.RepoAccesTokken, hostingDeployKey.Message[1], model.ProjectName);

                    if (repoUserKey.Success)
                    {
                        //4- get project files
                        var filePaths = new List<string>();
                        var fileContents = new List<string>();

                        var defaultStoreTypeSiteFileRead = await this._fileTransferrer.FilesToList(model.LocalPathToProjectTemplate);

                        filePaths = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FilePath));
                        fileContents = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FileContent));

                        //5- Push all files to repository
                        var pushToRepo = await this._repoService.PushDataToHub(createRepoHubId.Message[0], _repoOptions.RepoAccesTokken, filePaths, fileContents);

                        if (pushToRepo.Success)
                        {
                            //6- update Hosting repository name 
                            var pushRepositoryName = model.GitLabClientName + "/" + model.RepositoryName;

                            //7- deploy project try gitlab to netlify
                            var deployCall = await this._hostingService.CreateHubAsync(
                                model.ProjectName, pushRepositoryName, createRepoHubId.Message[0], hostingDeployKey.Message[0], _hostingOptions.HostAccesToken,
                                model.ProjectCmdCommand, model.ProjectBuildDirName);

                            if (deployCall.Success)
                            {
                                return RedirectToAction("Complete", "Home", new { project = model.ProjectName });
                            }
                            else
                            {
                                return RedirectToAction("Error", "Home", new { message = deployCall.Message[0] });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home", new { message = pushToRepo.Message[0] });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { message = repoUserKey.Message[0] });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = createRepoHubId.Message[0] });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { message = hostingDeployKey.Message[0] });
            }


        }

        /// <summary>
        /// Display success
        /// </summary>
        /// <param name="project">name of the created project</param>
        /// <returns>Model with prop for linking to created project. NOTE: DEPLOY NEED TIME - 3-4 MINS</returns>
        public IActionResult Complete(string project)
        {
            var model = new CompleteViewModel()
            {
                ProjectUrl = $"https://{project}.netlify.com"
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
