using AppQuickLaunch.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Controls;

namespace AppQuickLaunch
{
    public class ProcessComponent : IProcessComponent
    {
        private readonly QuickLaunchConfig _config;

        public ProcessComponent(IConfigCache cache)
        {
            _config = cache.GetConfig();
        }

        public void OpenSolution(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    //Open Solution
                    if (app.SlnBtnName == senderBtn.Name)
                    {
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo($"{_config.SourceDirectory}/{app.SolutionPath}")
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenFolder(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.FolderBtnName == senderBtn.Name)
                    {
                        OpenFolderMain(_config.SourceDirectory, app.ProjectFolder);
                        found = true;
                    }
                }
                foreach (var app in _config.AppApplicationWebApps)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.FolderBtnName == senderBtn.Name)
                    {
                        OpenFolderMain(_config.SourceDirectory, app.ProjectFolder);
                        found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenFolderMain(string sourceDirectory, string projectFolder)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo($"{sourceDirectory}/{projectFolder}")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public void OpenUrl(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    found = OpenUrlMain(senderBtn.Name, app);
                }

                foreach (var app in _config.AppApplicationWebApps)
                {
                    if (found)
                    {
                        break;
                    }
                    found = OpenUrlMain(senderBtn.Name, app);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool OpenUrlMain(string btnName, AppApplicationBase app)
        {
            if (app.OpenUrlLocalBtnName == btnName && !string.IsNullOrEmpty(app.AppLocalUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.AppLocalUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            if (app.OpenUrlDvBtnName == btnName && !string.IsNullOrEmpty(app.AppDvUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.AppDvUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.OpenUrlQaBtnName == btnName && !string.IsNullOrEmpty(app.AppQaUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.AppQaUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.OpenUrlSgBtnName == btnName && !string.IsNullOrEmpty(app.AppSgUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.AppSgUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.OpenUrlPdBtnName == btnName && !string.IsNullOrEmpty(app.AppPdUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.AppPdUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.RepositoryUrlBtnName == btnName && !string.IsNullOrEmpty(app.RepositoryUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.RepositoryUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.PullRequestsUrlBtnName == btnName && !string.IsNullOrEmpty(app.PullRequestsUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.PullRequestsUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.BuildsUrlBtnName == btnName && !string.IsNullOrEmpty(app.BuildsUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.BuildsUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            else if (app.ReleasesUrlBtnName == btnName && !string.IsNullOrEmpty(app.ReleasesUrl))
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo($"{app.ReleasesUrl}")
                {
                    UseShellExecute = true
                };
                p.Start();
                return true;
            }
            return false;
        }

        public void KillApp(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.KillBtnName == senderBtn.Name)
                    {
                        var process = GetProcessByTitle(Process.GetProcesses(), app.ProjectFolder).FirstOrDefault();
                        process?.Kill();
                        var process2 = GetProcessByTitle(Process.GetProcesses(), app.ProjectFolder).FirstOrDefault();
                        process2?.Kill();
                        found = true;
                    }
                }

                foreach (var app in _config.AppApplicationWebApps)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.KillBtnName == senderBtn.Name)
                    {
                        KillAllProcessesSpawnedBy((uint)app.ProcessId);
                        found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return;
            }
        }

        public void RunApp(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    //Run App
                    if (app.RunBtnName == senderBtn.Name)
                    {
                        ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd")
                        {
                            Arguments = $@"/K dotnet run --project {_config.SourceDirectory}/{app.ProjectFolder} {app.RunArguments} --urls {app.AppLocalUrl}",
                            UseShellExecute = true
                        };
                        Process.Start(cmdProcess);
                        found = true;
                    }

                    //Tests
                    if (app.TestBtnName == senderBtn.Name)
                    {
                        ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd")
                        {
                            Arguments = $@"/K dotnet test {_config.SourceDirectory}/{app.SolutionPath}",
                            UseShellExecute = true
                        };
                        Process.Start(cmdProcess);
                        found = true;
                    }
                }

                foreach (var app in _config.AppApplicationWebApps)
                {
                    if (found)
                    {
                        break;
                    }
                    //Run App
                    if (app.RunBtnName == senderBtn.Name)
                    {
                        ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd")
                        {
                            Arguments = $@"/K cd {_config.SourceDirectory}/{app.ProjectFolder}&&{app.RunArguments}",
                            UseShellExecute = true
                        };
                        var proc = Process.Start(cmdProcess);

                        app.ProcessId = proc.Id;
                        found = true;
                    }
                    //Tests
                    if (app.TestBtnName == senderBtn.Name)
                    {
                        ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd")
                        {
                            Arguments = $@"/K cd {_config.SourceDirectory}/{app.ProjectFolder}&&npm test a",
                            UseShellExecute = true
                        };
                        Process.Start(cmdProcess);
                        found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GitClone(object sender)
        {
            try
            {
                var senderBtn = (Button)sender;
                if (senderBtn == null)
                {
                    return;
                }

                bool found = false;
                foreach (var app in _config.AppApplicationServices)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.GitCloneBtnName == senderBtn.Name)
                    {
                        GitCloneMain(app.RepositoryUrl);
                        found = true;
                    }
                }
                foreach (var app in _config.AppApplicationWebApps)
                {
                    if (found)
                    {
                        break;
                    }
                    if (app.GitCloneBtnName == senderBtn.Name)
                    {
                        GitCloneMain(app.RepositoryUrl);
                        found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GitCloneMain(string repositoryUrl)
        {
            ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd")
            {
                Arguments = $@"/K cd {_config.SourceDirectory} && git clone {repositoryUrl}",
                UseShellExecute = true
            };
            Process.Start(cmdProcess);
        }

        public List<Process> GetProcessByTitle(IEnumerable<Process> processes, string name)
        {
            var result = processes.Where(p => p.MainWindowTitle.Contains(name)).ToList();
            return result.Count > 0 ? result : null;
        }

        public static void KillAllProcessesSpawnedBy(UInt32 parentProcessId)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * " +
                    "FROM Win32_Process " +
                    "WHERE ParentProcessId=" + parentProcessId);
                ManagementObjectCollection collection = searcher.Get();
                if (collection.Count > 0)
                {
                    foreach (var item in collection)
                    {
                        UInt32 childProcessId = (UInt32)item["ProcessId"];
                        if ((int)childProcessId != Process.GetCurrentProcess().Id)
                        {
                            KillAllProcessesSpawnedBy(childProcessId);

                            Process childProcess = Process.GetProcessById((int)childProcessId);
                            childProcess.Kill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}