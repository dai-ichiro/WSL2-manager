using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace WinForm
{
    internal static class WslManager
    {
        private static string DistroName => AppSettings.Current.DistroName;
        private static string StartupCommand => AppSettings.Current.StartupScript;

        public static void Start()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = $"-d {DistroName} -- bash -lc \"{StartupCommand}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            Process.Start(psi);
        }

        public static void Stop()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = $"-t {DistroName}",
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = Process.Start(psi))
            {
                process?.WaitForExit(5000);
            }
        }

        public static bool IsRunning()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = "-l -v",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                // wsl.exe writes UTF-16LE to redirected pipes regardless of console codepage.
                StandardOutputEncoding = Encoding.Unicode,
            };

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit(5000);

                foreach (var rawLine in output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string line = rawLine.Replace("*", "").Trim();
                    string[] parts = Regex.Split(line, @"\s+");

                    if (parts.Length >= 2 && string.Equals(parts[0], DistroName, StringComparison.OrdinalIgnoreCase))
                    {
                        return string.Equals(parts[1], "Running", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }

            return false;
        }
    }
}
