﻿using APIHubConnector.Services.Interfaces;
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

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DemoHubCreationViewModel model)
        {
            //Extract the process in custom service layer
            //1- create hosting deploy key
            var hostingDeployKey = await this._hostingService.CreateDeployKey(_hostingOptions.HostAccesToken);

            if (hostingDeployKey.Success)
            {
                //2- Create repo and
                var createRepoHubId = await this._repoService.CreateHubAsync(model.RepositoryName, _repoOptions.RepoAccesTokken);

                if (createRepoHubId.Success)
                {
                    var repoUserKey = await this._repoService.AddKey(_repoOptions.RepoAccesTokken, createRepoHubId.PublicKey, model.ProjectName);

                    if (repoUserKey.Success)
                    {
                        var filePaths = new List<string>();
                        var fileContents = new List<string>();

                        var defaultStoreTypeSiteFileRead = await this._fileTransferrer.FilesToList(model.LocalPathToProjectTemplate);

                        filePaths = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FilePath));
                        fileContents = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FileContent));

                        var pushToRepo = await this._repoService.PushDataToHub(createRepoHubId.Message, _repoOptions.RepoAccesTokken, filePaths, fileContents);

                        if (pushToRepo.Success)
                        {
                            var deployCall = await this._hostingService.CreateHubAsync(
                                model.RepositoryName, model.ProjectName, createRepoHubId.Message, hostingDeployKey.Message, _hostingOptions.HostAccesToken,
                                model.ProjectCmdCommand, model.ProjectBuildDirName);

                            if (deployCall.Success)
                            {
                                return RedirectToAction("Complete", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Error", "Home", new { message = deployCall.Message });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home", new { message = pushToRepo.Message });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { message = repoUserKey.Message });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Home", new { message = createRepoHubId.Message });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { message = hostingDeployKey.Message });
            }


        }

        public IActionResult Complete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
