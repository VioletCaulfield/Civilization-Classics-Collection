using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CivClassicLauncher
{
    public static class UpdateChecker
    {
        private const string VersionUrl =
            "https://raw.githubusercontent.com/ViCaulfield/CivClassicsUpdate/main/version.txt";

        public static async Task<string?> GetLatestVersionAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();

                string text = await client.GetStringAsync(VersionUrl);

                foreach (string line in text.Split('\n'))
                {
                    if (line.StartsWith("Version=", StringComparison.OrdinalIgnoreCase))
                    {
                        return line.Substring(8).Trim();
                    }
                }
            }
            catch
            {
                // Ignore network errors for now
            }

            return null;
        }

    public static async Task<string?> GetVersionFileAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();

                return await client.GetStringAsync(VersionUrl);
            }
            catch
            {
                return null;
            }
        }
    }
    }
