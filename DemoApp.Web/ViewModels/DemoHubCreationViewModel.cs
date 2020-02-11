namespace DemoApp.Web.ViewModels
{
    /// <summary>
    /// Required model parrameters for application execution
    /// </summary>
    public class DemoHubCreationViewModel
    {
        public string RepositoryName { get; set; }

        public string GitLabClientName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectCmdCommand { get; set; }

        public string ProjectBuildDirName { get; set; }

        public string LocalPathToProjectTemplate { get; set; }
    }
}
