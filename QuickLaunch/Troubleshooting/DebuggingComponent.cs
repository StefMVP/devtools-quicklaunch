using AppQuickLaunch.Models;
using System;
using System.IO;

namespace AppQuickLaunch.Troubleshooting
{
    public class DebuggingComponent : IDebuggingComponent
    {
        private readonly QuickLaunchConfig _config;

        public DebuggingComponent(IConfigCache cache)
        {
            _config = cache.GetConfig();
        }

        public void CheckAllAppPathsExist()
        {
            _config.AppApplicationServices.ForEach(app =>
            {
                if (!string.IsNullOrEmpty(app.ProjectFolder) && !Directory.Exists($"{_config.SourceDirectory}/{app.ProjectFolder}"))
                {
                    Console.WriteLine($"{app.DisplayName} : {app.ProjectFolder} doesn't exist");
                }
                if (!string.IsNullOrEmpty(app.GitFolder) && !Directory.Exists($"{_config.SourceDirectory}/{app.GitFolder}"))
                {
                    Console.WriteLine($"{app.DisplayName} : {app.GitFolder} doesn't exist");
                }
                if (!string.IsNullOrEmpty(app.SolutionPath) && !File.Exists($"{_config.SourceDirectory}/{app.SolutionPath}"))
                {
                    Console.WriteLine($"{app.DisplayName} : {app.SolutionPath} doesn't exist");
                }
            });
        }
    }
}