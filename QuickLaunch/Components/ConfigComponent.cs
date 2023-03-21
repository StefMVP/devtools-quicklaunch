using AppQuickLaunch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace AppQuickLaunch
{
    public class ConfigComponent : IConfigComponent
    {
        public QuickLaunchConfig GetQuickLaunchConfig()
        {
            var appAppRunArgs = "";
            return new QuickLaunchConfig
            {
                SourceDirectory = "",
                AppApplicationServices = new List<AppApplicationService>
                {
                    /* Svc-Core */
                    new AppApplicationService
                    {
                        DisplayName = "Sample App",
                        ProjectFolder = "proj/proj",
                        SolutionPath = "app.sln",
                        GitFolder = "C:\\Users\\stefa\\Desktop\\dev\\devtools.quicklaunch.git",
                        RunArguments = appAppRunArgs,
                        AppLocalUrl = "https://localhost:44302",
                        AppDvUrl = "",
                        AppQaUrl = "",
                        AppSgUrl = "",
                        AppPdUrl = "",
                        RepositoryUrl = "",
                        PullRequestsUrl = "",
                        BuildsUrl = "",
                        ReleasesUrl = "",
                        TabName = TabNameTypes.ServicesCore
                    }
                },
                AppApplicationWebApps = new List<AppApplicationWebApp>
                {
                    /* Web-Core */
                    new AppApplicationWebApp
                    {
                        DisplayName = "app",
                        ProjectFolder = "app",
                        GitFolder = "app/.git",
                        RunArguments = "set HTTPS=true&&set port=3000&&npm start",
                        AppLocalUrl = "https://localhost:3000",
                        AppDvUrl = "",
                        AppQaUrl = "",
                        AppSgUrl = "",
                        AppPdUrl = "",
                        RepositoryUrl = "",
                        PullRequestsUrl = "",
                        BuildsUrl = "",
                        ReleasesUrl = "",
                        TabName = TabNameTypes.WebCore
                    }
                }
            };
        }

        public bool CheckIfConfigInvalid(QuickLaunchConfig config)
        {
            if (string.IsNullOrEmpty(config.SourceDirectory))
            {
                MessageBox.Show("Edit QuickLaunchConfig.json and restart the application. SourceDirectory must be populated.");
                return false;
            }

            return true;
        }

        public void CreateConfigIfDoesntExists()
        {
            if (!File.Exists("QuickLaunchConfig.json"))
            {
                GenerateConfig();
            }
        }

        public QuickLaunchConfig LoadConfig()
        {
            try
            {
                using (StreamReader r = new StreamReader("QuickLaunchConfig.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<QuickLaunchConfig>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading config {ex.Message}");
                return null;
            }
        }

        public void OpenConfigFile()
        {
            try
            {
                var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var file = Path.Combine(directory, "QuickLaunchConfig.json");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(file)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GenerateConfig()
        {
            try
            {
                using (StreamWriter file = File.CreateText(@"QuickLaunchConfig.json"))
                {
                    var config = GetQuickLaunchConfig();
                    var serializer = new JsonSerializer { Formatting = Formatting.Indented };
                    serializer.Serialize(file, config);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}