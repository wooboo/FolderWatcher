using System.Diagnostics;
using System.IO;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Script
{
    public class ScriptPlugin : IPlugin
    {
        private readonly ScriptPluginConfig _config;

        public ScriptPlugin(ScriptPluginConfig config)
        {
            _config = config;
        }

        public void OnFile(FileSystemItem file)
        {
            ProcessStartInfo processStartInfo = null;
            var fileName = _config.FileName;
            if (string.IsNullOrWhiteSpace(fileName)
                && !string.IsNullOrWhiteSpace(_config.Script))
            {
                fileName = _config.GetPath("scripts", "tmp", fileName + ".cmd");
                File.WriteAllText(fileName, _config.Script);
            }
            processStartInfo = new ProcessStartInfo(fileName, string.Format(_config.Arguments, file.FullPath));
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            var process = Process.Start(processStartInfo);
            process.WaitForExit();
        }

    }
}