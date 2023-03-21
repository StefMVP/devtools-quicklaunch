using AppQuickLaunch.Models;

namespace AppQuickLaunch
{
    public interface IConfigCache
    {
        void SetConfig();

        QuickLaunchConfig GetConfig();
    }
}