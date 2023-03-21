using System.IO;
using System.Linq;

namespace AppQuickLaunch
{
    public static class TextUtilities
    {
        public static string GetBranchTextFromFolder(string gitFolder, string sourceDirectory)
        {
            try
            {
                if (string.IsNullOrEmpty(gitFolder))
                {
                    return string.Empty;
                }
                string headText = File.ReadAllText($"{sourceDirectory}/{gitFolder}/HEAD");
                return headText.Split('/').LastOrDefault().Replace("\n", "");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}