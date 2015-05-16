using System.Diagnostics;
using System.IO;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Script
{
    public class ScriptPlugin : PluginBase<ScriptPluginConfig>
    {
        private readonly ScriptPluginConfig _config;

        public ScriptPlugin(ScriptPluginConfig config) : base(config)
        {
            _config = config;
        }

        public override void OnFileCreated(FileChangeInfo file)
        {
            ProcessStartInfo processStartInfo = null;
            var fileName = _config.FileName;
            if (string.IsNullOrWhiteSpace(fileName)
                && !string.IsNullOrWhiteSpace(_config.Script))
            {
                fileName = _config.GetPath("scripts", "tmp", fileName + ".cmd");
                File.WriteAllText(fileName, _config.Script);
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var arguments = string.Format(_config.Arguments, file.FullPath);
                Debug.WriteLine("Executing: {0} {1}", fileName, arguments);
                processStartInfo = new ProcessStartInfo(fileName, arguments);
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                using (var process = Process.Start(processStartInfo))
                {
                    using (var reader = process.StandardOutput)
                    {
                        Debug.WriteLine(reader.ReadToEnd());

                    }
                    //process.WaitForExit();
                    Debug.WriteLine("Exit code: {0}", process.ExitCode);
                }

            }
        }

    }
}