using System.Timers;
using System.Windows;

namespace AppQuickLaunch
{
    public class UiEventHandlers : IUiEventHandlers
    {
        private readonly IConfigComponent _configComponent;
        private readonly IProcessComponent _processComponent;
        private readonly IStatusComponent _statusComponent;

        public UiEventHandlers(IConfigComponent configComponent, IProcessComponent processComponent, IStatusComponent statusComponent)
        {
            _configComponent = configComponent;
            _processComponent = processComponent;
            _statusComponent = statusComponent;
        }

        public void GenerateConfigClick(object sender, RoutedEventArgs e)
        {
            _configComponent.GenerateConfig();
        }

        public void OpenConfigClick(object sender, RoutedEventArgs e)
        {
            _configComponent.OpenConfigFile();
        }

        public void OpenSolutionClick(object sender, RoutedEventArgs e)
        {
            _processComponent.OpenSolution(sender);
        }

        public void RunAppClick(object sender, RoutedEventArgs e)
        {
            _processComponent.RunApp(sender);
        }

        public void KillAppClick(object sender, RoutedEventArgs e)
        {
            _processComponent.KillApp(sender);
        }

        public void OpenFolderClick(object sender, RoutedEventArgs e)
        {
            _processComponent.OpenFolder(sender);
        }

        public void OpenUrlClick(object sender, RoutedEventArgs e)
        {
            _processComponent.OpenUrl(sender);
        }

        public void GitCloneClick(object sender, RoutedEventArgs e)
        {
            _processComponent.GitClone(sender);
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _statusComponent.UpdateProcessStatuses();
        }
    }
}