using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.ViewModels
{
    public class DemoHubCreationViewModel
    {
        public string RepositoryName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectCmdCommand { get; set; }

        public string ProjectBuildDirName { get; set; }
    }
}
