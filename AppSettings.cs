using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WinForm
{
    internal sealed class AppSettings
    {
        public const string FileName = "settings.json";

        public string DistroName { get; set; }
        public string StartupScript { get; set; }

        public static AppSettings Current { get; private set; }

        public static string FilePath =>
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), FileName);

        public static void Load()
        {
            string path = FilePath;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"設定ファイルが見つかりません。\r\n{path}\r\n\r\n" +
                    "exe と同じフォルダに settings.json を配置してください。", path);
            }

            AppSettings settings;
            try
            {
                var serializer = new JavaScriptSerializer();
                settings = serializer.Deserialize<AppSettings>(File.ReadAllText(path, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                throw new InvalidDataException(
                    $"settings.json の形式が正しくありません。\r\n{ex.Message}", ex);
            }

            if (settings == null
                || string.IsNullOrWhiteSpace(settings.DistroName)
                || string.IsNullOrWhiteSpace(settings.StartupScript))
            {
                throw new InvalidDataException(
                    "settings.json に DistroName と StartupScript を指定してください。");
            }

            settings.DistroName = settings.DistroName.Trim();
            settings.StartupScript = settings.StartupScript.Trim();
            Current = settings;
        }
    }
}
