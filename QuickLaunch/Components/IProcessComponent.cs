using System.Collections.Generic;
using System.Diagnostics;

namespace AppQuickLaunch
{
    public interface IProcessComponent
    {
        void OpenSolution(object sender);

        void OpenFolder(object sender);

        void OpenUrl(object sender);

        void KillApp(object sender);

        void RunApp(object sender);

        void GitClone(object sender);

        List<Process> GetProcessByTitle(IEnumerable<Process> processes, string name);
    }
}