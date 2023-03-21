using AppQuickLaunch.Models;

namespace AppQuickLaunch
{
    public interface IConfigComponent
    {
        QuickLaunchConfig LoadConfig();

        void CreateConfigIfDoesntExists();

        bool CheckIfConfigInvalid(QuickLaunchConfig config);

        QuickLaunchConfig GetQuickLaunchConfig();

        void GenerateConfig();

        void OpenConfigFile();
    }
}