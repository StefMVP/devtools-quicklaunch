using AppQuickLaunch.Models;
using AppQuickLaunch.Troubleshooting;

namespace AppQuickLaunch
{
    internal sealed partial class MainWindow
    {
        private readonly QuickLaunchConfig _config;
        private readonly IStatusComponent _statusComponent;
        private readonly IUiSetupComponent _uiSetupComponent;
        private readonly IConfigComponent _configComponent;
        private readonly IDebuggingComponent _debuggingComponent;

        public MainWindow(IStatusComponent statusComponent, IUiSetupComponent uiSetupComponent, IConfigComponent configComponent, IDebuggingComponent debuggingComponent)
        {
            _statusComponent = statusComponent;
            _uiSetupComponent = uiSetupComponent;
            _configComponent = configComponent;
            _debuggingComponent = debuggingComponent;

            _configComponent.CreateConfigIfDoesntExists();
            _config = _configComponent.LoadConfig();
            if (_configComponent.CheckIfConfigInvalid(_config))
            {
                InitializeComponent();
                _uiSetupComponent.SetupStatusTimer();
                _uiSetupComponent.SetupControlsForEachType();
                _statusComponent.UpdateProcessStatuses();
                Show();
            }
            else
            {
                _configComponent.OpenConfigFile();
            }

            //_debuggingComponent.CheckAllAppPathsExist();
        }
    }
}