using System.Timers;
using System.Windows;

namespace AppQuickLaunch
{
    public interface IUiEventHandlers
    {
        void GenerateConfigClick(object sender, RoutedEventArgs e);

        void OpenConfigClick(object sender, RoutedEventArgs e);

        void OpenSolutionClick(object sender, RoutedEventArgs e);

        void RunAppClick(object sender, RoutedEventArgs e);

        void KillAppClick(object sender, RoutedEventArgs e);

        void OpenFolderClick(object sender, RoutedEventArgs e);

        void OpenUrlClick(object sender, RoutedEventArgs e);

        void GitCloneClick(object sender, RoutedEventArgs e);

        void OnTimedEvent(object source, ElapsedEventArgs e);
    }
}