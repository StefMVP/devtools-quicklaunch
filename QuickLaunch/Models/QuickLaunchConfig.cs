using System.Collections.Generic;

namespace AppQuickLaunch.Models
{
    public class QuickLaunchConfig
    {
        public string SourceDirectory { get; set; }

        public List<AppApplicationService> AppApplicationServices { get; set; }
        public List<AppApplicationWebApp> AppApplicationWebApps { get; set; }
    }
}