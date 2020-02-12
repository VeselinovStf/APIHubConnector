using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHubConnector.Services.Public.DTOs;
using APIHubConnector.Services.Public.Interfaces;
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
        /// Required services for APIHC - Create
        /// </summary>     
        private readonly AuthRepoHubConnectorOptions _repoOptions;
        private readonly AuthHostingConnectorOptions _hostingOptions;
        private readonly ISiteStorageCreatorService<SiteStorageCreatorResultDTO> siteStorageCreatorService;

        public HomeController(
            ISiteStorageCreatorService<SiteStorageCreatorResultDTO> siteStorageCreatorService,
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IOptions<AuthHostingConnectorOptions> hostingOptions)
        {
            this._repoOptions = repoOptions.Value;
            this._hostingOptions = hostingOptions.Value;
            this.siteStorageCreatorService = siteStorageCreatorService;
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
            //Call service with required params
            var serviceCall = await this.siteStorageCreatorService.ExecuteAsync(
                _hostingOptions.HostAccesToken, _repoOptions.RepoAccesTokken,
                model.RepositoryName, model.ProjectName, model.GitLabClientName,
                model.ProjectCmdCommand, model.ProjectBuildDirName, model.LocalPathToProjectTemplate);

            // Check result
            if (serviceCall.Success)
            {
                var param = serviceCall.Message[0];

                return RedirectToAction("Complete", "Home", new { project = param });
            }
            else
            {
                var param = serviceCall.Message[0];

                return RedirectToAction("Error", "Home", new { message = param });
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
