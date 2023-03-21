using System.Text.RegularExpressions;

namespace AppQuickLaunch.Models
{
    public abstract class AppApplicationBase
    {
        public string ProjectFolder { get; set; }

        public string RunArguments { get; set; }

        public string LabelName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "namelbl";

        public string GitStatusLabelName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "gitStatuslbl";

        public string RunBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "RunBtn";

        public string KillBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "KillBtn";

        public string TestBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "TestBtn";

        public string SlnBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "SlnBtn";

        public string FolderBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "FolderBtn";

        public string StatusTxtName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "StatusTxt";

        public string OpenUrlLocalBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "OpenUrlLocalBtn";

        public string OpenUrlDvBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "OpenUrlDvBtn";

        public string OpenUrlQaBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "OpenUrlQaBtn";

        public string OpenUrlSgBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "OpenUrlSgBtn";

        public string OpenUrlPdBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "OpenUrlPdBtn";

        public string RepositoryUrlBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "RepositoryUrlBtn";

        public string PullRequestsUrlBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "PullRequestsUrlBtn";

        public string BuildsUrlBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "BuildsUrlBtn";

        public string ReleasesUrlBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "ReleasesUrlBtn";

        public string GitCloneBtnName => new Regex("[^a-zA-Z -]").Replace(ProjectFolder, "").Replace("-", "") + "GitCloneBtn";

        public string TabName { get; set; }

        public string DisplayName { get; set; }

        public string AppLocalUrl { get; set; }

        public string AppDvUrl { get; set; }

        public string AppQaUrl { get; set; }

        public string AppSgUrl { get; set; }

        public string AppPdUrl { get; set; }

        public string RepositoryUrl { get; set; }

        public string PullRequestsUrl { get; set; }

        public string BuildsUrl { get; set; }

        public string ReleasesUrl { get; set; }

        public string GitFolder { get; set; }
    }
}