using AppQuickLaunch.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace AppQuickLaunch
{
    public class StatusComponent : IStatusComponent
    {
        private readonly QuickLaunchConfig _config;
        private readonly IProcessComponent _processComponent;

        public StatusComponent(IConfigCache cache, IProcessComponent processComponent)
        {
            _config = cache.GetConfig();
            _processComponent = processComponent;
        }

        //Executed on timer (3 seconds currently)
        public void UpdateProcessStatuses()
        {
            try
            {
                if (ApplicationIsActivated())
                {
                    SetServiceStatusLabels();
                    SetWebStatusLabels();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetServiceStatusLabels()
        {
            var processes = Process.GetProcesses();
            _config.AppApplicationServices.ForEach(app =>
            {
                var isProcessRunning = _processComponent.GetProcessByTitle(processes, app.ProjectFolder) != null;
                UpdateFoundStatusLabel(isProcessRunning, app);
            });
        }

        private void SetWebStatusLabels()
        {
            _config.AppApplicationWebApps.ForEach(app =>
            {
                bool isProcessRunning;
                try
                {
                    if (app.ProcessId == default(int))
                    {
                        isProcessRunning = false;
                    }
                    else
                    {
                        Process.GetProcessById(app.ProcessId);//Error if process doesnt exist
                        isProcessRunning = true;
                    }
                }
                catch
                {
                    isProcessRunning = false;
                }

                UpdateFoundStatusLabel(isProcessRunning, app);
            });
        }

        private void UpdateFoundStatusLabel(bool isProcessRunning, AppApplicationBase app)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var foundStatusTxt = UiUtils.GetTextBoxByName(app.TabName, app.StatusTxtName);
                var foundHeaderLbl = UiUtils.GetLabelByName(app.TabName, app.LabelName);

                if (foundStatusTxt != null)
                {
                    foundStatusTxt.Background = isProcessRunning ? Brushes.Green : Brushes.Red;
                }
                if (foundHeaderLbl != null)
                {
                    var branchText = TextUtilities.GetBranchTextFromFolder(app.GitFolder, _config.SourceDirectory);
                    if (!string.IsNullOrEmpty(branchText))
                    {
                        foundHeaderLbl.Content = $"{app.DisplayName} - Branch: {branchText}";
                    }
                }
            });
        }

        /// <summary>Returns true if the current application has focus, false otherwise</summary>
        public static bool ApplicationIsActivated()
        {
            var isActive = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = UiUtils.GetMainWindow();
                if (window != null)
                {
                    isActive = window.IsActive;
                }
                else
                {
                    isActive = false;
                }
            });
            return isActive;
        }
    }
}