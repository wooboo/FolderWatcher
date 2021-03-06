using System.Diagnostics;
using System.IO;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;

namespace FolderWatcher.Core.Plugins.Script
{
    public class ScriptPlugin : PluginBase<ScriptPluginConfig>
    {
        private readonly ScriptPluginConfig _config;

        public ScriptPlugin(ScriptPluginConfig config) : base(config)
        {
            _config = config;
        }

        public override void OnFileCreated(FileChangeInfo file, IValueBag valueBag)
        {
            ProcessStartInfo processStartInfo = null;
            var fileName = _config.FileName;
            if (string.IsNullOrWhiteSpace(fileName)
                && !string.IsNullOrWhiteSpace(_config.Script))
            {
                Directory.CreateDirectory(_config.GetPath(_config.GetName()));
                fileName = _config.GetPath(_config.GetName(), _config.GetName() + ".cmd");
                File.WriteAllText(fileName, _config.Script);
                _config.FileName = fileName;
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var arguments = string.Format(_config.Arguments, file.FullPath);
                Debug.WriteLine("Executing: {0} {1}", fileName, arguments);
                processStartInfo = new ProcessStartInfo(fileName, arguments);
                foreach (var keyValue in valueBag.Values)
                {
                    processStartInfo.Environment[keyValue.Key] = keyValue.Value;
                }
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